using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace Cribbage.Classes
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
            List<int> originalHand = new List<int>(cardList);
            cardList = StripSuit(cardList);

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
                                runPoints += + run;
                            }
                        }

                        run = 0;
                    }
                }


                //Find all ways to equal 15
                if (convertedPoints.Sum() == 15)
                {
                    points += 2;
                }
            });

            points += runPoints;

            //Find all pairs, triples, etc.
            var numberGroups = cardList.GroupBy(i => i);
            foreach (var grp in numberGroups)
            {
                if (grp.Count() == 2)
                    points += 2;
                else if (grp.Count() == 3)
                    points += 6;
                else if (grp.Count() == 4)
                    points += 12;
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

        static public Cards DealHand()
        {
            Cards cards = new Cards();
            Random rng = new Random();

            do
            {
                int card = new int();

                //Get card number
                card = rng.Next(1, 13);
                //Get card suit
                card += (rng.Next(1, 4)) * 100;

                if (!cards.Hand.Contains(card))
                {
                    cards.Hand.Add(card);
                }
            } while (cards.Hand.Count() < 13);
            
            return cards;
        }

        static public List<int> StripSuit(List<int> hand)
        {
            List<int> returnHand = new List<int>();
            
            foreach (int card in hand)
            {
                if (card > 100 && card < 199)
                    returnHand.Add(card - 100);
                if (card > 200 && card < 299)
                    returnHand.Add(card - 200);
                if (card > 300 && card < 399)
                    returnHand.Add(card - 300);
                if (card > 400 && card < 499)
                    returnHand.Add(card - 400);
            }

            return returnHand;
        }

        static public int StripSuit(int card)
        {
            if (card > 100 && card < 199)
                return (card - 100);
            if (card > 200 && card < 299)
                return (card - 200);
            if (card > 300 && card < 399)
                return (card - 300);
            if (card > 400 && card < 499)
                return (card - 400);

            throw new ArgumentOutOfRangeException();
        }

        static public BoardStatus AddPointsToBoard(BoardStatus boardStatus, int player, int points)
        {
            if (player == 1)
            {
                if (boardStatus.P1FirstPeg > boardStatus.P1SecondPeg)
                {
                    if (boardStatus.P1FirstPeg + points > 122)
                        boardStatus.P1SecondPeg = 122;
                    else
                        boardStatus.P1SecondPeg = boardStatus.P1FirstPeg + points;
                }
                else
                {
                    if (boardStatus.P1SecondPeg + points > 122)
                        boardStatus.P1SecondPeg = 122;
                    else
                        boardStatus.P1FirstPeg = boardStatus.P1SecondPeg + points;
                }
            }
            else
            {
                if (boardStatus.P2FirstPeg > boardStatus.P2SecondPeg)
                {
                    if (boardStatus.P2FirstPeg + points > 122)
                        boardStatus.P2FirstPeg = 122;
                    else
                        boardStatus.P2SecondPeg = boardStatus.P2FirstPeg + points;
                }
                else
                    if (boardStatus.P2SecondPeg + points > 122)
                        boardStatus.P2SecondPeg = 122;
                    else
                        boardStatus.P2FirstPeg = boardStatus.P2SecondPeg + points;
            }

            return boardStatus;
        }

        static public List<string> StoreOrder(List<int> played)
        {
            int myChar = 65;
            List<string> modifiedList = new List<string>();

            foreach (int index in played)
            {
                modifiedList.Add(((char)myChar).ToString() + index.ToString());
                myChar++;
            }

            return modifiedList;
        }

        static public List<int> RetrieveOrder(List<string> played)
        {
            List<int> returnedList = new List<int>();
            
            foreach (string index in played.OrderBy(x => x).ToList())
            {
                Regex myRegex = new Regex(@"[^\d]");

                string match = myRegex.Replace(index, "");

                returnedList.Add(Int16.Parse(match));
            }

            return returnedList;
        }

        static public int FindLastPlayer(Cards cards)
        {
            cards.Played.Reverse();

            foreach (int card in cards.Played)
            {
                if (card <= 5)
                    return 1;
                if (card >= 6 && card <= 11)
                    return 2;
            }

            throw new IndexOutOfRangeException();
        }

        static public bool LastCard(Cards cards, int player)
        {
            int score = FindScore(cards);
            int startingCard = 0;

            if (player == 1)
                startingCard = 0;
            else if (player == 2)
                startingCard = 6;

            for (int i = startingCard; i < startingCard + 6; i++)
            {
                if (cards.Played.IndexOf(i) == -1)
                {
                    int cardCheck = StripSuit(cards.Hand[i]);
                    if (cardCheck > 10)
                        cardCheck = 10;

                    if (score + cardCheck < 31)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        static public int FindScore(Cards cards)
        {
            int score = 0;
            
            foreach (int cardIndex in cards.Played)
            {
                int strippedCard = Compute.StripSuit(cards.Hand[cardIndex]);

                if (score + strippedCard > 31)
                {
                    score = strippedCard;
                }
                else
                {
                    score += strippedCard;
                }
            }

            return score;
        }

        static public PointBreakdown ComputePoints(Cards cards)
        {
            PointBreakdown results = new PointBreakdown();

            int totalPlayed = cards.Played.Count();
            int lastPlayer = FindLastPlayer(cards);
            int score = FindScore(cards);

            int lastCard = 0;
            int repeat = 1;

            cards.Played.Reverse();

            foreach (int index in cards.Played)
            {


                int card = Compute.StripSuit(cards.Hand[index]);

                if (card > 10)
                {
                    score -= 10;
                }
                else
                {
                    score -= card;
                }

                if (score < 0)
                {
                    break;
                }

                if ((card == lastCard && repeat != 0))
                {
                    repeat++;
                }
                else if (repeat > 1)
                {
                    switch (repeat)
                    {
                        case 2:
                            {
                                results.Points += 2;
                                results.Breakdown.Add("Player " + lastPlayer.ToString() + " scored a double for 2");
                                break;
                            }
                        case 3:
                            {
                                results.Points += 6;
                                results.Breakdown.Add("Player " + lastPlayer.ToString() + " scored a triple for 6");
                                break;
                            }
                        case 4:
                            {
                                results.Points += 12;
                                results.Breakdown.Add("Player " + lastPlayer.ToString() + " scored a quadruple for 12");
                                break;
                            }
                        default:
                            break;
                    }

                    repeat = 0;
                }

                lastCard = card;
            }

            if (FindScore(cards) == 15)
            {
                results.Points += 2;
                results.Breakdown.Add("Player " + lastPlayer.ToString() + " scored 15 for 2");
            }
            else if (FindScore(cards) == 31)
            {
                results.Points += 2;
                results.Breakdown.Add("Player " + lastPlayer.ToString() + " scored 31 for 2");
            }
            else if (LastCard(cards, 1) && LastCard(cards, 2))
            {
                results.Points++;
                results.Breakdown.Add("Player " + lastPlayer.ToString() + " scored 1 for last card");
            }

            return results;
        }

        static public bool AllDone(Cards cards, int player)
        {
            int startCard = 0;

            if (player == 1)
                startCard = 0;
            else if (player == 2)
                startCard = 6;
            else
                throw new ArgumentOutOfRangeException();

            for (int i = startCard; i < startCard + 6; i++)
            {
                if (cards.Played.IndexOf(i) == -1)
                {
                    return false;
                }
            }

            return true;
        }

    }
}