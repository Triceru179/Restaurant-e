using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Page : System.Web.UI.MasterPage
{
    public int perm = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Count == 0) Response.Redirect("/");
        
        perm = Convert.ToInt32(Session["permissao"]);
    }

    protected void btnSair_Click(object sender, EventArgs e)
    {
        Session.RemoveAll();
        Response.Redirect("/");
    }
}