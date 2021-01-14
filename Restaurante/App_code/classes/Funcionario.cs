using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Funcionario
{
    private int fun_id;
    private string fun_nome;
    private string fun_telefone;
    private string fun_email;
    private string fun_senha;
    private int fun_permissao;
    

    public int Fun_id
    {
        get{return fun_id;}
        set{fun_id = value;}
    }

    public string Fun_nome
    {
        get
        {
            return fun_nome;
        }

        set
        {
            fun_nome = value;
        }
    }

    public string Fun_email
    {
        get
        {
            return fun_email;
        }

        set
        {
            fun_email = value;
        }
    }

    public string Fun_senha
    {
        get
        {
            return fun_senha;
        }

        set
        {
            fun_senha = value;
        }
    }

    public string Fun_telefone
    {
        get
        {
            return fun_telefone;
        }

        set
        {
            fun_telefone = value;
        }
    }

    public int Fun_permissao
    {
        get
        {
            return fun_permissao;
        }

        set
        {
            fun_permissao = value;
        }
    }
}