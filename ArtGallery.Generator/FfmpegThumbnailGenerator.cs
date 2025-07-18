using System.Drawing;
using FFMpegCore;
using FFMpegCore.Enums;

namespace ArtGallery.Generator;

public class FfmpegThumbnailGenerator : ThumbnailGenerator {
	public FfmpegThumbnailGenerator(GeneratorOptions generatorOptions) : base(generatorOptions) { }

	protected async override Task<bool> GenerateThumbnail(string inputFile, string outputFile, int size) {
		Console.Error.WriteLine($"Generating thumbnail for {inputFile}");
		IMediaAnalysis analysis = await FFProbe.AnalyseAsync(inputFile);
		int width = analysis.PrimaryVideoStream!.Width;
		int height = analysis.PrimaryVideoStream!.Height;

		if (width < size && height < size) {
			return false;
		}

		int newWidth;
		int newHeight;
		if (width > height) {
			newWidth = size;
			newHeight = size * height / width;
		} else {
			newHeight = size;
			newWidth = size * width / height;
		}
		
		await FFMpegArguments
			.FromFileInput(inputFile)
			.OutputToFile(outputFile, overwrite: true, addArguments: opts => {
				opts.Resize(newWidth, newHeight);
			})
			.ProcessAsynchronously();
		
		return true;
	}
}
