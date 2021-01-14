using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

public partial class Pages_admin_Relatorio : System.Web.UI.Page
{
    protected int date = Convert.ToInt32(DateTime.Now.Year);

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
        txtAnoRenda.Text = DateTime.Now.Year + "";

        carregarEntradaDia(DateTime.Now.Day, DateTime.Now.Year);
        carregarEntradaMes(DateTime.Now.Month, DateTime.Now.Year);
        carregarEntradaAno(DateTime.Now.Year);
        
        carregarRenda(2, "", DateTime.Now.Year);
    }

    void carregarEntradaDia(int dia, int ano)
    {
        DataSet ds = AdminDB.selectEntradaNesteDia(dia, ano);
        if (ds.Tables[0].Rows.Count == 0)
            return;

        double med = 0;

        if (!(ds.Tables[0].Rows[0]["SUM(cai_valortotal)"] is DBNull))
            med = Convert.ToDouble(ds.Tables[0].Rows[0]["SUM(cai_valortotal)"].ToString());

        lblValorDia.Text = string.Format("R${0:0.00}", med);
    }

    void carregarEntradaMes(int mes, int ano)
    {
        DataSet ds = AdminDB.selectEntradaNesteMes(mes, ano);
        if (ds.Tables[0].Rows.Count == 0)
            return;

        double med = 0;

        if(!(ds.Tables[0].Rows[0]["SUM(cai_valortotal)"] is DBNull))
            med = Convert.ToDouble(ds.Tables[0].Rows[0]["SUM(cai_valortotal)"].ToString());

        lblValorMes.Text = string.Format("R${0:0.00}", med);
    }

    void carregarEntradaAno(int ano)
    {
        DataSet ds = AdminDB.selectEntradaNesteAno(ano);
        if (ds.Tables[0].Rows.Count == 0)
            return;

        double total = 0;

        for (int i = 0 ; i < ds.Tables[0].Rows.Count ; ++i)
        {
            if(!(ds.Tables[0].Rows[i]["SUM(cai_valortotal)"] is DBNull))
                total += Convert.ToDouble(ds.Tables[0].Rows[i]["SUM(cai_valortotal)"]);
        }

        lblValorAno.Text = string.Format("R${0:0.00}", total);
    }

    void carregarRenda(int opc, string data, int ano)
    {
        ltlRendaPorMes.Text = "<label class=\"w-100 text-center mt-3\"> Nenhum registro encontrado </label>";

        DataSet ds;
        if (opc == 1)
            ds = AdminDB.selectRendaPorDia(data, ano);
        else
        if (opc == 2)
            ds = AdminDB.selectRendaPorMes(ano);
        else
            ds = AdminDB.selectRendaPorAno(ano);

        if (ds.Tables[0].Rows.Count == 0)
            return;

        string chart = "";
        chart = "<canvas id=\"rendaPorMes\" width=\"100\" height=\"400\"/>";
        chart += "<script>";
        chart += "new Chart(document.getElementById('rendaPorMes'), { type: 'line', data: {";
        chart += "labels: [";

        string label = "";
        for (int i = 0; i < ds.Tables[0].Rows.Count; ++i)
        {
            if (opc == 1)
                label += "'" + ds.Tables[0].Rows[i]["dia"] + " - " + mes[Convert.ToInt32(ds.Tables[0].Rows[i]["mes"])] +  ds.Tables[0].Rows[i]["ano"] + "',";
            else
            if (opc == 2)
                label += "'" + mes[Convert.ToInt32(ds.Tables[0].Rows[i]["mes"])] + ds.Tables[0].Rows[i]["ano"] + "',";
            else
                label += "'" + ds.Tables[0].Rows[i]["ano"] + "',";
        }
        label = label.Substring(0, label.Length - 1);

        chart += label;
        chart += "]";
        chart += ", datasets: [{ data: [";

        string valorRenda = "";
        for (int i = 0; i < ds.Tables[0].Rows.Count; ++i)
        {
            if (opc == 1)
                valorRenda += "'" + (ds.Tables[0].Rows[i]["total_dia"]).ToString().Replace(",", ".") + "',";
            else
            if (opc == 2)
                valorRenda += "'" + (ds.Tables[0].Rows[i]["total_mes"]).ToString().Replace(",", ".") + "',";
            else
                valorRenda += "'" + (ds.Tables[0].Rows[i]["total_ano"]).ToString().Replace(",", ".") + "',";

        }
        valorRenda = valorRenda.Substring(0, valorRenda.Length - 1);

        chart += valorRenda;
        chart += "], label: '# R$', fill: false, backgroundColor: '#93a4d9', borderColor: '#93a4d9', borderWidth: 2}";
        chart += "]}, options: { responsive: true, maintainAspectRatio: false, title: { display: true, text: 'Renda'} , scales: { yAxes: [{ ticks: { beginAtZero: true } }] }}";
        chart += "});";
        chart += "</script>";

        ltlRendaPorMes.Text = chart;
    }

    protected void lstRendaDataFiltro_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstRendaDataFiltro.SelectedItem.Value.ToString().Equals("1") || lstRendaDataFiltro.SelectedItem.Value.ToString().Equals("2") || lstRendaDataFiltro.SelectedItem.Value.ToString().Equals("3"))
        {
            if (lstRendaDataFiltro.SelectedItem.Value.Equals("1"))
            {
                txtAnoRenda.Visible = false;
                txtDataRenda.Visible = true;
            }

            if (lstRendaDataFiltro.SelectedItem.Value.Equals("2"))
            {
                txtAnoRenda.Visible = true;
                txtDataRenda.Visible = false;
            }

            if (lstRendaDataFiltro.SelectedItem.Value.Equals("3"))
            {
                txtAnoRenda.Visible = true;
                txtDataRenda.Visible = false;
            }

            carregarRenda(Convert.ToInt32(lstRendaDataFiltro.SelectedItem.Value), "", DateTime.Now.Year);
            return;
        }
        atualizarPagina();
    }

    protected void txtDataRenda_TextChanged(object sender, EventArgs e)
    {
        if (lstRendaDataFiltro.SelectedItem.Value.ToString().Equals("1") || lstRendaDataFiltro.SelectedItem.Value.ToString().Equals("2") || lstRendaDataFiltro.SelectedItem.Value.ToString().Equals("3"))
        {
            carregarRenda(Convert.ToInt32(lstRendaDataFiltro.SelectedItem.Value), txtDataRenda.Text, Convert.ToInt32(txtDataRenda.Text.Substring(0, 4)));
            return;
        }
        atualizarPagina();
    }

    protected void txtAnoRenda_TextChanged(object sender, EventArgs e)
    {
        Decimal d;
        if (!decimal.TryParse(txtAnoRenda.Text, out d))
        {
            txtAnoRenda.Text = DateTime.Now.Year + "";
            return;
        }

        if (Convert.ToInt32(txtAnoRenda.Text) < 1950)
        {
            txtAnoRenda.Text = "1950";
            return;
        }

        if (lstRendaDataFiltro.SelectedItem.Value.Equals("1"))
        {
            carregarRenda(Convert.ToInt32(lstRendaDataFiltro.SelectedItem.Value), txtDataRenda.Text, Convert.ToInt32(txtDataRenda.Text.Substring(0, 4)));
        }
        else
            carregarRenda(Convert.ToInt32(lstRendaDataFiltro.SelectedItem.Value), "", Convert.ToInt32(txtAnoRenda.Text));
    }
}