using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Pedidos
{
    private int ped_id;
    private DateTime ped_dthrCriacao;
    private int ped_foiEntregue;
    private double ped_valor;

    private Comanda com_id;

    public int Ped_id
    {
        get
        {
            return ped_id;
        }

        set
        {
            ped_id = value;
        }
    }

    public DateTime Ped_dthrCriacao
    {
        get
        {
            return ped_dthrCriacao;
        }

        set
        {
            ped_dthrCriacao = value;
        }
    }

    public int Ped_foiEntregue
    {
        get
        {
            return ped_foiEntregue;
        }

        set
        {
            ped_foiEntregue = value;
        }
    }

    public double Ped_valor
    {
        get
        {
            return ped_valor;
        }

        set
        {
            ped_valor = value;
        }
    }

    public global::Comanda Com_id
    {
        get
        {
            return com_id;
        }

        set
        {
            com_id = value;
        }
    }
}