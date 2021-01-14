using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

public partial class Pages_garcom_CriarPedido : System.Web.UI.Page
{
    protected bool finalizar, excluir, excluirPedido, solicitarPreparo;
    protected bool foiFinalizada;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["permissao"] == null) Response.Redirect("/");
        if ((Convert.ToInt32(Session["permissao"]) & Permissoes.founder) == 0)
            if ((Convert.ToInt32(Session["permissao"]) & Permissoes.garcom) == 0) Response.Redirect("/Erro.aspx");
        if (Session["mesa"] == null) Response.Redirect("/Pages/garcom/Mesas.aspx");

        GarcomDB.resetI();

        /* Ativa o botão de iniciar comanda, esconde o botão de finalizar comanda, e reseta o campo resultado */
        setVisible(true, false, false, false, false);
        res.Attributes.Clear();
        res.Text = "";

        /* Variável de controle de exibição do modal */
        finalizar = false;
        excluir = false;
        excluirPedido = false;
        foiFinalizada = false;
        solicitarPreparo = false;

        /* Obtém o ID e IDENTIFICAÇÃO da mesa selecionada na tela anterior por sessão */
        Mesa mes = (Session["mesa"] as Mesa);
        
        /* Define a IDENTIFICAÇÃO da mesa na interface para o usuário */
        lblMesa.Text = mes.Mes_identificacao;

        /* Define a sessão comanda com a identificação da comanda encontrada na mesa */
        Session["comanda"] = GarcomDB.selectComanda(mes);
        
        /* Atualiza a página (realizando todas verificações necessárias) */
        atualizarPagina();
        aaaaaaaa.InnerHtml = GarcomDB.getI() + "<br>" + GarcomDB.getT();
    }

    void setVisible(bool iniciarComanda, bool finalizarComanda, bool excluirComanda, bool addPedido, bool pedido)
    {
        btnIniciar.Visible = iniciarComanda;
        divIniciar.Visible = iniciarComanda;
        divEsperar.Visible = iniciarComanda;
        btnFinalizar.Visible = finalizarComanda;
        divFinalizar.Visible = finalizarComanda;
        btnExcluir.Visible = excluirComanda;
        divExcluir.Visible = excluirComanda;
        btnAddPedido.Visible = addPedido;
        divAddPedido.Visible = addPedido;

        divPedido.Visible = pedido;
    }

    void atualizarPagina()
    {
        Comanda com = Session["comanda"] as Comanda;

        if (com.Com_id == -1)
        {
            setVisible(true, false, false, false, false);
            return;
        }

        /* Zera o div de resultado */
        res.Attributes.Clear();
        res.Text = "";
        
        /* Se a comanda não foi finalizada, o botão iniciar some, e o finalizar aparece */
        if (com.Com_foiFinalizada == 0)
        {
            /* Seleciona todos os pedidos que estão nesta comanda */
            DataSet ds = GarcomDB.selectAllPedido(com);

            /* Atribui o resultado do comando anterior na interface */
            rptPedido.DataSource = ds.Tables[0];
            rptPedido.DataBind();

            setVisible(false, true, true, true, true);

            //Pedidos ped = GarcomDB.selectPedido(com);
            
            return;
        }

        /* Se a comanda não foi paga, o botão iniciar some e o finalizar somem */
        if (com.Com_foiPaga == 0)
        {
            setVisible(false, false, false, false, false);
            foiFinalizada = true;
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "Esperando pagamento da comanda";
            return;
        }
    }



    protected void btnIniciar_Click(object sender, EventArgs e)
    {
        Mesa mes = (Session["mesa"] as Mesa);

        Comanda com = new Comanda();
        com.DthrCriacao = DateTime.Now;
        com.Com_foiPaga = 0;
        com.Mes_id = mes;
        com.Fun_id= new Funcionario();
        com.Fun_id.Fun_id = Convert.ToInt32(Session["fun_id"]);
        
        GarcomDB.createComanda(com);
        
        Session["comanda"] = GarcomDB.selectComanda(mes);

        atualizarPagina();

        res.Attributes.Clear();
        res.Attributes.Add("class", "btn btn-block text-success");
        res.Text = "Comanda iniciada com sucesso";
    }

    protected void btn_finalizar(object sender, EventArgs e)
    {
        Mesa mes = (Session["mesa"] as Mesa);
        finalizar = true;

        /* Atualiza os campos com as informações do card a ser editado*/
        lblComandaFinalizar.Text = mes.Mes_identificacao;
        hidIdmodalFinalizar.Value = mes.Mes_id + "";
    }

    protected void btnFinalizar_Click(object sender, EventArgs e)
    {
        Comanda com = Session["comanda"] as Comanda;
        DataSet ds = GarcomDB.selectIfComandaPodeFinalizar(com);
        
        if(ds.Tables[0].Rows.Count == 0)
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "A comanda deve possuir no mínimo um pedido antes de poder ser finalizada";
            return;
        }

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (dr["count(pnp.pnp_id)"] is DBNull)
            {
                res.Attributes.Clear();
                res.Attributes.Add("class", "btn btn-block text-danger");
                res.Text = "Todos pedidos devem possuim no mínimo um item adicionado antes de finalizar a comanda";
                return;
            }
        }

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (Convert.ToInt32(dr["ped_solicitarPreparo"]) == 0)
            {
                res.Attributes.Clear();
                res.Attributes.Add("class", "btn btn-block text-danger");
                res.Text = "Todos pedidos devem ter seu preparo solicitado antes de finalizar a comanda";
                return;
            }
        }
        
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (dr["sum(pnp.pnp_foiFeito)"] is DBNull)
                return;
            
            if (Convert.ToInt32(dr["count(pnp.pnp_id)"]) != (Convert.ToInt32(dr["sum(pnp.pnp_foiFeito)"]) + Convert.ToInt32(dr["sum(pnp.pnp_disabled)/2"])))
            {
                res.Attributes.Clear();
                res.Attributes.Add("class", "btn btn-block text-danger");
                res.Text = "Todos itens nos pedidos devem ter sido entregues antes de finalizar a comanda";
                return;
            }
        }

        GarcomDB.finalizarComanda(com);

        Mesa mes = (Session["mesa"] as Mesa);
        Session["comanda"] = GarcomDB.selectComanda(mes);

        atualizarPagina();

        res.Attributes.Clear();
        res.Attributes.Add("class", "btn btn-block text-success");
        res.Text = "Comanda finalizada com sucesso";
    }

    protected void btn_excluir(object sender, EventArgs e)
    {
        Mesa mes = (Session["mesa"] as Mesa);
        excluir = true;

        /* Atualiza os campos com as informações do card a ser editado*/
        lblComandaExcluir.Text = mes.Mes_identificacao;
        hidIdmodalExcluir.Value = mes.Mes_id + "";
    }

    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        if (txtMotivoComanda.Text.Length > 254)
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "O campo 'Motivo' deve possuir até 254 digitos";
            return;
        }

        /* Valida se o campo MOTIVO foi preenchido */
        if ((txtMotivoComanda.Text.Equals("")))
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "O campo 'Motivo' deve ser preenchido";
            return;
        }

        Comanda com = Session["comanda"] as Comanda;

        ComandaCancelada coc = new ComandaCancelada();
        coc.Coc_motivo = txtMotivoComanda.Text;
        coc.Com_id = new Comanda();
        coc.Com_id.Com_id = com.Com_id;
        coc.Coc_dthrCancelada = DateTime.Now;

        Funcionario fun = new Funcionario();
        fun.Fun_id = Convert.ToInt32(Session["fun_id"]);

        GarcomDB.deleteComanda(com, coc, fun);

        Mesa mes = (Session["mesa"] as Mesa);
        Session["comanda"] = GarcomDB.selectComanda(mes);

        Response.Redirect("/pages/garcom/CriarPedido.aspx");
        //atualizarPagina();

        //res.Attributes.Clear();
        //res.Attributes.Add("class", "btn btn-block text-success");
        //res.Text = "Comanda excluida com sucesso";
    }

    protected void btnAddPedido_Click(object sender, EventArgs e)
    {
        Comanda com = Session["comanda"] as Comanda;
        GarcomDB.createPedido(com);

        atualizarPagina();

        res.Attributes.Clear();
        res.Attributes.Add("class", "btn btn-block text-success");
        res.Text = "Pedido adicionado com sucesso";
    }

    protected void rptPedido_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Repeater rptItemPedido = (Repeater)e.Item.FindControl("rptItemPedido");
        Comanda com = Session["comanda"] as Comanda;

        HiddenField hf = (HiddenField)e.Item.FindControl("hidIdPedido");
        int ped_id = Convert.ToInt32(hf.Value);

        Pedidos p = new Pedidos();
        p.Ped_id = ped_id;

        DataSet dss = GarcomDB.selectItensNoPedido(com, p);
        rptItemPedido.DataSource = dss;
        rptItemPedido.DataBind();
        
        Label lbl = (Label)e.Item.FindControl("txtValorTotal");
        HiddenField hfd = (HiddenField)e.Item.FindControl("hidValorTotal");

        if (dss.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToInt32(dss.Tables[0].Rows[0]["pnp_disabled"]) == 2)
            {
                lbl.Text = String.Format("R${0:0.00}", dss.Tables[0].Rows[0]["ped_valor"]);
                hfd.Value = dss.Tables[0].Rows[0]["ped_valor"] + "";
            }
            else
            {
                double valor = 0;
                foreach (DataRow dr in dss.Tables[0].Rows)
                {
                    if(Convert.ToInt32(dr["pnp_disabled"]) == 0)
                        valor += Convert.ToInt32(dr["pnp_quantidade"]) * Convert.ToDouble(dr["pnp_valor"]);
                }

                lbl.Text = String.Format("R${0:0.00}", valor);
                hfd.Value = valor + "";
            }
        }
    }

    protected void btn_excluirPedido(object sender, EventArgs e)
    {
        excluirPedido = true;

        /* Busca qual foi o botão pressionado */
        LinkButton btn = (LinkButton)sender;

        /* Busca o o nome da origem do botão que foi pressionado */
        RepeaterItem item = (RepeaterItem)btn.NamingContainer;

        Mesa mes = (Session["mesa"] as Mesa);

        /* Atualiza os campos do card com as informações do pedido a ser excluido */
        lblPedido.Text = ((item.FindControl("lblNumPedido") as Label).Text).ToString();
        lblComandaExcluirPedido.Text = mes.Mes_identificacao;
        hidIdmodalExcluirPedido.Value = Convert.ToInt32((item.FindControl("hididPedido") as HiddenField).Value) + "";
    }

    protected void btnExcluirPedido_Click(object sender, EventArgs e)
    {
        if(txtMotivoPedido.Text.Length > 254)
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "O campo 'Motivo' deve possuir até 254 digitos";
            return;
        }

        /* Valida se os campos MOTIVO foi preenchido */
        if ((txtMotivoPedido.Text.Equals("")))
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "O campo 'Motivo' deve ser preenchido";
            return;
        }

        Pedidos ped = new Pedidos();
        ped.Ped_id = Convert.ToInt32(hidIdmodalExcluirPedido.Value);

        PedidoCancelado pec = new PedidoCancelado();
        pec.Pec_motivo = txtMotivoPedido.Text;
        pec.Pec_dthrCancelado = DateTime.Now;

        GarcomDB.createPedidoCancelado(ped, pec);

        atualizarPagina();

        res.Attributes.Clear();
        res.Attributes.Add("class", "btn btn-block text-success");
        res.Text = "Pedido removido com sucesso";
    }

    protected void btnEditarPedido_Click(object sender, EventArgs e)
    {
        /* Busca qual foi o botão pressionado */
        LinkButton btn = (LinkButton)sender;

        /* Busca o o nome da origem do botão que foi pressionado */
        RepeaterItem item = (RepeaterItem)btn.NamingContainer;

        Pedidos ped = new Pedidos();
        ped.Ped_id = Convert.ToInt32((item.FindControl("hidIdPedido") as HiddenField).Value);

        Session["pedido_identi"] = (item.FindControl("lblNumPedido") as Label).Text;
        Session["pedido"] = ped;

        Response.Redirect("/pages/garcom/EditarPedido.aspx");
    }

    protected void btn_solicitarPreparo(object sender, EventArgs e)
    {
        solicitarPreparo = true;

        /* Busca qual foi o botão pressionado */
        LinkButton btn = (LinkButton)sender;

        /* Busca o o nome da origem do botão que foi pressionado */
        RepeaterItem item = (RepeaterItem)btn.NamingContainer;

        Mesa mes = (Session["mesa"] as Mesa);

        /* Atualiza os campos do card com as informações do pedido a ser excluido */
        lblSolicitarPreparo.Text = ((item.FindControl("lblNumPedido") as Label).Text).ToString();
        lblComandaSolicitarPreparo.Text = mes.Mes_identificacao;
        hidIdmodalSolicitarPreparo.Value = Convert.ToInt32((item.FindControl("hidIdPedido") as HiddenField).Value) + "";
        if(!Convert.ToDouble((item.FindControl("hidValorTotal") as HiddenField).Value).Equals(""))
            hidValorTotalFinalizar.Value = Convert.ToDouble((item.FindControl("hidValorTotal") as HiddenField).Value) + "";
    }

    protected void btnSolicitarPreparo_Click(object sender, EventArgs e)
    {
        Pedidos ped = new Pedidos();
        ped.Ped_id = Convert.ToInt32(hidIdmodalSolicitarPreparo.Value);

        DataSet ds = GarcomDB.selectIfPodeSolicitarPedido(ped);
        if(Convert.ToInt32(ds.Tables[0].Rows[0]["count(pnp.pnp_id)"]) == 0)
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "Para solicitar o preparo deve haver no mínimo um item no pedido";
            return;
        }

        ped.Ped_valor = Convert.ToDouble(hidValorTotalFinalizar.Value);
        ped.Ped_dthrCriacao = DateTime.Now;
        GarcomDB.solicitarPreparo(ped);

        atualizarPagina();

        res.Attributes.Clear();
        res.Attributes.Add("class", "btn btn-block text-success");
        res.Text = "Pedido enviado a cozinha com sucesso";
    }
}