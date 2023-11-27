using CsvHelper;
using System.Globalization;
using TwitterBitcoinAPI.Controllers;

public class CsvDataExtractor {
    public IList<Tweet> ExtractDataFromCsv(string filePath, int maxRowsToRead) {
        int i = 0;
        List<Tweet> tweets = new List<Tweet>();
        using (var reader = new StreamReader(filePath))
        using(var csv = new CsvReader(reader, CultureInfo.InvariantCulture)) {
            while (csv.Read() && i < maxRowsToRead) {
                tweets.Add(csv.GetRecord<Tweet>());
                i++;
            }
            return tweets;
        }
    }
}
