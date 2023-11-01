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
                if (Positive is true) {
                    return "Positive";
                }
                else if (Positive is not true){
                    return "Negative";
                }
                else {
                    return "Other";
                }
                
            }
            set {
                if(value.ToLower().Equals("Positive") || value.ToLower().Equals("True")) {
                    Positive = true;
                }
                else if (value.ToLower().Equals("Negative") ||value.ToLower().Equals("False")) {
                    Positive = false;
                }
                else {
                    Positive = false;
                }
            }
        }


        private DateTime DateTime;
        private string Text;
        private bool Positive;

        public override string ToString() {
            return $"Date: {DateTime.ToString()}, Text: {Text.ToString()}, Sentiment: {Positive.ToString()}";
        }
    }
}
