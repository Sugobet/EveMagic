

namespace EveMagic.Data.Ocr
{
    public class CnOcr: ICloudOcr
    {
        HttpClient _httpClient;
        public string Url
        {
            get;
            set;
        }


        public CnOcr(HttpClient httpClient, string url)
        {
            this._httpClient = httpClient;
            this.Url = url + "/ocr";
        }


        public async Task<string> Request(byte[] byteFile)
        {
            MultipartFormDataContent content = new();

            content.Add(new ByteArrayContent(byteFile), "image", "1.jpg");
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
