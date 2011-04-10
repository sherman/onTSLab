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
using TSLab.Script;
using org.ontslab.misc;

namespace org.ontslab.trading.handlers {
	/// <summary>
	/// Description of Handlers.
	/// </summary>
	public static class Handlers {
		public static CompositeBarHandler composite() {
			return new CompositeBarHandler();
		}
		
		public static BalanceBarHandler balance(ISecurity source) {
			return new BalanceBarHandler(source);
		}
	}
}
