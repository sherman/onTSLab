/***************************************************************************
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

namespace org.ontslab.data {
	/// <summary>
	/// Description of SourceUtils.
	/// </summary>
	
	public static class SecurityExtensions {
        public static bool isLastTradeLoss(this ISecurity source) {
            IPosition lastPosition = source.Positions.LastPositionClosed;
			
			if (null == lastPosition)
				return false;
			
			return lastPosition.Profit() < 0;
        }
		
		public static Bar getLastEntryBar(this ISecurity source) {
			IPosition lastPosition = source.Positions.LastPositionClosed;
			
			if (null == lastPosition)
				return null;
			
			return lastPosition.EntryBar;
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
