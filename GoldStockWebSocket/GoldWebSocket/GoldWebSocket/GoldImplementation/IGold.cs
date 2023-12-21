using CsvHelper.Configuration.Attributes;
using System.Runtime.InteropServices;

namespace GoldWebSocket.GoldImplementation {
    public interface IGold {
        [Name("date")]
        public DateTime date {
            get;
            set;
        }

        [Name("xauusd_open")]
        public float open {
            get;
            set;
        }

        [Name("xauusd_high")]
        public float high {
            get;
            set;
        }

        [Name("xauusd_low")]
        public float low {
            get;
            set;
        }

        [Name("xauusd_close")]
        public float close {
            get;
            set;
        }
    }
}
