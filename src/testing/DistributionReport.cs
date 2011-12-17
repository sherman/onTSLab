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
using System.Linq;
using org.ontslab.statistic;
using org.ontslab.util;
using TSLab.Script;
using TSLab.Script.Handlers;
using TSLab.Script.Helpers;

namespace org.ontslab.testing {
	/// <summary>
	/// Description of DistributionReport.
	/// </summary>
	/// 
	
	public class DistributionReport {
		private Distribution<double> distribution;
		
		public DistributionReport(Distribution<double> distribution) {
			this.distribution = distribution;
		}
		
		public DistributionReport draw(IContext context, double round) {
			context.Log("Distribution:" + distribution.getName(), 0x000);
			context.Log("Average:" + distribution.getElts().Average(), 0x000);
			context.Log("Count:" + distribution.getElts().Count(), 0x000);
			
			IOrderedEnumerable<IGrouping<double, double>> elts = distribution.getElts().Select(
				elt => Math.Floor(elt / round) * round
			).GroupBy(
				elt => elt
			).OrderByDescending(
				elt => elt.Key
			);
			
			elts.ToList().ForEach(
				elt => context.Log(
					elt.Key  + ":" + StringUtils.Repeat('#', elt.Count() ),
					0x000
				)
			);
				
			return this;
		}
	}
}
