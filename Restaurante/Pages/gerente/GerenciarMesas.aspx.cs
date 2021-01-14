using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

public partial class Pages_gerente_GerenciarMesas : System.Web.UI.Page
{
    protected bool adicionar, editar, remover;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["permissao"] == null) Response.Redirect("/");
        if ((Convert.ToInt32(Session["permissao"]) & Permissoes.founder) == 0)
            if ((Convert.ToInt32(Session["permissao"]) & Permissoes.gerente) == 0) Response.Redirect("/Erro.aspx");

        adicionar = false;
        editar = false;
        remover = false;
        
        if (!IsPostBack)
        {
            atualizarPagina();
        }
    }

    void atualizarPagina()
    {
        /* Busca todas as mesas do banco de dados */
        DataSet ds = GerenteDB.selectMesa();
        
        /* Preenche os cards com todas as mesas do banco */
        rptMesa.DataSource = ds;
        rptMesa.DataBind();

        res.Attributes.Clear();
        res.Text = "";

        if (ds.Tables[0].Rows.Count > 0)
            divNenhumaMesa.Visible = false;
    }

    protected void btn_adicionar(object sender, EventArgs e)
    {
        adicionar = true;

        txtIdentAdicionar.Text = "";
    }

    protected void btnAdicionar_Click(object sender, EventArgs e)
    {
        if (txtIdentAdicionar.Text.Length > 254)
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "O campos 'Identificação' só aceita até 254 dígitos";
            return;
        }

        /* Valida se todos os campos estão preenchidos */
        if (txtIdentAdicionar.Text.Equals(""))
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "O campo 'Identificação' deve ser preenchidos";
            return;
        }

        res.Text = "";

        /* Registra a mesa no sistema */
        Mesa mes = new Mesa();
        mes.Mes_identificacao = txtIdentAdicionar.Text;

        GerenteDB.insertMesa(mes);
        
        /* Atualiza a tabela */
        atualizarPagina();

        res.Attributes.Clear();
        res.Attributes.Add("class", "btn btn-block text-success");
        res.Text = "Mesa registrada com sucesso";
    }

    protected void btn_editar(object sender, EventArgs e)
    {
        editar = true;

        /* Busca qual foi o botão pressionado */
        LinkButton btn = (LinkButton)sender;

        /* Busca o o nome da origem do botão que foi pressionado */
        RepeaterItem item = (RepeaterItem)btn.NamingContainer;

        /* Atualiza os campos com as informações do produto a ser editado*/
        txtIdentEditar.Text = (item.FindControl("lblIdentificacao") as Label).Text;
        hidIdEditar.Value = Convert.ToInt32((item.FindControl("hidid") as HiddenField).Value) + "";
    }

    protected void btnConfirmarEditar_Click(object sender, EventArgs e)
    {
        if (txtIdentEditar.Text.Length > 254)
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "O campos 'Identificação' só aceita até 254 dígitos";
            return;
        }

        /* Valida se os campos NOME, DESCRICAO e VALOR foram preenchidos */
        if (txtIdentEditar.Text.Equals(""))
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "O campo 'Identificação' a ser editado deve ser preenchido";
            return;
        }

        Mesa mes = new Mesa();
        mes.Mes_id = Convert.ToInt32(hidIdEditar.Value);
        mes.Mes_identificacao = txtIdentEditar.Text;

        res.Text = "";

        /* Atualiza o produto no sistema */
        GerenteDB.updateMesa(mes);

        /* Atualiza a tabela */
        atualizarPagina();

        res.Attributes.Clear();
        res.Attributes.Add("class", "btn btn-block text-success");
        res.Text = "Mesa atualizada com sucesso";
    }

    protected void btn_excluir(object sender, EventArgs e)
    {
        remover = true;

        /* Busca qual foi o botão pressionado */
        LinkButton btn = (LinkButton)sender;

        /* Busca o o nome da origem do botão que foi pressionado */
        RepeaterItem item = (RepeaterItem)btn.NamingContainer;

        /* Atualiza os campos com as informações da mesa a ser removido*/
        lblMesaRemover.Text = (item.FindControl("lblIdentificacao") as Label).Text;
        hidIdmodalRemover.Value = Convert.ToInt32((item.FindControl("hidid") as HiddenField).Value) + "";
    }

    protected void btnRemover_Click(object sender, EventArgs e)
    {
        Mesa mes = new Mesa();
        mes.Mes_id = Convert.ToInt32(hidIdmodalRemover.Value);
        GerenteDB.removerMesa(mes);

        atualizarPagina();

        res.Attributes.Clear();
        res.Attributes.Add("class", "btn btn-block text-success");
        res.Text = "Mesa removida com sucesso";
    }
}