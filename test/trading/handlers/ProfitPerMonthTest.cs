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
using NUnit.Framework;
using NUnit.Mocks;
using org.ontslab.trading.handlers;
using org.ontslab.misc;
using org.ontslab.analytic;
using TSLab.Script;

// FIXME: fix tests
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
			ProfitPerPeriod<Month> pnl = AnalyticTools.profitPerMonth((ISecurity)sourceMock.MockInstance);
			
			Assert.AreEqual(expectedExitBar, pnl.getLastExitBar());
		}
		
		[Test]
		public void testGetProfitPerList() {
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
			positionMock.ExpectAndReturn("Profit", 100.0);
			
			Bar bar = new Bar(0x000000, new DateTime(2011, 2, 2), 100, 105, 102, 103, 1);
			ProfitPerPeriod<Month> pnl = AnalyticTools.profitPerMonth((ISecurity)sourceMock.MockInstance);
			
			List<double> actual = pnl.getProfitPerPeriodList();
			
			Assert.AreEqual(1, actual.Count);
			Assert.AreEqual(100.0d, actual.Find( p => true));
		}
	}
}
