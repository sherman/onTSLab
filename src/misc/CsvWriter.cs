using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace org.ontslab.misc {

	public class CsvWriter {
		public delegate void LineHandler(IList<string> parts);

		private string fileName;
		private string separator;
		private StreamWriter writer;

		public CsvWriter(string fileName, string separator) {
			if (!File.Exists(fileName))
				throw new FileNotFoundException("There is no such file:" + fileName);

			this.fileName = fileName;
			this.separator = separator;
		}

		public void write(List<List<string>> lines) {
			writer = new StreamWriter(fileName);

			foreach (var line in lines) {
				writer.WriteLine(String.Join(separator, line));
			}

			writer.Close();
		}
	}
}
