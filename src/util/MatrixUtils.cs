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
		public delegate T DistFunction<T>(IList<T> list, int rows, int cols, int i, int j);
		
		public static T[,] asMatrix<T>(IList<T> data) {
			T[,] matrix = new T[1, data.Count];
			for (int i = 0; i < data.Count; i++) {
				matrix[0,i] = data[i];
			}
			return matrix;
		}
		
		public static T[,] asMatrix<T>(IList<T> data, int rows, int cols) {
			T[,] matrix = new T[rows, cols];
			for (int i = 0; i < rows; i++) {
				for (int j = 0; j < cols; j++) {
					matrix[i, j] = data[j];
				}
			}
			return matrix;
		}
		
		public static T[,] getDistanceMatrix<T>(
			T[,] matrix,
			DistFunction<T> func,
			T defaultValue
		) {
			List<T> valuesList = ListUtils.asList(matrix);
			List<T> distValuesList = new List<T>(valuesList.Count);
			T[,] distances = new T[matrix.GetLength(1), matrix.GetLength(1)];
			
			for (int i = 0; i <= matrix.GetLength(1); i++) {
				for (int j = i + 1; j < matrix.GetLength(1); j++) {
					var dist = func(
						valuesList,
						matrix.GetLength(1),
						matrix.GetLength(0),
						j,
						i
					);
					
					distValuesList.Add(dist);
				}
			}
			
			// convert list to matrix
			for (int i = 0; i < distances.GetLength(0); i++) {
				for (int j = i + 1; j < distances.GetLength(1); j++) {
					if (i == 0 || j == 0) {
						distances[i, j] = distValuesList[i + j - 1];
						distances[j, i] = distValuesList[i + j - 1];
					} else {
						distances[i, j] = distValuesList[i + j];
						distances[j, i] = distValuesList[i + j];
					}
				}
				
				distances[i, i] = defaultValue;
			}
			
			return distances;
		}
		
		public static T[,] getSlice<T>(T[,] matrix, int[] rowIndices, int[] colIndices) {
			T[,] slice = new T[rowIndices.Length, colIndices.Length];
			int sliceRows = 0, sliceCols = 0;
			
			rowIndices.ToList().ForEach(
				rowIndex => {
					colIndices.ToList().ForEach(
						colIndex => {
							slice[sliceRows, sliceCols++] = matrix[rowIndex, colIndex];
						}
					);
						
					sliceRows++;
					sliceCols = 0;
				}
			);
			
			return slice;
		}
	}
}
