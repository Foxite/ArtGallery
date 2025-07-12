using ArtGallery.Data;
using ArtGallery.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArtGallery.Pages;

public class IndexModel : PageModel {
	private readonly IArtRepository _artRepository;
	
	public ArtCollection ArtCollection { get; private set; }

	public IndexModel(ILogger<IndexModel> logger, IArtRepository artRepository) {
		_artRepository = artRepository;
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
	}
	public string GetSocialIcon(ArtistSocial social) {
		return social.Type switch {
			"Bluesky"     => "/img/social/bsky.png",
			"Carrd"       => "/img/social/carrd.png",
			"Furaffinity" => "/img/social/furaffinity.png",
			"Kofi"        => "/img/social/kofi.png",
			"Ko-fi"       => "/img/social/kofi.png",
			"Linktree"    => "/img/social/linktree.png",
			"Mastodon"    => "/img/social/mastodon.png",
			"Newgrounds"  => "/img/social/newgrounds.png",
			"Patreon"     => "/img/social/patreon.png",
			"Reddit"      => "/img/social/reddit.png",
			"Strawpage"   => "/img/social/strawpage.png",
			"Tumblr"      => "/img/social/tumblr.png",
			"Twitch"      => "/img/social/twitch.png",
			"Twitter"     => "/img/social/twitter.png",
			"X"           => "/img/social/twitter.png",
			"Youtube"     => "/img/social/youtube.png",
			_             => "/img/social/generic.svg",
		};
	}
}
