﻿/***************************************************************************
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
	/// Description of Cache.
	/// </summary>
	public interface Cache<K, V> {
		V get(K key);
		void set(K key, V elt);
		void delete(K key);
		void deleteAll();
	}
}
