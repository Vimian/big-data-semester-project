using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TwitterBitcoinAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TweetController : ControllerBase {
        [HttpGet]
        [Route("/")]//main page, used to check connection
        public Task<string> ConnectionCheck() {
            return Task.FromResult("200");
        }

        //TODO: Not this
        [HttpGet]
        [Route("/GetLatestTweets/{number}")]
        public async Task<string> GetLatestTweets(string number) {
            int _number;
            try {
                _number = int.Parse(number);
            }
            catch {
                return "400";
            }
            CsvDataExtractor csvDataExtractor = new CsvDataExtractor();
            List<Tweet> tweets = (List<Tweet>)csvDataExtractor.ExtractDataFromCsv("C:/Users/simon/source/repos/TwitterBitcoinAPI/mbsa.csv", _number);
            return tweets[0].ToString();
        }
    }
}