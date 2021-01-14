using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

public partial class Pages_gerente_Reclamacao : System.Web.UI.Page
{
    protected bool adicionar, editar, excluir;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["permissao"] == null) Response.Redirect("/");
        if ((Convert.ToInt32(Session["permissao"]) & Permissoes.founder) == 0)
            if ((Convert.ToInt32(Session["permissao"]) & Permissoes.gerente) == 0) Response.Redirect("/Erro.aspx");

        adicionar = false;
        editar = false;
        excluir = false;

        if(!IsPostBack)
        {
            atualizarPagina();
        }
    }

    void atualizarPagina()
    {
        /* Busca todas as mesas do banco de dados */
        DataSet ds = GerenteDB.selectReclamacao();

        /* Preenche os cards com todas as mesas do banco */
        rptReclamacao.DataSource = ds;
        rptReclamacao.DataBind();

        res.Attributes.Clear();
        res.Text = "";

        if (ds.Tables[0].Rows.Count > 0)
            divNenhumaReclamacao.Visible = false;
    }

    protected void btn_adicionar(object sender, EventArgs e)
    {
        adicionar = true;

        txtCategoriaAdicionar.Text = "";
        txtDescricaoAdicionar.Text = "";
    }

    protected void btnAdicionar_Click(object sender, EventArgs e)
    {
        if(txtCategoriaAdicionar.Text.Length > 254 || txtDescricaoAdicionar.Text.Length > 254)
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "Os campos 'Categoria' e 'Descrição' só aceitam até 254 dígitos";
            return;
        }
        
        if(txtCategoriaAdicionar.Text.Equals("") || txtDescricaoAdicionar.Text.Equals(""))
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "Os campos 'Categoria' e 'Descrição' deve ser preenchidos";
            return;
        }
        
        Reclamacao rec = new Reclamacao();
        rec.Rec_categoria = txtCategoriaAdicionar.Text;
        rec.Rec_descricao = txtDescricaoAdicionar.Text;
        rec.Rec_dthrCriacao = DateTime.Now;
        rec.Fun_id = new Funcionario();
        rec.Fun_id.Fun_id = Convert.ToInt32(Session["fun_id"]);

        GerenteDB.insertReclamacao(rec);

        atualizarPagina();

        res.Attributes.Clear();
        res.Attributes.Add("class", "btn btn-block text-success");
        res.Text = "Reclamação registrada com sucesso";
    }

    protected void btn_editar(object sender, EventArgs e)
    {
        editar = true;

        LinkButton btn = (LinkButton)sender;
        RepeaterItem item = (RepeaterItem)btn.NamingContainer;

        hidIdEditar.Value = (item.FindControl("hidIdRec") as HiddenField).Value;
        txtCategoriaEditar.Text = (item.FindControl("lblCategoria") as Label).Text;
        txtDescricaoEditar.Text = (item.FindControl("lblDescricao") as Label).Text;
        hidEditarRecIdent.Text = (item.FindControl("hidRecIdent") as Label).Text;
    }

    protected void btnEditar_Click(object sender, EventArgs e)
    {
        if (txtCategoriaEditar.Text.Length > 254 || txtDescricaoEditar.Text.Length > 254)
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "Os campos 'Categoria' e 'Descrição' só aceitam até 254 dígitos";
            return;
        }

        if (txtCategoriaEditar.Text.Equals("") || txtDescricaoEditar.Text.Equals(""))
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "O campo 'Categoria' e 'Descrição' deve ser preenchidos";
            return;
        }

        Reclamacao rec = new Reclamacao();
        rec.Rec_categoria = txtCategoriaEditar.Text;
        rec.Rec_descricao = txtDescricaoEditar.Text;
        rec.Fun_id = new Funcionario();
        rec.Fun_id.Fun_id = Convert.ToInt32(Session["fun_id"]);
        rec.Rec_id = Convert.ToInt32(hidIdEditar.Value);

        GerenteDB.updateReclamacao(rec);

        atualizarPagina();

        res.Attributes.Clear();
        res.Attributes.Add("class", "btn btn-block text-success");
        res.Text = "Reclamação atualizada com sucesso";
    }

    protected void btn_excluir(object sender, EventArgs e)
    {
        excluir = true;

        LinkButton btn = (LinkButton)sender;
        RepeaterItem item = (RepeaterItem)btn.NamingContainer;

        hidIdExcluir.Value = (item.FindControl("hidIdRec") as HiddenField).Value;
        hidExcluirRecIdent.Text = (item.FindControl("hidRecIdent") as Label).Text;
    }
    
    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        Reclamacao rec = new Reclamacao();
        rec.Rec_id = Convert.ToInt32(hidIdExcluir.Value);

        GerenteDB.deleteReclamacao(rec);

        atualizarPagina();

        res.Attributes.Clear();
        res.Attributes.Add("class", "btn btn-block text-success");
        res.Text = "Reclamação excluída com sucesso";
    }
}