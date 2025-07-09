using ArtGallery.Generator;
using Newtonsoft.Json;

ArtCollectionGenerator generator = new FilesystemArtCollectionGenerator(Environment.GetEnvironmentVariable("ART_DIRECTORY") ?? throw new Exception("Environment variable ART_DIRECTORY must be set"));
ArtCollection collection = await generator.GenerateArtCollection();
string json = JsonConvert.SerializeObject(collection);

// Explicitly referring to Console.Out here as this is meant to be a CLI tool
// The distinction between stdout and stderr is important
// If this throws an exception, it will be written to stderr.
Console.Out.Write(json);
