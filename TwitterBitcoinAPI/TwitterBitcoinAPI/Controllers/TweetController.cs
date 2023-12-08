using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace TwitterBitcoinAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TweetController : ControllerBase {
        [HttpGet]
        [Route("/")]//main page, used to check connection
        public Task<string> ConnectionCheck() {
            Console.WriteLine(200);
            return Task.FromResult("200");
        }

        //TODO: Not this
        [HttpGet]
        [Route("/GetLatestTweets/{number}")]
        public async Task<string> GetLatestTweets(string number) {
            Console.WriteLine($"GetLatestTweets/{number}");
            int _number;
            try {
                _number = int.Parse(number);
            }
            catch {
                return "400";
            }
            CsvDataExtractor csvDataExtractor = new CsvDataExtractor();
            List<Tweet> tweets = (List<Tweet>)csvDataExtractor.ExtractDataFromCsv("../mbsa.csv", _number);
            return tweets[0].ToString();
        }

        [HttpGet]
        [Route("/GetFirst/{number}")]
        public async Task<string> GetTenFirst(string number) {
            Console.WriteLine($"GetFirst/{number}");
            int _number;
            try {
                _number = int.Parse(number);
            }
            catch {
                return "400";
            }
            CsvDataExtractor csvDataExtractor = new CsvDataExtractor();
            List<Tweet> tweets = (List<Tweet>)csvDataExtractor.ExtractDataFromCsv("../mbsa.csv", _number);
            return JsonSerializer.Serialize(tweets);
        }

        [HttpGet]
        [Route("/GetBetween+{number0}&{number1}")]
        public async Task<string> GetBetween(string number0, string number1) {
            Console.WriteLine($"/GetBetween+{number0}&{number1}");
            int _number0;
            int _number1;
            try {
                _number0 = int.Parse(number0);
                _number1 = int.Parse(number1);
            }
            catch {
                return "400";
            }

            CsvDataExtractor csvDataExtractor = new CsvDataExtractor();
            List<Tweet> tweets = (List<Tweet>)csvDataExtractor.ExtractDataFromCsv("../mbsa.csv", _number1);
            tweets.RemoveRange(0, _number0 -1);
            return JsonSerializer.Serialize(tweets);
        }
    }
}