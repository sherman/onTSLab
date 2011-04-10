/*
 * Created by SharpDevelop.
 * User: Sherminator
 * Date: 08.04.2011
 * Time: 23:39
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
/***************************************************************************
*   Copyright (C) 2011 by Denis M. Gabaydulin                             *
*                                                                         *
*   This program is free software; you can redistribute it and/or modify  *
*   it under the terms of the GNU Lesser General Public License as        *
*   published by the Free Software Foundation; either version 3 of the    *
*   License, or (at your option) any later version.                       *
*                                                                         *
***************************************************************************/

using TSLab.Script;
using org.ontslab.misc;

namespace org.ontslab.analytic {
	/// <summary>
	/// Description of AnalyticTools.
	/// </summary>
	public static class AnalyticTools {
		public static ProfitPerPeriod<Year> profitPerYear(ISecurity source) {
			return new ProfitPerPeriod<Year>(source);
		}
		
		public static ProfitPerPeriod<Month> profitPerMonth(ISecurity source) {
			return new ProfitPerPeriod<Month>(source);
		}
		
		public static ProfitPerPeriod<Day> profitPerDay(ISecurity source) {
			return new ProfitPerPeriod<Day>(source);
		}
		
		public static MaxLossPerPeriod<Month> maxLossPerMonth(ISecurity source) {
			return new MaxLossPerPeriod<Month>(source);
		}
	}
}
