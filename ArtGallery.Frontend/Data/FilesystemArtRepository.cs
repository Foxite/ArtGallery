using ArtGallery.Domain;
using Microsoft.Extensions.Options;

namespace ArtGallery.Data;

public class FilesystemArtRepository : InMemoryArtRepository {
	private readonly IOptions<Options> _options;
	private readonly ILogger<FilesystemArtRepository> _logger;

	public FilesystemArtRepository(IOptions<Options> options, ILogger<FilesystemArtRepository> logger) {
		_options = options;
		_logger = logger;
	}

	protected override ICollection<Artist> GetArtItems() {
		string[] artistDirectories = Directory.GetDirectories(_options.Value.Path);

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
					_logger.LogWarning("{Filename}", artItemPath);
					continue;
				}
				
				DateOnly date = DateOnly.ParseExact(filename[..firstSpace], "O");
				string title = filename[(firstSpace + 1)..];
				
				artItems.Add(new ArtItem() {
					Artist = artist,
					Date = date,
					Description = "TODO",
					Title = title,
					Url = MapPathToUrl(artItemPath),
				});
			}
		}

		return result;
	}

	private string MapPathToUrl(string path) {
		// TODO
		return path;
	}

	public class Options {
		public string Path { get; set; }
	}
}
