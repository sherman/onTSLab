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

namespace org.ontslab.math {
	/// <summary>
	/// Description of CombinationGenerator.
	/// </summary>
	public static class CombinationGenerator {
		public static void generate<T>(
			IList<T> input,
			IList<T> ouput,
			IList<IList<T>> combinations,
			int depth
		) {
			if (depth == 0) {
				combinations.Add(new List<T>(ouput));
			} else {
				for (int i = 0; i < input.Count; i++) {
					ouput.Add(input[i]);
					generate(input, ouput, combinations, depth - 1);
					ouput.RemoveAt(ouput.Count - 1);
				}
			}
		}
	}
}
