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

namespace org.ontslab.trading.cache
{
	/// <summary>
	/// Description of MemoryCache.
	/// </summary>
	public class MemoryCache<K, V> : Cache<K, V> {
		private IDictionary<K, V> storage = new Dictionary<K, V>();
		
		public V @get(K key) {
			if (storage.ContainsKey(key))
				return storage[key];
			else {
				return default(V);
			}
		}
		
		public void @set(K key, V elt) {
			storage[key] = elt;
		}
		
		public void delete(K key) {
			storage.Remove(key);
		}
		
		public void deleteAll() {
			storage.Clear();
		}
	}
}
