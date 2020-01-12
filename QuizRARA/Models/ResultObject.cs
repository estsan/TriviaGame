using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace TriviaGame.Models
{
    class ResultObject
    {
        [JsonProperty("response_code")]
        public string ResponseCode { get; set; }

        [JsonProperty("results")]
        public Result[] Results { get; set; }

        public async void CreateQuestion()
        {
            // Geografi = category=22
            string url = "https://opentdb.com/api.php?amount=1&category=9&difficulty=medium&type=multiple";
            HttpClient ApiHelper = new HttpClient();
            ApiHelper.DefaultRequestHeaders.Accept.Clear();
            ApiHelper.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            ResultObject _resultObject;

            using (HttpResponseMessage response = await ApiHelper.GetAsync(url))
            {
                var answer = response.Content.ReadAsStringAsync();
                _resultObject = JsonConvert.DeserializeObject<ResultObject>(answer.Result);
                ResponseCode = _resultObject.ResponseCode;
                Results = _resultObject.Results;
            }
        }
    }
}
