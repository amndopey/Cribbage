using System;
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
        private int points;
        public int Points
        {
            get { return points; }
            set { points = value; }
        }

        private List<string> breakdown;
        public List<string> Breakdown
        {
            get {
                if (breakdown == null)
                    breakdown = new List<string>();
                return breakdown;
            }
            set { breakdown = value; }
        }
    }

    public class Cards
    {
        private List<int> hand;
        public List<int> Hand
        {
            get {
                if (hand == null)
                    hand = new List<int>();
                return hand;
            }
            set { hand = value; }
        }

        private List<int> crib;
        public List<int> Crib
        {
            get {
                if (crib == null)
                    crib = new List<int>();
                return crib;
            }
            set { crib = value; }
        }

        private List<int> played;
        public List<int> Played
        {
            get {
                if (played == null)
                    played = new List<int>();
                return played;
            }
            set { played = value; }
        }

        public int PointCard { get; set; }
    }
}