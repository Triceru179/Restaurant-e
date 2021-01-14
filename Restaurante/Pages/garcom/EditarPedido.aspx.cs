using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Pages_garcom_EditarPedido : System.Web.UI.Page
{
    protected bool adicionar;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["permissao"] == null) Response.Redirect("/");
        if ((Convert.ToInt32(Session["permissao"]) & Permissoes.founder) == 0)
            if ((Convert.ToInt32(Session["permissao"]) & Permissoes.garcom) == 0) Response.Redirect("/Erro.aspx");
        if (Session["mesa"] == null) Response.Redirect("/Pages/garcom/Mesas.aspx");
        if (Session["comanda"] == null) Response.Redirect("/Pages/garcom/Mesas.aspx");
        if (Session["pedido"] == null) Response.Redirect("/Pages/garcom/Mesas.aspx");

        adicionar = false;

        Mesa mes = (Session["mesa"] as Mesa);
        string ident = Session["pedido_identi"].ToString();

        lblMesa.Text = mes.Mes_identificacao;
        lblPedido.Text = ident + "";

        if (!IsPostBack)
        {
            atualizarPagina();
        }
    }

    void atualizarPagina()
    {
        Comanda com = Session["comanda"] as Comanda;
        Pedidos ped = Session["pedido"] as Pedidos;
        ped.Ped_valor = 0;

        /* Busca todos os funcionários do banco de dados */
        DataSet ds = GarcomDB.selectItensNoPedido(com, ped);

        /* Preenche a tabela com todos os produtos do banco */
        rptProdutosNoPedido.DataSource = ds;
        rptProdutosNoPedido.DataBind();

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            ped.Ped_valor += Convert.ToDouble(dr["pnp_valor"]) * Convert.ToInt32(dr["pnp_quantidade"]);
        }

        txtValorTotal.Text = String.Format("R${0:0.00}", ped.Ped_valor);



        /* Busca todos os funcionários do banco de dados */
        DataSet dss = ProdutoDB.selectProduto();

        /* Preenche a tabela com todos os produtos do banco */
        rptCardapio.DataSource = dss;
        rptCardapio.DataBind();
    }

    protected void btn_adicionar(object sender, EventArgs e)
    {
        adicionar = true;

        /* Busca qual foi o botão pressionado */
        LinkButton btn = (LinkButton)sender;

        /* Busca o o nome da origem do botão que foi pressionado */
        RepeaterItem item = (RepeaterItem)btn.NamingContainer;
        
        /* Atualiza os campos com as informações do produto a ser adicionado */
        txtPro_nome.Text = ((item.FindControl("lblpro_nome") as Label).Text).ToString();
        txtPro_valor.Text = ((item.FindControl("lblpro_valor") as Label).Text).ToString();
        hidProdutoIdmodalAdicionar.Value = (item.FindControl("hidIdProduto") as HiddenField).Value;
        hidProdutoValorAdicionar.Value = (item.FindControl("hidValorProduto") as HiddenField).Value;
    }

    protected void btnAdicionarProduto_Click(object sender, EventArgs e)
    {
        Decimal d;
        if (!decimal.TryParse(txtQuantidade.Text, out d))
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "O campo 'Quantidade' só aceita números";
            return;
        }

        if (Convert.ToInt32(txtQuantidade.Text) < 1)
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn text-danger btn-block");
            res.Text = "A quantidade mínima é 1";
            return;
        }

        if (Convert.ToInt32(txtQuantidade.Text) > 99)
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn text-danger btn-block");
            res.Text = "A quantidade máxima é 99";
            return;
        }

        if(txtObservacao.Text.Length > 254)
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn text-danger btn-block");
            res.Text = "O campo 'Observação' só aceita até 254 dígitos";
            return;
        }

        Pedidos ped = Session["pedido"] as Pedidos;
        Produto pro = new Produto();
        ProdutosNoPedido pnp = new ProdutosNoPedido();

        HiddenField hf = hidProdutoIdmodalAdicionar as HiddenField;

        pro.Pro_id = Convert.ToInt32(hf.Value);
        pnp.Pnp_quantidade = Convert.ToInt32(txtQuantidade.Text);
        pnp.Pnp_valor = Convert.ToDouble(hidProdutoValorAdicionar.Value);
        pnp.Pnp_observacao = txtObservacao.Text;
        pnp.Pnp_dthrCozinha = DateTime.Now;

        GarcomDB.insertProdutoNoPedido(pnp, ped, pro);

        atualizarPagina();

        res.Attributes.Clear();
        res.Attributes.Add("class", "btn text-success btn-block");
        res.Text = "Produto adicionado com sucesso";
    }

    protected void btnRemoverProduto_Click(object sender, EventArgs e)
    {
        /* Busca qual foi o botão pressionado */
        LinkButton btn = (LinkButton)sender;

        /* Busca o o nome da origem do botão que foi pressionado */
        RepeaterItem item = (RepeaterItem)btn.NamingContainer;

        ProdutosNoPedido pnp = new ProdutosNoPedido();
        HiddenField hf =  item.FindControl("hidIdProdutosNoPedido") as HiddenField;
        pnp.Pnp_id = Convert.ToInt32(hf.Value);

        GarcomDB.removerProdutoNoPedido(pnp);

        atualizarPagina();

        res.Attributes.Clear();
        res.Attributes.Add("class", "btn text-success btn-block");
        res.Text = "Produto removido com sucesso";
    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("/pages/garcom/criarPedido.aspx");
    }
}