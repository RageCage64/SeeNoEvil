using System.IO;
using System.Text.Json;

namespace SeeNoEvil.Character {
    public static class AnimationParser {
        public static AnimationSetModel ReadAnimationJson(string fileName) {
            StreamReader streamReader = File.OpenText(fileName);
            string text = streamReader.ReadToEnd();
            var options = new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true,
            };
            return JsonSerializer.Deserialize<AnimationSetModel>(text, options);
        }
    }
}