using System;
using System.IO;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Display;

namespace Common.Logger;

public class FileSink : ILogEventSink
{
	private readonly MessageTemplateTextFormatter _formatter;
	private readonly string _logFilePath;

	public FileSink(string logDirectory, MessageTemplateTextFormatter formatter)
	{
		_formatter = formatter;
		_logFilePath = Path.Combine(logDirectory, "Logs", $"log-{DateTime.Now:yyyy-MM-dd}.log");
		ConfigureLogging();
	}

	private void ConfigureLogging()
	{
		var loggerConfig = new LoggerConfiguration()
			.WriteTo.File(
				_formatter,
				_logFilePath,
				rollingInterval: RollingInterval.Day,
				retainedFileCountLimit: 5,
				rollOnFileSizeLimit: false,
				fileSizeLimitBytes: null,
				shared: true)
			.CreateLogger();
		Log.Logger = loggerConfig;
	}

	public void Emit(LogEvent logEvent)
	{
		var message = new StringWriter();
		_formatter.Format(logEvent, message);
		var maskedMessage = MaskSensitiveInfo(message.ToString());
		File.AppendAllText(_logFilePath, maskedMessage + Environment.NewLine);
	}

	private static string MaskSensitiveInfo(string message)
	{
		return CustomRegex.MaskingRegex().Replace(message, match =>
		{
			var wordToMask = match.Groups[1].Value;
			return new string('*', wordToMask.Length);
		});
	}
}