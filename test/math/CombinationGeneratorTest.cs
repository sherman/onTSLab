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
using org.ontslab.math;
	
namespace org.ontslab.test.math {
	[TestFixture]
	public class CombinationGeneratorTest {
		[Test]
		public void testGeneration() {
			IList<IList<string>> generatedStrings = new List<IList<string>>();
			CombinationGenerator.generate(
				new List<string>(){"a", "b"},
				new List<string>(),
				generatedStrings,
				2
			);
			
			Assert.AreEqual(4, generatedStrings.Count);
			
			foreach (var generatedString in generatedStrings) {
				Assert.AreEqual(2, generatedString.Count);
			}
			
			Assert.AreEqual(new List<string>{"a", "a"}, generatedStrings[0]);
			Assert.AreEqual(new List<string>{"a", "b"}, generatedStrings[1]);
			Assert.AreEqual(new List<string>{"b", "a"}, generatedStrings[2]);
			Assert.AreEqual(new List<string>{"b", "b"}, generatedStrings[3]);
		}
	}
}
