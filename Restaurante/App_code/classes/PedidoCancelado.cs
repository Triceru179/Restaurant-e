using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class PedidoCancelado
{
    private int pec_id;
    private string pec_motivo;
    private DateTime pec_dthrCancelado;

    private Pedidos ped_id;

    public int Pec_id
    {
        get { return pec_id; }
        set { pec_id = value; }
    }

    public string Pec_motivo
    {
        get { return pec_motivo; }
        set { pec_motivo = value; }
    }

    public DateTime Pec_dthrCancelado
    {
        get { return pec_dthrCancelado; }
        set { pec_dthrCancelado = value; }
    }

    public global::Pedidos Ped_id
    {
        get { return ped_id; }
        set { ped_id = value; }
    }
}