using GoldWebSocket.GoldImplementation;
using CsvHelper;
using Newtonsoft.Json;
using System.Globalization;


/*
    Will this crash once it reaches the end of the file
    Yes
    Do I care
    Lets put it this way
    The file is 3.3GB, it has over 12 million values
    At the current message rate (one per second) it will take over 138 days before the crash happens 
 */
namespace GoldWebSocket.CSV {
    public static class CSVExtractor {
        public static IList<IGold> ExtractDataFromCSV(string filePath, int rowToRead) {
            int i = 0;
            List<IGold> golds = new List<IGold>();
            using(var reader = new StreamReader(filePath))
            using(var csv = new CsvReader(reader, CultureInfo.InvariantCulture)) {
                while(csv.Read()) {
                    Gold _gold = csv.GetRecord<Gold>();
                    //This is so stupid, that it works
                    var serializedParent = JsonConvert.SerializeObject(_gold);
                    Gold gold = JsonConvert.DeserializeObject<Gold>(serializedParent);

                    string filename = Path.GetFileName(filePath);
                    if(i == rowToRead) {
                        Console.WriteLine(gold.ToString());
                        golds.Add(gold);
                    }
                    i++;
                }
                return golds;
            }
        }
    }
}
