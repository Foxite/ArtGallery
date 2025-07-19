using ArtGallery.Generator;
using CommandLine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

try {
	ParserResult<CliOptions>? arguments;
	using (var commandParser = new Parser(settings => {
			settings.HelpWriter = Console.Error;
			settings.EnableDashDash = true;
			settings.CaseInsensitiveEnumValues = true;
			settings.AutoHelp = true;
			settings.AutoVersion = true;
		})) {
		arguments = commandParser.ParseArguments<CliOptions>(args);
		if (arguments.Errors.Any()) {
			// Errors have been displayed by ParseArguments
			return 1;
		}
	}
	
	Logger.CreateLogger(arguments.Value);

	ArtCollectionGenerator generator = new FilesystemArtCollectionGenerator(arguments.Value.ArtDirectory);
	ArtCollection collection = await generator.GenerateArtCollection();
	
	if (arguments.Value.GenerateThumbnails) {
		ThumbnailGenerator thumbnailGenerator = new FfmpegThumbnailGenerator(arguments.Value);
		await thumbnailGenerator.GenerateThumbnails(collection);
	}
	
	string json = JsonConvert.SerializeObject(collection, new JsonSerializerSettings() {
		NullValueHandling = NullValueHandling.Ignore,
		Converters = {
			new StringEnumConverter(),
		},
		Formatting = Formatting.Indented,
	});

	if (arguments.Value.OutputFile == "-") {
		// Explicitly referring to Console.Out here as this is meant to be a CLI tool
		// The distinction between stdout and stderr is important
		Console.Out.Write(json);
	} else {
		File.WriteAllText(arguments.Value.OutputFile, json);
	}

	return 0;
} catch (Exception e) {
	while (true) {
		Logger.Instance.LogError($"{e.GetType().FullName}: {e.Message}");
		Logger.Instance.LogError(e.StackTrace);
		if (e.InnerException != null) {
			e = e.InnerException;
			continue;
		}
		break;
	}
	return 2;
}
