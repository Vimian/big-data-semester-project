using System.Text.Json;
using System.Xml.Linq;
using CsvHelper.Configuration.Attributes;

namespace GoldWebSocket.GoldImplementation {
    public class Gold : IGold {
        private DateTime dt;
        private float Open;
        private float High;
        private float Low;
        private float Close;
        private string name = "";


        public Gold() {
        }


        [Name("date")]
        public DateTime date {
            get {
                return dt;
            }
            set {
                this.dt = value;
            }
        }

        [Name("xauusd_open")]
        public float open {
            get {
                return Open;
            }
            set {
                Open = value;
            }
        }

        [Name("xauusd_high")]
        public float high {
            get {
                return High;
            }
            set {
                High = value;
            }
        }

        [Name("xauusd_low")]
        public float low {
            get {
                return Low;
            }
            set {
                Low = value;
            }
        }

        [Name("xauusd_close")]
        public float close {
            get {
                return Close;
            }
            set {
                Close = value;
            }
        }

        public override string ToString() {
            return $"date: {dt.ToString()}, open: {open.ToString()}, high: {high.ToString()}, low {low.ToString()}, close: {close.ToString()}";
        }
    }
}
