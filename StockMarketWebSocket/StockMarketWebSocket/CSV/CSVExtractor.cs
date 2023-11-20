using CsvHelper;
using System.Globalization;

namespace StockMarketWebSocket.CSV {
    public static class CSVExtractor {
        public static IList<Stock> ExtractDataFromCsv(string filePath, int maxRowsToRead) {
            int i = 0;
            List<Stock> stocks = new List<Stock>();
            using(var reader = new StreamReader(filePath))
            using(var csv = new CsvReader(reader, CultureInfo.InvariantCulture)) {
                while(csv.Read() && i < maxRowsToRead) {
                    Stock stock = csv.GetRecord<Stock>();
                    string filename = Path.GetFileName(filePath);
                    stock.name = filename.Remove(filename.Length - 5, 4);//check if the name comes out correct
                    Console.WriteLine(stock.ToString());
                    stocks.Add(stock);
                    i++;
                }
                return stocks;
            }
        }
    }
}
