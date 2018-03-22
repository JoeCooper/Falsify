using System.Collections.Immutable;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Falsify.Encodings
{
	public class Base64URLEncoding : IFalsify
	{
		const int bufferSize = 1024;

		static readonly IImmutableSet<char> Base64UrlCharacters = ImmutableHashSet.CreateRange("qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890_-=".ToCharArray());

		readonly Encoding encoding;

		//The default encoding of UTF8 is intended to match the default encoding of StreamReader (APTM)
		public Base64URLEncoding(): this(Encoding.UTF8)
		{
		}

		public Base64URLEncoding(Encoding encoding)
		{
			this.encoding = encoding;
		}

		public bool Falsify(Stream stream)
		{
			var buffer = new char[bufferSize];
			using (var reader = new StreamReader(stream, encoding))
			{
				int bytesRead;
				do
				{
					bytesRead = reader.ReadBlock(buffer, 0, buffer.Length);
					for (var i = 0; i < bytesRead; i++)
					{
						if (!Base64UrlCharacters.Contains(buffer[i]))
						{
							return true;
						}
					}
				}
				while (bytesRead > 0);
			}
			return false;
		}

		public async Task<bool> FalsifyAsync(Stream stream)
		{
			var buffer = new char[bufferSize];
			using (var reader = new StreamReader(stream, encoding))
			{
				int bytesRead;
				do
				{
					bytesRead = await reader.ReadBlockAsync(buffer, 0, buffer.Length);
					for (var i = 0; i < bytesRead; i++)
					{
						if (!Base64UrlCharacters.Contains(buffer[i]))
						{
							return true;
						}
					}
				}
				while (bytesRead > 0);
			}
			return false;
		}
	}
}
