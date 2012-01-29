/***************************************************************************
*   Copyright (C) 2012 by Denis M. Gabaydulin                             *
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

namespace org.ontslab.util
{
	/// <summary>
	/// Description of ListUtils.
	/// </summary>
	public static class ListUtils {
		// TODO: optimize
		public static List<double> getRank<T>(List<T> list) {
			Dictionary<T, double> eltToRanks = new Dictionary<T, double>();
			List<double> ranks = new List<double>();
			List<T> copy = new List<T>(list);
			int rank = 1;
			double actualRank = 0;
			T current = default(T);
			// this flag is required because default value can equals to the first in the copy list
			bool started = false;
			copy.Sort();
			copy.ForEach(
				elt => {
					if (!current.Equals(elt) || !started) {
						current = elt;
						int count = copy.Count( nextElt => nextElt.Equals(current) );
						
						double sum = 0;
						for (int i = 0; i < count; i++) {
							sum += rank;
							rank++;
						}
						
						actualRank = sum / count;
						started = true;
					}
					
					if (!eltToRanks.ContainsKey(current)) {
						eltToRanks.Add(current, actualRank);
					}
				}
			);
			
			return list.Select(elt => eltToRanks[elt]).ToList();
		}
	}
}
