/***************************************************************************
*   Copyright (C) 2020 by Denis M. Gabaydulin                             *
*                                                                         *
*   This program is free software; you can redistribute it and/or modify  *
*   it under the terms of the GNU Lesser General Public License as        *
*   published by the Free Software Foundation; either version 3 of the    *
*   License, or (at your option) any later version.                       *
*                                                                         *
***************************************************************************/

using System;
using System.Linq;
using TSLab.Script;

namespace org.ontslab.analytic {
    public class ZStatistic {
        private ISecurity source;

        public ZStatistic(ISecurity source) {
            this.source = source;
        }

        public double getStatistic() {
            var trades = source.Positions
                .Where(position => !position.IsActive)
                .ToList();

            var n = trades.Count;

            var direction = 0; // 1 - long, -1 - short
            var changes = 0;
            var win = 0;
            var loss = 0;
            for (var i = 0; i < n; i++) {
                int next = trades[i].IsLong ? 1 : -1;
                if (direction == 0) {
                    direction = next;
                    changes++;
                } else {
                    if (direction != next) {
                        direction = next;
                        changes++;
                    }
                }

                if (trades[i].Profit() > 0) {
                    win++;
                } else {
                    loss++;
                }
            }
           
            var x = 2 * win * loss;
            
            var v1 = n * (changes - 0.5) - x;
            var v2 = (double)(x * (x - n)) / (n - 1);

            var stat = v1 / Math.Pow(v2, 0.5);
            
            return stat;
        }
    }
}