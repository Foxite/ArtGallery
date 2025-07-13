using ArtGallery.Data;
using ArtGallery.Domain;
using ArtGallery.Frontend.Options;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace ArtGallery.Pages;

public class IndexModel : PageModel {
	public const string NsfwCookieName = "nsfwAccepted";
	
	private readonly IArtRepository _artRepository;
	
	public ArtCollection ArtCollection { get; private set; }
	public IOptions<PageOptions> PageOptions { get; }
	public bool ShowNsfwWarning { get; private set; }

	public IndexModel(ILogger<IndexModel> logger, IArtRepository artRepository, IOptions<PageOptions> pageOptions) {
		_artRepository = artRepository;
		PageOptions = pageOptions;
	}

	public async Task OnGet() {
		ArtCollection = await _artRepository.GetAllArtItems(null, null, null);

		foreach (Artist artist in ArtCollection.Artists) {
			foreach (ArtItem artItem in artist.ArtItems) {
				artItem.Artist = new Artist() {
					Name = artItem.Artist.Name,
				};
			}
		}

		// Nsfw is set to true, and the cookie is not set to "true"
		bool cookieIsSet = Request.Cookies.TryGetValue(NsfwCookieName, out string? nsfwCookieValue);
		if (PageOptions.Value.Nsfw && (!cookieIsSet || nsfwCookieValue != "true")) {
			ShowNsfwWarning = true;
		}
	}
	
	public string GetSocialIcon(ArtistSocial social) {
		return social.Type switch {
			"Artfight"    => "/img/social/artfight.png",
			"Bluesky"     => "/img/social/bluesky.svg",
			"Bsky"        => "/img/social/bluesky.svg",
			"Carrd"       => "/img/social/carrd.png",
			"Furaffinity" => "/img/social/furaffinity.png",
			"Instagram"   => "/img/social/instagram.png",
			"Kofi"        => "/img/social/kofi.png",
			"Linktree"    => "/img/social/linktree.svg",
			"Mastodon"    => "/img/social/mastodon.svg",
			"Neocities"   => "/img/social/neocities.svg",
			"Newgrounds"  => "/img/social/newgrounds.png",
			"Patreon"     => "/img/social/patreon.png",
			"Reddit"      => "/img/social/reddit.png",
			"Strawpage"   => "/img/social/strawpage.png",
			"Spacehey"    => "/img/social/spacehey.svg",
			"TikTok"      => "/img/social/tiktok.svg",
			"Toyhouse"    => "/img/social/toyhouse.svg",
			"Trello"      => "/img/social/trello.svg",
			"Tumblr"      => "/img/social/tumblr.svg",
			"Twitch"      => "/img/social/twitch.png",
			"Twitter"     => "/img/social/twitter.png",
			"VGen"        => "/img/social/vgen.png",
			"X"           => "/img/social/twitter.png",
			"Youtube"     => "/img/social/youtube.png",
			_             => "/img/social/generic.svg",
			
		};
	}
}
