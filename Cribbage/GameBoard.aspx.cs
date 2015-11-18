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
            
            if (!IsPostBack)
            {

            }
            else
            {
                if (Session["Crib"] == null)
                {
                    int[] hand = (int[])Session["Hand"];

                    //TODO: Verify hand comes back

                    foreach (int card in hand)
                    {
                        int index = hand.ToList().IndexOf(card);
                        if (index < 7)
                        {
                            string fileString = "images/Cards/" + card.ToString() + ".png";
                        }
                        else
                        {
                            string fileString = "images/Cards"
                        }
                        ImageButton img = new ImageButton();
                        img.ImageUrl = fileString;

                        var control = this.FindControl("PlayerCard" + index.ToString());
                        control.Controls.Add(img);
                    }
                }

            }

        }

        protected void DealButton_Click(object sender, EventArgs e)
        {
            DealButtonDiv.Visible = false;

            Session["Hand"] = Compute.DealHand();
        }
    }
}