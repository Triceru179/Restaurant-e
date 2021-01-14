using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class ProdutosNoPedido
{
    private int pnp_id;
    private int pnp_foiFeito;
    private DateTime pnp_dthrCozinha;
    private int pnp_quantidade;
    private double pnp_valor;
    private string pnp_observacao;

    private Pedidos ped_id;
    private Produto pro_id;

    public int Pnp_id
    {
        get{return pnp_id;}
        set{pnp_id = value;}
    }

    public int Pnp_foiFeito
    {
        get{return pnp_foiFeito;}
        set{pnp_foiFeito = value;}
    }

    public DateTime Pnp_dthrCozinha
    {
        get{return pnp_dthrCozinha;}
        set {pnp_dthrCozinha = value;}
    }

    public int Pnp_quantidade
    {
        get { return pnp_quantidade; }
        set { pnp_quantidade = value; }
    }

    public double Pnp_valor
    {
        get { return pnp_valor; }
        set { pnp_valor = value; }
    }

    public string Pnp_observacao
    {
        get { return pnp_observacao; }
        set { pnp_observacao = value; }
    }

    public global::Pedidos Ped_id
    {
        get{return ped_id;}
        set{ped_id = value;}
    }

    public global::Produto Pro_id
    {
        get{return pro_id;}
        set{pro_id = value;}
    }
}