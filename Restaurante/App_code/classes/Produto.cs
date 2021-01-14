using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Produto
{
    private int pro_id;
    private string pro_nome;
    private string pro_descricao;
    private double pro_valor;
    private int pro_complemento;
    private int pro_disponivel;

    public int Pro_id
    {
        get
        {
            return pro_id;
        }

        set
        {
            pro_id = value;
        }
    }

    public string Pro_nome
    {
        get
        {
            return pro_nome;
        }

        set
        {
            pro_nome = value;
        }
    }

    public string Pro_descricao
    {
        get
        {
            return pro_descricao;
        }

        set
        {
            pro_descricao = value;
        }
    }

    public double Pro_valor
    {
        get
        {
            return pro_valor;
        }

        set
        {
            pro_valor = value;
        }
    }

    public int Pro_complemento
    {
        get
        {
            return pro_complemento;
        }

        set
        {
            pro_complemento = value;
        }
    }

    public int Pro_disponivel
    {
        get
        {
            return pro_disponivel;
        }

        set
        {
            pro_disponivel = value;
        }
    }
}