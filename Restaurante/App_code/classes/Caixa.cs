using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Caixa
{
    private int cai_id;
    private DateTime cai_dthrPagamento;
    private double cai_valorTotal;
    private double cai_gorjeta;
    private string cai_descricao;

    private Funcionario fun_id;
    private Comanda com_id;
    private Pedidos ped_id;

    public DateTime Cai_dthrPagamento
    {
        get{return cai_dthrPagamento;}
        set{cai_dthrPagamento = value;}
    }

    public double Cai_valorTotal
    {
        get{return cai_valorTotal;}
        set{cai_valorTotal = value;}
    }

    public double Cai_gorjeta
    {
        get{return cai_gorjeta;}
        set{cai_gorjeta = value;}
    }

    public string Cai_descricao
    {
        get { return cai_descricao; }
        set { cai_descricao = value;}
    }

    public global::Funcionario Fun_id
    {
        get { return fun_id; }
        set { fun_id = value; }
    }

    public global::Comanda Com_id
    {
        get { return com_id; }
        set { com_id = value; }
    }

    public global::Pedidos Ped_id
    {
        get { return ped_id; }
        set { ped_id = value; }
    }
}