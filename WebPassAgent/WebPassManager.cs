using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;

namespace WebPassAgent
{
    public class WebPassManager
    {
        private static string _UserPassFile = "user_pass.json";
        public static async Task<List<WebPass>> GetWebPassesAsync()
        {
            var file = await ApplicationData.Current.LocalFolder.GetFileAsync(_UserPassFile);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<WebPass>>(await FileIO.ReadTextAsync(file));
        }

        public static async Task SaveAsync(List<WebPass> webPasses)
        {
            var passJson = Newtonsoft.Json.JsonConvert.SerializeObject(webPasses);
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(_UserPassFile, CreationCollisionOption.OpenIfExists);
            await FileIO.WriteTextAsync(file, passJson);
        }

        private static List<WebPass> InitTestData()
        {
            var passes = new List<WebPass>();
            passes.Add(new WebPass { Id = Guid.NewGuid(), Host = "bing.com", Uri = "bing.com", Username = "bing.user", Password = "password" });
            return passes;
        }
    }
}
