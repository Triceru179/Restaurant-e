using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_admin_GerenciarReclamacao : System.Web.UI.Page
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
        /* Busca todas as reclamações do banco de dados */
        DataSet ds = AdminDB.selectReclamacao();

        /* Preenche os cards com todas as reclamações do banco */
        rptReclamacao.DataSource = ds;
        rptReclamacao.DataBind();

        if (ds.Tables[0].Rows.Count > 0)
            divNenhumaReclamacao.Visible = false;

        ds = AdminDB.selectQuantidadeReclamacao();
        lblQntReclamacao.Text = ds.Tables[0].Rows[0]["COUNT(rec_id)"] + "";
    }
}