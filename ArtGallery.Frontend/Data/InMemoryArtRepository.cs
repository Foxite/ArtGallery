using ArtGallery.Domain;

namespace ArtGallery.Data;

public abstract class InMemoryArtRepository : IArtRepository {
	protected abstract ICollection<Artist> GetArtItems();

	public Task<ICollection<ArtItem>> GetAllArtItems(DateOnly? min, DateOnly? max) {
		IEnumerable<ArtItem> result = GetArtItems().SelectMany(artist => artist.ArtItems);
		if (min.HasValue) {
			result = result.Where(item => item.Date > min.Value);
		}
		if (max.HasValue) {
			result = result.Where(item => item.Date < max.Value);
		}

		return Task.FromResult<ICollection<ArtItem>>(result.ToList());
	}
	
	public Task<ICollection<ArtItem>?> GetAllArtItems(string artistName) {
		Artist? artist = GetArtItems().FirstOrDefault(artist => artist.Name == artistName);
		if (artist == null) {
			return Task.FromResult<ICollection<ArtItem>?>(null);
		}

		return Task.FromResult<ICollection<ArtItem>?>(artist.ArtItems);
	}
}
