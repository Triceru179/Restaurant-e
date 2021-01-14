using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Text;
using System.Security.Cryptography;

public partial class Pages_admin_Funcionario : System.Web.UI.Page
{
    protected bool adicionar, editar, remover;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["permissao"] == null) Response.Redirect("/");
        if ((Convert.ToInt32(Session["permissao"]) & Permissoes.founder) == 0)
            if ((Convert.ToInt32(Session["permissao"]) & Permissoes.admin) == 0) Response.Redirect("/Erro.aspx");

        adicionar = false;
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
        DataSet ds = AdminDB.selectFuncionario();
        

        /* Preenche os cards com todos os funcionários do banco */
        rptFuncionario.DataSource = ds;
        rptFuncionario.DataBind();

        res.Attributes.Clear();
        res.Text = "";
    }

    protected void btn_adicionar(object sender, EventArgs e)
    {
        adicionar = true;

        txtNomeAdicionar.Text = "";
        txtEmailAdicionar.Text = "";
        txtTelefoneAdicionar.Text = "";
        txtSenhaAdicionar.Text = "";
        txtRepitaSenhaAdicionar.Text = "";

        ckbFundaEditar.Checked    = false;
        ckbAdminAdicionar.Checked = false;
        ckbGerenAdicionar.Checked = false;
        ckbGarcoAdicionar.Checked = false;
        ckbCaixaAdicionar.Checked = false;
        ckbCozinAdicionar.Checked = false;
    }
    
    protected void btnAdicionar_Click(object sender, EventArgs e)
    {
        if (txtNomeAdicionar.Text.Length > 254 || txtEmailAdicionar.Text.Length > 254 || txtTelefoneAdicionar.Text.Length > 254 || txtSenhaAdicionar.Text.Length > 254 || txtRepitaSenhaAdicionar.Text.Length > 254)
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "Os campos 'Nome', 'Telefone', 'Email', 'Senha' e 'Repita a senha' só aceitam até 254 dígitos";
            return;
        }

        /* Valida se todos os campos estão preenchidos */
        if (txtNomeAdicionar.Text.Equals("") || txtEmailAdicionar.Text.Equals("") || txtSenhaAdicionar.Text.Equals("") || txtRepitaSenhaAdicionar.Text.Equals(""))
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "Os campos 'Nome', 'Email', 'Senha' e 'Repita a senha' devem ser preenchidos";
            return;
        }

        /* Valida se ambas senhas digitadas são iguais */
        if (!txtSenhaAdicionar.Text.Equals(txtRepitaSenhaAdicionar.Text))
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "As senhas digitadas devem ser iguais";
            return;
        }

        /* Valida se ambas senhas digitadas são iguais */
        if (txtSenhaAdicionar.Text.Length < 5)
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "A senha deve ter no mínimo 6 digitos";
            return;
        }

        /* Verifica quais permissões o funcionário terá */
        int permissao =   0;
        if (ckbAdminAdicionar.Checked) permissao += 16;
        if (ckbGerenAdicionar.Checked) permissao +=  8;
        if (ckbGarcoAdicionar.Checked) permissao +=  4;
        if (ckbCozinAdicionar.Checked) permissao +=  2;
        if (ckbCaixaAdicionar.Checked) permissao +=  1;

        /* Verifica se o email não existe */
        Funcionario fun = new Funcionario();
        fun.Fun_email = txtEmailAdicionar.Text;
        DataSet ds = AdminDB.validarEmail(fun);
        if (ds.Tables[0].Rows.Count > 0)
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "Já existe um funcionário com este email";
            return;
        }

        res.Text = "";

        /* Registra o funcionário no sistema */
        fun.Fun_nome = txtNomeAdicionar.Text;
        fun.Fun_telefone = txtTelefoneAdicionar.Text;
        fun.Fun_email = txtEmailAdicionar.Text;
        fun.Fun_permissao = permissao;

        UnicodeEncoding UE = new UnicodeEncoding();
        byte[] HashValue, MessageBytes = UE.GetBytes(txtSenhaAdicionar.Text);        SHA512Managed SHhash = new SHA512Managed();
        HashValue = SHhash.ComputeHash(MessageBytes);        string strHex = "";        foreach (byte b in HashValue)
        {
            strHex += String.Format("{0:x2}", b);
        }


        fun.Fun_senha = strHex;

        AdminDB.insertFuncionario(fun);


        /* Atualiza os cards, para mostrar o novo funcionário registrado */
        atualizarPagina();

        res.Attributes.Clear();
        res.Attributes.Add("class", "btn btn-block text-success");
        res.Text = "Funcionário registrado com sucesso";
    }

    protected void btn_editar(object sender , EventArgs e)
    {
        editar = true;

        /* Busca qual foi o botão pressionado */
        LinkButton btn = (LinkButton) sender;

        /* Busca o o nome da origem do botão que foi pressionado */
        RepeaterItem item = (RepeaterItem)btn.NamingContainer;
        
        /* Atualiza os campos com as informações do card a ser editado*/
        txtNomeEditar.Text = (item.FindControl("lblNome") as Label).Text;
        txtTelefoneEditar.Text = (item.FindControl("lblTelefone") as Label).Text;
        txtEmailEditar.Text = (item.FindControl("lblEmail") as Label).Text;
        txtSenhaEditar.Text = "";
        txtRepitaSenhaEditar.Text = "";
        hidIdEditar.Value = Convert.ToInt32((item.FindControl("hidid") as HiddenField).Value) + "";

        /* Define as permissões do usuário a ser editado */
        int perm = Convert.ToInt32((item.FindControl("hidPermissao") as HiddenField).Value);
        hidPermissaoEditar.Value = (perm & 32) != 0 ? "32" : (perm & 16) != 0 ? "16" : "0";

        if (perm == 32)
        {
            ckbFundaEditar.Visible = true;
            ckbAdminEditar.Visible = false;
            ckbAdminEditar.Enabled = true;
            ckbGerenEditar.Visible = false;
            ckbGarcoEditar.Visible = false;
            ckbCozinEditar.Visible = false;
            ckbCaixaEditar.Visible = false;
        }
        else
        {
            ckbFundaEditar.Visible = false;
            ckbAdminEditar.Visible = true;
            if(Convert.ToInt32(Session["permissao"]) == 32)
                ckbAdminEditar.Enabled = true;
            else
                ckbAdminEditar.Enabled = false;
            ckbGerenEditar.Visible = true;
            ckbGarcoEditar.Visible = true;
            ckbCozinEditar.Visible = true;
            ckbCaixaEditar.Visible = true;
        }

        ckbFundaEditar.Checked = false;
        ckbAdminEditar.Checked = false;
        ckbGerenEditar.Checked = false;
        ckbGarcoEditar.Checked = false;
        ckbCozinEditar.Checked = false;
        ckbCaixaEditar.Checked = false;

        if ( (perm & Permissoes.founder) != 0) ckbFundaEditar.Checked = true;
        if ( (perm & Permissoes.admin)   != 0) ckbAdminEditar.Checked = true;
        if ( (perm & Permissoes.gerente) != 0) ckbGerenEditar.Checked = true;
        if ( (perm & Permissoes.garcom)  != 0) ckbGarcoEditar.Checked = true;
        if ( (perm & Permissoes.cozinha) != 0) ckbCozinEditar.Checked = true;
        if ( (perm & Permissoes.caixa)   != 0) ckbCaixaEditar.Checked = true;
    }

    protected void btnConfirmarEditar_Click(object sender, EventArgs e)
    {
        if (txtNomeEditar.Text.Length > 254 || txtEmailEditar.Text.Length > 254 || txtTelefoneEditar.Text.Length > 254 || txtSenhaEditar.Text.Length > 254 || txtRepitaSenhaEditar.Text.Length > 254)
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "Os campos 'Nome', 'Telefone', 'Email', 'Senha' e 'Repita a senha' só aceitam até 254 dígitos";
            return;
        }

        /* Valida se os campos NOME, EMAIL foram preenchidos */
        if ((txtNomeEditar.Text.Equals("") || txtEmailEditar.Text.Equals("")))
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "Os campos 'Nome' e 'Email' a serem editados devem ser preenchidos";
            return;
        }

        /* Valida se os campos NOME, SENHA, REPITA A SENHA e EMAIL foram preenchidos (caso uma das senhas tenham sido digitadas) */
        if (((txtNomeEditar.Text.Equals("") || txtEmailEditar.Text.Equals("")) && (txtSenhaEditar.Text.Equals("") || txtRepitaSenhaEditar.Text.Equals(""))))
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "Os campos 'Nome', 'Senha', 'Repita a senha' e 'Email' a serem editados devem ser preenchidos e as senhas devem ser iguais";
            return;
        }

        /* Se todos os campos foram digitados, inclusive a senha, valida se as senhas são iguais */
        if (!txtSenhaEditar.Text.Equals(txtRepitaSenhaEditar.Text))
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "As senhas digitadas devem ser iguais";
            return;
        }
        
        /* Busca o ID do funcionário que está sendo editado */
            
        Funcionario fun = new Funcionario();
        fun.Fun_id = Convert.ToInt32(hidIdEditar.Value);

        /* Valida se o novo email não existe no sistema */
        fun.Fun_email = txtEmailEditar.Text;
        DataSet ds = AdminDB.validarEmailUpdate(fun);

        if (ds.Tables[0].Rows.Count >= 1)
        {
            res.Attributes.Clear();
            res.Attributes.Add("class", "btn btn-block text-danger");
            res.Text = "Já existe um funcionário com este email";
            return;
        }


        /* Verifica quais permissões o funcionário terá */
        int permissao = 0;

        if (hidPermissaoEditar.Value.Equals("32"))
        {
            permissao += 32;
        }

        if (hidPermissaoEditar.Value.Equals("16") && (Convert.ToInt32(Session["permissao"]) & 16) != 0 && !ckbAdminEditar.Checked)
        {
            permissao += 16;
        }
        
        if (ckbAdminEditar.Checked) permissao += 16;
        if (ckbGerenEditar.Checked) permissao +=  8;
        if (ckbGarcoEditar.Checked) permissao +=  4;
        if (ckbCozinEditar.Checked) permissao +=  2;
        if (ckbCaixaEditar.Checked) permissao +=  1;

        fun.Fun_nome = txtNomeEditar.Text;
        fun.Fun_telefone = txtTelefoneEditar.Text;
        fun.Fun_email = txtEmailEditar.Text;
        fun.Fun_permissao = permissao;

        /* Verifica se a senha deve ser alterada ou não junto com as demais alterações */
        if (txtSenhaEditar.Text.Equals("") && txtRepitaSenhaEditar.Text.Equals(""))
        {
            /* Atualiza o funcionário */
            AdminDB.updateFunSemTrocaSenha(fun);
        }
        else
        {
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] HashValue, MessageBytes = UE.GetBytes(txtSenhaEditar.Text);
            SHA512Managed SHhash = new SHA512Managed();
            HashValue = SHhash.ComputeHash(MessageBytes);

            string strHex = "";
            foreach (byte b in HashValue)
            {
                strHex += String.Format("{0:x2}", b);
            }

            /* Atualiza o funcionário */
            fun.Fun_senha = strHex;
            AdminDB.updateFun(fun);
        }
            
        
        /* Atualiza os cards, para mostrar o novo funcionário registrado */
        atualizarPagina();

        res.Attributes.Clear();
        res.Attributes.Add("class", "btn btn-block text-success");
        res.Text = "Funcionário atualizado com sucesso";
    }

    protected void btn_excluir(object sender, EventArgs e)
    {
        remover = true;

        /* Busca qual foi o botão pressionado */
        LinkButton btn = (LinkButton)sender;

        /* Busca o o nome da origem do botão que foi pressionado */
        RepeaterItem item = (RepeaterItem)btn.NamingContainer;

        /* Atualiza os campos com as informações do card a ser editado*/
        lblNomeRemover.Text = (item.FindControl("lblNome") as Label).Text;
        hidIdmodalRemover.Value = Convert.ToInt32((item.FindControl("hidId") as HiddenField).Value) + "";
    }

    protected void btnRemover_Click(object sender, EventArgs e)
    {
        Funcionario fun = new Funcionario();
        fun.Fun_id = Convert.ToInt32(hidIdmodalRemover.Value);

        AdminDB.removerFuncionario(fun);

        atualizarPagina();

        res.Attributes.Clear();
        res.Attributes.Add("class", "btn btn-block text-success");
        res.Text = "Funcionário excluido com sucesso";
    }
}