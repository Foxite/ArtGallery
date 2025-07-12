namespace ArtGallery.Domain;

public class Artist {
	public string Name { get; set; }
	public ICollection<ArtItem> ArtItems { get; set; }
	
	// name => url
	public ICollection<ArtistSocial>? Socials { get; set; }
}
