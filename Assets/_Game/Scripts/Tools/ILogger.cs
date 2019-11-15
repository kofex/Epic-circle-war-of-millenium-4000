namespace Scripts
{
	public enum LogType
	{
		None,
		Warning,
		Error,
		Info,
		MissCall,
	}

	public interface ILogger
	{
		void Log(object message, LogType type);
		void Log(object message);
	}
}