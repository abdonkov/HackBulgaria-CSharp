using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClickCounter
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void submitButton_Click(object sender, EventArgs e)
        {
            Session["clickerName"] = nameTextBox.Text;
            int numberOfClicks = Application[nameTextBox.Text] == null ? 0 : (int)Application[nameTextBox.Text];
            Application[nameTextBox.Text] = numberOfClicks;
            Response.Redirect("ClickCounter.aspx");
        }
    }
}