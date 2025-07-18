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
}
