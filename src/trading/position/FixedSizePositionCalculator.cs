/*
 * Created by SharpDevelop.
 * User: Sherminator
 * Date: 07.04.2011
 * Time: 15:34
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace org.ontslab.trading.position {
	/// <summary>
	/// Description of FixedSizePositionCalculator.
	/// </summary>
	public class FixedSizePositionCalculator : PositionCalculator {
		private double size;
		
		public FixedSizePositionCalculator(double size) { this.size = size; }
		
		public double getPositionSize() {
			return size;
		}
	}
}
