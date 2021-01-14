using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Pages_garcom_VerProduto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["permissao"] == null) Response.Redirect("/");
        if ((Convert.ToInt32(Session["permissao"]) & Permissoes.founder) == 0)
            if ((Convert.ToInt32(Session["permissao"]) & Permissoes.garcom) == 0) Response.Redirect("/Erro.aspx");

        if (!IsPostBack)
        {
            atualizarPagina();
        }
    }

    void atualizarPagina()
    {
        /* Busca todos os produtos do banco de dados */
        DataSet ds = ProdutoDB.selectProduto();

        /* Preenche a tabela com todos os produtos do banco */
        rptProduto.DataSource = ds;
        rptProduto.DataBind();
    }
}