using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Text;
using System.Security.Cryptography;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            res.Attributes.Clear();
            res.Text = "";
        }
    }

    protected void btnEntrar_Click(object sender, EventArgs e)
    {
        if(txtEmail.Text.Length > 254 || txtSenha.Text.Length > 254)
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "text-danger");
            res.Text = "Os campos 'Email' e 'Senha' só aceitam até 254 digitos";
            return;
        }

        UnicodeEncoding UE = new UnicodeEncoding();
        byte[] HashValue, MessageBytes = UE.GetBytes(txtSenha.Text);        SHA512Managed SHhash = new SHA512Managed();
        HashValue = SHhash.ComputeHash(MessageBytes);        string strHex = "";        foreach (byte b in HashValue)
        {
            strHex += String.Format("{0:x2}", b);
        }        
        Funcionario fun = new Funcionario();
        fun.Fun_email = txtEmail.Text;
        fun.Fun_senha = strHex;

        DataSet ds = PerfilDB.validarLogin(fun);
        
        if (ds.Tables[0].Rows.Count == 1)
        {
            Session["nome"] = ds.Tables[0].Rows[0]["fun_nome"].ToString();
            Session["email"] = ds.Tables[0].Rows[0]["fun_email"].ToString();
            Session["fun_id"] = ds.Tables[0].Rows[0]["fun_id"].ToString();
            Session["permissao"] = ds.Tables[0].Rows[0]["fun_permissao"].ToString();
        
            int permissao = Convert.ToInt32(Session["permissao"]);
        
            Response.Redirect("/Pages/");
        }
        else
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "text-danger");
            res.Text = "Email ou senha inválidos";
            return;
        }
    }
}