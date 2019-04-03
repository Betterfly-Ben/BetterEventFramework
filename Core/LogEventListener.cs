using UnityEngine;

namespace Betterfly.BetterEventFramework
{
	public class LogEventListener : MonoBehaviour
	{
		private void Awake()
		{
//			Debug.Log($"{typeof(LogEventListener)} Awake");
			BetterEventFactory.AddCall<LogEvent>(OnLogCall, 0);
			BetterEventFactory.AddCall<LogFormatEvent>(OnLogFormatCall, 0);
			BetterEventFactory.AddCall<LogWarningEvent>(OnLogWarningCall, 0);
			BetterEventFactory.AddCall<LogWarningFormatEvent>(OnLogWarningFormatCall, 0);
			BetterEventFactory.AddCall<LogErrorEvent>(OnLogErrorCall, 0);
			BetterEventFactory.AddCall<LogErrorFormatEvent>(OnLogErrorFormatCall,0);
			BetterEventFactory.AddCall<LogExceptionEvent>(OnLogExceptionCall,0);
		}

		private void OnDestroy()
		{
//			Debug.Log($"{typeof(LogEventListener)} OnDestroy");
			BetterEventFactory.RemoveCall<LogEvent>(OnLogCall, 0);
			BetterEventFactory.RemoveCall<LogFormatEvent>(OnLogFormatCall, 0);
			BetterEventFactory.RemoveCall<LogWarningEvent>(OnLogWarningCall, 0);
			BetterEventFactory.RemoveCall<LogWarningFormatEvent>(OnLogWarningFormatCall, 0);
			BetterEventFactory.RemoveCall<LogErrorEvent>(OnLogErrorCall, 0);
			BetterEventFactory.RemoveCall<LogErrorFormatEvent>(OnLogErrorFormatCall,0);
			BetterEventFactory.RemoveCall<LogExceptionEvent>(OnLogExceptionCall,0);
		}

		private void OnLogExceptionCall(ref LogExceptionEvent eventdata)
		{
			if (eventdata.LogContext != null)
			{
				Debug.LogException(eventdata.LogException,eventdata.LogContext);
			}
			else
			{
				Debug.LogException(eventdata.LogException);
			}
		}

		private void OnLogErrorFormatCall(ref LogErrorFormatEvent eventdata)
		{
			if (eventdata.LogContext != null)
			{
				Debug.LogErrorFormat(eventdata.LogContext,eventdata.LogFormat,eventdata.LogArgs);
			}
			else
			{
				Debug.LogErrorFormat(eventdata.LogFormat,eventdata.LogArgs);
			}
		}

		private void OnLogErrorCall(ref LogErrorEvent eventdata)
		{
			if (eventdata.LogContext != null)
			{
				Debug.LogError(eventdata.LogMessage,eventdata.LogContext);
			}
			else
			{
				Debug.LogError(eventdata.LogMessage);
			}
		}

		private void OnLogWarningFormatCall(ref LogWarningFormatEvent eventdata)
		{
			if (eventdata.LogContext != null)
			{
				Debug.LogWarningFormat(eventdata.LogContext,eventdata.LogFormat,eventdata.LogArgs);
			}
			else
			{
				Debug.LogWarningFormat(eventdata.LogFormat,eventdata.LogArgs);
			}
		}

		private void OnLogWarningCall(ref LogWarningEvent eventdata)
		{
			if (eventdata.LogContext != null)
			{
				Debug.LogWarning(eventdata.LogMessage,eventdata.LogContext);
			}
			else
			{
				Debug.LogWarning(eventdata.LogMessage);
			}
		}

		private void OnLogFormatCall(ref LogFormatEvent eventdata)
		{
			if (eventdata.LogContext != null)
			{
				Debug.LogFormat(eventdata.LogContext,eventdata.LogFormat,eventdata.LogArgs);
			}
			else
			{
				Debug.LogFormat(eventdata.LogFormat,eventdata.LogArgs);
			}
		}

		protected virtual void OnLogCall(ref LogEvent eventData)
		{
			if (eventData.LogContext != null)
			{
				Debug.Log(eventData.LogMessage,eventData.LogContext);
			}
			else
			{
				Debug.Log(eventData.LogMessage);
			}
		}
	}
}