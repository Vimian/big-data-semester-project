using CsvHelper.Configuration.Attributes;
using System.Runtime.InteropServices;

namespace StockMarketWebSocket.CSV {
    public interface IStock {
        [Name("date")]
        public DateTime date {
            get;
            set;
        }

        [Name("open")]
        public float open {
            get;
            set;
        }

        [Name("high")]
        public float high {
            get;
            set;
        }

        [Name("low")]
        public float low {
            get;
            set;
        }

        [Name("close")]
        public float close {
            get;
            set;
        }
    }
}
