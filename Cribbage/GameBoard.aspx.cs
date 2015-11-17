using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cribbage.Computation;
using System.Drawing;
using System.IO;

namespace Cribbage
{
    public partial class GameBoard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Random rng = new Random();

            string card1 = "images/Cards/" + rng.Next(1, 13).ToString() + Compute.GetSuit(rng.Next(1, 4)) + ".png";
            string card2 = "images/Cards/" + rng.Next(1, 13).ToString() + Compute.GetSuit(rng.Next(1, 4)) + ".png";
            string card3 = "images/Cards/" + rng.Next(1, 13).ToString() + Compute.GetSuit(rng.Next(1, 4)) + ".png";
            string card4 = "images/Cards/" + rng.Next(1, 13).ToString() + Compute.GetSuit(rng.Next(1, 4)) + ".png";
            string card5 = "images/Cards/" + rng.Next(1, 13).ToString() + Compute.GetSuit(rng.Next(1, 4)) + ".png";
            string card6 = "images/Cards/" + rng.Next(1, 13).ToString() + Compute.GetSuit(rng.Next(1, 4)) + ".png";


            ImageButton img = new ImageButton();
            img.ImageUrl = card1;
            this.PlayerCard1.Controls.Add(img);

            ImageButton img2 = new ImageButton();
            img2.ImageUrl = card2;
            this.PlayerCard2.Controls.Add(img2);

            ImageButton img3 = new ImageButton();
            img3.ImageUrl = card3;
            this.PlayerCard3.Controls.Add(img3);

            ImageButton img4 = new ImageButton();
            img4.ImageUrl = card4;
            this.PlayerCard4.Controls.Add(img4);

            ImageButton img5 = new ImageButton();
            img5.ImageUrl = card5;
            this.PlayerCard5.Controls.Add(img5);

            ImageButton img6 = new ImageButton();
            img6.ImageUrl = card6;
            this.PlayerCard6.Controls.Add(img6);

            Bitmap bmp = new Bitmap(Server.MapPath(@"images/cribbage_board.jpg"));
            Graphics g = Graphics.FromImage(bmp);
            g.FillEllipse(Brushes.Red, 36, 34, 10, 10);
            Session.Add("Board", bmp);

            System.Web.UI.WebControls.Image board = new System.Web.UI.WebControls.Image();
            using (MemoryStream ms = new MemoryStream())
            {

                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                byte[] byteImage = ms.ToArray();



                Convert.ToBase64String(byteImage);

                board.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);

            }

            Cribbage_Board.Controls.Add(board);
        }
    }
}