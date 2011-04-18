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

namespace org.ontslab.trading.cache
{
	/// <summary>
	/// Description of MemoryCacheManager.
	/// </summary>
	public class MemoryCacheManager<K, V> : CacheManager<MemoryCache<K, V>, K, V> {
		private MemoryCacheManager() {}
		
		public static MemoryCacheManager<K, V> instance {
			get {
				return NestedInstance.instance;
			}
		}
		
		public MemoryCache<K, V> createCache() {
			return new MemoryCache<K, V>();
		}
		
		public void dropCache(MemoryCache<K, V> cache) {
			throw new NotImplementedException();
		}
		
		class NestedInstance {
			static NestedInstance() {}
			
			internal static readonly MemoryCacheManager<K, V> instance =
				new MemoryCacheManager<K, V>();
			
		}
	}
}
