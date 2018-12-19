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
using NUnit.Mocks;
using org.ontslab.data;
using TSLab.DataSource;
using TSLab.Script;

namespace org.ontslab.test.data {
	[TestFixture]
	public class HourlySourceTest {
		private HourlySource source;
		
		[SetUp]
		protected void init() {
			source = new HourlySource(
				new List<IDataBar>() {
					{new DataBar(new DateTime(2011, 4, 13, 10, 0, 0), 100, 105, 99, 99, 1)},
					{new DataBar(new DateTime(2011, 4, 13, 11, 0, 0), 102, 106, 101, 107, 1)},
					{new DataBar(new DateTime(2011, 4, 13, 12, 0, 0), 108, 108, 106, 107, 1)},
					{new DataBar(new DateTime(2011, 4, 13, 13, 0, 0), 99, 101, 84, 100, 21)},
					{new DataBar(new DateTime(2011, 4, 13, 14, 0, 0), 101, 102, 98, 101.5, 1)},
					{new DataBar(new DateTime(2011, 4, 16, 10, 0, 0), 102, 103, 100, 101.5, 21)}
				}
			);
		}
		
		[Test]
		public void testGetBar() {
			// Bar is not ValueObject :-/
			Assert.AreEqual(
				new DataBar(new DateTime(2011, 4, 13, 12, 0, 0), 108, 108, 106, 107, 1).ToString(),
				source.getBar(new DateTime(2011, 4, 13, 12, 0, 0)).ToString()
			);
		}
		
		[Test]
		public void testGetPreviousBar() {
			Assert.AreEqual(
				new DataBar(new DateTime(2011, 4, 13, 10, 0, 0), 100, 105, 99, 99, 1).ToString(),
				source.getPreviousBar(new DateTime(2011, 4, 13, 11, 0, 0)).ToString()
			);
			
			Assert.AreEqual(
				new DataBar(new DateTime(2011, 4, 13, 14, 0, 0), 101, 102, 98, 101.5, 1).ToString(),
				source.getPreviousBar(new DateTime(2011, 4, 16, 10, 0, 0)).ToString()
			);
			
			Assert.AreEqual(
				null,
				source.getPreviousBar(new DateTime(2011, 4, 13, 10, 0, 0))
			);
		}
		
		[Test]
		public void testGetNextBar() {
			Assert.AreEqual(
				new DataBar(new DateTime(2011, 4, 13, 12, 0, 0), 108, 108, 106, 107, 1).ToString(),
				source.getNextBar(new DateTime(2011, 4, 13, 11, 0, 0)).ToString()
			);
			
			Assert.AreEqual(
				new DataBar(new DateTime(2011, 4, 16, 10, 0, 0), 102, 103, 100, 101.5, 21).ToString(),
				source.getNextBar(new DateTime(2011, 4, 13, 14, 0, 0)).ToString()
			);
			
			Assert.AreEqual(
				null,
				source.getNextBar(new DateTime(2011, 4, 16, 10, 0, 0))
			);
		}
	}
}
