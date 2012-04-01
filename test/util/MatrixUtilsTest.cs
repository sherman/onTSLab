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
using NUnit.Framework;
using org.ontslab.util;

namespace org.ontslab.test.util
{
	[TestFixture]
	public class MatrixUtilsTest {
		[Test]
		public void getDistanceMatrix() {
			var valuesList = new List<double>();
			valuesList.Add(4);
			valuesList.Add(4);
			valuesList.Add(4);
			valuesList.Add(3);
			valuesList.Add(4);
			valuesList.Add(4);
			valuesList.Add(4);
			valuesList.Add(3);
			
			var originalMatrix = MatrixUtils.asMatrix(valuesList, 2, 4);					
			
			var distMatrix = MatrixUtils.getDistanceMatrix(
				originalMatrix,
				// euclidean distance
				(list, rows, cols, i, j) => {
					var dist = 0.0;
					var count = 0;
					
					for (int k = 0; k < cols; k++) {
						var dev = Math.Abs(list[i] - list[j]);
						dist += dev * dev;
						++count;
						i += rows;
						j += rows;
					}
					
					if (count != cols) {
						dist /= count / cols;
					}
					
					return Math.Sqrt(dist);
				},
				-1.0
			);
			
			/*
			   -1.0  0    0     1.41 
				0   -1.0  0     1.41 
				0    0   -1.0   1.41 
				1.41 1.41 1.41 -1.0 
			 */
			
			var expectedDistances = new double[4, 4];
			expectedDistances[0,0] = -1.0;
			expectedDistances[1,1] = -1.0;
			expectedDistances[2,2] = -1.0;
			expectedDistances[3,3] = -1.0;
			
			expectedDistances[0,3] = 1.41;
			expectedDistances[1,3] = 1.41;
			expectedDistances[2,3] = 1.41;
			
			expectedDistances[3,0] = 1.41;
			expectedDistances[3,1] = 1.41;
			expectedDistances[3,2] = 1.41;
			
			for (int i = 0; i < distMatrix.GetLength(0); i++) {
				for (int j = 0; j < distMatrix.GetLength(1); j++) {
					Assert.AreEqual(expectedDistances[i,j], distMatrix[i,j], 0.01);
				}
			}
		}
	}
}
