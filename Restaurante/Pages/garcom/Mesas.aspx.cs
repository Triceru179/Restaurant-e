using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

public partial class Pages_garcom_Mesas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["permissao"] == null) Response.Redirect("/");
        if ((Convert.ToInt32(Session["permissao"]) & Permissoes.founder) == 0)
            if ((Convert.ToInt32(Session["permissao"]) & Permissoes.garcom) == 0) Response.Redirect("/Erro.aspx");

        Session["comanda"] = null;

        if (!IsPostBack)
        {
            atualizarPagina();
        }
    }

    void atualizarPagina()
    {
        /* Busca todos os funcionários do banco de dados */
        DataSet ds = GarcomDB.selectMesa();
        
        /* Preenche os cards com todos os funcionários do banco */
        rptMesa.DataSource = ds.Tables[0];
        rptMesa.DataBind();

        if (ds.Tables[0].Rows.Count > 0)
            divNenhumaMesa.Visible = false;
    }

    protected void crdMesa_Click(object sender, EventArgs e)
    {
        /* Busca qual foi o botão pressionado */
        LinkButton btn = (LinkButton)sender;

        /* Busca o o nome da origem do botão que foi pressionado */
        RepeaterItem item = (RepeaterItem)btn.NamingContainer;
        
        /* Criar objeto da classe Mesa, e a atribuir na sessão */
        Mesa mes = new Mesa();
        mes.Mes_id = Convert.ToInt32((item.FindControl("hidId") as HiddenField).Value);
        mes.Mes_identificacao = (item.FindControl("lblIdentificacao") as Label).Text;
        Session["mesa"] = mes;

        /* Criar objeto da classe Comanda, e a atribuir na sessão */
        int com_id = Convert.ToInt32((item.FindControl("hidComId") as HiddenField).Value);
        if (com_id != -1)
        {
            Comanda com = new Comanda();
            com.Com_id= Convert.ToInt32((item.FindControl("hidComId") as HiddenField).Value);

            Session["comanda"] = com;
        }
        Response.Redirect("/Pages/garcom/CriarPedido.aspx");
    }
}