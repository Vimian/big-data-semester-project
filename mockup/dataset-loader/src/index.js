const { Kafka } = require("kafkajs");
const fs = require("fs");
const { parse } = require("csv-parse");

const kafka = new Kafka({
    clientId: "mockup-dataset-loader",
    brokers: ["kafka-cluster-kafka-bootstrap.kafka:9092"],
});

const producer = kafka.producer({
    allowAutoTopicCreation: true,
});

const run = async () => {
    console.log("Connecting to Kafka1");

    await producer.connect();

    console.log("Connected to Kafka1");

    // load tweets
    const RSBT = fs
        .createReadStream("./datasets/BitcoinTweets/mbsa.csv")
        .pipe(parse({ delimiter: ",", from_line: 2 }));
    RSBT.on("data", async function (row) {
        RSBT.pause();
        setTimeout(function () {
            RSBT.resume();
        }, 10e3);

        await producer.send({
            topic: "tweet-json",
            messages: [
                {
                    value: JSON.stringify({
                        Date: row[0],
                        text: row[1],
                        Sentiment: row[2],
                    }),
                },
            ],
        });
    });

    // load gold
    const RSG = fs
        .createReadStream("./datasets/Gold/export/train/m5.csv")
        .pipe(parse({ delimiter: ",", from_line: 2 }));
    RSG.on("data", async function (row) {
        RSG.pause();
        setTimeout(function () {
            RSG.resume();
        }, 10e3);

        await producer.send({
            topic: "gold-json",
            messages: [
                {
                    value: JSON.stringify({
                        index: row[1],
                        timestamp: row[2],

                        xauusd_open: row[3],
                        xauusd_high: row[4],
                        xauusd_low: row[5],
                        xauusd_close: row[6],
                        xauusd_volume: row[7],
                        xauusd_tail: row[8],
                        xauusd_head: row[9],
                        xauusd_body: row[10],
                        xauusd_abs_body: row[11],
                        xauusd_movement: row[12],

                        audasd_open: row[13],
                        audusd_high: row[14],
                        audusd_low: row[15],
                        audusd_close: row[16],
                        audusd_volume: row[17],
                        audusd_tail: row[18],
                        audusd_head: row[19],
                        audusd_body: row[20],
                        audusd_abs_body: row[21],
                        audusd_movement: row[22],

                        nzdusd_open: row[23],
                        nzdusd_high: row[24],
                        nzdusd_low: row[25],
                        nzdusd_close: row[26],
                        nzdusd_volume: row[27],
                        nzdusd_tail: row[28],
                        nzdusd_head: row[29],
                        nzdusd_body: row[30],
                        nzdusd_abs_body: row[31],
                        nzdusd_movement: row[32],

                        usdchf_open: row[33],
                        usdchf_high: row[34],
                        usdchf_low: row[35],
                        usdchf_close: row[36],
                        usdchf_volume: row[37],
                        usdchf_tail: row[38],
                        usdchf_head: row[39],
                        usdchf_body: row[40],
                        usdchf_abs_body: row[41],
                        usdchf_movement: row[42],

                        usdcad_open: row[43],
                        usdcad_high: row[44],
                        usdcad_low: row[45],
                        usdcad_close: row[46],
                        usdcad_volume: row[47],
                        usdcad_tail: row[48],
                        usdcad_head: row[49],
                        usdcad_body: row[50],
                        usdcad_abs_body: row[51],
                        usdcad_movement: row[52],

                        eurusd_open: row[53],
                        eurusd_high: row[54],
                        eurusd_low: row[55],
                        eurusd_close: row[56],
                        eurusd_volume: row[57],

                        date: row[58],

                        eurusd_tail: row[59],
                        eurusd_head: row[60],
                        eurusd_body: row[61],
                        eurusd_abs_body: row[62],
                        eurusd_movement: row[63],

                        xauusd_open_smooth: row[64],
                        xauusd_high_smooth: row[65],
                        xauusd_low_smooth: row[66],
                        xauusd_close_smooth: row[67],

                        audusd_open_smooth: row[68],
                        audusd_high_smooth: row[69],
                        audusd_low_smooth: row[70],
                        audusd_close_smooth: row[71],

                        nzdusd_open_smooth: row[72],
                        nzdusd_high_smooth: row[73],
                        nzdusd_low_smooth: row[74],
                        nzdusd_close_smooth: row[75],

                        usdchf_open_smooth: row[76],
                        usdchf_high_smooth: row[77],
                        usdchf_low_smooth: row[78],
                        usdchf_close_smooth: row[79],

                        usdcad_open_smooth: row[80],
                        usdcad_high_smooth: row[81],
                        usdcad_low_smooth: row[82],
                        usdcad_close_smooth: row[83],

                        eurusd_open_smooth: row[84],
                        eurusd_high_smooth: row[85],
                        eurusd_low_smooth: row[86],
                        eurusd_close_smooth: row[87],

                        xauusd_tail_smooth: row[88],
                        xauusd_head_smooth: row[89],
                        xauusd_body_smooth: row[90],
                        xauusd_abs_body_smooth: row[91],
                        xauusd_movement_smooth: row[92],

                        audusd_tail_smooth: row[93],
                        audusd_head_smooth: row[94],
                        audusd_body_smooth: row[95],
                        audusd_abs_body_smooth: row[96],
                        audusd_movement_smooth: row[97],

                        nzdusd_tail_smooth: row[98],
                        nzdusd_head_smooth: row[99],
                        nzdusd_body_smooth: row[100],
                        nzdusd_abs_body_smooth: row[101],
                        nzdusd_movement_smooth: row[102],

                        usdchf_tail_smooth: row[103],
                        usdchf_head_smooth: row[104],
                        usdchf_body_smooth: row[105],
                        usdchf_abs_body_smooth: row[106],
                        usdchf_movement_smooth: row[107],

                        usdcad_tail_smooth: row[108],
                        usdcad_head_smooth: row[109],
                        usdcad_body_smooth: row[110],
                        usdcad_abs_body_smooth: row[111],
                        usdcad_movement_smooth: row[112],

                        eurusd_tail_smooth: row[113],
                        eurusd_head_smooth: row[114],
                        eurusd_body_smooth: row[115],
                        eurusd_abs_body_smooth: row[116],
                        eurusd_movement_smooth: row[117],

                        sydney_catg: row[118],
                        tokyo_catg: row[119],
                        london_catg: row[120],
                        new_york_catg: row[121],
                        hammer_catg: row[122],
                        engulfing_catg: row[123],
                        shooting_star_catg: row[124],
                        doji_catg: row[125],
                        doji_star_catg: row[126],
                        inside_bar_catg: row[127],
                        morning_star_catg: row[128],
                        evening_star_catg: row[129],
                        upperband: row[130],
                        middleband: row[131],
                        lowerband: row[132],
                        sma_5: row[133],
                        sma_30: row[134],
                        sma_62: row[135],
                        atr_7: row[136],
                        atr_14: row[137],
                        atr_20: row[138],
                        std_1_5: row[139],
                        std_2_5: row[140],
                        std_3_5: row[141],
                        std_1_14: row[142],
                        std_2_14: row[143],
                        std_3_14: row[144],
                        std_1_20: row[145],
                        std_2_20: row[146],
                        std_3_20: row[147],
                        rsi_14: row[148],
                        rsi_28: row[149],
                        rsi_62: row[150],
                        slowk_3: row[151],
                        slowd_3: row[152],
                        slowk_7: row[153],
                        slowd_7: row[154],
                        slowk_14: row[155],
                        slowd_14: row[156],
                        adx_7: row[157],
                        adx_14: row[158],
                        adx_28: row[159],
                        macd: row[160],
                        macdsignal: row[161],
                        macdhist: row[162],
                        cmf: row[163],
                        obv: row[164],
                        group: row[165],
                        return: row[166],
                        return_smooth: row[167],
                        labels: row[168],
                        labels_smooth: row[169],
                        xauusd_close_pre: row[170],
                        xauusd_close_smooth_pre: row[171],
                    }),
                },
            ],
        });
    });

    // load stocks
    fs.readdir("./datasets/Stock/", function (err, files) {
        files.forEach(function (file) {
            const RSS = fs
                .createReadStream("./datasets/Stock/" + file)
                .pipe(parse({ delimiter: ",", from_line: 2 }));
            RSS.on("data", async function (row) {
                RSS.pause();
                setTimeout(function () {
                    RSS.resume();
                }, 10e3);

                await producer.send({
                    topic: "stock-json",
                    messages: [
                        {
                            value: JSON.stringify({
                                date: row[0],
                                open: row[1],
                                high: row[2],
                                low: row[3],
                                close: row[4],
                                volume: row[5],
                                sma5: row[6],
                                sma10: row[7],
                                sma15: row[8],
                                sma20: row[9],
                                ema5: row[10],
                                ema10: row[11],
                                ema15: row[12],
                                ema20: row[13],
                                upperband: row[14],
                                middleband: row[15],
                                lowerband: row[16],
                                HT_TRENDLINE: row[17],
                                KAMA10: row[18],
                                KAMA20: row[19],
                                KAMA30: row[20],
                                SAR: row[21],
                                TRIMA5: row[22],
                                TRIMA10: row[23],
                                TRIMA15: row[24],
                                ADX5: row[25],
                                ADX10: row[26],
                                ADX15: row[27],
                                APO: row[28],
                                CCI5: row[29],
                                CCI10: row[30],
                                CCI15: row[31],
                                macd510: row[32],
                                macd520: row[33],
                                macd1020: row[34],
                                macd1520: row[35],
                                macd1226: row[36],
                                MOM10: row[37],
                                MOM15: row[38],
                                MOM20: row[39],
                                ROC5: row[40],
                                ROC10: row[41],
                                ROC20: row[42],
                                PPO: row[43],
                                RSI14: row[44],
                                RSI8: row[45],
                                slowk: row[46],
                                slowd: row[47],
                                fastk: row[48],
                                fastd: row[49],
                                fastksr: row[50],
                                fastdsr: row[51],
                                ULTOSC: row[52],
                                WILLR: row[53],
                                ATR: row[54],
                                Trange: row[55],
                                TYPPRICE: row[56],
                                HT_DCPERIOD: row[57],
                                BETA: row[58],
                                name: file.split("_")[0],
                            }),
                        },
                    ],
                });
            });
        });
    });
};

run().catch(console.error);
