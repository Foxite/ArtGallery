namespace ArtGallery.Frontend;

public class ArtistSocial {
	public string Url { get; set; }

	public string Type {
		get {
			string host;
			try {
				host = new Uri(Url).Host;
			} catch (UriFormatException) {
				return "Generic";
			}

			if (host.EndsWith(".straw.page")) {
				return "Strawpage";
			} else if (host.EndsWith(".carrd.co")) {
				return "Carrd";
			} else if (host.EndsWith(".bsky.social")) {
				return "Bluesky";
			} else if (host.EndsWith(".tumblr.com")) {
				return "Tumblr";
			} else if (host.EndsWith(".neocities.org")) {
				return "Neocities";
			}

			if (host.StartsWith("www.")) {
				host = host["www.".Length..];
			}
			
			return host switch {
				"artfight.net"    => "Artfight",
				"bsky.app"        => "Bluesky",
				"furaffinity.net" => "Furaffinity",
				"instagram.com"   => "Instagram",
				"ko-fi.com"       => "Kofi",
				"linktr.ee"       => "Linktree",
				"newgrounds.com"  => "Newgrounds",
				"patreon.com"     => "Patreon",
				"reddit.com"      => "Reddit",
				"redd.it"         => "Reddit",
				"spacehey.com"    => "Spacehey",
				"tiktok.com"      => "TikTok",
				"toyhou.se"       => "Toyhouse",
				"trello.com"      => "Trello",
				"tumblr.com"      => "Tumblr",
				"twitch.tv"       => "Twitch",
				"twitter.com"     => "Twitter",
				"vgen.co"         => "VGen",
				"x.com"           => "Twitter",
				"youtube.com"     => "Youtube",
				"youtu.be"        => "Youtube",
				_                 => "Generic",
			};
		}
	}

	public string GetSocialIconUrl() {
		return Type switch {
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
