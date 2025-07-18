using CommandLine;

public class GeneratorOptions {
	[Option("directory", Default = ".")]
	public string ArtDirectory { get; set; }
	
	[Option("output", Default = "-")]
	public string OutputFile { get; set; }
	
	[Option("thumbnails", Default = true)]
	public bool GenerateThumbnails { get; set; }
	
	[Option("thumbsize", Default = 320, HelpText = "The image's largest dimension will be set to this, and the other scaled accordingly.")]
	public int ThumbnailSize { get; set; }
	
	[Option("thumbdir", Default = "")]
	public string ThumbnailDirectory { get; set; }
}
