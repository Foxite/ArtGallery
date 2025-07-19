namespace ArtGallery.Generator;

public abstract class ThumbnailGenerator {
	protected CliOptions CliOptions { get; }
	
	protected ThumbnailGenerator(CliOptions cliOptions) {
		CliOptions = cliOptions;
	}
	
	protected abstract Task<bool> GenerateThumbnail(string inputFile, string outputFile, int size);

	public async Task GenerateThumbnails(ArtCollection collection) {
		string thumbnailDirectory = CliOptions.ThumbnailDirectory;
		if (string.IsNullOrEmpty(thumbnailDirectory)) {
			thumbnailDirectory = Path.Combine(CliOptions.ArtDirectory, "_thumbs");
		}
		
		foreach (Artist artist in collection.Artists) {
			string artistDirectory = artist.Name;
			string artistThumbDirectory = Path.Combine(thumbnailDirectory, artistDirectory);
			Directory.CreateDirectory(artistThumbDirectory);

			foreach (ArtItem artItem in artist.ArtItems) {
				string inputFile = artItem.Path;

				foreach (string rawSize in CliOptions.ThumbnailSize.Split(',')) {
					int size;
					try {
						size = int.Parse(rawSize);
					} catch (FormatException ex) {
						throw new FormatException("Invalid value for parameter thumbsize: Must be integers separated by comma", ex);
					}

					string filename = $"{Path.GetFileNameWithoutExtension(artItem.Path)}@{size}{Path.GetExtension(artItem.Path)}";
					string outputFile = Path.Combine(artistThumbDirectory, filename);
					
					bool thumbnailWasGenerated = await GenerateThumbnail(inputFile, outputFile, size);

					if (thumbnailWasGenerated) {
						artItem.Thumbnails[rawSize] = Path.Combine(artistDirectory, filename);
					}
				}
			}
		}
	}
}
