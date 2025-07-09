using ArtGallery.Domain;

namespace ArtGallery.Generator;

/// <summary>
/// Generates an ArtCollection based on a directory
/// </summary>
public class FilesystemArtCollectionGenerator : ArtCollectionGenerator {
	private readonly string _path;
	
	public FilesystemArtCollectionGenerator(string path) {
		_path = path;
	}

	public override async Task<ArtCollection> GenerateArtCollection() {
		string[] artistDirectories = Directory.GetDirectories(_path);

		var result = new List<Artist>();

		foreach (string artistPath in artistDirectories) {
			string artistName = Path.GetFileName(artistPath);

			var artItems = new List<ArtItem>();
			var artist = new Artist() {
				Name = artistName,
				ArtItems = artItems
			};
			
			string[] artItemFiles = Directory.GetFiles(artistPath);
			foreach (string artItemPath in artItemFiles) {
				string filename = Path.GetFileNameWithoutExtension(artItemPath);
				int firstSpace = filename.IndexOf(' ');
				if (firstSpace == -1) {
					continue;
				}
				
				DateOnly date = DateOnly.ParseExact(filename[..firstSpace], "O");
				string title = filename[(firstSpace + 1)..];
				
				artItems.Add(new ArtItem() {
					Artist = artist,
					Date = date,
					Description = "TODO",
					Title = title,
					//Url = MapPathToUrl(artItemPath),
				});
			}
		}

		return new ArtCollection() {
			Artists = result,
		};
	}
}
