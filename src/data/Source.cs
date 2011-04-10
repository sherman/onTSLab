/*
 * Created by SharpDevelop.
 * User: Sherminator
 * Date: 10.04.2011
 * Time: 18:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using TSLab.Script;

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
    }   
}
