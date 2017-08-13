/***************************************************************************
*   Copyright (C) 2017 by Denis M. Gabaydulin                             *
*                                                                         *
*   This program is free software; you can redistribute it and/or modify  *
*   it under the terms of the GNU Lesser General Public License as        *
*   published by the Free Software Foundation; either version 3 of the    *
*   License, or (at your option) any later version.                       *
*                                                                         *
***************************************************************************/
using System;
using org.ontslab.misc;
using NUnit.Framework;

namespace org.ontslab.test.misc {
    [TestFixture]
    public class IntervalUtilsTest {
        [Test]
        public void fifteenMinutesKey() {
            Assert.AreEqual("2017_1_10_15_0", IntervalUtils.fifteenMinutesKey(DateTime.Parse("2017-01-10T12:14:59.000Z")));
            Assert.AreEqual("2017_1_10_15_45", IntervalUtils.fifteenMinutesKey(DateTime.Parse("2017-01-10T12:59:44.000Z")));
            Assert.AreEqual("2017_1_10_15_30", IntervalUtils.fifteenMinutesKey(DateTime.Parse("2017-01-10T12:31:44.000Z")));
        }
    }
}