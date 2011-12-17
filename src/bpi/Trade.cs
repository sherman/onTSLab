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

namespace org.ontslab.bpi
{
	/// <summary>
	/// Description of Trade.
	/// </summary>
	public class Trade {
		private DateTime date;
		private double price;
		private int size;
		private int id;
		
		public Trade(int id, DateTime date, double price, int size) {
			this.date = date;
			this.price = price;
			this.size = size;
			this.id = id;
		}
		
		public int getId() {
			return id;
		}
		
		public DateTime getDate() {
			return date;
		}
		
		public double getPrice() {
			return price;
		}
		
		public int getSize() {
			return size;
		}
		
		public override string ToString(){
			return "{" + date + "," + price + "," + size + "}";
		}
		
		public bool isShort() {
			return getSize() < 0;
		}
	}
}
