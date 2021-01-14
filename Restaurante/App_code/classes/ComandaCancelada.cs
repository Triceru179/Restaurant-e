using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class ComandaCancelada
{
    private int coc_id;
    private string coc_motivo;
    private DateTime coc_dthrCancelada;

    private Comanda com_id;

    public int Coc_id
    {
        get { return coc_id; }
        set { coc_id= value; }
    }

    public string Coc_motivo
    {
        get { return coc_motivo; }
        set { coc_motivo = value; }
    }

    public DateTime Coc_dthrCancelada
    {
        get { return coc_dthrCancelada; }
        set { coc_dthrCancelada = value; }
    }

    public global::Comanda Com_id
    {
        get { return com_id; }
        set { com_id = value; }
    }
}