/*
 * Created by SharpDevelop.
 * User: Sherminator
 * Date: 09.04.2011
 * Time: 19:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;

using TSLab.Script;
using TSLab.Script.Handlers;
using TSLab.Script.Helpers;
using TSLab.DataSource;

namespace org.ontslab.data {
	/// <summary>
	/// Description of DataHelpers.
	/// </summary>
	public static class DataHelpers {
		public static IList<double> roundList(IList<Double> data, int precision) {
			return data.Select(
				elt => Math.Round(elt, precision) 
			).ToList();
		}
		
		public static IList<double> generateATR(IContext ctx, ISecurity source, int period) {
			return ctx.GetData(
				"ATR" + period.ToString(),
				new[] {period.ToString()}, delegate {
					return Series.AverageTrueRange(source.Bars, period);
				}
			);
		}
		
		public static IList<double> generateADX(IContext ctx, ISecurity source, int period) {
			ADXFull generator = new ADXFull();
			generator.Context = ctx;
			generator.Period = period;
			
			return ctx.GetData(
				"ADX" + period.ToString(),
				new[] {period.ToString()}, delegate {
					return generator.Execute(source);
				}
			);
		}
		
		public static IList<double> generateEMA(IContext ctx, ISecurity source, int period) {
			return generateEMA(ctx, source.ClosePrices, period);
		}
		
		public static IList<double> generateEMA(IContext ctx, IList<double> source, int period) {
			return ctx.GetData(
				"EMA" + period.ToString(),
				new[] {period.ToString()}, delegate {
					return Series.EMA(source, period);
				}
			);
		}
		
		public static IList<double> generateSMA(IContext ctx, ISecurity source, int period) {
			return generateSMA(ctx, source.ClosePrices, period);
		}
		
		public static IList<double> generateSMA(IContext ctx, IList<double> source, int period) {
			return ctx.GetData(
				"SMA" + period.ToString(),
				new[] {period.ToString()}, delegate {
					return Series.SMA(source, period);
				}
			);
		}
		
		public static IList<double> generateHighest(IContext ctx, ISecurity source, int period) {
			return generateHighest(ctx, source.HighPrices, period);
		}
		
		public static IList<double> generateHighest(IContext ctx, IList<double> source, int period) {
			return ctx.GetData(
				"Highest" + period.ToString(),
				new[] {period.ToString()}, delegate {
					return Series.Highest(source, period);
				}
			);
		}
		
		public static IList<double> generateLowest(IContext ctx, ISecurity source, int period) {
			return generateLowest(ctx, source.LowPrices, period);
		}
		
		public static IList<double> generateLowest(IContext ctx, IList<double> source, int period) {
			return ctx.GetData(
				"Lowest" + period.ToString(),
				new[] {period.ToString()}, delegate {
					return Series.Lowest(source, period);
				}
			);
		}
		
		public static IList<double> generateShift(IContext ctx, IList<double> source, int period) {
			return ctx.GetData(
				"Shift" + period.ToString(),
				new[] {period.ToString()}, delegate {
					return Series.Shift(source, period);
				}
			);
		}
		
		public static IList<double> generateStDev(IContext ctx, IList<double> source, int period) {
			return ctx.GetData(
				"STDEV" + period.ToString(),
				new[] {period.ToString()}, delegate {
					return Series.StDev(source, period);
				}
			);
		}
		
		public static IList<double> generateRSI(IContext ctx, IList<double> source, int period) {
			return ctx.GetData(
				"RSI" + period.ToString(),
				new[] {period.ToString()}, delegate {
					return Series.CuttlerRSI(source, period);
				}
			);
		}
		
		public static double highest(int barIndex, IList<double> data, int period) {
			double max = 0;
			int lastBar = Math.Min(barIndex + period, data.Count);
			
			for (int i = barIndex; i < lastBar; i++) {
				max = Math.Max(max, data[i]);
			}
			
			return max;
		}
		
		public static double lowest(int barIndex, IList<double> data, int period) {
			double min = Double.MaxValue;
			int lastBar = Math.Min(barIndex + period, data.Count);
			
			for (int i = barIndex; i < lastBar; i++) {
				min = Math.Min(min, data[i]);
			}
			
			return min;
		}
	}
}
