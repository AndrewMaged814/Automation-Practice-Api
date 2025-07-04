using System.Collections.Generic;
using System.IO;
using System.Linq;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Xunit.Abstractions;

namespace Common.Logger;

public class ConsoleSink(ITestOutputHelper output, ITextFormatter formatter) : ILogEventSink
{
	public void Emit(LogEvent logEvent)
	{
		var maskedLogEvent = new LogEvent(
			logEvent.Timestamp,
			logEvent.Level,
			logEvent.Exception,
			logEvent.MessageTemplate,
			ConvertProperties(logEvent.Properties));
		var message = new StringWriter();
		formatter.Format(maskedLogEvent, message);
		var maskedMessage = MaskSensitiveInfo(message.ToString());
		output.WriteLine(maskedMessage);
	}

	private static string MaskSensitiveInfo(string message)
	{
		return CustomRegex.MaskingRegex().Replace(message, match =>
		{
			var wordToMask = match.Groups[1].Value;
			return new string('*', wordToMask.Length);
		});
	}

	private static IEnumerable<LogEventProperty> ConvertProperties(
		IReadOnlyDictionary<string, LogEventPropertyValue> properties)
	{
		return properties.Select(property => new LogEventProperty(property.Key, property.Value)).ToList();
	}
}