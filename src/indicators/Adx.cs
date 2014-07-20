/***************************************************************************
 *   Copyright (C) 2011-2014 by Denis M. Gabaydulin                         *
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
using System.Text;
using TSLab.Script.Helpers;

namespace TSLab.Script.Handlers
{
	public abstract class BasePeriodIndicatorHandler
	{
		private int m_period;

		public int Period
		{
			get { return Math.Max(1, m_period); }
			set { m_period = value; }
		}
	}
	
	public static class ADXHelper
	{
		public static IList<double> CalcDIP(ISecurity source, IContext context, int period)
		{
			int count = source.Bars.Count;
			IList<double> diP = new double[count];

			var high = source.HighPrices;
			var low = source.LowPrices;

			var atr = Series.EMA(Series.TrueRange(source.Bars), period);

			for (int i = 1; i < count; i++)
			{
				var dmP = high[i] - high[i - 1];
				var dmM = low[i - 1] - low[i];
				if ((dmP < 0 && dmM < 0) || dmP == dmM)
				{
					dmP = dmM = 0;
				}
				if (dmM > dmP)
				{
					dmP = 0;
				}
				diP[i] = dmP;
			}
			diP = Series.EMA(diP, period);
			for (int i = 1; i < count; i++)
			{
				diP[i] = atr[i] == 0 ? 0 : diP[i] / atr[i];
			}
			return diP;
		}

		public static IList<double> CalcDIM(ISecurity source, IContext context, int period)
		{
			int count = source.Bars.Count;
			IList<double> diM = new double[count];

			var high = source.HighPrices;
			var low = source.LowPrices;

			var atr = Series.EMA(Series.TrueRange(source.Bars), period);

			for (int i = 1; i < count; i++)
			{
				var dmP = high[i] - high[i - 1];
				var dmM = low[i - 1] - low[i];
				if ((dmP < 0 && dmM < 0) || dmP == dmM)
				{
					dmP = dmM = 0;
				}
				if (dmP > dmM)
				{
					dmM = 0;
				}
				diM[i] = dmM;
			}
			diM = Series.EMA(diM, period);
			for (int i = 1; i < count; i++)
			{
				diM[i] = atr[i] == 0 ? 0 : diM[i] / atr[i];
			}
			return diM;
		}

		public static IList<double> CalcADX(IList<double> source1, IList<double> source2, int period)
		{
			int count = source1.Count;
			IList<double> dx = new double[count];

			for (int i = 1; i < count; i++)
			{
				dx[i] = source1[i] == 0 && source2[i] == 0
					? 0
					: Math.Abs(source1[i] - source2[i]) / (source1[i] + source2[i]) * 100;
			}
			dx = Series.EMA(dx, period);
			return dx;
		}
	}

	[HandlerName("+DI")]
	[HandlerCategory("Indicators")]
	public class DIP : BasePeriodIndicatorHandler, IContextUses, IBar2DoubleHandler
	{
		public IList<double> Execute(ISecurity source)
		{
			return ADXHelper.CalcDIP(source, Context, Period);
		}

		public IContext Context { get; set; }
	}

	[HandlerName("-DI")]
	[HandlerCategory("Indicators")]
	public class DIM : BasePeriodIndicatorHandler, IContextUses, IBar2DoubleHandler
	{
		public IList<double> Execute(ISecurity source)
		{
			return ADXHelper.CalcDIM(source, Context, Period);
		}

		public IContext Context { get; set; }
	}

	[HandlerName("ADX")]
	[HandlerCategory("Indicators")]
	public class ADXFull : BasePeriodIndicatorHandler, IContextUses, IBar2DoubleHandler
	{
		public IList<double> Execute(ISecurity source)
		{
			var dip = ADXHelper.CalcDIP(source, Context, Period);
			var dim = ADXHelper.CalcDIM(source, Context, Period);
			return ADXHelper.CalcADX(dip, dim, Period);
		}

		public IContext Context { get; set; }
	}

	[HandlerName("ADX (Old)")]
	[HandlerCategory("Indicators")]
	public class ADX : BasePeriodIndicatorHandler, IDoubleAccumHandler
	{
		public IList<double> Execute(IList<double> source1, IList<double> source2)
		{
			return ADXHelper.CalcADX(source1, source2, Period);
		}
	}
}
