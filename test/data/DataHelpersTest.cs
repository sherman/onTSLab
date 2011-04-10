/***************************************************************************
*   Copyright (C) 2011 by Denis M. Gabaydulin                             *
*                                                                         *
*   This program is free software; you can redistribute it and/or modify  *
*   it under the terms of the GNU Lesser General Public License as        *
*   published by the Free Software Foundation; either version 3 of the    *
*   License, or (at your option) any later version.                       *
*                                                                         *
***************************************************************************/

using System;
using System.Collections.Generic;
using NUnit.Framework;
using org.ontslab.data;

// TODO: use parameters

namespace org.ontslab.test.data {
	[TestFixture]
	public class DataHelpersTest {
		[Test]
		public void testHighest() {
			IList<double> data = new List<double>{1, 2, 6, 1};
			
			Assert.AreEqual(2, DataHelpers.highest(0, data, 2));
			Assert.AreEqual(6, DataHelpers.highest(0, data, 10));
			Assert.AreEqual(1, DataHelpers.highest(3, data, 10));
			Assert.AreEqual(2, DataHelpers.highest(1, data, 1));
		}
		
		[Test]
		public void testLowest() {
			IList<double> data = new List<double>{1, 2, 6, 1};
			
			Assert.AreEqual(1, DataHelpers.lowest(0, data, 2));
			Assert.AreEqual(1, DataHelpers.lowest(0, data, 10));
			Assert.AreEqual(1, DataHelpers.lowest(3, data, 10));
			Assert.AreEqual(2, DataHelpers.lowest(1, data, 1));
		}
	}
}
