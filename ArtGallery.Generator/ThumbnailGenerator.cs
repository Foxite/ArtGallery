using ArtGallery.Domain;

namespace ArtGallery.Generator;

public abstract class ThumbnailGenerator {
	protected GeneratorOptions GeneratorOptions { get; }
	
	protected ThumbnailGenerator(GeneratorOptions generatorOptions) {
		GeneratorOptions = generatorOptions;
	}
	
	protected abstract Task GenerateThumbnail(string inputFile, string outputFile);

	public async Task GenerateThumbnails(ArtCollection collection) {
		string thumbnailDirectory = GeneratorOptions.ThumbnailDirectory;
		if (string.IsNullOrEmpty(thumbnailDirectory)) {
			thumbnailDirectory = Path.Combine(GeneratorOptions.ArtDirectory, "_thumbs");
		}
		
		foreach (Artist artist in collection.Artists) {
			string artistThumbDirectory = Path.Combine(thumbnailDirectory, artist.Name);
			Directory.CreateDirectory(artistThumbDirectory);
			
			foreach (ArtItem artItem in artist.ArtItems) {
				string inputFile = artItem.Path;
				string outputFile = Path.Combine(artistThumbDirectory, Path.GetFileName(artItem.Path));

				await GenerateThumbnail(inputFile, outputFile);
			}
		}
	}
}
