using System.Text.RegularExpressions;

namespace Common.Logger;

public partial class CustomRegex
{
	[GeneratedRegex(
		@"(?<=ClientSecret = |""client_secret"":""|Password = |""password"":""|EncryptionKey = |""encryption_key"":"")(.+?)(?=\s|,|$|"")",
		RegexOptions.Multiline)]
	public static partial Regex MaskingRegex();
}