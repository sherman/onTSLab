/*
 * Created by SharpDevelop.
 * User: Sherminator
 * Date: 10.04.2011
 * Time: 21:24
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

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
