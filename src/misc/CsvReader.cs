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
using System.IO;
using System.Collections.Generic;

namespace org.ontslab.misc
{
	/// <summary>
	/// Description of CsvReader.
	/// </summary>
	public class CsvReader<T> {
		public delegate T LineHandler(IList<string> parts);
		
		private string fileName;
		private string separator;
		private StreamReader reader;
		
		public CsvReader(string fileName, string separator) {
			if (!File.Exists(fileName))
				throw new FileNotFoundException("There is no such file:" + fileName);
			
			this.fileName = fileName;
			this.separator = separator;
		}
		
		public IList<T> read(LineHandler lineHandler) {
			reader = new StreamReader(fileName);
			
			IList<T> result = new List<T>();
			do {
				String line = reader.ReadLine();
				result.Add(lineHandler(line.Split(separator.ToCharArray())));
			} while (reader.Peek() != -1);
			
			return result;
		}
	}
}
