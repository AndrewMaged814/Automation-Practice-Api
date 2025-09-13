using System;
using System.IO;
using Serilog;
using Serilog.Formatting.Display;
using Xunit.Abstractions;

namespace Common.Logger;

public static class LoggerFactory
{
	public static ILogger GetLogger(ITestOutputHelper testOutputHelper)
	{
		var loggingDirectory =
			Path.Combine(Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.Parent!.FullName, "Common");
		const string format =
			"{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {ClassName}:{MethodName}@{LineNumber} -> {Message:lj}{Exception}";
		var formatter = new MessageTemplateTextFormatter(format);
		var consoleSink = new ConsoleSink(testOutputHelper, formatter);
		var fileSink = new FileSink(loggingDirectory, formatter);

		return new LoggerConfiguration()
			.MinimumLevel.Debug()
			.Enrich.FromLogContext()
			.WriteTo.Sink(consoleSink)
			.WriteTo.Sink(fileSink)
			.CreateLogger();
	}
}