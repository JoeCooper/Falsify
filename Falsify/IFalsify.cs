using System.IO;
using System.Threading.Tasks;

namespace Falsify
{
	public interface IFalsify
	{
		bool Falsify(Stream stream);
		Task<bool> FalsifyAsync(Stream stream);
	}
}
