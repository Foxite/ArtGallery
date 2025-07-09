using ArtGallery.Domain;

namespace ArtGallery.Data;

public interface IArtRepository {
	Task<ICollection<ArtItem>> GetAllArtItems(DateOnly? min, DateOnly? max);
	Task<ICollection<ArtItem>?> GetAllArtItems(string artistName);
}
