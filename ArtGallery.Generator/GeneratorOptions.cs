using CommandLine;

public class GeneratorOptions {
	[Option("directory", Default = ".")]
	public string ArtDirectory { get; set; }
	
	[Option("output", Default = "-")]
	public string OutputFile { get; set; }
}
