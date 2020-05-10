using System;
using System.Collections.Generic;
using NUnit.Framework;
using org.ontslab.data;
using TSLab.DataSource;

namespace org.ontslab.test.data {
    [TestFixture]
    public class CustomDailySourceTest {
        [Test]
        public void NotEnoughBars() {
            var source = new CustomDailySource(
                new List<IDataBar> {
                    {new DataBar(new DateTime(2020, 1, 1, 10, 0, 0), 100, 105, 99, 99, 1)}
                },
                1
            );
            
            Assert.AreEqual(0, source.GetBars().Count);
        }
        
        [Test]
        public void SimpleGetBar() {
            var source = new CustomDailySource(
                new List<IDataBar> {
                    {new DataBar(new DateTime(2020, 1, 1, 10, 0, 0), 100, 105, 99, 99, 1)},
                    {new DataBar(new DateTime(2020, 1, 1, 10, 1, 0), 102, 106, 101, 107, 1)},
                    {new DataBar(new DateTime(2020, 1, 1, 10, 2, 0), 108, 108, 106, 107, 1)},
                    {new DataBar(new DateTime(2020, 1, 1, 10, 3, 0), 99, 101, 84, 100, 21)},
                    {new DataBar(new DateTime(2020, 1, 1, 10, 4, 0), 101, 102, 98, 101.5, 1)}
                },
                1
            );

            var expected = new DataBar(new DateTime(2020, 1, 1), 102, 108, 84, 101.5, 24);

            Assert.AreEqual(
                expected.ToString(),
                source.GetBar(new DateTime(2020, 1, 1)).ToString()
            );

            Assert.AreEqual(24, expected.Volume);
            
            Assert.AreEqual(1, source.GetBars().Count);
        }
        
        [Test]
        public void TwoDaysOneSkippedGetBar() {
            var source = new CustomDailySource(
                new List<IDataBar> {
                    {new DataBar(new DateTime(2020, 1, 1, 10, 0, 0), 100, 105, 99, 99, 1)},
                    {new DataBar(new DateTime(2020, 1, 1, 10, 1, 0), 102, 106, 101, 107, 1)},
                    {new DataBar(new DateTime(2020, 1, 1, 10, 2, 0), 108, 108, 106, 107, 1)},
                    {new DataBar(new DateTime(2020, 1, 1, 10, 3, 0), 99, 101, 84, 100, 21)},
                    {new DataBar(new DateTime(2020, 1, 1, 10, 4, 0), 101, 102, 98, 101.5, 1)},
                    {new DataBar(new DateTime(2020, 1, 2, 10, 0, 0), 101, 102, 98, 101.5, 1)}
                },
                1
            );

            var expected = new DataBar(new DateTime(2020, 1, 1), 102, 108, 84, 101.5, 24);

            Assert.AreEqual(
                expected.ToString(),
                source.GetBar(new DateTime(2020, 1, 1)).ToString()
            );

            Assert.AreEqual(24, expected.Volume);
            
            Assert.AreEqual(1, source.GetBars().Count);
        }
        
        [Test]
        public void TwoDaysGetBar() {
            var source = new CustomDailySource(
                new List<IDataBar> {
                    {new DataBar(new DateTime(2020, 1, 1, 10, 0, 0), 100, 105, 99, 99, 1)},
                    {new DataBar(new DateTime(2020, 1, 1, 10, 1, 0), 102, 106, 101, 107, 1)},
                    {new DataBar(new DateTime(2020, 1, 1, 10, 2, 0), 108, 108, 106, 107, 1)},
                    {new DataBar(new DateTime(2020, 1, 1, 10, 3, 0), 99, 101, 84, 100, 21)},
                    {new DataBar(new DateTime(2020, 1, 1, 10, 4, 0), 101, 102, 98, 101.5, 1)},
                    {new DataBar(new DateTime(2020, 1, 2, 10, 0, 0), 101, 102, 98, 101.5, 1)},
                    {new DataBar(new DateTime(2020, 1, 2, 10, 1, 0), 101, 102, 98, 101.5, 1)}
                },
                1
            );

            var expected = new DataBar(new DateTime(2020, 1, 1), 102, 108, 84, 101.5, 24);

            Assert.AreEqual(
                expected.ToString(),
                source.GetBar(new DateTime(2020, 1, 1)).ToString()
            );

            Assert.AreEqual(24, expected.Volume);
            
            Assert.AreEqual(2, source.GetBars().Count);
        }
        
        [Test]
        public void GetNextBar() {
            var source = new CustomDailySource(
                new List<IDataBar> {
                    {new DataBar(new DateTime(2020, 1, 1, 10, 0, 0), 100, 105, 99, 99, 1)},
                    {new DataBar(new DateTime(2020, 1, 1, 10, 1, 0), 102, 106, 101, 107, 1)},
                    {new DataBar(new DateTime(2020, 1, 1, 10, 2, 0), 108, 108, 106, 107, 1)},
                    {new DataBar(new DateTime(2020, 1, 1, 10, 3, 0), 99, 101, 84, 100, 21)},
                    {new DataBar(new DateTime(2020, 1, 1, 10, 4, 0), 101, 102, 98, 101.5, 1)},
                    {new DataBar(new DateTime(2020, 1, 2, 10, 0, 0), 101, 102, 98, 101.5, 1)},
                    {new DataBar(new DateTime(2020, 1, 2, 10, 1, 0), 101, 103, 98, 101.5, 1)}
                },
                1
            );

            var expected = new DataBar(new DateTime(2020, 1, 2), 101, 103, 98, 101.5, 1);

            Assert.AreEqual(
                expected.ToString(),
                source.GetNextBar(new DateTime(2020, 1, 1)).ToString()
            );
        }
        
