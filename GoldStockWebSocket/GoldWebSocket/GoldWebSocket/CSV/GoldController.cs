using System.Text.Json;


namespace GoldWebSocket.CSV {
    public class GoldController {
        private string filepath;
        static int runs = 0;
        int retrievementIndex = 0;
        public GoldController(string filepath) {
            this.filepath = filepath;
        }

        public string getGoldString() {
            runs++;
            return JsonSerializer.Serialize(CSVExtractor.ExtractDataFromCSV(filepath, runs)[0]);
            
        }
    }
}
