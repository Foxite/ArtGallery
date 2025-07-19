using CommandLine;

public class CliOptions {
	[Option('d', "directory", Default = ".", HelpText = "Directory to find art in.")]
	public string ArtDirectory { get; set; }
	
	[Option('o', "output", Default = "-", HelpText = "Output path for json document. Use - for stdout.")]
	public string OutputFile { get; set; }
	
	[Option("thumbnails", HelpText = "Generate thumbnails.")]
	public bool GenerateThumbnails { get; set; }
	
	[Option("thumbsize", Separator = ',', Default = new[] { 320 }, HelpText = "The different sizes to generate thumbnails for, separated by comma.\nThe image's largest dimension will be set to this, and the other scaled accordingly.")]
	public IEnumerable<int> ThumbnailSize { get; set; }
	
	[Option("thumbdir", Default = "_thumbs", HelpText = "Directory to output thumbnails.")]
	public string ThumbnailDirectory { get; set; }
	
	// FlagCounter is broken for default verbs https://github.com/commandlineparser/commandline/issues/888
	[Option('v', "verbose", HelpText = "Enable verbose logging.")]
	public bool VerboseLogging { get; set; }
}
