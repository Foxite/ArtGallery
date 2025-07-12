using ArtGallery.Domain;
using ArtGallery.Generator;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

try {
	ArtCollectionGenerator generator = new FilesystemArtCollectionGenerator(Environment.GetEnvironmentVariable("ART_DIRECTORY") ?? throw new Exception("Environment variable ART_DIRECTORY must be set"));
	ArtCollection collection = await generator.GenerateArtCollection();
	string json = JsonConvert.SerializeObject(collection, new JsonSerializerSettings() {
		NullValueHandling = NullValueHandling.Ignore,
		Converters = {
			new StringEnumConverter(),
		},
	});

	// Explicitly referring to Console.Out here as this is meant to be a CLI tool
	// The distinction between stdout and stderr is important
	Console.Out.Write(json);
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
