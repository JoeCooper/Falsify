using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Falsify.Encodings.Tests
{
	public class Base64UrlEncodingTests
	{
		[Theory(DisplayName = "Base64URLEncoding.Falsify accepts valid data.")]
		[InlineData("cJ1vJy4uLmo4y4GnslkET0QwFQSvqQtKVoWXLs6YbZ7laBsDAnlCBaeHm6sewloBkcYHtp7gn8eWJNCkmv-boFniwkY-BUzK8hNhJA1gK5QvlZ0EkLr8BMxTaN2onWqn7_g")]
		[InlineData("TgAF8OIeTjAj393ujH5PHkFYpMPnua5ee6cbyJUpLk4EhLzkBfSEI9X9nCFVJk25GwgYUFY67JVRp3U7A4ab-bGP6QfCahhN1IHt6RyNCVVZYa2MEd33ypgmk15_SR9PYqNvxTxF97Dwy3zVTQJ4w54f4aHZD-fUlx9vm9a6zYpinod5HUMYE2r8m0VgInyWrs-_X2d9dlK7my4c1lvaNg")]
		[InlineData("8nKve_-lWgfON9xzonN0otjtBvptIuQnNL2CFfs8DyJlR22wzRuuKmVYmUaZ3hgouXr-btM7kM-Oh0ZBI0ULkii6DlsFxNS9PtyVFYmdjlWkWVuBPqBOKradIUbUbQYfTQce3tC9IvWlQTD2pntG8PjwnMnqiVOHbOSlZ6KO6NDWdsiCf6-F")]
		[InlineData("N2bc_XBXhDtXMU3k0Dw1w7OXcuwYulIWWF8gMLPEcUlrtJKDrFYXFGBpEHFABACZQATO6D2Yz3wUxaQQNlSIksAEeBuqsRZcf12Mrqrg1lRfh--u9YkL0uUVb-eyBt4L4X2xO4vk74_tcmjgDCHSM-9Psa9k2Z02-aY2lEEvqnh_gMwsL5rJe5dfwIIgK8bDQzz3YlUupwjgekLkrVtNvsjvYItUpQRVhjUXBRg")]
		public void Accept(string s)
		{
			var bytes = System.Text.Encoding.UTF8.GetBytes(s);
			using (var stream = new MemoryStream(bytes))
			{
				var falsifier = new Base64URLEncoding();
				Assert.False(falsifier.Falsify(stream));
			}
		}

		[Theory(DisplayName = "Base64URLEncoding.FalsifyAsync accepts valid data.")]
		[InlineData("Fs3gclEFN0cjUNnETLRX5c08EgK6MMo6bp04otwfFqHvxWR1BDoJSIaBzBCykFlP_aBn2_IWYOUDpgO0zZVqAzk31YZQ4uCTMsp7OWS5h95D6KQ7uDuv2GV-cV02VXrwFsnXerrS_hu_1KgomiaBSR4lcOTxLkr9iP0h0XCDXd1PiyKa4gwjcOmJe-OThj0-_Sg_EeWW_w")]
		[InlineData("ftnyvbEtqjx3y9kPBOZmEQH_-miyZ2oBv-LTSENM97iLOhwJGmW_vN4-bMitLjoXwi0P5ZuXNBdv8P7PWWL5K9Mgg8nKXfecQD2JdPqiVJGovIUWPBIc9AUEmSln8GN8RUH0jKpwTGQ2c6VeDcAdzZtsJpzc4u7BA47DxsPNNFDaDS64rcfmN3Ah38lsvudF58yO-snGn2JTM4kbrIagaD5UgwCSZCDThu4suwIkVESnft73PzqHFA")]
		[InlineData("y2V6PsuL6YB3ixg757KCq7p02prkYC7eRXQtp6gpBM1vksf-pZW7Y0M6i5k8CMDMXEuExax6iWs2m_lWXUHXTYZKzFTrhAYkuXdWqdSVoTyzYUtdpD4h9AhcxwmcZAUBYz4S8jxr4ddFXww1I9-Wofq8ovz1eqhHKddMjrz8iFVxVUaVg68ASH9zF0Qy7NEpxSYP38EJDOiVyslkH1IbSde3w86YAB1KvFp8gWNjFTwFIsmV7Kk")]
		[InlineData("aEhe_7jmmyktWM5teygDkMQIxSJnjIqFMrWMXvyIhlwuI1uFOgFlZCAxTymU9ZkUApIFjCeGLNu-2M0DWj0spNJxP1jNfVXDo2EBNj1h8Dwbd1xqg_k1p_d_x2EzYQrjBKpXI7ZBR4Q8QenFhPWeXeiQ5-LhwiHYPJdgTrb0j0FXHTGTCc2G2jDXLUEPbyMB4Gp5o3knAkI")]
		public async Task AcceptAsync(string s)
		{
			var bytes = System.Text.Encoding.UTF8.GetBytes(s);
			using (var stream = new MemoryStream(bytes))
			{
				var falsifier = new Base64URLEncoding();
				Assert.False(await falsifier.FalsifyAsync(stream));
			}
		}

		[Theory(DisplayName = "Base64URLEncoding.Falsify rejects invalid data.")]
		[InlineData("asdfqwerasdfhweimerv$1512g")]
		[InlineData("asdfqwer;asdfhweimerv151512g")]
		[InlineData("asdfqwe1240/asdfhweimerv151512g")]
		public void Reject(string s)
		{
			var bytes = System.Text.Encoding.UTF8.GetBytes(s);
			using(var stream = new MemoryStream(bytes))
			{
				var falsifier = new Base64URLEncoding();
				Assert.True(falsifier.Falsify(stream));
			}
		}

		[Theory(DisplayName = "Base64URLEncoding.FalsifyAsync rejects invalid data.")]
		[InlineData("asdfqwerasdfhweimerv$1512g")]
		[InlineData("asdfqwer;asdfhweimerv151512g")]
		[InlineData("asdfqwe1240/asdfhweimerv151512g")]
		public async Task RejectAsync(string s)
		{
			var bytes = System.Text.Encoding.UTF8.GetBytes(s);
			using (var stream = new MemoryStream(bytes))
			{
				var falsifier = new Base64URLEncoding();
				Assert.True(await falsifier.FalsifyAsync(stream));
			}
		}
	}
}
