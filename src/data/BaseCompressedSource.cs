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
using TSLab.Script;
using org.ontslab.misc;

namespace org.ontslab.data {
	/// <summary>
	/// Description of BaseCompressedSource.
	/// </summary>
	public abstract class BaseCompressedSource<T>  : CompressedSource where T : Interval, new() {
		protected IDictionary<string, Bar> compressedSource;
		protected T period = new T();
		
		public Bar getBar(DateTime date) {
			return compressedSource[period.keyFromTime(date)];
		}
		public abstract Bar getPreviousBar(DateTime date);
		public abstract Bar getNextBar(DateTime date);
	}
}
