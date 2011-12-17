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
using System.IO;
using System.Collections.Generic;
using org.ontslab.misc;
using org.ontslab.bpi;
using NUnit.Framework;

namespace org.ontslab.test.misc
{
	/// <summary>
	/// Description of CvsReaderTest.
	/// </summary>
	[TestFixture]
	public class CsvReaderTest {
		[Test]
		public void read() {
			CsvReader<Trade> csvReader = new CsvReader<Trade>(
				Path.GetFullPath(
					"..\\..\\test\\resources\\azhuravlev.txt"
				),
				","
			);
			
			IList<Trade> trades = csvReader.read(
				parts => {
					Trade trade = new Trade(
						1,
						DateTime.Parse(parts[3]),
						double.Parse(parts[5].Split('.')[0]),
						int.Parse(parts[2])
					);
					return trade;
				}
			);
			
			Assert.AreEqual(3447, trades.Count);
			Assert.AreEqual("{07.10.2011 19:24:56,127625,1}", trades[0].ToString());
		}
	}
}
