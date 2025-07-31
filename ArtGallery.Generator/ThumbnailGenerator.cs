namespace ArtGallery.Generator;

public abstract class ThumbnailGenerator {
	protected CliOptions CliOptions { get; }
	
	protected ThumbnailGenerator(CliOptions cliOptions) {
		CliOptions = cliOptions;
	}
	
	protected abstract Task<bool> GenerateThumbnail(string inputFile, string outputFile, int size);

	public async Task GenerateThumbnails(ArtCollection collection) {
		Directory.CreateDirectory(CliOptions.ThumbnailDirectory);
		
		foreach (Artist artist in collection.Artists) {
			string artistDirectory = artist.Name;
			string artistThumbDirectory = Path.Combine(CliOptions.ThumbnailDirectory, artistDirectory);
			Directory.CreateDirectory(artistThumbDirectory);

			foreach (ArtItem artItem in artist.ArtItems) {
				string inputFile = Path.Combine(CliOptions.ArtDirectory, artItem.Path);

				foreach (int size in CliOptions.ThumbnailSize) {
					string filename = $"{Path.GetFileNameWithoutExtension(artItem.Path)}@{size}{Path.GetExtension(artItem.Path)}";
					string outputFile = Path.Combine(artistThumbDirectory, filename);

					if (File.Exists(outputFile)) {
						continue;
					}
					
					bool thumbnailWasGenerated = await GenerateThumbnail(inputFile, outputFile, size);

					if (thumbnailWasGenerated) {
						artItem.Thumbnails[size] = Path.Combine(artistDirectory, filename);
					}
				}
			}
		}
	}
}
