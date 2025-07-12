using ArtGallery.Domain;

namespace ArtGallery.Generator;

public abstract class ArtCollectionGenerator {
	public abstract Task<ArtCollection> GenerateArtCollection();
}
