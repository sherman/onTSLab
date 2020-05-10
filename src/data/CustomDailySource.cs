using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using org.ontslab.misc;
using TSLab.DataSource;
using TSLab.Script;

namespace org.ontslab.data {
    public class CustomDailySource {
        private readonly IReadOnlyList<IDataBar> _original;
        private readonly int _skipBars;
        private readonly IDictionary<string, IDataBar> _newSourceBars = new Dictionary<string, IDataBar>();
        private IDataBar first;
        private IDataBar last;
        private readonly Day period = new Day();

        public CustomDailySource(ISecurity original, int skipBars) : this(original.Bars, skipBars) {
        }

        public CustomDailySource(IReadOnlyList<IDataBar> original, int skipBars) {
            _original = original;
            _skipBars = skipBars;
            Create();
        }

        private IDataBar CreateDayBar(IList<IDataBar> bars) {
            Assert.True(bars.Count > _skipBars, "At least " + _skipBars + " bars are required!");

            var min = double.MaxValue;
            var max = double.MinValue;
            var volume = 0d;

            for (var barIndex = _skipBars; barIndex < bars.Count; barIndex++) {
                var currentBar = bars[barIndex];
                min = Math.Min(currentBar.Low, min);
                max = Math.Max(currentBar.High, max);
                volume += currentBar.Volume;
            }

            var firstIndex = Math.Max(_skipBars, 0);

            var barDate = bars[firstIndex].Date;
            var open = bars[firstIndex].Open;
            var close = bars[bars.Count - 1].Close;

            return new DataBar(barDate.Date, open, max, min, close, volume);
        }

        private void Create() {
            var dt = "";
            var bars = new List<IDataBar>();
            for (var barIndex = 0; barIndex < _original.Count; barIndex++) {
                var currentBar = _original[barIndex];

                if (period.keyFromTime(currentBar.Date) != dt) {
                    if (bars.Count > _skipBars) {
                        var dayBar = CreateDayBar(bars);
                        _newSourceBars.Add(period.keyFromTime(dayBar.Date), dayBar);
                        if (first == null) {
                            first = dayBar;
                        }

                        last = dayBar;
                    }

                    dt = period.keyFromTime(currentBar.Date);
                    bars.Clear();
                    bars.Add(currentBar);
                }
                else {
                    bars.Add(currentBar);
                }
            }

            if (bars.Count > _skipBars) {
                var dayBar = CreateDayBar(bars);
                _newSourceBars.Add(period.keyFromTime(dayBar.Date), dayBar);
                if (first == null) {
                    first = dayBar;
                }

                last = dayBar;
            }
        }

        public IList<IDataBar> GetBars() {
            return _newSourceBars.Values.ToList();
        }

        public IDataBar GetBar(DateTime dateTime) {
            return _newSourceBars[period.keyFromTime(dateTime.Date)];
        }

        public IDataBar GetPrevBar(DateTime dateTime) {
            string previousBarKey;

            var startBarDate = dateTime;

            do {
                startBarDate = startBarDate.Subtract(period.getTimeSpan());
                previousBarKey = period.keyFromTime(startBarDate);

                if (_newSourceBars.ContainsKey(previousBarKey)) {
                    return _newSourceBars[previousBarKey];
                }
            } while (startBarDate >= first.Date);

            return null;
        }

        public IDataBar GetNextBar(DateTime dateTime) {
            string nextBarKey;

            DateTime startBarDate = dateTime;

            do {
                startBarDate = startBarDate.Add(period.getTimeSpan());
                nextBarKey = period.keyFromTime(startBarDate);

                if (_newSourceBars.ContainsKey(nextBarKey)) {
                    return _newSourceBars[nextBarKey];
                }
            } while (startBarDate <= last.Date);


            return null;
        }

        public IDataBar GetFirstBar() {
            return first;
        }
    }
}