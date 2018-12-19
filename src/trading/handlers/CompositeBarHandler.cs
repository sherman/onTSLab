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
using TSLab.DataSource;
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
		
		public void handleBar(IDataBar bar) {
			foreach (BarHandler handler in handlers) {
				handler.handleBar(bar);
			}
		}
	}
}
