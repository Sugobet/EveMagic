

namespace EveMagic.Data.Ocr
{
    public class IinsideOcr: ICloudOcr
    {
        HttpClient _httpClient;
        public string Url = "http://www.iinside.cn:7001/api_req";

        public IinsideOcr(HttpClient hc)
        {
            this._httpClient = hc;
        }

        public string Request(byte[] fileByte)
        {
            MultipartFormDataContent content = new();

            // content.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
            content.Add(new StringContent("ocr_pp"), "reqmode");
            content.Add(new StringContent("8907"), "password");
            content.Add(new ByteArrayContent(fileByte), "image_ocr_pp", "1.png");
            string res = "";
            try
            {
                res = this._httpClient.PostAsync(Url, content).Result.Content.ReadAsStringAsync().Result;
            }
            catch (Exception)
            {
                return res;
            }
            return res;
        }
    }
}
