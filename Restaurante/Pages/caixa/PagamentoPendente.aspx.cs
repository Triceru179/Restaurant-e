using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

public partial class Pages_caixa_PagamentoPendente : System.Web.UI.Page
{
    protected bool adicionar, concluir, cancelar;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["permissao"] == null) Response.Redirect("/");
        if ((Convert.ToInt32(Session["permissao"]) & Permissoes.founder) == 0)
            if ((Convert.ToInt32(Session["permissao"]) & Permissoes.caixa) == 0) Response.Redirect("/Erro.aspx");

        concluir = false;
        cancelar = false;
        adicionar = false;

        if (!IsPostBack)
        {
            atualizarPagina();
        }
    }

    void atualizarPagina()
    {
        DataSet ds = CaixaDB.selectPagamentoPendente();

        if (ds.Tables[0].Rows.Count != 0)
        {
            int idcom = Convert.ToInt32(ds.Tables[0].Rows[0]["com_id"]);
            int idped = Convert.ToInt32(ds.Tables[0].Rows[0]["ped_id"]);

            int contador = 1, cont = 0;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (idped != Convert.ToInt32(ds.Tables[0].Rows[cont]["ped_id"]))
                    contador++;

                if (idcom != Convert.ToInt32(ds.Tables[0].Rows[cont]["com_id"]))
                    contador = 1;

                idped = Convert.ToInt32(ds.Tables[0].Rows[cont]["ped_id"]);
                idcom = Convert.ToInt32(ds.Tables[0].Rows[cont]["com_id"]);

                ds.Tables[0].Rows[cont]["mes_id"] = contador;
                cont++;
            }
        }

        rptPagamentoPendente.DataSource = ds;
        rptPagamentoPendente.DataBind();
    }

    protected void btnAtualizar_Click(object sender, EventArgs e)
    {
        atualizarPagina();
    }

    protected void btn_concluir(object sender, EventArgs e)
    {
        concluir = true;

        LinkButton btn = (LinkButton)sender;
        RepeaterItem item = (RepeaterItem)btn.NamingContainer;

        Caixa cai = new Caixa();
        cai.Ped_id = new Pedidos();
        cai.Ped_id.Ped_id = Convert.ToInt32((item.FindControl("hidIdPed") as HiddenField).Value);

        cai.Com_id = new Comanda();
        cai.Com_id.Com_id = Convert.ToInt32((item.FindControl("hidIdCom") as HiddenField).Value);

        lblMesaPedidoConcluir.Text = (item.FindControl("lblMesa") as Label).Text + " " + (item.FindControl("lblPedido") as Label).Text;
        lblValorConcluir.Text = (item.FindControl("lblValor") as Label).Text;
        hidIdPedConcluir.Value = (item.FindControl("hidIdPed") as HiddenField).Value;
        hidIdComConcluir.Value = (item.FindControl("hidIdCom") as HiddenField).Value;
        HidPedValorConcluir.Value = (item.FindControl("hidPedValor") as HiddenField).Value;

        DataSet ds = CaixaDB.selectItensNoPedido(cai);
        rptPagamentoConcluir.DataSource = ds;
        rptPagamentoConcluir.DataBind();

    }

    protected void btnConcluirPagamento_Click(object sender, EventArgs e)
    {
        Caixa cai = new Caixa();
        cai.Cai_valorTotal = Convert.ToDouble(HidPedValorConcluir.Value);
        cai.Ped_id = new Pedidos();
        cai.Ped_id.Ped_id = Convert.ToInt32(hidIdPedConcluir.Value);
        cai.Com_id = new Comanda();
        cai.Com_id.Com_id = Convert.ToInt32(hidIdComConcluir.Value);
        cai.Fun_id = new Funcionario();
        cai.Fun_id.Fun_id = Convert.ToInt32(Session["fun_id"]);

        CaixaDB.realizarPagamento(cai);
        
        DataSet ds = CaixaDB.validarConcluir(cai);
        int qnt = 0;
        int foiPago = 0;

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (dr["sum(ped.ped_foiPago)"] is DBNull)
            {
                foiPago++;
                continue;
            }
            qnt++;

            if (Convert.ToInt32(dr["ped_disabled"]) == 2)
                foiPago++;

            if (Convert.ToInt32(dr["count(ped.ped_id)"]) == Convert.ToInt32(dr["sum(ped.ped_foiPago)"]))
                foiPago++;
        }

        if (qnt == foiPago)
            CaixaDB.concluirComanda(cai);

        atualizarPagina();
    }

    protected void btn_cancelar(object sender, EventArgs e)
    {
        cancelar = true;

        LinkButton btn = (LinkButton)sender;
        RepeaterItem item = (RepeaterItem)btn.NamingContainer;

        Pedidos ped = new Pedidos();
        ped.Ped_id = Convert.ToInt32((item.FindControl("hidIdPed") as HiddenField).Value);

        Comanda com = new Comanda();
        com.Com_id = Convert.ToInt32((item.FindControl("hidIdCom") as HiddenField).Value);

        DataSet ds = GarcomDB.selectItensNoPedido(com, ped);
        rptPagamentoCancelar.DataSource = ds;
        rptPagamentoCancelar.DataBind();

        lblMesaPedidoCancelar.Text = (item.FindControl("lblMesa") as Label).Text + " " + (item.FindControl("lblPedido") as Label).Text;
        lblValorCancelar.Text = (item.FindControl("lblValor") as Label).Text;
        hidIdPedCancelar.Value = (item.FindControl("hidIdPed") as HiddenField).Value;
        hidIdComCancelar.Value = (item.FindControl("hidIdCom") as HiddenField).Value;
    }

    protected void btnConfirmarCancelar_Click(object sender, EventArgs e)
    {
        Caixa cai = new Caixa();
        cai.Ped_id = new Pedidos();
        cai.Ped_id.Ped_id = Convert.ToInt32(hidIdPedCancelar.Value);
        cai.Com_id = new Comanda();
        cai.Com_id.Com_id = Convert.ToInt32(hidIdComCancelar.Value);

        CaixaDB.cancelarPagamento(cai);

        DataSet ds = CaixaDB.validarConcluir(cai);
        int qnt = 0;
        int foiPago = 0;

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (dr["sum(ped.ped_foiPago)"] is DBNull)
            {
                foiPago++;
                continue;
            }
            qnt++;

            if (Convert.ToInt32(dr["ped_disabled"]) == 2)
                foiPago++;

            if (Convert.ToInt32(dr["count(ped.ped_id)"]) == Convert.ToInt32(dr["sum(ped.ped_foiPago)"]))
                foiPago++;
        }

        if (qnt == foiPago)
            CaixaDB.concluirComanda(cai);

        atualizarPagina();
    }

    protected void btn_PagamentoNaoAgendado(object sender, EventArgs e)
    {
        adicionar = true;
        txtOrigemAdicionar.Text = "";
        txtValorAdicionar.Text = "";
        ckbGorjeta.Checked = false;
    }

    protected void btnConfirmarPagamentoNaoAgendado_Click(object sender, EventArgs e)
    {
        Decimal d;
        if (!decimal.TryParse(txtValorAdicionar.Text, out d))
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "O campo 'Valor' só aceita números";
            return;
        }

        if(txtOrigemAdicionar.Text.Length > 254)
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "O campo 'Origem' deve ter até 254 digitos";
            return;
        }

        if (Convert.ToDouble(txtValorAdicionar.Text) > 9999)
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "O campo 'Valor' deve ter um valor até R$ 9999,00";
            return;
        }

        if (Convert.ToDouble(txtValorAdicionar.Text) < -9999)
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "O campo 'Valor' deve ter um valor mínimo de R$ -9999,00";
            return;
        }

        if (txtValorAdicionar.Text.Length == 0 || txtOrigemAdicionar.Text.Length == 0)
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "Os campos 'Origem' e 'Valor' só aceitam até 254 dígitos";
            return;
        }

        Caixa cai = new Caixa();
        cai.Cai_descricao = txtOrigemAdicionar.Text;
        cai.Fun_id = new Funcionario();
        cai.Fun_id.Fun_id = Convert.ToInt32(Session["fun_id"]);

        if (ckbGorjeta.Checked)
        {
            cai.Cai_valorTotal = 0;
            cai.Cai_gorjeta = Convert.ToDouble(txtValorAdicionar.Text);
        }
        else
        {
            cai.Cai_valorTotal = Convert.ToDouble(txtValorAdicionar.Text);
            cai.Cai_gorjeta = 0;
        }

        CaixaDB.inserirPagamentoNaoAgendado(cai);
    }
}