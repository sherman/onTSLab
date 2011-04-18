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
	/// Description of CacheManager.
	/// </summary>
	public interface CacheManager<T, K, V> where T : Cache<K, V> {
		T createCache();
		void dropCache(T cache);
	}}
