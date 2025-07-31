using ArtGallery.Frontend;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ArtGallery.Data;

public class FilesystemArtRepository(IOptions<FilesystemArtRepository.Options> options) : InMemoryArtRepository {
	protected async override Task<ArtCollection> GetArtItems() {
		var result = JsonConvert.DeserializeObject<ArtCollection>(await File.ReadAllTextAsync(options.Value.JsonFilePath)) ?? throw new Exception("Unable to deserialize art file into List<Artist>");

		foreach (Artist artist in result.Artists) {
			foreach (ArtItem artItem in artist.ArtItems) {
				artItem.Path = Path.Combine(options.Value.BaseUrl, artItem.Path);

				var newThumbnailDictionary = new Dictionary<string, string>();
				foreach ((string size, string path) in artItem.Thumbnails) {
					newThumbnailDictionary[size] = Path.Combine(options.Value.BaseThumbUrl, path);
				}
				artItem.Thumbnails = newThumbnailDictionary;
			}
		}

		return result;
	}

	public class Options {
		public string JsonFilePath { get; set; }
		public string BaseUrl { get; set; }
		public string BaseThumbUrl { get; set; }
	}
}
