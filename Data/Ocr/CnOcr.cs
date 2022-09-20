

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


        public string Request(byte[] byteFile)
        {
            MultipartFormDataContent content = new();

            content.Add(new ByteArrayContent(byteFile), "image", "1.png");
            string res = "";
            try
            {
                res = this._httpClient.PostAsync(Url, content).Result.Content.ReadAsStringAsync().Result;
            }
            catch (Exception e)
            {
                return res;
            }
            return res;
        }
    }
}
