/***************************************************************************
*   Copyright (C) 2017 by Denis M. Gabaydulin                             *
*                                                                         *
*   This program is free software; you can redistribute it and/or modify  *
*   it under the terms of the GNU Lesser General Public License as        *
*   published by the Free Software Foundation; either version 3 of the    *
*   License, or (at your option) any later version.                       *
*                                                                         *
***************************************************************************/
using System.Collections.Generic;
using TSLab.Script;
using TSLab.DataSource;
using org.ontslab.misc;

namespace org.ontslab.data {
    public class FifteenMinuteSource : BaseCompressedSource<FifteenMinutes> {
        private ISecurity compressed;
        
        public FifteenMinuteSource(ISecurity original) {
            createFifteenMinutesSourceFrom(original);
        }
		
        public FifteenMinuteSource(IList<Bar> original) {
            createCompressedSourceFrom(original);
        }
        
        // TODO: implement in base class
        public ISecurity getCompressed() {
            return compressed;
        }
		
        private void createFifteenMinutesSourceFrom(ISecurity original) {
            ISecurity fifteenMinutesSource = original.CompressTo(
                new TSLab.DataSource.Interval(15, DataIntervals.MINUTE)
            );
			
            IDictionary<string, Bar> newSourceBars =
                new Dictionary<string, Bar>(fifteenMinutesSource.Bars.Count);
			
            // convert to source bars
            foreach (Bar bar in fifteenMinutesSource.Bars) {
                var key = period.keyFromTime(bar.Date);
				
                if (!newSourceBars.ContainsKey(key)) {
                    newSourceBars.Add(key, bar);
                }
            }
			
            this.compressedSource = newSourceBars;
            this.first = original.Bars[0].Date;
            this.last = original.Bars[original.Bars.Count - 1].Date;
            this.compressed = fifteenMinutesSource;
        }
    }
}