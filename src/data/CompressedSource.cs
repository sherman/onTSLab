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

namespace org.ontslab.data {
	/// <summary>
	/// Description of CompressedSource.
	/// </summary>
	public interface CompressedSource {
		Bar getBar(DateTime date);
		Bar getPreviousBar(DateTime date);
		Bar getNextBar(DateTime date);
	}
}
