using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClickCounter
{
    public partial class ClickCounter : System.Web.UI.Page
    {
        string username;
        protected void Page_Load(object sender, EventArgs e)
        {
            username = (string)Session["clickerName"];
        }

        protected void clickButton_Click(object sender, EventArgs e)
        {
            Application[username] = (int)Application[username] + 1;
            Application["totalClicks"] = (int)Application["totalClicks"] + 1;
        }
    }
}