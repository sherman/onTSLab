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
using NUnit.Framework;
using org.ontslab.trading.cache;
	
namespace org.ontslab.test.trading.cache{
	[TestFixture]
	public class MemoryCacheManagerTest
	{
		[Test]
		public void createCacheTest() {
			var cacheManager = MemoryCacheManager<string, double>.instance;
			var cache = cacheManager.createCache();
			cache.set("test", 0.00001);
			Assert.AreEqual(0.00001, cache.get("test"));
			Assert.AreEqual(default(Double), cache.get("test1"));
		}
	}
}
