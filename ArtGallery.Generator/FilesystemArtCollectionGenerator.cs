using FFMpegCore;
using YamlDotNet.Serialization;

namespace ArtGallery.Generator;

/// <summary>
/// Generates an ArtCollection based on a directory
/// </summary>
public class FilesystemArtCollectionGenerator : ArtCollectionGenerator {
	private readonly CliOptions _options;

	private static readonly string[] SupportedExtensions = [
		"png", "jpg", "jpeg", "gif", "webp", "apng",
	];
	
	public FilesystemArtCollectionGenerator(CliOptions options) {
		_options = options;
	}

	public async override Task<ArtCollection> GenerateArtCollection() {
		string[] artistDirectories = Directory.GetDirectories(_options.ArtDirectory);

		var result = new List<Artist>();

		foreach (string artistPath in artistDirectories) {
			Logger.Instance.LogTrace(artistPath);
			string artistName = Path.GetFileName(artistPath);
			
			var artItems = new List<ArtItem>();
			var artist = new Artist() {
				Name = artistName,
				ArtItems = artItems
			};

			artist = await PopulateArtist(artist, artistPath);
			
			string[] artItemFiles = Directory.GetFiles(artistPath);
			foreach (string artItemPath in artItemFiles) {
				Logger.Instance.LogTrace(artItemPath);
				string extension = Path.GetExtension(artItemPath);
				if (!SupportedExtensions.Contains(extension[1..])) {
					continue;
				}
				
				string filename = Path.GetFileNameWithoutExtension(artItemPath);
				int firstSpace = filename.IndexOf(' ');
				if (firstSpace == -1) {
					continue;
				}

				string dateSegment = filename[..firstSpace];
				if (_options.IncludeMarked && dateSegment[^1] == 'x') {
					dateSegment = dateSegment[..^1];
				}

				if (!DateOnly.TryParseExact(dateSegment, "O", out DateOnly date)) {
					continue;
				}
				
				string title = filename[(firstSpace + 1)..];
				bool isPixel = await ImageIsPixel(artItemPath);
				
				artItems.Add(new ArtItem() {
					Date = date,
					Description = "", // TODO Description
					Title = title,
					Path = Path.GetRelativePath(_options.ArtDirectory, artItemPath),
					IsPixel = isPixel,
				});
			}
			Logger.Instance.LogTrace("--");

			if (artist.ArtItems.Count == 0) {
				continue;
			}
			
			artist.ArtItems = artItems
				.OrderBy(artItem => artItem.Date)
				.ThenBy(artItem => artItem.Title)
				.ToList();

			result.Add(artist);
		}

		return new ArtCollection() {
			Artists = result.OrderBy(artist => artist.Name).ToList(),
		};
	}

	private async Task<Artist> PopulateArtist(Artist artist, string artistPath) {
		const string artistFileName = "artist.yaml";
		string artistFile = Path.Combine(artistPath, artistFileName);
		string artistDocumentYaml;
		try {
			artistDocumentYaml = await File.ReadAllTextAsync(artistFile);
		} catch (FileNotFoundException) {
			return artist;
		}

		var deserializer = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();
		
		var auxArtist = deserializer.Deserialize<Artist>(artistDocumentYaml);
		artist.Name = auxArtist.Name ?? artist.Name;
		artist.Socials = auxArtist.Socials;
		
		return artist;
	}

	private async Task<bool> ImageIsPixel(string filePath) {
		// TODO need a better method than by file size.
		// Probably have to use a file marker.
		IMediaAnalysis ffprobeResult = await FFProbe.AnalyseAsync(filePath);
		if (ffprobeResult.PrimaryVideoStream == null) {
			Logger.Instance.LogWarning($"Trying to determine if a non-image file is pixel art: {filePath}");
			return false;
		}

		return ffprobeResult.PrimaryVideoStream.Height < 250 || ffprobeResult.PrimaryVideoStream.Width < 250;
	}
}
