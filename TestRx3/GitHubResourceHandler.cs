using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.IO;
using CefSharp;
namespace TestRx3 {
    public class GitHubResourceHandler : DefaultResourceHandlerFactory {
        private const string baseAddress = "http://github-app/";

        //private static readonly Logger log = LogManager.GetCurrentClassLogger();

        public static IResourceHandlerFactory Instance = new GitHubResourceHandler();

        private static readonly Dictionary<string, string> mappings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
		{
			{
				".css",
				"text/css"
			},
			{
				".gif",
				"image/gif"
			},
			{
				".html",
				"text/html"
			},
			{
				".jpg",
				"image/jpeg"
			},
			{
				".js",
				"text/javascript"
			},
			{
				".png",
				"image/png"
			},
			{
				".svg",
				"image/svg+xml"
			},
			{
				".ttf",
				"application/x-font-ttf"
			},
			{
				".woff",
				"application/font-woff"
			}
		};

        public override ResourceHandler GetResourceHandler(IWebBrowser browser, IRequest request) {
            Stream stream = GitHubResourceHandler.ResolveResourceAsStream(request.Url);
            if (stream == null || stream.Length == 0L) {
                return base.GetResourceHandler(browser, request);
            }
            string extension = Path.GetExtension(GitHubResourceHandler.PathFromUrl(request.Url));
            string mimeType;
            if (!GitHubResourceHandler.mappings.TryGetValue(extension ?? "", out mimeType)) {
                mimeType = "application/octet-stream";
            }
            return ResourceHandler.FromStream(stream, mimeType);
        }

        internal static string ResolveComparisonGraphUrl(string resource) {
            string str = resource.TrimStart(new char[]
			{
				'/'
			});
            return "http://github-app/comparison-graph/" + str;
        }

        internal static string ResolveOcticonsUrl(string resource) {
            string str = resource.TrimStart(new char[]
			{
				'/'
			});
            return "http://github-app/octicons/" + str;
        }

        internal static Stream ResolveResourceAsStream(string url) {
            if (!url.StartsWith("http://github-app/", StringComparison.Ordinal)) {
                if (url.IndexOf("chrome-devtools://", StringComparison.OrdinalIgnoreCase) != 0) {
                    //GitHubResourceHandler.log.Error(CultureInfo.InvariantCulture, "The resource {0} was not expected and cannot be resolved to an embedded resource", url);
                }
                return null;
            }
            string text = GitHubResourceHandler.PathFromUrl(url);
            string fileName = Path.GetFileName(text);
            string text2 = text.Substring(0, text.Length - fileName.Length).Replace("/", ".").Replace("-", "_");
            string name = string.Format(CultureInfo.InvariantCulture, "GitHub.data.{0}{1}", new object[]
			{
				text2,
				fileName
			});
            Stream manifestResourceStream = typeof(App).Assembly.GetManifestResourceStream(name);
            if (manifestResourceStream == null) {
                //GitHubResourceHandler.log.Error(CultureInfo.InvariantCulture, "The resource {0} does not have a matching resource embedded in the assembly", url);
            }
            return manifestResourceStream;
        }

        internal static string ResolveResourceAsText(string url) {
            Stream stream = GitHubResourceHandler.ResolveResourceAsStream(url);
            if (stream == null) {
                return null;
            }
            string result;
            using (StreamReader streamReader = new StreamReader(stream)) {
                result = streamReader.ReadToEnd();
            }
            return result;
        }

        internal static string PathFromUrl(string url) {
            string text = url.Substring("http://github-app/".Length);
            int num = text.IndexOf('#');
            if (num > -1) {
                text = text.Substring(0, num);
            }
            return text;
        }

        public GitHubResourceHandler()
            : base(null) {
        }
    }
}
