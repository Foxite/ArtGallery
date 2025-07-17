using ArtGallery.Domain;
using ArtGallery.Generator;
using CommandLine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

var commandParser = new Parser(settings => {
	settings.HelpWriter = Console.Error;
	settings.EnableDashDash = true;
	settings.CaseInsensitiveEnumValues = true;
});
		
try {
	ParserResult<GeneratorOptions>? arguments = commandParser.ParseArguments<GeneratorOptions>(args);
	if (arguments.Errors.Any()) {
		// TODO handle
	}
	
	ArtCollectionGenerator generator = new FilesystemArtCollectionGenerator(arguments.Value.ArtDirectory);
	ArtCollection collection = await generator.GenerateArtCollection();
	string json = JsonConvert.SerializeObject(collection, new JsonSerializerSettings() {
		NullValueHandling = NullValueHandling.Ignore,
		Converters = {
			new StringEnumConverter(),
		},
	});

	if (arguments.Value.OutputFile == "-") {
		// Explicitly referring to Console.Out here as this is meant to be a CLI tool
		// The distinction between stdout and stderr is important
		Console.Out.Write(json);
	} else {
		File.WriteAllText(arguments.Value.OutputFile, json);
	}
} catch (Exception e) {
	while (true) {
		Console.Error.WriteLine($"{e.GetType().FullName}: {e.Message}");
		Console.Error.WriteLine(e.StackTrace);
		if (e.InnerException != null) {
			e = e.InnerException;
			continue;
		}
		break;
	}
}
