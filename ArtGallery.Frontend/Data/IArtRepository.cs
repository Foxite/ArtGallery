using ArtGallery.Frontend;

namespace ArtGallery.Data;

public interface IArtRepository {
	Task<ArtCollection> GetAllArtItems(DateOnly? min, DateOnly? max, string? artistName);
}
