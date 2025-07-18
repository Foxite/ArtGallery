using System.Drawing;
using FFMpegCore;
using FFMpegCore.Enums;

namespace ArtGallery.Generator;

public class FfmpegThumbnailGenerator : ThumbnailGenerator {
	public FfmpegThumbnailGenerator(GeneratorOptions generatorOptions) : base(generatorOptions) { }
	
	protected async override Task GenerateThumbnail(string inputFile, string outputFile) {
		Console.Error.WriteLine($"Generating thumbnail for {inputFile}");
		IMediaAnalysis analysis = await FFProbe.AnalyseAsync(inputFile);
		int width = analysis.PrimaryVideoStream!.Width;
		int height = analysis.PrimaryVideoStream!.Height;

		int newWidth;
		int newHeight;
		if (width > height) {
			newWidth = GeneratorOptions.ThumbnailSize;
			newHeight = GeneratorOptions.ThumbnailSize * height / width;
		} else {
			newHeight = GeneratorOptions.ThumbnailSize;
			newWidth = GeneratorOptions.ThumbnailSize * width / height;
		}
		
		await FFMpegArguments
			.FromFileInput(inputFile)
			.OutputToFile(outputFile, overwrite: true, addArguments: opts => {
				opts.Resize(newWidth, newHeight);
			})
			.ProcessAsynchronously();
	}
}
