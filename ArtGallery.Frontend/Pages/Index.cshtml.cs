using ArtGallery.Data;
using ArtGallery.Frontend;
using ArtGallery.Frontend.Options;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace ArtGallery.Pages;

public class IndexModel : PageModel {
	private readonly IArtRepository _artRepository;
	
	public ArtCollection ArtCollection { get; private set; }
	public IOptions<PageOptions> PageOptions { get; }
	public bool ShowNsfwWarning { get; private set; }
	public int MinYear { get; set; }
	public int MaxYear { get; set; }

	public IndexModel(ILogger<IndexModel> logger, IArtRepository artRepository, IOptions<PageOptions> pageOptions) {
		_artRepository = artRepository;
		PageOptions = pageOptions;
	}

	public async Task OnGet() {
		(MinYear, MaxYear) = await _artRepository.GetYearRange();
		
		ArtCollection = await _artRepository.GetAllArtItems(null, null, null);

		// TODO DRY
		// Nsfw is set to true, and the cookie is not set to "true"
		bool cookieIsSet = Request.Cookies.TryGetValue(Constants.NsfwCookieName, out string? nsfwCookieValue);
		if (PageOptions.Value.Nsfw && (!cookieIsSet || nsfwCookieValue != "true")) {
			ShowNsfwWarning = true;
		}
	}
}
