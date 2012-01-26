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
		public delegate T DistFunction<T>(T[,] matrix, int i, int j);
		
		public static T[,] asMatrix<T>(IList<T> data) {
			T[,] matrix = new T[1,data.Count];
			for (int i = 0; i < data.Count; i++) {
				matrix[0,i] = data[i];
			}
			return matrix;
		}
		
		public static T[,] getDistanceMatrix<T>(
			T[,] matrix,
			DistFunction<T> func,
			T defaultValue
		) {
			long len = matrix.Length;

			T[,] distances = new T[len,len];
			
			for (int i = 0; i < len; i++) {
 				for (int j = i + 1; j < len; j++) {
					distances[i,j] = func(matrix, i, j);
  					distances[j,i] = distances[j,i];
				}
				distances[i,i] = defaultValue;
			}
			
			return distances;
		}
	}
}
