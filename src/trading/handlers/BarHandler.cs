/*
 * Created by SharpDevelop.
 * User: Sherminator
 * Date: 07.04.2011
 * Time: 13:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using TSLab.Script;

namespace org.ontslab.trading.handlers
{
	/// <summary>
	/// Description of BarHandler.
	/// </summary>
	public interface BarHandler {
		void handleBar(Bar bar);
	}
}
