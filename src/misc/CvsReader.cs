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
	/// Description of CvsReader.
	/// </summary>
	public class CvsReader {
		private string fileName;
		private string splitter;
		private StreamReader reader;
		
		public CvsReader(string fileName, string splitter) {
			if (!File.Exists(fileName))
				throw new FileNotFoundException("There is no such file:" + fileName);
			
			this.fileName = fileName;
			this.splitter = splitter;
		}
		
		public IList<IList<string>> read() {
			reader = new StreamReader(fileName);
			
			do {
				String line = reader.ReadLine();
			} while (reader.Peek() != -1);
			
			return null;
		}
	}
}
