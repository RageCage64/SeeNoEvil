using System.IO;
using System.Text.Json;

namespace SeeNoEvil.Tiled {
    public static class TiledParser {
        public static TiledModel ReadMapJson(string fileName) {
			StreamReader streamReader = File.OpenText(fileName);
			string text = streamReader.ReadToEnd();
			var options = new JsonSerializerOptions {
				PropertyNameCaseInsensitive = true,
			};
			return JsonSerializer.Deserialize<TiledModel>(text, options);
        }
    }
}