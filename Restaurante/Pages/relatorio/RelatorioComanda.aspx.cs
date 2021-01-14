using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Relatorio_RelatorioComanda : System.Web.UI.Page
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

        if (!IsPostBack)
        {
            atualizarPagina();
        }
    }

    void atualizarPagina()
    {
        txtHistoricoAno.Text = DateTime.Now.Year + "";

        carregarComandaEfetivada(Convert.ToInt32(txtHistoricoAno.Text));
        carregarComandaEfetivadaECancelada(Convert.ToInt32(txtHistoricoAno.Text));
    }

    void carregarComandaEfetivada(int ano)
    {
        ltlComandaEfetivada.Text = "<label class=\"w-100 text-center mt-3\"> Nenhum registro encontrado </label>";

        DataSet ds = AdminDB.selectQuantidadeComandaPorMes(ano);
        if (ds.Tables[0].Rows.Count == 0)
            return;

        string chart = "";
        chart = "<canvas id=\"comanda1\" width=\"100%\" height=\"400\"/>";
        chart += "<script>";
        chart += "new Chart(document.getElementById('comanda1'), { type: 'bar', data: {";
        chart += "labels: [";

        string label = "";
        for (int i = 0; i < ds.Tables[0].Rows.Count; ++i)
        {
            label += "'" + mes[Convert.ToInt32(ds.Tables[0].Rows[i]["month(com_dthrCriacao)"])] + "',";
        }
        label = label.Substring(0, label.Length - 1);

        chart += label;
        chart += "]";
        chart += ", datasets: [{ data: [";

        string valorComanda = "";
        for (int i = 0; i < ds.Tables[0].Rows.Count; ++i)
        {
            valorComanda += ds.Tables[0].Rows[i]["COUNT(com_id)"] + ",";
        }
        valorComanda = valorComanda.Substring(0, valorComanda.Length - 1);

        chart += valorComanda;
        chart += "], label: '# de comandas', backgroundColor: '#93a4d9', borderColor: '#586282', borderWidth: 2}";
        chart += "]}, options: { responsive: true, maintainAspectRatio: false,  title: { display: true, text: 'Comanda'} , scales: { yAxes: [{ ticks: { beginAtZero: true } }] }}";
        chart += "});";
        chart += "</script>";

        ltlComandaEfetivada.Text = chart;
    }

    void carregarComandaEfetivadaECancelada(int ano)
    {
        ltlComandaEfetivadaECancelada.Text = "<label class=\"w-100 text-center mt-3\"> Nenhum registro encontrado </label>";

        DataSet ds = AdminDB.selectQuantidadeComandaEfetivada(ano);
        if (ds.Tables[0].Rows.Count == 0)
            return;

        string chart = "";
        chart = "<canvas id=\"comanda2\" width=\"100%\" height=\"400\"/>";
        chart += "<script>";
        chart += "new Chart(document.getElementById('comanda2'), { type: 'pie', data: {";
        chart += "labels: [";

        string label = "";
        for (int i = 0; i < ds.Tables[0].Rows.Count; ++i)
        {
            label += "'" + (ds.Tables[0].Rows[i]["com_disabled"].ToString().Equals("1") ? "Cancelada" : "Efetivada") + "',";
        }
        label = label.Substring(0, label.Length - 1);

        chart += label;
        chart += "]";
        chart += ", datasets: [{ data: [";

        string valorComanda = "";
        for (int i = 0; i < ds.Tables[0].Rows.Count; ++i)
        {
            valorComanda += ds.Tables[0].Rows[i]["COUNT(com_id)"] + ",";
        }
        valorComanda = valorComanda.Substring(0, valorComanda.Length - 1);

        chart += valorComanda;
        chart += "], label: '# de comandas', backgroundColor: ['#E9BD97', '#4D6DB3'], borderColor: ['#E6A386', '#44619E'], borderWidth: 2}";
        chart += "]}, options: { responsive: true, maintainAspectRatio: false,  title: { display: true, text: 'Comanda'}} ";
        chart += "});";
        chart += "</script>";

        ltlComandaEfetivadaECancelada.Text = chart;
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

        carregarComandaEfetivada(Convert.ToInt32(txtHistoricoAno.Text));
        carregarComandaEfetivadaECancelada(Convert.ToInt32(txtHistoricoAno.Text));
    }
}