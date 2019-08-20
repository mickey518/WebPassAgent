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
            StorageFile file = null;
            try
            {
                file = await ApplicationData.Current.LocalFolder.GetFileAsync(_UserPassFile);
            }
            catch (System.IO.FileNotFoundException e)
            {
                file = await ApplicationData.Current.LocalFolder.CreateFileAsync(_UserPassFile);
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<WebPass>>(await FileIO.ReadTextAsync(file));
        }

        public static async Task SaveAsync(IList<WebPass> webPasses)
        {
            var passJson = Newtonsoft.Json.JsonConvert.SerializeObject(webPasses);
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(_UserPassFile, CreationCollisionOption.OpenIfExists);
            await FileIO.WriteTextAsync(file, passJson);
        }

        public static async Task Add(WebPass webPass)
        {
            if (webPass == null) return;
            if (string.IsNullOrWhiteSpace(webPass.Host) && string.IsNullOrWhiteSpace(webPass.Username) && string.IsNullOrWhiteSpace(webPass.Password)) return;
            var webPasses = await GetWebPassesAsync();
            if (webPasses == null)
            {
                webPasses = new List<WebPass>();
            }
            webPasses.Add(webPass);
            await SaveAsync(webPasses);
        }

        public static async Task Delete(WebPass webPass)
        {
            if (webPass == null) return;
            var webPasses = await GetWebPassesAsync();
            if (webPasses == null)
            {
                webPasses = new List<WebPass>();
            }
            var pass = webPasses.Find(p => p.Id == webPass.Id);
            webPasses.Remove(pass);
            await SaveAsync(webPasses);
        }

        private static List<WebPass> InitTestData()
        {
            var passes = new List<WebPass>();
            passes.Add(new WebPass { Id = Guid.NewGuid(), Host = "bing.com", Uri = "bing.com", Username = "bing.user", Password = "password" });
            return passes;
        }
    }
}
