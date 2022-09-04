

using Microsoft.Maui.Storage;

namespace EveMagic.Data.Ocr
{
    public class EveMagicOcr : ICloudOcr
    {
        HttpClient _httpClient;
        public string Url
        {
            get;
            set;
        }
        string maxSideLen;


        public EveMagicOcr(HttpClient httpClient, string url, string maxSideLen="600")
        {
            this._httpClient = httpClient;
            this.Url = url + "/file";
            this.maxSideLen = maxSideLen;
        }


        public async Task<string> Request(byte[] byteFile)
        {
            MultipartFormDataContent content = new();

            content.Add(new StringContent(this.maxSideLen), "maxSideLen");
            content.Add(new ByteArrayContent(byteFile), "file", "1.png");
            string res = "";
            try
            {
                res = (await this._httpClient.PostAsync(Url, content)).Content.ReadAsStringAsync().Result;
            }
            catch (Exception)
            {
                return res;
            }
            return res;
        }
    }
}
