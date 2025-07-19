namespace ArtGallery.Generator;

public class Logger {
	public static Logger Instance { get; private set; }
	
	private readonly int _verboseLevel;

	private Logger(CliOptions cliOptions) {
		_verboseLevel = cliOptions.VerboseLogging ? 1 : 0;
	}

	private void Log(int level, object? message) {
		if (_verboseLevel >= level) {
			Console.Error.WriteLine(message);
		}
	}

	public void LogError(object? message) => Log(0, message);
	public void LogInfo (object? message) => Log(1, message);
	public void LogDebug(object? message) => Log(2, message);
	public void LogTrace(object? message) => Log(3, message);
	
	public static void CreateLogger(CliOptions cliOptions) {
		if (Instance != null) {
			throw new Exception("Logger has already been created");
		}
		
		Instance = new Logger(cliOptions);
	}
}
