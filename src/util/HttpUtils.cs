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
using System.Net;

namespace org.ontslab.util
{
	/// <summary>
	/// Description of HttpUtils.
	/// </summary>
	public static class HttpUtils {
		public static String get(String url) {
        	using (TimeoutSupportWebClient client = new TimeoutSupportWebClient(1000)) {
            	return client.DownloadString(url);
        	}
		}
	}
	
	public class TimeoutSupportWebClient: WebClient {
   		// milliseconds
    	private int timeout;
   		public int Timeout {
           get { return timeout; }
           set { timeout = value; }
    	}

	    public TimeoutSupportWebClient() {
	           this.timeout = 5000;
	    }
		
	    public TimeoutSupportWebClient(int timeout) {
    		this.timeout = timeout;
    	}
		
	    protected override WebRequest GetWebRequest(Uri address) {
	           var result = base.GetWebRequest(address);
	           result.Timeout = this.timeout;
	           return result;
	    }
	}
}
