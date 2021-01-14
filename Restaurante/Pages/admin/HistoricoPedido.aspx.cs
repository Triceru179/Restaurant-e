using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_admin_HistoricoPedido : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["permissao"] == null) Response.Redirect("/");
        if ((Convert.ToInt32(Session["permissao"]) & Permissoes.founder) == 0)
            if ((Convert.ToInt32(Session["permissao"]) & Permissoes.admin) == 0) Response.Redirect("/Erro.aspx");

        if(!IsPostBack)
        {
            atualizarPagina();
        }
    }

    void atualizarPagina()
    {
        txtHistoricoAno.Text = DateTime.Now.Year + "";
        DataSet ds = AdminDB.selectHistoricoPedido(Convert.ToInt32(txtHistoricoAno.Text));

        rptPedido.DataSource = ds;
        rptPedido.DataBind();
    }

    protected void txtHistoricoAno_TextChanged(object sender, EventArgs e)
    {
        Decimal d;
        if (!decimal.TryParse(txtHistoricoAno.Text, out d))
        {
            txtHistoricoAno.Text = DateTime.Now.Year + "";
            return;
        }

        if (Convert.ToInt32(txtHistoricoAno.Text) < 1950)
        {
            txtHistoricoAno.Text = "1950";
            return;
        }

        DataSet ds = AdminDB.selectHistoricoPedido(Convert.ToInt32(txtHistoricoAno.Text));

        rptPedido.DataSource = ds;
        rptPedido.DataBind();
    }
}