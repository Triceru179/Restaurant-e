using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_cozinha_PedidoPendente : System.Web.UI.Page
{
    protected bool finalizar, excluir;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["permissao"] == null) Response.Redirect("/");
        if ((Convert.ToInt32(Session["permissao"]) & Permissoes.founder) == 0)
            if ((Convert.ToInt32(Session["permissao"]) & Permissoes.cozinha) == 0) Response.Redirect("/Erro.aspx");

        finalizar = false;
        excluir = false;

        if(!IsPostBack)
        {
            atualizarPagina();
        }
    }

    void atualizarPagina()
    {
        DataSet ds = CozinhaDB.selectPedidoPendente();
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

        rptPedidoPendente.DataSource = ds;
        rptPedidoPendente.DataBind();
    }

    protected void btnAtualizar_Click(object sender, EventArgs e)
    {
        atualizarPagina();
    }

    protected void btn_finalizar(object sender, EventArgs e)
    {
        finalizar = true;

        LinkButton btn = (LinkButton)sender;
        RepeaterItem item = (RepeaterItem)btn.NamingContainer;

        lblProdutoFinalizar.Text = (item.FindControl("lblPnp") as Label).Text;
        lblMesaFinalizar.Text = (item.FindControl("lblMesa") as Label).Text + " " + (item.FindControl("lblPedido") as Label).Text;
        hidIdFinalizar.Value = (item.FindControl("hidIdPnp") as HiddenField).Value;
        hidIdPedFinalizar.Value = (item.FindControl("hidIdPed") as HiddenField).Value;
    }

    protected void btnConfirmarFinalizar_Click(object sender, EventArgs e)
    {
        ProdutosNoPedido pnp = new ProdutosNoPedido();
        pnp.Pnp_id = Convert.ToInt32(hidIdFinalizar.Value);
        pnp.Pnp_dthrCozinha = DateTime.Now;

        Pedidos ped = new Pedidos();
        ped.Ped_id = Convert.ToInt32(hidIdPedFinalizar.Value); ;

        CozinhaDB.prepararProduto(pnp);

        DataSet ds = CozinhaDB.verificarEntregarPedido(ped);

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (Convert.ToInt32(dr["count(pnp.pnp_id)"]) == ((Convert.ToInt32(dr["sum(pnp.pnp_foiFeito)"]) + (Convert.ToInt32(dr["sum(pnp.pnp_disabled)/2"])))))
                CozinhaDB.entregarPedido(ped);
        }

        atualizarPagina();
    }

    protected void btn_excluir(object sender, EventArgs e)
    {
        excluir = true;

        LinkButton btn = (LinkButton)sender;
        RepeaterItem item = (RepeaterItem)btn.NamingContainer;

        lblProdutoExcluir.Text = (item.FindControl("lblPnp") as Label).Text;
        lblMesaExcluir.Text = (item.FindControl("lblMesa") as Label).Text + " " + (item.FindControl("lblPedido") as Label).Text;
        hidIdExcluir.Value = (item.FindControl("hidIdPnp") as HiddenField).Value;
        hidIdPedExcluir.Value = (item.FindControl("hidIdPed") as HiddenField).Value;
        hidQntPnpExcluir.Value = (item.FindControl("hidQntPnp") as HiddenField).Value;
        hidValorProExcluir.Value = (item.FindControl("hidValorPro") as HiddenField).Value;
    }

    protected void btnConfirmarExcluir_Click(object sender, EventArgs e)
    {
        ProdutosNoPedido pnp = new ProdutosNoPedido();
        pnp.Pnp_id = Convert.ToInt32(hidIdExcluir.Value);
        pnp.Pnp_quantidade = Convert.ToInt32(hidQntPnpExcluir.Value);
        pnp.Pnp_dthrCozinha = DateTime.Now;

        Pedidos ped = new Pedidos();
        ped.Ped_id = Convert.ToInt32(hidIdPedExcluir.Value);

        Produto pro = new Produto();
        pro.Pro_valor = Convert.ToDouble(hidValorProExcluir.Value);

        pnp.Ped_id = new Pedidos();
        pnp.Ped_id = ped;
            
        CozinhaDB.cancelarProduto(pnp, pro);

        DataSet ds = CozinhaDB.verificarEntregarPedido(ped);

        foreach(DataRow dr in ds.Tables[0].Rows)
        {
            if (dr["count(pnp.pnp_id)"] is DBNull)
                continue;
            if (dr["sum(pnp.pnp_foiFeito)"] is DBNull)
                continue;
            if (Convert.ToInt32(dr["count(pnp.pnp_id)"]) == ((Convert.ToInt32(dr["sum(pnp.pnp_foiFeito)"]) + (Convert.ToInt32(dr["sum(pnp.pnp_disabled)/2"])))))
                CozinhaDB.entregarPedido(ped);
        }

        atualizarPagina();
    }
}