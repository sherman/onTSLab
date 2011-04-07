/*
 * Created by SharpDevelop.
 * User: Sherminator
 * Date: 07.04.2011
 * Time: 14:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using TSLab.Script;

namespace org.ontslab.trading.handlers
{
	/// <summary>
	/// Description of CompositeBarHandler.
	/// </summary>
	public class CompositeBarHandler : BarHandler {
		private List<BarHandler> handlers = new List<BarHandler>();
		
		public CompositeBarHandler() {}
		
		public CompositeBarHandler add(BarHandler handler) {
			this.handlers.Add(handler);
			return this;
		}
		
		public void handleBar(Bar bar) {
			foreach (BarHandler handler in handlers) {
				handler.handleBar(bar);
			}
		}
	}
}
