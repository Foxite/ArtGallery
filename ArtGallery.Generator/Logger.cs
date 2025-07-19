namespace ArtGallery.Generator;

public class Logger {
	public static Logger Instance { get; private set; }
	
	private readonly int _verboseLevel;

	private Logger(CliOptions cliOptions) {
		_verboseLevel = cliOptions.VerboseLogging;
	}

	private void Log(int level, string? message) {
		if (_verboseLevel >= level) {
			Console.Error.WriteLine(message);
		}
	}

	public void LogError(string? message) => Log(1, message);
	public void LogInfo (string? message) => Log(1, message);
	public void LogDebug(string? message) => Log(2, message);
	public void LogTrace(string? message) => Log(3, message);
	
	public static void CreateLogger(CliOptions cliOptions) {
		if (Instance != null) {
			throw new Exception("Logger has already been created");
		}
		
		Instance = new Logger(cliOptions);
	}
}
