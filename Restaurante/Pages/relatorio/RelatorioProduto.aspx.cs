using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Relatorio_RelatorioProduto : System.Web.UI.Page
{
    protected string[] mes = {
        "",
        "Jan",
        "Fev",
        "Mar",
        "Abr",
        "Mai",
        "Jun",
        "Jul",
        "Ago",
        "Set",
        "Out",
        "Nov",
        "Dez"
    };

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
        carregarPorProdutoQuantidade(Convert.ToInt32(txtHistoricoAno.Text));
        carregarPorProdutoValor(Convert.ToInt32(txtHistoricoAno.Text));

        DataSet ds = AdminDB.selectTodosProdutos(Convert.ToInt32(txtHistoricoAno.Text));
        rptProduto.DataSource = ds;
        rptProduto.DataBind();
    }

    void carregarPorProdutoQuantidade(int ano)
    {
        ltlPorProdutoQuantidade.Text = "<label class=\"w-100 text-center mt-3\"> Nenhum registro encontrado </label>";

        DataSet ds = AdminDB.selectTodosProdutos5Limit(ano);
        if (ds.Tables[0].Rows.Count == 0)
            return;
        DataSet ds2 = AdminDB.selectTodosProdutos5LimitOffset(ano);

        string chart = "";
        chart = "<canvas id=\"PorProdutoQuantidade\" width=\"100%\" height=\"400\" />";
        chart += "<script>";
        chart += "new Chart(document.getElementById('PorProdutoQuantidade'), { type: 'pie', data: {";
        chart += "labels: [";

        string label = "";
        for (int i = 0; i < ds.Tables[0].Rows.Count; ++i)
        {
            label += "'" + ds.Tables[0].Rows[i]["nome"] + "',";
        }

        if (ds2.Tables[0].Rows.Count > 0)
            label += "'Outros',";

        label = label.Substring(0, label.Length - 1);
        chart += label;
        chart += "]";
        chart += ", datasets: [";
        chart += "{ data: [";

        string quantidade = "";
        for (int i = 0; i < ds.Tables[0].Rows.Count; ++i)
        {
            quantidade += "'" + ds.Tables[0].Rows[i]["quantidade"] + "',";
        }

        int outros = 0;
        for (int i = 0; i < ds2.Tables[0].Rows.Count; ++i)
        {
            outros += Convert.ToInt32(ds2.Tables[0].Rows[i]["quantidade"]);
        }

        if (ds2.Tables[0].Rows.Count > 0)
            quantidade += "'" + outros.ToString() + "',";

        quantidade = quantidade.Substring(0, quantidade.Length - 1);

        chart += quantidade;



        chart += "], label: '# Quantidade de vendas', backgroundColor: ['#8F90B3', '#4D6DB3', '#CEDBE5', '#E9BD97', '#B39A8F'], borderColor: ['#8090B0', '#44619E', '#BCDDE3', '#E6A386', '#B08880'], borderWidth: 2},";
        chart += "]}, options: { responsive: true, maintainAspectRatio: false,  title: { display: true, text: 'Itens que mais venderam'} } ";
        chart += "});";
        chart += "</script>";
        ltlPorProdutoQuantidade.Text = chart;
    }

    void carregarPorProdutoValor(int ano)
    {
        ltlPorProdutoValor.Text = "<label class=\"w-100 text-center mt-3\"> Nenhum registro encontrado </label>";

        DataSet ds = AdminDB.selectTodosProdutosValor5Limit(ano);
        if (ds.Tables[0].Rows.Count == 0)
            return;

        DataSet ds2 = AdminDB.selectTodosProdutosValor5LimitOffset(ano);

        string chart = "";
        chart = "<canvas id=\"PorProdutoValor\" width=\"100%\" height=\"400\" />";
        chart += "<script>";
        chart += "new Chart(document.getElementById('PorProdutoValor'), { type: 'pie', data: {";
        chart += "labels: [";

        string label = "";
        for (int i = 0; i < ds.Tables[0].Rows.Count; ++i)
        {
            label += "'" + ds.Tables[0].Rows[i]["nome"] + "',";
        }

        if (ds2.Tables[0].Rows.Count > 0)
            label += "'Outros',";

        label = label.Substring(0, label.Length - 1);
        chart += label;
        chart += "]";
        chart += ", datasets: [";
        chart += "{ data: [";

        string valor = "";
        for (int i = 0; i < ds.Tables[0].Rows.Count; ++i)
        {
            valor += "'" + Convert.ToDouble(ds.Tables[0].Rows[i]["valor"]) + "',";
        }

        double outros = 0;
        for (int i = 0; i < ds2.Tables[0].Rows.Count; ++i)
        {
            outros += Convert.ToDouble(ds2.Tables[0].Rows[i]["valor"]);
        }

        if (ds2.Tables[0].Rows.Count > 0)
            valor += "'" + outros.ToString().Replace(",", ".") + "',";

        valor = valor.Substring(0, valor.Length - 1);

        chart += valor;


        chart += "], label: '# Valor de vendas', backgroundColor: ['#8F90B3', '#4D6DB3', '#CEDBE5', '#E9BD97', '#B39A8F'], borderColor: ['#8090B0', '#44619E', '#BCDDE3', '#E6A386', '#B08880'], borderWidth: 2},";
        chart += "]}, options: { responsive: true, maintainAspectRatio: false,  title: { display: true, text: 'Itens que mais renderam'} } ";
        chart += "});";
        chart += "</script>";
        ltlPorProdutoValor.Text = chart;
    }


    /*
    void carregarPorProduto()
    {
        DataSet ds = AdminDB.selectPorProdutoQuantidade(DateTime.Now);

        string chart = "";
        chart = "<canvas id=\"PorProdutoQuantidade\" width=\"100%\"  />";
        chart += "<script>";
        chart += "new Chart(document.getElementById('PorProdutoQuantidade'), { type: 'pie', data: {";
        chart += "labels: [";

        string label = "";
        for (int i = 0; i < ds.Tables[0].Rows.Count; ++i)
        {
            label += "'" + ds.Tables[0].Rows[i]["pro_nome"] + "',";
        }
        label = label.Substring(0, label.Length - 1);

        chart += label;
        chart += "]";
        chart += ", datasets: [";
        chart += "{ data: [";

        string quantidade = "";
        for (int i = 0; i < ds.Tables[0].Rows.Count; ++i)
        {
            quantidade += "'" + (ds.Tables[0].Rows[i]["pnp_quantidade"]).ToString().Replace(",", ".") + "',";
        }
        quantidade = quantidade.Substring(0, quantidade.Length - 1);

        chart += quantidade;
        chart += "], label: '# Quantidade de vendas', backgroundColor: '#93a4d9', borderColor: '#474f69', borderWidth: 2},";
        chart += "{ data: [";

        string valor = "";
        for (int i = 0; i < ds.Tables[0].Rows.Count; ++i)
        {
            valor += "'" + (ds.Tables[0].Rows[i]["(pro_valor * pnp_quantidade)"]).ToString().Replace(",", ".") + "',";
        }
        valor = valor.Substring(0, valor.Length - 1);

        chart += valor;
        chart += "], label: '# Valor de vendas', backgroundColor: '#7885b0', borderColor: '#586282', borderWidth: 2},";
        chart += "]}, options: { title: { display: true, text: 'Itens vendidos'} , scales: { yAxes: [{ ticks: { beginAtZero: true } }] }} ";
        chart += "});";
        chart += "</script>";
        ltlPorProduto.Text = chart;
    }
    */

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

        carregarPorProdutoQuantidade(Convert.ToInt32(txtHistoricoAno.Text));
        carregarPorProdutoValor(Convert.ToInt32(txtHistoricoAno.Text));

        DataSet ds = AdminDB.selectTodosProdutos(Convert.ToInt32(txtHistoricoAno.Text));

        rptProduto.DataSource = ds;
        rptProduto.DataBind();
    }
}