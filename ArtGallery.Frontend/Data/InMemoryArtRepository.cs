using ArtGallery.Domain;

namespace ArtGallery.Data;

public abstract class InMemoryArtRepository : IArtRepository {
	protected abstract ArtCollection GetArtItems();

	public Task<ArtCollection> GetAllArtItems(DateOnly? min, DateOnly? max, string? artistName) {
		var artCollection = GetArtItems();

		if (artistName != null) {
			artCollection.Artists = artCollection.Artists.Where(artist => artist.Name == artistName).ToList();
		}
		
		foreach (var artist in artCollection.Artists) {
			var artItems = artist.ArtItems.AsEnumerable();
			
			if (min.HasValue) {
				artItems = artItems.Where(ai => ai.Date >= min.Value);
			}

			if (max.HasValue) {
				artItems = artItems.Where(ai => ai.Date < max.Value);
			}

			artist.ArtItems = artItems.ToList();
			
			foreach (ArtItem artItem in artist.ArtItems) {
				artItem.Artist = artist;
			}
		}
		
		return Task.FromResult(artCollection);
	}
}
