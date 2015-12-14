﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cribbage.Classes;
using System.Collections.Generic;

namespace CribbageTest
{
    [TestClass]
    public class ComputePointsTest
    {
        [TestMethod]
        public void ComputePoints()
        {
            List<int> hand = new List<int>();
            hand.Add(106);
            hand.Add(107);
            hand.Add(108);
            hand.Add(202);
            
            
            //hand.Add(103);
            //hand.Add(104);
            //hand.Add(405);
            //hand.Add(106);
            //hand.Add(107);

            //hand.Add(110);

            int expected = 8;

            int actual = Compute.CountHand(hand, 109);

            Assert.AreEqual(expected, actual);
        }
    }
}
