using System.Text;
using CliWrap;

namespace ArtGallery.Generator;

public class FfmpegThumbnailGenerator : ThumbnailGenerator {
	public FfmpegThumbnailGenerator(CliOptions cliOptions) : base(cliOptions) { }

	protected async override Task<bool> GenerateThumbnail(string inputFile, string outputFile, int size) {
		Logger.Instance.LogDebug($"Generating thumbnail for {inputFile} @ {size} ({outputFile})");

		string command;
		string[] arguments;
		if (Path.GetExtension(inputFile) == ".gif") {
			command = "gifski";
			arguments = [
				"-W", size.ToString(),
				"-o", outputFile,
				inputFile,
			];
		} else {
			command = "ffmpeg";
			arguments = [
				"-i", inputFile,
				// https://trac.ffmpeg.org/wiki/Scaling#fit
				// Do not mind the image they use on that page.
				"-vf", $"scale=w={size}:h={size}:force_original_aspect_ratio=decrease",
				outputFile,
				"-y",
			];
		}
		
		Logger.Instance.LogTrace($"{command} {string.Join(" ", arguments)}");
		
		var outputStringBuilder = new StringBuilder();
		var errorStringBuilder = new StringBuilder();
		CommandResult commandResult = await Cli
			.Wrap(command)
			.WithArguments(arguments)
			.WithStandardErrorPipe(PipeTarget.ToStringBuilder(errorStringBuilder))
			.WithStandardOutputPipe(PipeTarget.ToStringBuilder(outputStringBuilder)) // If you remove this, gifski will crash
			.WithValidation(CommandResultValidation.None)
			.ExecuteAsync();

		if (!commandResult.IsSuccess) {
			throw new Exception($"{command} exited with a non-zero exit code: {commandResult.ExitCode}; stdout: {outputStringBuilder}; stderr: {errorStringBuilder}");
		}
		
		return true;
	}
}
