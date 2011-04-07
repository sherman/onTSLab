/*
 * Created by SharpDevelop.
 * User: Sherminator
 * Date: 07.04.2011
 * Time: 15:39
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace org.ontslab.trading.position {
	/// <summary>
	/// Description of Positions.
	/// </summary>
	public static class Positions {
		public static PositionCalculator fixedSize(double size) {
			return new FixedSizePositionCalculator(size);
		}
	}
}
