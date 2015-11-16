﻿using System.Collections.Generic;
using System.Linq;

namespace Cribbage.PointCounter
{
    public class PointCounter
    {
        static private IEnumerable<int> constructSetFromBits(int i)
        {
            for (int n = 0; i != 0; i /= 2, n++)
            {
                if ((i & 1) != 0)
                    yield return n;
            }
        }

        static private IEnumerable<List<int>> produceEnumeration(List<int> allValues)
        {
            for (int i = 0; i < (1 << allValues.Count); i++)
            {
                yield return
                    constructSetFromBits(i).Select(n => allValues[n]).ToList();
            }
        }

        static private List<List<int>> produceList(List<int> allValues)
        {
            return produceEnumeration(allValues).ToList();
        }

        static public int CountPoints(List<int> cardList)
        {
            int points = 0;

            produceList(cardList).ForEach(item =>
            {
                if (item.Sum() == 15)
                {
                    points = points + 2;
                }
            });

            var numberGroups = cardList.GroupBy(i => i);
            foreach (var grp in numberGroups)
            {
                if (grp.Count() == 2)
                    points = points + 2;
                else if (grp.Count() == 3)
                    points = points + 6;
                else if (grp.Count() == 4)
                    points = points + 12;
            }

            int run = 0;
            for (int i = 1; i <= 13; i++)
            {
                if (cardList.Contains(i))
                {
                    run++;
                }
                else
                {
                    if (run >= 3)
                        points = points + run;

                    run = 0;
                }
            }

            return points;
        }
    }
}