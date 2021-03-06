﻿/***************************************************************************
*   Copyright (C) 2011-2012 by Denis M. Gabaydulin                        *
*                                                                         *
*   This program is free software; you can redistribute it and/or modify  *
*   it under the terms of the GNU Lesser General Public License as        *
*   published by the Free Software Foundation; either version 3 of the    *
*   License, or (at your option) any later version.                       *
*                                                                         *
***************************************************************************/

using TSLab.Script;
using System.Linq;
using System;
using System.Collections.Generic;
using TSLab.DataSource;

namespace org.ontslab.data {
	/// <summary>
	/// Description of SourceUtils.
	/// </summary>
	
	public static class SecurityExtensions {
		public static IList<IPosition> getLastPosititons(this ISecurity source, int count) {
			return source.Positions.Where(postition => !postition.IsActive).Skip(source.Positions.Count() - count).ToList();
		}

		public static bool isLastTradeLoss(this ISecurity source, int barIndex) {
            IPosition lastPosition = source.Positions.GetLastPositionClosed(barIndex);

			if (null == lastPosition) {
				return false;
			}
			
			return lastPosition.Profit() < 0;
        }

		public static double getLastTradeProfit(this ISecurity source, int barIndex) {
			IPosition lastPosition = source.Positions.GetLastPositionClosed(barIndex);

			if (null == lastPosition) {
				return -1;
			}

			return lastPosition.Profit();
		}
		
		public static IDataBar getLastEntryBar(this ISecurity source, int barIndex) {
			IPosition lastPosition = source.Positions.GetLastPositionClosed(barIndex);

			if (null == lastPosition) {
				return null;
			}
			
			return lastPosition.EntryBar;
		}

		public static int getLastEntryBarNum(this ISecurity source, int barIndex) {
			IPosition lastPosition = source.Positions.GetLastPositionClosed(barIndex);

			if (null == lastPosition) {
				return -1;
			}

			return lastPosition.EntryBarNum;
		}

		public static IPosition getFirstTrade(this ISecurity source) {
			IEnumerator<IPosition> positionEnum = source.Positions.GetEnumerator();
			while (positionEnum.MoveNext()) {
				if (!positionEnum.Current.IsActive) {
					return positionEnum.Current;
				}
			}
			
			return null;
		}
    }
}
