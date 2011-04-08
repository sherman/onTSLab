/*
 * Created by SharpDevelop.
 * User: Sherminator
 * Date: 08.04.2011
 * Time: 17:35
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace org.ontslab.data
{
	/// <summary>
	/// Description of NotEnoughDataException.
	/// </summary>
	public class NotEnoughDataException : Exception {
		public NotEnoughDataException(string message) : base(message) {}
	}
}
