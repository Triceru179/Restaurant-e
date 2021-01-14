using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

public partial class Pages_gerente_Cardapio : System.Web.UI.Page
{
    protected bool adicionar, editar, remover;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["permissao"] == null) Response.Redirect("/");
        if ((Convert.ToInt32(Session["permissao"]) & Permissoes.founder) == 0)
            if ((Convert.ToInt32(Session["permissao"]) & Permissoes.gerente) == 0) Response.Redirect("/Erro.aspx");

        editar = false;
        remover = false;
        if (!IsPostBack)
        {
            atualizarPagina();
        }
    }

    void atualizarPagina()
    {
        /* Busca todos os funcionários do banco de dados */
        DataSet ds = ProdutoDB.selectProduto();
        
        /* Preenche a tabela com todos os produtos do banco */
        rptProduto.DataSource = ds;
        rptProduto.DataBind();
    }

    protected void btn_adicionar(object sender, EventArgs e)
    {
        adicionar = true;

        txtDescricaoAdicionar.Text = "";
        txtNomeAdicionar.Text = "";
        txtValorAdicionar.Text = "";
        ckbAdicionarComplemento.Checked = false;
        ckbAdicionarDisponivel.Checked = false;
    }

    protected void btnAdicionar_Click(object sender, EventArgs e)
    {
        if(txtNomeAdicionar.Text.Length > 254 || txtDescricaoAdicionar.Text.Length > 254)
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "Os campos 'Nome' e 'Descrição' só aceitam até 254 dígitos";
            return;
        }

        /* Valida se todos os campos estão preenchidos */
        if (txtNomeAdicionar.Text.Equals("") || txtDescricaoAdicionar.Text.Equals("") || txtValorAdicionar.Text.Equals(""))
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "Os campos 'Nome', 'Descrição' e 'Valor' devem ser preenchidos";
            return;
        }

        Decimal d;
        if (!decimal.TryParse(txtValorAdicionar.Text, out d))
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "O campo 'Valor' só aceita números";
            return;
        }

        if (Convert.ToDouble(txtValorAdicionar.Text) > 9999)
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "O campo 'Valor' deve ter um valor até R$ 9999,00";
            return;
        }


        Produto pro = new Produto();

        /* Armazena as informações a serem adicionadas no sistema */
        pro.Pro_nome = txtNomeAdicionar.Text;
        pro.Pro_descricao = txtDescricaoAdicionar.Text;
        pro.Pro_valor = Convert.ToDouble(txtValorAdicionar.Text);
        pro.Pro_complemento = ckbAdicionarComplemento.Checked == true ? 1 : 0;
        pro.Pro_disponivel = ckbAdicionarDisponivel.Checked == true ? 1 : 0;
        
        res.Text = "";

        /* Registra o funcionário no sistema */
        ProdutoDB.inserirProduto(pro);
        res.Attributes.Clear();
        res.Attributes.Add("class", "btn btn-block text-success");
        res.Text = "Produto registrado com sucesso";

        /* Atualiza a tabela */
        atualizarPagina();
    }

    protected void btn_editar(object sender, EventArgs e)
    {
        editar = true;

        /* Busca qual foi o botão pressionado */
        Button btn = (Button)sender;

        /* Busca o o nome da origem do botão que foi pressionado */
        RepeaterItem item = (RepeaterItem)btn.NamingContainer;

        /* Atualiza os campos com as informações do produto a ser editado*/
        txtNomeEditar.Text = (item.FindControl("lblNome") as Label).Text;
        txtDescricaoEditar.Text = (item.FindControl("lblDescricao") as Label).Text;
        txtValorEditar.Text = Convert.ToDouble((item.FindControl("hidValor") as HiddenField).Value) + "";


        hidIdEditar.Value = (item.FindControl("hidid") as HiddenField).Value + "";
        
        ckbEditarComplemento.Checked = ((item.FindControl("hidComplemento") as HiddenField).Value).ToString().Equals("True");
        ckbEditarDisponivel.Checked =  ((item.FindControl("hidDisponivel")  as HiddenField).Value).ToString().Equals("True");
    }
    
    protected void btnConfirmarEditar_Click(object sender, EventArgs e)
    {
        if (txtNomeEditar.Text.Length > 254 || txtDescricaoEditar.Text.Length > 254)
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "Os campos 'Nome' e 'Descrição' só aceitam até 254 dígitos";
            return;
        }

        /* Valida se os campos NOME, DESCRICAO e VALOR foram preenchidos */
        if (txtNomeEditar.Text.Equals("") || txtDescricaoEditar.Text.Equals("") || txtValorEditar.Text.Equals(""))
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "Os campos 'Nome', 'Descrição' e 'Valor' a serem editados devem ser preenchidos";
            return;
        }

        Decimal d;
        if (!decimal.TryParse(txtValorEditar.Text, out d))
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "O campo 'Valor' só aceita números";
            return;
        }

        if (Convert.ToDouble(txtValorEditar.Text) > 9999)
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "O campo 'Valor' deve ter um valor até R$ 9999,00";
            return;
        }


        Produto pro = new Produto();
        pro.Pro_id = Convert.ToInt32(hidIdEditar.Value);
        pro.Pro_nome = txtNomeEditar.Text;
        pro.Pro_valor = Convert.ToDouble(txtValorEditar.Text);
        pro.Pro_descricao = txtDescricaoEditar.Text;
        pro.Pro_complemento = ckbEditarComplemento.Checked == true ? 1 : 0;
        pro.Pro_disponivel = ckbEditarDisponivel.Checked == true ? 1 : 0;

        res.Text = "";

        /* Atualiza o produto no sistema */
        ProdutoDB.updateProduto(pro);
        res.Attributes.Clear();
        res.Attributes.Add("class", "btn btn-block text-success");
        res.Text = "Produto atualizado com sucesso";

        /* Atualiza a tabela */
        atualizarPagina();
    }

    protected void btn_excluir(object sender, EventArgs e)
    {
        remover = true;

        /* Busca qual foi o botão pressionado */
        Button btn = (Button)sender;

        /* Busca o o nome da origem do botão que foi pressionado */
        RepeaterItem item = (RepeaterItem)btn.NamingContainer;

        /* Atualiza os campos com as informações do produto a ser removido*/
        lblNomeRemover.Text = (item.FindControl("lblNome") as Label).Text;
        hidIdRemover.Value = Convert.ToInt32((item.FindControl("hidid") as HiddenField).Value) + "";
    }

    protected void btnRemover_Click(object sender, EventArgs e)
    {
        Produto pro = new Produto();
        pro.Pro_id = Convert.ToInt32(hidIdRemover.Value);
        ProdutoDB.removerProduto(pro);

        atualizarPagina();

        res.Attributes.Clear();
        res.Attributes.Add("class", "btn btn-block text-success");
        res.Text = "Produto removido com sucesso";
    }
}