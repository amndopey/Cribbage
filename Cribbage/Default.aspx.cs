using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cribbage.Computation;

namespace Cribbage
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //List<int> myList = PointCounter.DealHand();
            List<int> myList = new List<int> { 11, 13, 12, 5, 10, 5 };

            foreach (int card in myList)
                Label1.Text = Label1.Text + " " + card.ToString();

            List<int> bestHand = Compute.FindBestHand(myList);

            foreach (int card in bestHand)
                Label2.Text = Label2.Text + " " + card.ToString();

            Label3.Text = Compute.CountPoints(bestHand).ToString();
        }

    }
}