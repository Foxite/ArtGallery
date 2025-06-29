namespace ArtGallery.Domain;

public class ArtItem {
	public DateOnly Date { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public string Url { get; set; }
	public Artist Artist { get; set; }
}

public class Artist {
	public string Name { get; set; }
	public ICollection<ArtItem> ArtItems { get; set; }
}
