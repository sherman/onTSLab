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

// TODO: refactor with Linq

namespace org.ontslab.data {
	/// <summary>
	/// Description of DataUtils.
	/// </summary>
	public static class DataUtils {
		public static bool isIndicatorGrows(
			IList<double> data,
			int barIndex,
			int bars,
			double tresholdValue
		) {
			if (barIndex - bars < 0) {
				throw new NotEnoughDataException("Not enough data!");
			}
			
			for (int i = 0; i < bars; i++) {
				double currValue = data[barIndex - i];
				double prevValue = data[barIndex - (i + 1)];
				
				if (prevValue >= currValue || (currValue - prevValue) < tresholdValue)
					return false;
			}
			
			return true;
		}
		
		public static int getDirectionChangingCount(
			IList<double> data,
			int barIndex,
			int bars
		) {
			bool grow = false;
			if (isIndicatorGrows(data, barIndex, bars, 0))
				grow = true;
			
			int counter = 0;
			
			for (int i = 0; i < bars; i++) {
				double curr = data[barIndex - i];
				double prev = data[barIndex - i - 1];
				
				if (grow) {
					if (prev < curr)
						continue;
					else {
						grow = false;
						++counter;
					}
				} else {
					if (prev > curr)
						continue;
					else {
						grow = true;
						++counter;
					}
				}
			}
			
			return counter;
		}
	}
}
