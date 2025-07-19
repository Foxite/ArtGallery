namespace ArtGallery.Frontend.Options;

public class PageOptions {
	public const string NsfwCookieName = "nsfwAccepted";
	
	public string Title { get; set; }
	public string? OgDescription { get; set; }
	public string? OgImageUrl { get; set; }
	public bool Nsfw { get; set; }
}
