using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace TriviaGame.Models
{
    public class ResultObject
    {
        [JsonProperty("response_code")]
        public string ResponseCode { get; set; }

        [JsonProperty("results")]
        public Result[] Results { get; set; }

        public async Task<ResultObject> CreateQuestion(int[] categoryNumbers, string difficulty)
        {
            string category;
            Random random = new Random();
            int index;
            
            index = random.Next(0, categoryNumbers.Length);
            category = categoryNumbers[index].ToString();
            
            // Difficulties = easy, meduim, hard

            string url = "https://opentdb.com/api.php?amount=1&category=" + category + "&difficulty=" + difficulty + "&type=multiple";
            HttpClient ApiHelper = new HttpClient();
            ApiHelper.DefaultRequestHeaders.Accept.Clear();
            ApiHelper.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            ResultObject _resultObject = new ResultObject();

            using (HttpResponseMessage response = await ApiHelper.GetAsync(url))
            {
                var answer = response.Content.ReadAsStringAsync();
                _resultObject = JsonConvert.DeserializeObject<ResultObject>(answer.Result);
                ResponseCode = _resultObject.ResponseCode;
                Results = _resultObject.Results;
            }
            return _resultObject;
        }
    }
}
