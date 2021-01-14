using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Mesa
{
    private int mes_id;
    private string mes_identificacao;

    public int Mes_id
    {
        get{return mes_id;}
        set{mes_id = value;}
    }

    public string Mes_identificacao
    {
        get{return mes_identificacao;}
        set{mes_identificacao = value;}
    }
}