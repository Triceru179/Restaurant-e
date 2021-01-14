using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Comanda
{
    private int com_id;
    private DateTime dthrCriacao;
    private int com_foiPaga;
    private int com_foiFinalizada;

    /* chaves estrangeiras */
    private Mesa mes_id;
    private Funcionario fun_id;

    public int Com_id
    {
        get {return com_id;}
        set {com_id = value;}
    }

    public DateTime DthrCriacao
    {
        get{return dthrCriacao;}
        set{dthrCriacao = value;}
    }

    public int Com_foiPaga
    {
        get{return com_foiPaga;}
        set{com_foiPaga = value;}
    }

    public int Com_foiFinalizada
    {
        get{return com_foiFinalizada;}
        set{com_foiFinalizada = value;}
    }

    public global::Mesa Mes_id
    {
        get{return mes_id;}
        set{mes_id = value;}
    }

    public global::Funcionario Fun_id
    {
        get{return fun_id;}
        set{fun_id = value;}
    }

    
}