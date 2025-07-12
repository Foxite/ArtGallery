namespace ArtGallery.Domain;

public class ArtItem {
	public DateOnly Date { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public string Path { get; set; }
	public Artist Artist { get; set; }
}
