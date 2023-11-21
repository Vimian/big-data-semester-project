using System.Text.Json;

namespace StockMarketWebSocket.CSV {
    public class StockController {
        List<string> filepaths;
        int runs = 0;
        int retrievementIndex = 0;
        public StockController(IList<string> filepaths) {
            this.filepaths = filepaths as List<string>;
        }

        public StockController(bool usePredefined) {
            string[] files = {
                "ACC",
                "ADANIENT",
                "ADANIGREEN",
                "ADANIPORTS",
                "AMBUJACEM",
                "APOLLOHOSP",
                "ASIANPAINT",
                "AUROPHARMA",
                "AXISBANK",
                "BAJAJ-AUTO",
                "BAJAJFINSV",
                "BAJAJHLDNG",
                "BAJFINANCE",
                "BANDHANBNK",
                "BANKBARODA",
                "BERGEPAINT",
                "BHARTIARTL",
                "BIOCON",
                "BOSCHLTD",
                "BPCL",
                "BRITANNIA",
                "CADILAHC",
                "CHOLAFIN",
                "CIPLA",
                "COALINDIA",
                "COLPAL",
                "DABUR",
                "DIVISLAB",
                "DLF",
                "DMART",
                "DRREDDY",
                "EICHERMOT",
                "GAIL",
                "GLAND",
                "GODREJCP",
                "GRASIM",
                "HAVELLS",
                "HCLTECH",
                "HDFC",
                "HDFCAMC",
                "HDFCBANK",
                "HDFCLIFE",
                "HEROMOTOCO",
                "HINDALCO",
                "HINDPETRO",
                "HINDUNILVR",
                "ICICIBANK",
                "ICICIGI",
                "ICICIPRULI",
                "IGL",
                "INDIGO",
                "INDUSINDBK",
                "INDUSTOWER",
                "INFY",
                "IOC",
                "ITC",
                "JINDALSTEL",
                "JSWSTEEL",
                "JUBLFOOD",
                "KOTAKBANK",
                "LT",
                "LTI",
                "LUPIN",
                "M_M",
                "MARICO",
                "MARUTI",
                "MCDOWELL-N",
                "MUTHOOTFIN",
                "NAUKRI",
                "NESTLEIND",
                "NIFTY 50",
                "NIFTY BANK",
                "NMDC",
                "NTPC",
                "ONGC",
                "PEL",
                "PGHH",
                "PIDILITIND",
                "PIIND",
                "PNB",
                "POWERGRID",
                "RELIANCE",
                "SAIL",
                "SBICARD",
                "SBILIFE",
                "SBIN",
                "SHREECEM",
                "SIEMENS",
                "SUNPHARMA",
                "TATACONSUM",
                "TATAMOTORS",
                "TATASTEEL",
                "TCS",
                "TECHM",
                "TITAN",
                "TORNTPHARM",
                "ULTRACEMCO",
                "UPL",
                "VEDL",
                "WIPRO",
                "YESBANK"
            };
            filepaths = new List<string>();
            foreach (string file in files) {
                this.filepaths.Add(Server.path + file + "_with_indicators_.csv");
            }
        }

        public string getStockString(int index) {
            if(runs == filepaths.Count) retrievementIndex++;
            runs++;
            return JsonSerializer.Serialize(CSVExtractor.ExtractDataFromCsv(filepaths[index], retrievementIndex)[0]);
            
        }
    }
}
