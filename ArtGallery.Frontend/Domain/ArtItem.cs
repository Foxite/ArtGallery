using System.Text.Encodings.Web;
using System.Text.Json.Serialization;

namespace ArtGallery.Frontend;

public class ArtItem {
	public DateOnly Date { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public string Path { get; set; }
	public Artist Artist { get; set; }

	// size -> path
	public Dictionary<string, string> Thumbnails { get; set; } = new();
}
