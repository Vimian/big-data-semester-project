using System.Text.Json;
using CsvHelper.Configuration.Attributes;

namespace StockMarketWebSocket.CSV {
    public class Stock : IStock {
        private DateTime dt;
        private float Open;
        private float High;
        private float Low;
        private float Close;

        public Stock() { }


        [Name("date")]
        public DateTime date {
            get {
                return dt;
            }
            set {
                this.dt = value;
            }
        }

        [Name("open")]
        public float open {
            get {
                return Open;
            }
            set {
                Open = value;
            }
        }

        [Name("high")]
        public float high {
            get {
                return High;
            }
            set {
                High = value;
            }
        }

        [Name("low")]
        public float low {
            get {
                return Low;
            }
            set {
                Low = value;
            }
        }

        [Name("close")]
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