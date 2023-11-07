using System.Text.Json;

namespace StockMarketWebSocket.CSV {
    public class StockController {
        List<string> filepaths;
        public StockController(IList<string> filepaths) {
            this.filepaths = filepaths as List<string>;
        }

        public string getStockString(int index) {
            return JsonSerializer.Serialize(CSVExtractor.ExtractDataFromCsv(filepaths[index], 1)[0]);
        }
    }
}
