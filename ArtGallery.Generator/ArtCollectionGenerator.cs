namespace ArtGallery.Generator;

public abstract class ArtCollectionGenerator {
	public abstract Task<ArtCollection> GenerateArtCollection();
}
