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

namespace org.ontslab.statistic
{
	/// <summary>
	/// Description of Distribution.
	/// </summary>
	public class Distribution<T> {
		private String name;
		private IList<T> elts = new List<T>();
			
		public Distribution(String name) {
			this.name = name;
		}
		
		public Distribution<T> addElt(T elt) {
			this.elts.Add(elt);
			return this;
		}
		
		public string getName() {
			return name;
		}
		
		public IList<T> getElts() {
			return elts;
		}
	}
}
