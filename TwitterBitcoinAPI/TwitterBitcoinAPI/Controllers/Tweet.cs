using CsvHelper.Configuration.Attributes;

namespace TwitterBitcoinAPI.Controllers {
    public class Tweet {
        [Name("Date")]
        public DateTime Date {
            get {
                return DateTime;
            }
            set {
                this.DateTime = value;
            }
        }

        [Name("text")]
        public string TweetText {
            get {
                return Text;
            }
            set {
                Text = value;
            }
        }

        [Name("Sentiment")]
        public string Sentiment {
            get {
                if (Positive == true) {
                    return "Positive";
                }
                else if (Positive != true){
                    return "Negative";
                }
                else if (Positive is null) {
                    return "Other";
                }
                return "other";
            }
            set {
                if(value.ToLower().Equals("positive") || value.ToLower().Equals("true")) {
                    Positive = true;
                }
                else if (value.ToLower().Equals("negative") ||value.ToLower().Equals("false")) {
                    Positive = false;
                }
                else {
                    Positive = null;
                }
            }
        }


        private DateTime DateTime;
        private string Text;
        private bool? Positive;

        public override string ToString() {
            return $"Date: {DateTime.ToString()}, Text: {Text.ToString()}, Sentiment: {Positive.ToString()}";
        }
    }
}
