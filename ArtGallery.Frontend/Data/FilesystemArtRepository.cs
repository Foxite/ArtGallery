using ArtGallery.Domain;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ArtGallery.Data;

public class FilesystemArtRepository(IOptions<FilesystemArtRepository.Options> options) : InMemoryArtRepository {
	protected override ArtCollection GetArtItems() {
		var result = JsonConvert.DeserializeObject<ArtCollection>(File.ReadAllText(options.Value.JsonFilePath)) ?? throw new Exception("Unable to deserialize art file into List<Artist>");

		foreach (Artist artist in result.Artists) {
			foreach (ArtItem artItem in artist.ArtItems) {
				artItem.Path = MapPathToUrl(artItem.Path);
			}
		}

		return result;
	}

	private string MapPathToUrl(string path) {
		return Path.Combine(options.Value.BaseUrl, path);
	}

	public class Options {
		public string JsonFilePath { get; set; }
		public string BaseUrl { get; set; }
	}
}
