/***************************************************************************
*   Copyright (C) 2012 by Denis M. Gabaydulin                             *
*                                                                         *
*   This program is free software; you can redistribute it and/or modify  *
*   it under the terms of the GNU Lesser General Public License as        *
*   published by the Free Software Foundation; either version 3 of the    *
*   License, or (at your option) any later version.                       *
*                                                                         *
***************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;

namespace org.ontslab.util
{
	/// <summary>
	/// Description of MatrixUtils.
	/// </summary>
	public static class MatrixUtils {
		public static T[,] asMatrix<T>(IList<T> data) {
			T[,] matrix = new T[1,data.Count];
			for (int i = 0; i < data.Count; i++) {
				matrix[0,i] = data[i];
			}
			return matrix;
		}                      
	}
}
