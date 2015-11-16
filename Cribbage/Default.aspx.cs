using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cribbage.PointCounter;

namespace Cribbage
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<int> myList = new List<int> { 9, 8, 7, 6 };



            Label1.Text = PointCounter.PointCounter.CountPoints(myList).ToString();
        }

    }
}