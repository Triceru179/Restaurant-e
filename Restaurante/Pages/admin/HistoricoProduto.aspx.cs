using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_admin_HistoricoProduto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["permissao"] == null) Response.Redirect("/");
        if ((Convert.ToInt32(Session["permissao"]) & Permissoes.founder) == 0)
            if ((Convert.ToInt32(Session["permissao"]) & Permissoes.admin) == 0) Response.Redirect("/Erro.aspx");

        if (!IsPostBack)
        {
            atualizarPagina();
        }
    }

    void atualizarPagina()
    {
        /* Busca todos os produtos do banco de dados */
        txtHistoricoAno.Text = DateTime.Now.Year + "";
        DataSet ds = AdminDB.selectHistoricoPnp(Convert.ToInt32(txtHistoricoAno.Text));

        /* Preenche a tabela com todos os produtos do banco */
        rptPnp.DataSource = ds;
        rptPnp.DataBind();

        /* Busca todos os funcionários do banco de dados */
        ds = AdminDB.selectHistoricoProduto();

        /* Preenche a tabela com todos os produtos do banco */
        rptProduto.DataSource = ds;
        rptProduto.DataBind();
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

        DataSet ds = AdminDB.selectHistoricoPnp(Convert.ToInt32(txtHistoricoAno.Text));

        rptPnp.DataSource = ds;
        rptPnp.DataBind();
    }
}