        [Test]
        public void GetPrevBar() {
            var source = new CustomDailySource(
                new List<IDataBar> {
                    {new DataBar(new DateTime(2020, 1, 1, 10, 0, 0), 100, 105, 99, 99, 1)},
                    {new DataBar(new DateTime(2020, 1, 1, 10, 1, 0), 102, 106, 101, 107, 1)},
                    {new DataBar(new DateTime(2020, 1, 1, 10, 2, 0), 108, 108, 106, 107, 1)},
                    {new DataBar(new DateTime(2020, 1, 1, 10, 3, 0), 99, 101, 84, 100, 21)},
                    {new DataBar(new DateTime(2020, 1, 1, 10, 4, 0), 101, 102, 98, 101.5, 1)},
                    {new DataBar(new DateTime(2020, 1, 2, 10, 0, 0), 101, 102, 98, 101.5, 1)},
                    {new DataBar(new DateTime(2020, 1, 2, 10, 1, 0), 101, 103, 98, 101.5, 1)}
                },
                1
            );

            var expected = new DataBar(new DateTime(2020, 1, 1), 102, 108, 84, 101.5, 24);

            Assert.AreEqual(
                expected.ToString(),
                source.GetPrevBar(new DateTime(2020, 1, 2)).ToString()
            );
        }
        
        [Test]
        public void GetPrevBarWithGap() {
            var source = new CustomDailySource(
                new List<IDataBar> {
                    {new DataBar(new DateTime(2020, 1, 1, 10, 0, 0), 100, 105, 99, 99, 1)},
                    {new DataBar(new DateTime(2020, 1, 1, 10, 1, 0), 102, 106, 101, 107, 1)},
                    {new DataBar(new DateTime(2020, 1, 1, 10, 2, 0), 108, 108, 106, 107, 1)},
                    {new DataBar(new DateTime(2020, 1, 1, 10, 3, 0), 99, 101, 84, 100, 21)},
                    {new DataBar(new DateTime(2020, 1, 1, 10, 4, 0), 101, 102, 98, 101.5, 1)},
                    {new DataBar(new DateTime(2020, 1, 5, 10, 0, 0), 101, 102, 98, 101.5, 1)},
                    {new DataBar(new DateTime(2020, 1, 5, 10, 1, 0), 101, 103, 98, 101.5, 1)}
                },
                1
            );

            var expected = new DataBar(new DateTime(2020, 1, 1), 102, 108, 84, 101.5, 24);

            Assert.AreEqual(
                expected.ToString(),
                source.GetPrevBar(new DateTime(2020, 1, 5)).ToString()
            );
        }
        
        [Test]
        public void GetNextBarWithGap() {
            var source = new CustomDailySource(
                new List<IDataBar> {
                    {new DataBar(new DateTime(2020, 1, 1, 10, 0, 0), 100, 105, 99, 99, 1)},
                    {new DataBar(new DateTime(2020, 1, 1, 10, 1, 0), 102, 106, 101, 107, 1)},
                    {new DataBar(new DateTime(2020, 1, 1, 10, 2, 0), 108, 108, 106, 107, 1)},
                    {new DataBar(new DateTime(2020, 1, 1, 10, 3, 0), 99, 101, 84, 100, 21)},
                    {new DataBar(new DateTime(2020, 1, 1, 10, 4, 0), 101, 102, 98, 101.5, 1)},
                    {new DataBar(new DateTime(2020, 1, 5, 10, 0, 0), 101, 102, 98, 101.5, 1)},
                    {new DataBar(new DateTime(2020, 1, 5, 10, 1, 0), 101, 103, 98, 101.5, 1)}
                },
                1
            );

            var expected = new DataBar(new DateTime(2020, 1, 5), 101, 103, 98, 101.5, 1);

            Assert.AreEqual(
                expected.ToString(),
                source.GetNextBar(new DateTime(2020, 1, 1)).ToString()
            );
        }

        [Test]
        public void CheckInfiniteLoops() {
            var source = new CustomDailySource(
                new List<IDataBar> {
                    {new DataBar(new DateTime(2020, 1, 1, 10, 0, 0), 100, 105, 99, 99, 1)},
                    {new DataBar(new DateTime(2020, 1, 1, 10, 1, 0), 102, 106, 101, 107, 1)},
                    {new DataBar(new DateTime(2020, 1, 1, 10, 2, 0), 108, 108, 106, 107, 1)},
                    {new DataBar(new DateTime(2020, 1, 1, 10, 3, 0), 99, 101, 84, 100, 21)},
                    {new DataBar(new DateTime(2020, 1, 1, 10, 4, 0), 101, 102, 98, 101.5, 1)}
                },
                1
            );

            Assert.IsNull(source.GetNextBar(new DateTime(2020, 1, 1)));
            Assert.IsNull(source.GetPrevBar(new DateTime(2020, 1, 1)));
        }
    }
}