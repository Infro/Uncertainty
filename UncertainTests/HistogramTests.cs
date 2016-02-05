﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Microsoft.Research.Uncertain;
using Microsoft.Research.Uncertain.Histogram;

namespace UncertainTests
{
    [TestClass]
    public class HistogramTests
    {
        [TestMethod]
        public void Test_Flattening()
        {
            var someDist = new Multinomial<int>(1, 2, 2, 3, 3, 3, 4);

            var someHist = Histogram.flatten(someDist);
        }

        public static double doubler(double x) {
            return 2.0 * x;
        }

        [TestMethod]
        public void Test_Lifting()
        {
            // You can lift C# lambdas.
            var liftedLambda = CSLifting.lift<double, double>(x => 2.0 * x);

            // You can also, apparently, pass static methods in the same way.
            // C#'s type inference is evidently not strong enough to figure
            // out the Func<> type parameters, alas, so we need type
            // annotations in both cases.
            var liftedStaticMethod = CSLifting.lift<double, double>(doubler);

            // Apply a lifted function to a distribution and flatten it back to
            // a histogram.
            var hist = Histogram.flatten(new Multinomial<double>(2.0, 2.0, 4.0));
            var doubledDist = liftedLambda(hist);
            var doubledHist = Histogram.reflatten(doubledDist);
        }
    }
}