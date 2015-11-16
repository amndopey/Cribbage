using System;
using System.Collections.Generic;
using System.Linq;

namespace Cribbage.Computation
{
    public class Compute
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
            int numCards = 0;
            int runPoints = 0;

            produceList(cardList).ForEach(item =>
            {
                List<int> convertedPoints = new List<int>(item);
                //Convert face cards to 10 points
                for (int i = 0; i < convertedPoints.Count() - 1; i++)
                    if (convertedPoints[i] > 10)
                        convertedPoints[i] = 10;


                //Find all runs
                int run = 0;
                for (int i = 1; i <= 14; i++)
                {
                    if (item.Contains(i))
                    {
                        run++;
                    }
                    else
                    {
                        if (run >= 3)
                        {
                            if (run > numCards)
                            {
                                runPoints = run;
                                numCards = run;
                            }
                            else if (run == item.Count())
                            {
                                runPoints = runPoints + run;
                            }
                        }

                        run = 0;
                    }
                }


                //Find all ways to equal 15
                if (convertedPoints.Sum() == 15)
                {
                    points = points + 2;
                }
            });

            points += runPoints;

            //Find all pairs, triples, etc.
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

            return points;
        }

        static public List<int> FindBestHand(List<int> cardList)
        {
            int points = 0;
            List<int> bestHand = new List<int>();

            produceList(cardList).ForEach(item =>
            {
                if (item.Count() != 4)
                    return;

                int check = CountPoints(item);

                if (check > points)
                {
                    bestHand = item;
                    points = check;
                }
            });

            return bestHand;
        }

        static public List<int> DealHand()
        {
            List<int> hand = new List<int>();
            Random rng = new Random();

            for (int i = 0; i < 6; i++)
            {
                hand.Add(rng.Next(1, 13));
            }

            return hand;
        }
    }
}