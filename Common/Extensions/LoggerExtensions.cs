using Serilog.Events;
using System.Diagnostics;
using ILogger = Serilog.ILogger;

namespace Common.Extensions;

public static class LoggerExtensions
{
	public static void LogFatal(this ILogger logger, string messageTemplate,
		params object[] propertyValues)
	{
		var stackFrame = new StackFrame(1, true);
		var method = stackFrame.GetMethod();
		var className = method!.DeclaringType!.Name;
		var methodName = method.Name;
		var lineNumber = stackFrame.GetFileLineNumber();

		logger.ForContext("ClassName", className)
			.ForContext("MethodName", methodName)
			.ForContext("LineNumber", lineNumber)
			.Write(LogEventLevel.Fatal, messageTemplate, propertyValues);
	}

	public static void LogError(this ILogger logger, string messageTemplate,
		params object[] propertyValues)
	{
		var stackFrame = new StackFrame(1, true);
		var method = stackFrame.GetMethod();
		var className = method!.DeclaringType!.Name;
		var methodName = method.Name;
		var lineNumber = stackFrame.GetFileLineNumber();

		logger.ForContext("ClassName", className)
			.ForContext("MethodName", methodName)
			.ForContext("LineNumber", lineNumber)
			.Write(LogEventLevel.Error, messageTemplate, propertyValues);
	}

	public static void LogWarning(this ILogger logger, string messageTemplate,
		params object[] propertyValues)
	{
		var stackFrame = new StackFrame(1, true);
		var method = stackFrame.GetMethod();
		var className = method!.DeclaringType!.Name;
		var methodName = method.Name;
		var lineNumber = stackFrame.GetFileLineNumber();

		logger.ForContext("ClassName", className)
			.ForContext("MethodName", methodName)
			.ForContext("LineNumber", lineNumber)
			.Write(LogEventLevel.Warning, messageTemplate, propertyValues);
	}

	public static void LogInfo(this ILogger logger, string messageTemplate,
		params object[] propertyValues)
	{
		var stackFrame = new StackFrame(1, true);
		var method = stackFrame.GetMethod();
		var className = method!.DeclaringType!.Name;
		var methodName = method.Name;
		var lineNumber = stackFrame.GetFileLineNumber();

		logger.ForContext("ClassName", className)
			.ForContext("MethodName", methodName)
			.ForContext("LineNumber", lineNumber)
			.Write(LogEventLevel.Information, messageTemplate, propertyValues);
	}

	public static void LogDebug(this ILogger logger, string messageTemplate,
		params object[] propertyValues)
	{
		var stackFrame = new StackFrame(1, true);
		var method = stackFrame.GetMethod();
		var className = method!.DeclaringType!.Name;
		var methodName = method.Name;
		var lineNumber = stackFrame.GetFileLineNumber();

		logger.ForContext("ClassName", className)
			.ForContext("MethodName", methodName)
			.ForContext("LineNumber", lineNumber)
			.Write(LogEventLevel.Debug, messageTemplate, propertyValues);
	}

	public static void LogVerbose(this ILogger logger, string messageTemplate,
		params object[] propertyValues)
	{
		var stackFrame = new StackFrame(1, true);
		var method = stackFrame.GetMethod();
		var className = method!.DeclaringType!.Name;
		var methodName = method.Name;
		var lineNumber = stackFrame.GetFileLineNumber();

		logger.ForContext("ClassName", className)
			.ForContext("MethodName", methodName)
			.ForContext("LineNumber", lineNumber)
			.Write(LogEventLevel.Verbose, messageTemplate, propertyValues);
	}
}