﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cribbage.Classes;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace Cribbage
{
    public partial class GameBoard : System.Web.UI.Page
    {
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            BoardStatus boardStatus = (BoardStatus)Session["BoardStatus"];
            if (boardStatus == null)
            {
                boardStatus = new BoardStatus();
                boardStatus.P1Color = Brushes.Red;
                boardStatus.P1FirstPeg = 0;
                boardStatus.P1SecondPeg = 1;
                boardStatus.P2Color = Brushes.Blue;
                boardStatus.P2FirstPeg = 0;
                boardStatus.P2SecondPeg = 1;

                Session["BoardStatus"] = boardStatus;
                Session["PlayerCrib"] = 1;

                Cribbage_Board.Controls.Add(RenderBoard.UpdateBoard(boardStatus));
            }
            else
            {
                Cribbage_Board.Controls.Add(RenderBoard.UpdateBoard(boardStatus));
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {

            ScriptManager.GetCurrent(Page).RegisterPostBackControl(ReloadButton);                
        }

        protected void DealButton_Click(object sender, EventArgs e)
        {
            DealButtonDiv.Visible = false;

            Session["Hand"] = Compute.DealHand();

            List<int> hand = (List<int>)Session["Hand"];

            if (hand == null)
                throw new ArgumentOutOfRangeException();

            foreach (int card in hand)
            {
                string fileString = String.Empty;
                int index = hand.ToList().IndexOf(card) + 1;
                if (index < 7)
                {
                    fileString = "images/Cards/" + card.ToString() + ".png";
                }
                else
                {
                    fileString = "images/Card_Backs/b1fv.png";
                }

                dynamic control = this.FindControl("PlayerCard" + index.ToString());
                //control.ToolTip = card.ToString();
                control.ImageUrl = fileString;
                if (index < 7)
                    control.Enabled = true;
                control.DataBind();
            }
        }

        protected void CardClick(object sender, CommandEventArgs e)
        {
            List<int> crib = (List<int>)Session["Crib"] ?? new List<int>();
            List<int> hand = (List<int>)Session["Hand"];

            if (crib.Count() < 1)
            {
                crib.Add(Int32.Parse(e.CommandName) - 1);
                dynamic control = this.FindControl("PlayerCard" + e.CommandName);
                control.ImageUrl = null;
                control.DataBind();

                CribCard1.ImageUrl = "images/Card_Backs/b2fv.png";
                CribCard1.DataBind();
            }
            else if (crib.Count() < 4)
            {
                List<int> allCards = (List<int>)Session["Hand"];
                List<int> compHand = new List<int>();

                crib.Add(Int32.Parse(e.CommandName) - 1);
                dynamic control = this.FindControl("PlayerCard" + e.CommandName);
                control.ImageUrl = null;
                control.DataBind();

                CribCard2.ImageUrl = "images/Card_Backs/b2pr.png";
                CribCard2.DataBind();

                for (int i = 6; i <= 11; i++)
                {
                    compHand.Add(allCards[i]);
                }

                List<int> bestHand = Compute.FindBestHand(compHand);

                for (int i = 6; i <= 11; i++)
                {
                    if (!bestHand.Contains(allCards[i]))
                    {
                        crib.Add(i);
                        dynamic control2 = this.FindControl("PlayerCard" + (i + 1).ToString());
                        control2.ImageUrl = null;
                        control2.DataBind();

                        if (crib.Count() == 3)
                        {
                            CribCard3.ImageUrl = "images/Card_Backs/b1pr.png";
                            CribCard3.DataBind();
                        }
                        if (crib.Count() == 4)
                        {
                            CribCard4.ImageUrl = "images/Card_Backs/b1pr.png";
                            CribCard4.DataBind();
                        }
                    }
                }

                //Show extra card
                this.PlayerCard13.ImageUrl = "images/cards/" + hand[12].ToString() + ".png";

                //Show counter
                this.CounterDiv.Visible = true;
                this.CounterLabel.Text = "0";

            }
            else
            {
                //Shade played card and disable the link
                dynamic control = this.FindControl("PlayerCard" + e.CommandName);
                control.ImageUrl = "images/Cards/" + hand[Int32.Parse(e.CommandName) - 1].ToString() + ".png";
                control.CssClass = "ShadedCards";
                control.Enabled = false;
                control.DataBind();

                //Get stripped card
                int cardValue = Compute.StripSuit(hand[Int32.Parse(e.CommandName) - 1]);

                //Pull played cards and add to list
                List<int> played = Compute.RetrieveOrder((List<string>)Session["Played"] ?? new List<string>());
                played.Add(Int32.Parse(e.CommandName) - 1);
                List<string> storeIt = Compute.StoreOrder(played);

                Session["Played"] = storeIt;

                //Add to counter
                if (cardValue > 10)
                    cardValue = 10;
                CounterLabel.Text = (Int32.Parse(CounterLabel.Text) + cardValue).ToString();

                ComputePoints(FindLastPlayer(played));

                if (LastCard(2) && !LastCard(1))
                {
                    return;
                }

                if (CounterLabel.Text != "0")
                    ScriptManager.RegisterStartupScript(this, GetType(), "Reload", "myVar = setInterval('ComputerTurn()', 3000)", true);

                if (AllDone(1))
                {
                    if (AllDone(2))
                        ScriptManager.RegisterStartupScript(this, GetType(), "Reload", "myVar = setInterval('FinalCount()', 3000)", true);
                    else
                        ScriptManager.RegisterStartupScript(this, GetType(), "Reload", "myVar = setInterval('ComputerTurn()', 3000)", true);
                }
            }


            Session["Crib"] = crib;
        }

        private List<int> GetComputerHand(List<int> hand)
        {
            List<int> crib = (List<int>)Session["Crib"];
            List<int> compHand = new List<int>();
            
            for (int i = 6; i <= 11; i++)
            {
                dynamic control = this.FindControl("PlayerCard" + (i + 1).ToString());

                if ((!crib.Contains(i)) && control.ImageUrl == "images/Card_Backs/b1fv.png" && control.Visible == true)
                    compHand.Add(i);
            }

            return compHand;
        }

        private int FindLastPlayer(List<int> played)
        {
            foreach (int card in played)
            {
                if (card <= 5)
                    return 1;
                if (card >= 6 && card <= 11)
                    return 2;
            }

            throw new IndexOutOfRangeException();
        }

        private void ComputerPlay()
        {
            //Get computer hand
            List<int> hand = (List<int>)Session["Hand"];
            List<int> crib = (List<int>)Session["Crib"];
            List<int> played = Compute.RetrieveOrder((List<string>)Session["Played"] ?? new List<string>());

            List<int> compHand = GetComputerHand(hand);

            //if (LastCard(2))
            //{
            //    ComputePoints(FindLastPlayer(played));

            //    CounterLabel.Text = "0";

            //    if (FindLastPlayer(played) == 2)
            //        ScriptManager.RegisterStartupScript(this, GetType(), "Reload", "myVar = setInterval('ComputerTurn()', 3000)", true);

            //    return;
            //}

            int cardToPlay = 0;
            int highestCount = 0;

            foreach (int index in compHand)
            {
                int card = Compute.StripSuit(hand[index]);
                if (card > 10)
                    card = 10;
                int points = Int32.Parse(CounterLabel.Text) + card;

                if (points == 15)
                {
                    cardToPlay = index;
                }

                if (points == 31)
                {
                    cardToPlay = index;
                    highestCount = 31;
                }
                else if (points < 31)
                {
                    if (points > highestCount)
                    {
                        cardToPlay = index;
                        highestCount = points;
                    }
                }
            }
         
            if (cardToPlay == 0)
            {
                if (AllDone(1) && AllDone(2))
                    ScriptManager.RegisterStartupScript(this, GetType(), "Reload", "myVar = setInterval('FinalCount()', 3000)", true);

                if (LastCard(1))
                {
                    int lastPlayed = FindLastPlayer(played);
                    ComputePoints(lastPlayed);
                    CounterLabel.Text = "0";
                    if (lastPlayed == 2)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Reload", "myVar = setInterval('ComputerTurn()', 3000)", true);
                    }
                }
                else
                {
                    CribGoDiv.Visible = true;
                }
                return;
            }
            else
            {
                dynamic control = this.FindControl("PlayerCard" + (cardToPlay + 1).ToString());
                control.ImageUrl = "images/Cards/" + hand[cardToPlay].ToString() + ".png";
                control.Enabled = false;
                control.DataBind();

                //Add card to played array
                played.Add(cardToPlay);
                List<string> storeIt = Compute.StoreOrder(played);
                Session["Played"] = storeIt;

                //Strip suit and tally counter
                int cardValue = Compute.StripSuit(hand[cardToPlay]);
                if (cardValue > 10)
                    cardValue = 10;
                CounterLabel.Text = (Int32.Parse(CounterLabel.Text) + cardValue).ToString();

                if (LastCard(1) && !AllDone(1))
                    ScriptManager.RegisterStartupScript(this, GetType(), "Reload", "myVar = setInterval('ComputerTurn()', 3000)", true);
                
                ComputePoints(FindLastPlayer(played));
                
                if (AllDone(1))
                {
                    if (AllDone(2))
                        ScriptManager.RegisterStartupScript(this, GetType(), "Reload", "myVar = setInterval('FinalCount()', 3000)", true);
                    else
                    {
                        //LastCardDiv.Visible = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "Reload", "myVar = setInterval('ComputerTurn()', 3000)", true);
                    }
                }
            }


        }

        protected void ReloadButton_Click(object sender, EventArgs e)
        {
            ComputerPlay();
        }

        //protected void ComputerLastCardButton_Click(object sender, EventArgs e)
        //{
        //    ComputerLastCard();
        //}

        protected void ComputePoints(int player)
        {
            List<int> hand = (List<int>)Session["Hand"];
            List<int> played = Compute.RetrieveOrder((List<string>)Session["Played"] ?? new List<string>());
            
            int totalPlayed = played.Count();
            int points = 0;
            int score = Int16.Parse(CounterLabel.Text);

            int lastCard = 0;
            int repeat = 1;
            int sum = 0;

            //played.Reverse();
            //played = played.AsEnumerable().Reverse().ToList();

            foreach (int index in played)
            {
                int card = Compute.StripSuit(hand[index]);

                if (card > 10)
                {
                    sum += 10;
                    score -= 10;
                }
                else
                {
                    sum += card;
                    score -= card;
                }

                if (score < 0)
                {
                    sum -= card;
                    break;
                }

                if (card == lastCard && repeat != 0)
                {
                    repeat++;
                }
                else if (repeat > 1)
                {
                    switch (repeat)
                    {
                        case 2:
                            {
                                points = points + 2;
                                Scoreboard.Items.Add("Player " + player.ToString() + " scored a double for 2");
                                Scoreboard.DataBind();
                                break;
                            }
                        case 3:
                            {
                                points = points + 6;
                                Scoreboard.Items.Add("Player " + player.ToString() + " scored a triple for 6");
                                Scoreboard.DataBind();
                                break;
                            }
                        case 4:
                            {
                                points = points + 12;
                                Scoreboard.Items.Add("Player " + player.ToString() + " scored a quadruple for 12");
                                Scoreboard.DataBind();
                                break;
                            }
                        default:
                            break;
                    }

                    repeat = 0;
                }
                else if (lastCard != 0)
                {
                    repeat = 0;
                }

                lastCard = card;
            }

            if (sum == 15)
            {
                points = points + 2;
                Scoreboard.Items.Add("Player " + player.ToString() + " scored 15 for 2");
                Scoreboard.DataBind();
            }
                
            else if (sum == 31)
            {
                points = points + 2;
                CounterLabel.Text = "0";
                CounterLabel.DataBind();
                CribGoDiv.Visible = false;
                Scoreboard.Items.Add("Player " + player.ToString() + " scored 31 for 2");
                Scoreboard.DataBind();
            }
            else if (LastCard(1) && LastCard(2))
            {
                points++;
                CribGoDiv.Visible = false;
                Scoreboard.Items.Add("Player " + player.ToString() + " scored 1 for last card");
                Scoreboard.DataBind();
                CounterLabel.Text = "0";
            }

            if (points > 0)
                Session["BoardStatus"] = Compute.AddPointsToBoard((BoardStatus)Session["BoardStatus"], player, points);
        }

        //protected void ComputerLastCard()
        //{
        //    List<int> hand = (List<int>)Session["Hand"];
        //    List<int> crib = (List<int>)Session["Crib"];
        //    List<int> played = (List<int>)Session["Played"];

        //    //Check for last card
        //    bool lastCard = true;
        //    for (int i = 6; i <= 11; i++)
        //    {
        //        dynamic control = this.FindControl("PlayerCard" + (i + 1).ToString());
        //        if (((!crib.Contains(i)) && control.ImageUrl == "images/Card_Backs/b1fv.png") && control.Visible == true)
        //        {
        //            if (Compute.StripSuit(hand[i]) + Int32.Parse(CounterLabel.Text) <= 31)
        //            {
        //                lastCard = false;
        //                ScriptManager.RegisterStartupScript(this, GetType(), "Reload", "myVar = setInterval('ComputerLastCard()', 3000)", true);
        //                break;
        //            }
        //        }
        //    }

        //    ComputePoints(FindLastPlayer(played));
        //}

        //protected void LastCardButton_Click(object sender, EventArgs e)
        //{
        //    List<int> played = (List<int>)Session["Played"];
        //    ComputePoints(FindLastPlayer(played));

        //    CribGoDiv.Visible = false;
        //    LastCardDiv.Visible = false;
        //}

        protected bool AllDone(int player)
        {
            bool allDone = true;
            if (player == 1)
            {
                for (int i = 0; i <= 5; i++)
                {
                    dynamic control2 = this.FindControl("PlayerCard" + (i + 1).ToString());
                    if (control2.CssClass != "ShadedCards" && !String.IsNullOrEmpty(control2.ImageUrl))
                    {
                        allDone = false;
                    }
                }
            }
            else if (player == 2)
            {
                for (int i = 6; i <= 11; i++)
                {
                    dynamic control2 = this.FindControl("PlayerCard" + (i + 1).ToString());
                    if (control2.ImageUrl == "images/Card_Backs/b1fv.png")
                    {
                        allDone = false;
                    }
                }
            }
            else
                throw new ArgumentOutOfRangeException();

            return allDone;
        }

        protected bool LastCard(int Player)
        {
            bool lastCard = true;
            List<int> hand = (List<int>)Session["Hand"];

            if (Player == 1)
            {
                for (int i = 0; i <= 5; i++)
                {
                    dynamic control = this.FindControl("PlayerCard" + (i + 1).ToString());
                    if (!String.IsNullOrEmpty(control.ImageUrl) && control.CssClass != "ShadedCards")
                    {
                        int card = Compute.StripSuit(hand[i]);
                        if (card > 10)
                            card = 10;
                        if (card + Int32.Parse(CounterLabel.Text) <= 31)
                        {
                            lastCard = false;
                            break;
                        }
                    }
                }
            }
            else if (Player == 2)
            {
                for (int i = 6; i <= 11; i++)
                {
                    dynamic control = this.FindControl("PlayerCard" + (i + 1).ToString());
                    if (control.Visible == true && control.ImageUrl != "images/Card_Backs/b1fv.png")
                    {
                        int card = Compute.StripSuit(hand[i]);
                        if (card > 10)
                            card = 10;
                        if (card + Int32.Parse(CounterLabel.Text) <= 31)
                        {
                            lastCard = false;
                            break;
                        }
                    }
                }
            }

            return lastCard;
        }

        protected void FinalCountButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
            
            //List<int> hand = (List<int>)Session["Hand"];
            //List<int> crib = (List<int>)Session["Crib"] ?? new List<int>();
            //int playerCrib = (int)Session["PlayerCrib"];

            //if (crib.Count() == 0)
            //{

            //}
        }
    }
}