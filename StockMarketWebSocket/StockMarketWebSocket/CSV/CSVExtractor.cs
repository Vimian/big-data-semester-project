using CsvHelper;
using Newtonsoft.Json;
using System.Globalization;

namespace StockMarketWebSocket.CSV {
    public static class CSVExtractor {
        public static IList<JsonStock> ExtractDataFromCsv(string filePath, int rowToRead) {
            int i = 0;
            List<JsonStock> stocks = new List<JsonStock>();
            using(var reader = new StreamReader(filePath))
            using(var csv = new CsvReader(reader, CultureInfo.InvariantCulture)) {
                while(csv.Read() && i != rowToRead) {
                    if (i == rowToRead) {
                        Stock _stock = csv.GetRecord<Stock>();
                        //This is so stupid, that it works
                        var serializedParent = JsonConvert.SerializeObject(_stock);
                        JsonStock stock = JsonConvert.DeserializeObject<JsonStock>(serializedParent);

                        if(stock is null) Console.WriteLine("2");
                        string filename = Path.GetFileName(filePath);
                        stock.Name = filename.Remove(filename.Length - 21, Server.path.Length);//check if the name comes out correct
                        stock.Name = stock.Name.Replace("csv", "");
                        Console.WriteLine(stock.ToString());
                        stocks.Add(stock);
                    }
                    i++;
                }
                return stocks;
            }
        }
    }
}
