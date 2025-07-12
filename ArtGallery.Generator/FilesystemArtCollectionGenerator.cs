using ArtGallery.Domain;
using YamlDotNet.Serialization;

namespace ArtGallery.Generator;

/// <summary>
/// Generates an ArtCollection based on a directory
/// </summary>
public class FilesystemArtCollectionGenerator : ArtCollectionGenerator {
	private readonly string _path;

	private static readonly string[] SupportedExtensions = [
		"png", "jpg", "jpeg", "gif", "webp", "apng",
	];
	
	public FilesystemArtCollectionGenerator(string path) {
		_path = path;
	}

	public override Task<ArtCollection> GenerateArtCollection() {
		string[] artistDirectories = Directory.GetDirectories(_path);

		var result = new List<Artist>();

		foreach (string artistPath in artistDirectories) {
			string artistName = Path.GetFileName(artistPath);
			
			var artItems = new List<ArtItem>();
			var artist = new Artist() {
				Name = artistName,
				ArtItems = artItems
			};

			artist = PopulateArtist(artist, artistPath);
			
			string[] artItemFiles = Directory.GetFiles(artistPath);
			foreach (string artItemPath in artItemFiles) {
				string extension = Path.GetExtension(artItemPath);
				if (!SupportedExtensions.Contains(extension[1..])) {
					continue;
				}
				
				string filename = Path.GetFileNameWithoutExtension(artItemPath);
				int firstSpace = filename.IndexOf(' ');
				if (firstSpace == -1) {
					continue;
				}
				
				DateOnly date = DateOnly.ParseExact(filename[..firstSpace], "O");
				string title = filename[(firstSpace + 1)..];
				
				artItems.Add(new ArtItem() {
					//Artist = artist,
					Date = date,
					Description = "TODO Description",
					Title = title,
					Path = Path.GetRelativePath(_path, artItemPath),
				});
			}

			artist.ArtItems = artItems
				.OrderBy(ai => ai.Date)
				.ThenBy(ai => ai.Title)
				.ToList();
			
			result.Add(artist);
		}

		return Task.FromResult(new ArtCollection() {
			Artists = result.OrderBy(artist => artist.Name).ToList(),
		});
	}

	private Artist PopulateArtist(Artist artist, string artistPath) {
		const string artistFileName = "artist.yaml";
		string artistFile = Path.Combine(artistPath, artistFileName);
		string artistDocumentYaml;
		try {
			artistDocumentYaml = File.ReadAllText(artistFile);
		} catch (FileNotFoundException) {
			return artist;
		}

		var auxArtist = new Deserializer().Deserialize<Artist>(artistDocumentYaml);
		artist.Name = auxArtist.Name ?? artist.Name;
		artist.Socials = auxArtist.Socials;
		
		return artist;
	}
}
