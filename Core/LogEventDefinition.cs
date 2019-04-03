
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Object = UnityEngine.Object;
	
namespace Betterfly.BetterEventFramework
{
	public struct LogEvent : IEventUnit
	{
		public EventUnitState UnitState { get; set; }
		public object LogMessage { get; set; }
		public Object LogContext { get; set; }

		public LogEvent(object logMessage, Object logContext)
		{
			UnitState = EventUnitState.Continue;
			LogMessage = logMessage;
			LogContext = logContext;
		}

	}

	public struct LogWarningEvent : IEventUnit
	{
		public EventUnitState UnitState { get; set; }
		public object LogMessage { get; set; }
		public Object LogContext { get; set; }

		public LogWarningEvent(object logMessage, Object logContext)
		{
			UnitState = EventUnitState.Continue;
			LogMessage = logMessage;
			LogContext = logContext;
		}
	}

	public struct LogErrorEvent : IEventUnit
	{
		public EventUnitState UnitState { get; set; }
		public object LogMessage { get; set; }
		public Object LogContext { get; set; }

		public LogErrorEvent(object logMessage, Object logContext)
		{
			UnitState = EventUnitState.Continue;
			LogMessage = logMessage;
			LogContext = logContext;
		}
	}

	public struct LogExceptionEvent : IEventUnit
	{
		public EventUnitState UnitState { get; set; }
		public Exception LogException { get; set; }
		public Object LogContext { get; set; }

		public LogExceptionEvent(Exception logException, Object logContext)
		{
			UnitState = EventUnitState.Continue;
			LogException = logException;
			LogContext = logContext;
		}
	}

	public struct LogFormatEvent : IEventUnit
	{
		public EventUnitState UnitState { get; set; }
		public Object LogContext { get; set; }
		public string LogFormat { get; set; }
		public object[] LogArgs { get; set; }

		public LogFormatEvent(Object logContext, string logFormat, object[] logArgs)
		{
			UnitState = EventUnitState.Continue;
			LogContext = logContext;
			LogFormat = logFormat;
			LogArgs = logArgs;
		}
	}

	public struct LogWarningFormatEvent : IEventUnit
	{
		public EventUnitState UnitState { get; set; }
		public Object LogContext { get; set; }
		public string LogFormat { get; set; }
		public object[] LogArgs { get; set; }

		public LogWarningFormatEvent(Object logContext, string logFormat, object[] logArgs)
		{
			UnitState = EventUnitState.Continue;
			LogContext = logContext;
			LogFormat = logFormat;
			LogArgs = logArgs;
		}
	}

	public struct LogErrorFormatEvent : IEventUnit
	{
		public EventUnitState UnitState { get; set; }
		public Object LogContext { get; set; }
		public string LogFormat { get; set; }
		public object[] LogArgs { get; set; }

		public LogErrorFormatEvent(Object logContext, string logFormat, object[] logArgs)
		{
			UnitState = EventUnitState.Continue;
			LogContext = logContext;
			LogFormat = logFormat;
			LogArgs = logArgs;
		}
	}
}