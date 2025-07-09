using ArtGallery.Domain;
using Microsoft.Extensions.Options;

namespace ArtGallery.Data;

public class FilesystemArtRepository : IArtRepository {
	private readonly IOptions<Options> _options;
	private readonly ILogger<FilesystemArtRepository> _logger;

	public FilesystemArtRepository(IOptions<Options> options, ILogger<FilesystemArtRepository> logger) {
		_options = options;
		_logger = logger;
	}

	private string MapPathToUrl(string path) {
		// TODO
		return path;
	}

	public class Options {
		public string Path { get; set; }
	}
}
