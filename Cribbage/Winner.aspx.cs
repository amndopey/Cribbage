using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cribbage
{
    public partial class Winner : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string player = Request.QueryString["player"];
            if (Int16.Parse(player) == 1)
            {
                WinnerLabel.Text = "You have won!";
            }
            else
            {
                WinnerLabel.Text = "The computer has won. :(";
            }
        }
    }
}