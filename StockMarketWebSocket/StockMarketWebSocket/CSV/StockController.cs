using System.Text.Json;

namespace StockMarketWebSocket.CSV {
    public class StockController {
        List<string> filepaths;
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
                "BAJAFINSV",
                "BAJAHLDNG",
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
                "CPILA",
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
                "MCDOWELL",
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
            return JsonSerializer.Serialize(CSVExtractor.ExtractDataFromCsv(filepaths[index], 1)[0]);
        }
    }
}
