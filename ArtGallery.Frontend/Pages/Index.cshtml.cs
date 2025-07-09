using ArtGallery.Data;
using ArtGallery.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArtGallery.Pages;

public class IndexModel : PageModel {
	private readonly IArtRepository _artRepository;
	
	public IList<ArtItem> ArtItems { get; private set; }

	public IndexModel(ILogger<IndexModel> logger, IArtRepository artRepository) {
		_artRepository = artRepository;
	}

	public async Task OnGet() {
		var artItems = await _artRepository.GetAllArtItems(null, null);
		ArtItems = artItems
			.Select(art => new ArtItem() {
				Date = art.Date,
				Title = art.Title,
				Description = art.Description,
				Url = art.Url,
				Artist = new Artist() {
					Name = art.Artist.Name,
				},
			})
			.ToList();

		ArtItems = new List<ArtItem>(ArtItems.Concat(ArtItems).Concat(ArtItems));
	}
}
