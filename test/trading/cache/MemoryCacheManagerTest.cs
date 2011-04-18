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
	public class MemoryCacheManagerTest {
		[Test]
		public void createCacheTest() {
			var cacheManager = MemoryCacheManager<string, double?>.instance;
			var cache = cacheManager.createCache();
			cache.set("test", 0.00001);
			Assert.AreEqual(0.00001, cache.get("test"));
			Assert.AreEqual(null, cache.get("test1"));
			
			var cacheManager1 = MemoryCacheManager<string, double>.instance;
			var cache1 = cacheManager1.createCache();
			cache1.set("anotherTest", 0.00001);
			Assert.AreEqual(0.00001, cache1.get("anotherTest"));
			Assert.AreEqual(default(double), cache1.get("anotherTest1"));
			
			var cacheManager2 = MemoryCacheManager<string, string>.instance;
			var cache2 = cacheManager2.createCache();
			cache2.set("yetAnotherTest", "Zomg!");
			Assert.AreEqual("Zomg!", cache2.get("yetAnotherTest"));
			Assert.AreEqual(null, cache2.get("yetAnotherTest1"));
		}
	}
}
