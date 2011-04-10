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

namespace org.ontslab.trading.position {
	/// <summary>
	/// Description of TakeProfit.
	/// </summary>
	public interface TakeProfit {
		void createOn(IPosition position, int barIndex);
	}
}
