using System.Text.Encodings.Web;
using System.Text.Json.Serialization;

namespace ArtGallery.Domain;

public class ArtItem {
	public DateOnly Date { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public string Path { get; set; }
	public Artist Artist { get; set; }

	[JsonInclude]
	// The JumpHash will be used as a HTML element ID, url jump anchor, and CSS selector.
	// This sanitization will suffice for now, but we need to update this if we ever start
	// adding artists/titles with funny characters.
	public string JumpHash => $"art-{Artist.Name}-{Title}".Replace(" ", "_");
}
