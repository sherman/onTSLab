﻿/***************************************************************************
*   Copyright (C) 2011-2017 by Denis M. Gabaydulin                        *
*                                                                         *
*   This program is free software; you can redistribute it and/or modify  *
*   it under the terms of the GNU Lesser General Public License as        *
*   published by the Free Software Foundation; either version 3 of the    *
*   License, or (at your option) any later version.                       *
*                                                                         *
***************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using TSLab.Script;
using TSLab.DataSource;
using Interval = org.ontslab.misc.Interval;

namespace org.ontslab.data {
	/// <summary>
	/// Description of BaseCompressedSource.
	/// </summary>
	public abstract class BaseCompressedSource<T>  : CompressedSource where T : Interval, new() {
		protected IDictionary<string, IDataBar> compressedSource;
		protected IDataBar first;
		protected IDataBar last;
		protected T period = new T();
		
		protected void createCompressedSourceFrom(IList<IDataBar> source) {
			compressedSource = new Dictionary<string, IDataBar>(source.Count);
			
			source.ToList().ForEach(
				bar => compressedSource.Add(period.keyFromTime(bar.Date), bar)
			);
			
			first = source.First();
			last = source.Last();
		}
		
		public IDataBar GetBar(DateTime date) {
			return compressedSource[period.keyFromTime(date)];
		}
		
		public IDataBar GetPrevBar(DateTime date) {
			string previousBarKey = null;
			
			DateTime startBarDate = date;
			
			do {
				startBarDate = startBarDate.Subtract(period.getTimeSpan());
				previousBarKey = period.keyFromTime(startBarDate);
				
				if (compressedSource.ContainsKey(previousBarKey)) {
					break;
				}
			} while (startBarDate >= first.Date);
			
			if (compressedSource.ContainsKey(previousBarKey))
				return compressedSource[previousBarKey];
			else
				return null;
		}
		
		public IDataBar GetNextBar(DateTime date) {
			string nextBarKey = null;
			
			DateTime startBarDate = date;
			
			do {
				startBarDate = startBarDate.Add(period.getTimeSpan());
				nextBarKey = period.keyFromTime(startBarDate);
				
				if (compressedSource.ContainsKey(nextBarKey)) {
					break;
				}
			} while (startBarDate <= last.Date);
			
			if (compressedSource.ContainsKey(nextBarKey))
				return compressedSource[nextBarKey];
			else
				return null;
		}

		public IList<IDataBar> GetBars() {
			return compressedSource.Values.ToList();
		}

		public IReadOnlyList<IDataBar> GetBarsAsReadonly() {
			return compressedSource.Values.ToList().AsReadOnly();
		}

		public IDataBar GetFirstBar() {
			return first;
		}
	}
}
