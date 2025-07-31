using ArtGallery.Frontend;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ArtGallery.Data;

public abstract class InMemoryArtRepository : IArtRepository {
	protected abstract Task<ArtCollection> GetArtItems();

	public async Task<(int Min, int Max)> GetYearRange() {
		var artCollection = await GetArtItems();
		int min = -1;
		int max = -1;

		foreach (ArtItem item in artCollection.Artists.SelectMany(artist => artist.ArtItems)) {
			if (min == -1 || item.Date.Year < min) {
				min = item.Date.Year;
			}
			
			if (max == -1 || item.Date.Year > max) {
				max = item.Date.Year;
			}
		}

		return (min, max);
	}

	public async Task<ArtCollection> GetAllArtItems(DateOnly? min, DateOnly? max, string? artistName) {
		var artCollection = await GetArtItems();

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
		
		return artCollection;
	}
}
