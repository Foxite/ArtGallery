using ArtGallery.Frontend;
using Newtonsoft.Json;

namespace ArtGallery.Pages;

public class ArtListModel {
	public IEnumerable<ArtListItem> ArtItems { get; set; }
	public bool DisplayArtist { get; set; }
}

public class ArtListItem {
	public DateOnly Date { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public string Path { get; set; }
	public string ArtistName { get; set; }

	[JsonProperty]
	// The JumpHash will be used as a HTML element ID, url jump anchor, and CSS selector.
	// This sanitization will suffice for now, but we need to update this if we ever start
	// adding artists/titles with funny characters.
	public string JumpHash => $"art-{ArtistName}-{Title}".Replace(" ", "_");

	[JsonIgnore]
	public Dictionary<string, string> Thumbnails { get; set; } = new();
	
	[JsonIgnore]
	public ICollection<ArtistSocial> ArtistSocials { get; set; }
}
