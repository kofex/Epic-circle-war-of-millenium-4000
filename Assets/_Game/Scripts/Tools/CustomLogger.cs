using UnityEngine;
using Object = UnityEngine.Object;

namespace Scripts
{
	public class CustomLogger : ILogger
	{
		public void Log(object message, LogType type) => Log(message, null, type);


		public void Log(object message) => UnityEngine.Debug.Log(message);

		public void Log(object message, Object context, LogType type)
		{
			Color color;

			switch (type)
			{
				case LogType.None:
					color = Color.gray;
					break;
				case LogType.Warning:
					color = Color.yellow;
					break;
				case LogType.Error:
					color = Color.red;
					break;
				case LogType.Info:
					color = Color.blue;
					break;
				default:
					color = Color.white;
					break;
			}
			
			var str = $"[{ColoredLogType(type, color)}] {message}";
			if (context)
				UnityEngine.Debug.Log(str);
			else
				UnityEngine.Debug.Log(str, context);
		}

		private string ColoredLogType(LogType type, Color color)
		{
			return $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{type}</color>"
			;
		}
	}
}