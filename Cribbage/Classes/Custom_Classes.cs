﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace Cribbage.Classes
{
    public class BoardStatus
    {
        public Brush P1Color { get; set; }
        public int P1FirstPeg { get; set; }
        public int P1SecondPeg { get; set; }

        public Brush P2Color { get; set; }
        public int P2FirstPeg { get; set; }
        public int P2SecondPeg { get; set; }
    }

    public class PointBreakdown
    {
        public int Points { get; set; }
        public List<string> Breakdown { get; set; }
    }

    public class Cards
    {
        public List<int> Hand { get; set; }
        public List<int> Crib { get; set; }
        public List<int> Played { get; set; }
    }
}