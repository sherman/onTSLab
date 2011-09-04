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
        	using (WebClient client = new WebClient()) {
            	return client.DownloadString(url);
        	}
		}
	}
}
