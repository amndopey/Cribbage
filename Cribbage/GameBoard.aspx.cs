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
        protected void Page_Load(object sender, EventArgs e)
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

                Cribbage_Board.Controls.Add(RenderBoard.UpdateBoard(boardStatus));
            }
            else
            {
                Cribbage_Board.Controls.Add(RenderBoard.UpdateBoard(boardStatus));
            }

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

                for (int i = 6; i <= 12; i++)
                {
                    compHand.Add(allCards[i]);
                }

                List<int> bestHand = Compute.FindBestHand(compHand);

                for (int i = 6; i < 12; i++)
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

                //Add to counter
                int cardValue = Compute.StripSuit(hand[Int32.Parse(e.CommandName) - 1]);
                if (cardValue > 10)
                    cardValue = 10;
                CounterLabel.Text = (Int32.Parse(CounterLabel.Text) + cardValue).ToString();

                ComputePoints(1);

                if (CribGoDiv.Visible == false)
                    ScriptManager.RegisterStartupScript(this, GetType(), "Reload", "myVar = setInterval('ComputerTurn()', 3000)", true);
            }


            Session["Crib"] = crib;
        }

        private void ComputerPlay()
        {
            //Get computer hand
            List<int> hand = (List<int>)Session["Hand"];
            List<int> crib = (List<int>)Session["Crib"];
            List<int> compHand = new List<int>();

            for (int i = 6; i <= 12; i++)
            {
                dynamic control = this.FindControl("PlayerCard" + (i + 1).ToString());

                if ((!crib.Contains(i)) && control.ImageUrl == "images/Card_Backs/b1fv.png" && control.Visible == true)
                    compHand.Add(i);
            }

            bool cribGo = true;
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
                    cribGo = false;
                }

                if (points == 31)
                {
                    cardToPlay = index;
                    cribGo = false;
                    highestCount = 31;
                }
                else if (points < 31)
                {
                    if (points > highestCount)
                    {
                        cardToPlay = index;
                        cribGo = false;
                        highestCount = points;
                    }
                }
            }
         
            if (cribGo)
            {
                CribGoDiv.Visible = true;
                return;
            }
            else
            {
                dynamic control = this.FindControl("PlayerCard" + (cardToPlay + 1).ToString());
                control.ImageUrl = "images/Cards/" + hand[cardToPlay].ToString() + ".png";
                control.Enabled = false;
                control.DataBind();

                int cardValue = Compute.StripSuit(hand[cardToPlay]);
                if (cardValue > 10)
                    cardValue = 10;
                CounterLabel.Text = (Int32.Parse(CounterLabel.Text) + cardValue).ToString();

                ComputePoints(2);
            }
        }

        protected void ReloadButton_Click(object sender, EventArgs e)
        {
            ComputerPlay();
        }

        protected void ComputePoints(int player)
        {
            if (Int32.Parse(CounterLabel.Text) == 15)
                Session["BoardStatus"] = Compute.AddPointsToBoard((BoardStatus)Session["BoardStatus"], player, 2);
            if (Int32.Parse(CounterLabel.Text) == 31)
                Session["BoardStatus"] = Compute.AddPointsToBoard((BoardStatus)Session["BoardStatus"], player, 2);
        }
    }
}