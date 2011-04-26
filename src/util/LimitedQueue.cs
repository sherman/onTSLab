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

namespace org.ontslab.util {
	/// <summary>
	/// Description of LimitedQueue.
	/// </summary>
	public class LimitedQueue<T> : Queue<T> {
    	private int limit = -1;
    	
    	 public LimitedQueue(int limit) : base(limit) {
	        this.limit = limit;
    	} 

    	public new void Enqueue(T item) {
        	if (this.Count >= this.limit) {
            	this.Dequeue();
        	}
    		
        	base.Enqueue(item);
    	}
	}
}
