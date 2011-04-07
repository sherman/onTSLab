/*
 * Created by SharpDevelop.
 * User: Sherminator
 * Date: 07.04.2011
 * Time: 16:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using NUnit.Framework;
using NUnit.Mocks;
using org.ontslab.trading.handlers;
using TSLab.Script;

namespace org.ontslab.test.trading.handlers {
	[TestFixture]
	public class ProfitPerMonthTest {
		private DynamicMock sourceMock;
		private DynamicMock positionMock;
		private DynamicMock positionListMock;
		
		[SetUp]
		public void init() {
			sourceMock = new DynamicMock(typeof(ISecurity));
			positionMock = new DynamicMock(typeof(IPosition));
			positionListMock = new DynamicMock(typeof(IPositionsList));
			
			sourceMock.ExpectAndReturn(
				"get_Positions",
				(IPositionsList)positionListMock.MockInstance
			);
			positionListMock.ExpectAndReturn(
				"get_LastPositionClosed",
				(IPosition)positionMock.MockInstance
			);
			
		}
		
		[Test]
		public void testHandleBar() {
			Bar expectedExitBar = new Bar(
				0x000000,
				new DateTime(2011, 2, 1),
				100,
				105,
				99,
				99,
				1
			);
			positionMock.SetReturnValue("get_ExitBar", expectedExitBar);
			
			Bar bar = new Bar(0x000000, new DateTime(2011, 2, 2), 100, 105, 102, 103, 1);
			ProfitPerMonth pnl = new ProfitPerMonth((ISecurity)sourceMock.MockInstance);
			pnl.handleBar(bar);
			
			Assert.AreEqual(expectedExitBar, pnl.getLastExitBar());
		}
	}
}
