/***************************************************************************
*   Copyright (C) 2011-2107 by Denis M. Gabaydulin                        *
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

namespace org.ontslab.data {
    /// <summary>
    /// Description of CompressedSource.
    /// </summary>
    public interface CompressedSource {
        IDataBar GetBar(DateTime date);
        IDataBar GetPrevBar(DateTime date);
        IDataBar GetNextBar(DateTime date);
        IDataBar GetFirstBar();

        IList<IDataBar> GetBars();

        // Deprecated
        IReadOnlyList<IDataBar> GetBarsAsReadonly();
    }
}