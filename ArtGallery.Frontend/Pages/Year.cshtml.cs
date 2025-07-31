using ArtGallery.Data;
using ArtGallery.Frontend;
using ArtGallery.Frontend.Options;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace ArtGallery.Pages;

public class YearModel : PageModel {
	private readonly IArtRepository _artRepository;
	
	public ArtCollection ArtCollection { get; private set; }
	public IOptions<PageOptions> PageOptions { get; }
	public bool ShowNsfwWarning { get; private set; }
	public int MinYear { get; set; }
	public int MaxYear { get; set; }
	public IEnumerable<ArtListItem> ArtItems { get; set; }
	
	public YearModel(IArtRepository artRepository, IOptions<PageOptions> pageOptions) {
		_artRepository = artRepository;
		PageOptions = pageOptions;
	}

	public async Task OnGet(int year) {
		ViewData[Constants.SelectedYearViewData] = year;
		(MinYear, MaxYear) = await _artRepository.GetYearRange();
		
		ArtCollection = await _artRepository.GetAllArtItems(new DateOnly(year, 1, 1), new DateOnly(year + 1, 1, 1), null);
		ArtItems = ArtCollection.Artists
			.SelectMany(artist => artist.ArtItems)
			.Select(ai => new ArtListItem {
				Date = ai.Date,
				Title = ai.Title,
				Description = ai.Description,
				Path = ai.Path,
				ArtistName = ai.Artist.Name,
				Thumbnails = ai.Thumbnails,
				ArtistSocials = ai.Artist.Socials,
			})
			.OrderByDescending(ali => ali.Date);
		
		// TODO DRY
		// Nsfw is set to true, and the cookie is not set to "true"
		bool cookieIsSet = Request.Cookies.TryGetValue(Constants.NsfwCookieName, out string? nsfwCookieValue);
		if (PageOptions.Value.Nsfw && (!cookieIsSet || nsfwCookieValue != "true")) {
			ShowNsfwWarning = true;
		}
	}
}
