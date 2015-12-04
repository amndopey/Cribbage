using System;
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
                Session["PlayerCount"] = 2;

                Cribbage_Board.Controls.Add(RenderBoard.UpdateBoard(boardStatus));
            }
            else
            {
                if (boardStatus.P1FirstPeg == 122 || boardStatus.P1SecondPeg == 122)
                    Winner(1);
                else if (boardStatus.P2FirstPeg == 122 || boardStatus.P2SecondPeg == 122)
                    Winner(2);
                
                Cribbage_Board.Controls.Add(RenderBoard.UpdateBoard(boardStatus));
            }

            Scoreboard.SelectedIndex = Scoreboard.Items.Count - 1;
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //WhosTurnLabel.Text = "It's your turn";
            //ScriptManager.GetCurrent(Page).RegisterPostBackControl(ReloadButton);
        }

        protected void DealButton_Click(object sender, EventArgs e)
        {
            DealButtonDiv.Visible = false;
            WhosTurnDiv.Visible = true;
            Cards cards = Compute.DealHand();

            if (cards.Hand == null)
                throw new ArgumentOutOfRangeException();

            foreach (int card in cards.Hand)
            {
                string fileString = String.Empty;
                int index = cards.Hand.ToList().IndexOf(card) + 1;
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

                Session["Cards"] = cards;
            }
        }

        protected void CardClick(object sender, CommandEventArgs e)
        {
            Cards cards = (Cards)Session["Cards"];

            CribGoDiv.Visible = false;

            if (cards.Crib.Count() < 1)
            {
                cards.Crib.Add(Int32.Parse(e.CommandName) - 1);
                dynamic control = this.FindControl("PlayerCard" + e.CommandName);
                control.ImageUrl = null;
                control.DataBind();

                CribCard1.ImageUrl = "images/Card_Backs/b2fv.png";
                CribCard1.DataBind();
            }
            else if (cards.Crib.Count() < 4)
            {
                //List<int> allCards = (List<int>)Session["Hand"];
                List<int> compHand = new List<int>();

                cards.Crib.Add(Int32.Parse(e.CommandName) - 1);
                dynamic control = this.FindControl("PlayerCard" + e.CommandName);
                control.ImageUrl = null;
                control.DataBind();

                CribCard2.ImageUrl = "images/Card_Backs/b2pr.png";
                CribCard2.DataBind();

                for (int i = 6; i <= 11; i++)
                {
                    compHand.Add(cards.Hand[i]);
                }

                List<int> bestHand = Compute.FindBestHand(compHand);

                for (int i = 6; i <= 11; i++)
                {
                    if (!bestHand.Contains(cards.Hand[i]))
                    {
                        cards.Crib.Add(i);
                        dynamic control2 = this.FindControl("PlayerCard" + (i + 1).ToString());
                        control2.ImageUrl = null;
                        control2.DataBind();

                        if (cards.Crib.Count() == 3)
                        {
                            CribCard3.ImageUrl = "images/Card_Backs/b1pr.png";
                            CribCard3.DataBind();
                        }
                        if (cards.Crib.Count() == 4)
                        {
                            CribCard4.ImageUrl = "images/Card_Backs/b1pr.png";
                            CribCard4.DataBind();
                        }
                    }
                }

                //Show extra card
                this.PlayerCard13.ImageUrl = "images/cards/" + cards.Hand[12].ToString() + ".png";

                //Show counter
                this.CounterDiv.Visible = true;
                this.CounterLabel.Text = "0";

                if ((int)Session["PlayerCrib"] == 1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Reload", "myVar = setInterval('ComputerTurn()', 3000)", true);
                }
            }
            else
            {
                //Shade played card and disable the link
                dynamic control = this.FindControl("PlayerCard" + e.CommandName);
                control.ImageUrl = "images/Cards/" + cards.Hand[Int32.Parse(e.CommandName) - 1].ToString() + ".png";
                control.CssClass = "ShadedCards";
                control.Enabled = false;
                control.DataBind();

                //Get stripped card
                int cardValue = Compute.StripSuit(cards.Hand[Int32.Parse(e.CommandName) - 1]);

                //Pull played cards and add to list
                //List<int> played = Compute.RetrieveOrder((List<string>)Session["Played"] ?? new List<string>());
                //played.Add(Int32.Parse(e.CommandName) - 1);
                //List<string> storeIt = Compute.StoreOrder(played);

                //Session["Played"] = storeIt;

                cards.Played.Add(Int32.Parse(e.CommandName) - 1);

                //Add to counter
                if (cardValue > 10)
                    cardValue = 10;
                CounterLabel.Text = (Int32.Parse(CounterLabel.Text) + cardValue).ToString();

                PointBreakdown points = Compute.ComputePoints(cards);

                if (points.Points > 0)
                {
                    foreach (string result in points.Breakdown)
                    {
                        Scoreboard.Items.Add(result);
                    }

                    Session["BoardStatus"] = Compute.AddPointsToBoard((BoardStatus)Session["BoardStatus"], Compute.FindLastPlayer(cards), points.Points);
                }

                //if (LastCard(2) && !LastCard(1))
                //{
                //    return;
                //}

                if (CounterLabel.Text != "0")
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "Reload", "myVar = setInterval('ComputerTurn()', 3000)", true);
                }

                if (Compute.AllDone(cards, 1))
                {
                    if (Compute.AllDone(cards, 2))
                        ScriptManager.RegisterStartupScript(this, GetType(), "Reload", "myVar = setInterval('FinalCount()', 3000)", true);
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Reload", "myVar = setInterval('ComputerTurn()', 3000)", true);
                    }
                }
            }


            Session["Cards"] = cards;
        }

        private List<int> GetComputerHand(Cards cards)
        {
            //List<int> crib = (List<int>)Session["Crib"];
            //Cards cards = (Cards)Session["Cards"];
            List<int> compHand = new List<int>();
            
            for (int i = 6; i <= 11; i++)
            {
                dynamic control = this.FindControl("PlayerCard" + (i + 1).ToString());

                if ((!cards.Crib.Contains(i)) && control.ImageUrl == "images/Card_Backs/b1fv.png" && control.Visible == true)
                    compHand.Add(i);
            }

            return compHand;
        }

        private void ComputerPlay()
        {
            //Get computer hand
            //List<int> hand = (List<int>)Session["Hand"];
            //List<int> crib = (List<int>)Session["Crib"];
            //List<int> played = Compute.RetrieveOrder((List<string>)Session["Played"] ?? new List<string>());
            Cards cards = (Cards)Session["Cards"];

            List<int> compHand = GetComputerHand(cards);

            int cardToPlay = 0;
            int highestCount = 0;

            foreach (int index in compHand)
            {
                int card = Compute.StripSuit(cards.Hand[index]);
                if (card > 10)
                    card = 10;
                int points = Int32.Parse(CounterLabel.Text) + card;

                if (points == 15)
                {
                    cardToPlay = index;
                }

                else if (points == 31)
                {
                    cardToPlay = index;
                    highestCount = 31;
                }
                else if (cards.Played.Count() > 0 && card == cards.Played[0])
                {
                    cardToPlay = index;
                    highestCount = 30;
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
                if (Compute.AllDone(cards, 1) && Compute.AllDone(cards, 2))
                    ScriptManager.RegisterStartupScript(this, GetType(), "Reload", "myVar = setInterval('FinalCount()', 3000)", true);

                if (Compute.LastCard(cards, 1))
                {
                    PointBreakdown points = Compute.ComputePoints(cards);

                    if (points.Points > 0)
                    {
                        foreach (string result in points.Breakdown)
                        {
                            Scoreboard.Items.Add(result);
                        }

                        Session["BoardStatus"] = Compute.AddPointsToBoard((BoardStatus)Session["BoardStatus"], Compute.FindLastPlayer(cards), points.Points);
                    }

                    CounterLabel.Text = "0";
                    if (Compute.FindLastPlayer(cards) == 2)
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
                control.ImageUrl = "images/Cards/" + cards.Hand[cardToPlay].ToString() + ".png";
                control.Enabled = false;
                control.DataBind();

                //Add card to played array
                cards.Played.Add(cardToPlay);
                //List<string> storeIt = Compute.StoreOrder(played);
                //Session["Played"] = storeIt;

                //Strip suit and tally counter
                int cardValue = Compute.StripSuit(cards.Hand[cardToPlay]);
                if (cardValue > 10)
                    cardValue = 10;
                CounterLabel.Text = (Int32.Parse(CounterLabel.Text) + cardValue).ToString();

                if (Compute.LastCard(cards, 1) && !Compute.AllDone(cards, 1))
                    ScriptManager.RegisterStartupScript(this, GetType(), "Reload", "myVar = setInterval('ComputerTurn()', 3000)", true);

                PointBreakdown points = Compute.ComputePoints(cards);

                if (points.Points > 0)
                {
                    foreach (string result in points.Breakdown)
                    {
                        Scoreboard.Items.Add(result);
                    }

                    Session["BoardStatus"] = Compute.AddPointsToBoard((BoardStatus)Session["BoardStatus"], Compute.FindLastPlayer(cards), points.Points);
                }

                if (Compute.AllDone(cards, 1))
                {
                    if (Compute.AllDone(cards, 2))
                        ScriptManager.RegisterStartupScript(this, GetType(), "Reload", "myVar = setInterval('FinalCount()', 3000)", true);
                    else
                    {
                        //LastCardDiv.Visible = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "Reload", "myVar = setInterval('ComputerTurn()', 3000)", true);
                    }
                }
            }

            Session["Cards"] = cards;
        }

        protected void ReloadButton_Click(object sender, EventArgs e)
        {
            ComputerPlay();
        }

        //protected void ComputerLastCardButton_Click(object sender, EventArgs e)
        //{
        //    ComputerLastCard();
        //}

        protected void FinalCountButton_Click(object sender, EventArgs e)
        {
            CounterDiv.Visible = false;
            WhosTurnDiv.Visible = false;

            List<int> hand = (List<int>)Session["Hand"];
            List<int> crib = (List<int>)Session["Crib"] ?? new List<int>();
            int playerCrib = (int)Session["PlayerCrib"];
            int playerCount = (int)Session["PlayerCount"];
            int points = 0;
            int beginCount = 0;
            List<int> countHand = new List<int>();

            if (hand.Count() == 0)
            {
                for (int i = 0; i <= 12; i++)
                {
                    dynamic control = this.FindControl("PlayerCard" + (i + 1).ToString());
                    control.ImageUrl = null;
                    control.Enabled = false;
                }

                if (playerCrib == 1)
                    beginCount = 0;
                else
                    beginCount = 6;

                for (int i = beginCount; i < (beginCount + 5); i++)
                {
                    dynamic control = this.FindControl("PlayerCard" + (i + 1).ToString());
                    control.ImageUrl = "images/cards/" + crib[(i - beginCount)].ToString() + ".png";
                    control.CssClass = null;
                    countHand.Add(crib[i - beginCount]);
                }

                for (int i = beginCount; i < (beginCount + 4); i++)
                {
                    dynamic control2 = this.FindControl("CribCard" + ((i - beginCount) + 1).ToString());
                    control2.ImageUrl = null;
                }
            }
            else if (playerCount == 1)
            {
                beginCount = 0;
            }
            else if (playerCount == 2)
            {
                beginCount = 6;
            }
            else
                throw new IndexOutOfRangeException();

            if (hand.Count() != 0)
            {
                for (int i = beginCount; i < (beginCount + 6); i++)
                {
                    dynamic control = this.FindControl("PlayerCard" + (i + 1).ToString());
                    if (!String.IsNullOrEmpty(control.ImageUrl))
                    {
                        countHand.Add(hand[i]);
                    }
                }

                countHand.Add(hand[12]);

                if (countHand.Count() != 5)
                {
                    throw new ArgumentOutOfRangeException();
                }
            }

            

            points = Compute.CountPoints(countHand);

            if (hand.Count() != 0)
                Scoreboard.Items.Add("Player " + playerCount.ToString() + "'s hand scored " + points.ToString() + " points");
            else
                Scoreboard.Items.Add("Player " + playerCount.ToString() + "'s crib scored " + points.ToString() + " points");
            Scoreboard.DataBind();

            if (points > 0)
                Session["BoardStatus"] = Compute.AddPointsToBoard((BoardStatus)Session["BoardStatus"], playerCount, points);

            if (playerCount == playerCrib && hand.Count() != 0)
            {
                List<int> fullCrib = new List<int>();
                foreach (int index in crib)
                {
                    fullCrib.Add(hand[index]);
                }
                fullCrib.Add(hand[12]);
                Session["Hand"] = new List<int>();
                Session["Crib"] = fullCrib;
            }
            else if (playerCount == 1)
            {
                Session["PlayerCount"] = 2;
            }
            else if (playerCount == 2)
            {
                Session["PlayerCount"] = 1;
            }

            if (crib.Count() != 5)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Reload", "myVar = setInterval('FinalCount()', 5000)", true);
            }
            else
            {
                Session["crib"] = new List<int>();
                ScriptManager.RegisterStartupScript(this, GetType(), "Reload", "myVar = setInterval('ResetBoard()', 6000)", true);
                return;
            }

        }

        protected void ResetPlayArea(object sender, EventArgs e)
        {
            Scoreboard.Items.Add("------------------");
            Scoreboard.DataBind();

            int playerCrib = (int)Session["PlayerCrib"];


            if (playerCrib == 1)
            {
                Session["PlayerCrib"] = 2;
                Session["PlayerCount"] = 1;
            }
            else
            {
                Session["PlayerCrib"] = 1;
                Session["PlayerCount"] = 2;
            }

            //Clear out all cards
            for (int i = 0; i <= 12; i++)
            {
                dynamic control = this.FindControl("PlayerCard" + (i + 1).ToString());
                control.ImageUrl = null;
                control.CssClass = null;
                control.Enabled = false;
            }

            //Hide counter
            CounterDiv.Visible = false;

            //Enable deal button
            DealButtonDiv.Visible = true;
        }

        protected void Winner(int player)
        {
            Response.Redirect("Winner.aspx?Player=" + player.ToString());
        }
    }
}