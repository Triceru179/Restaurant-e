using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descrição resumida de Reclamacao
/// </summary>
public class Reclamacao
{
    private int rec_id;
    private string rec_descricao;
    private string rec_categoria;
    private DateTime rec_dthrCriacao;

    private Funcionario fun_id;

    public int Rec_id
    {
        get{return rec_id;}
        set{rec_id = value;}
    }

    public string Rec_descricao
    {
        get{return rec_descricao;}
        set {rec_descricao = value;}
    }

    public string Rec_categoria
    {
        get { return rec_categoria; }
        set { rec_categoria = value; }
    }

    public DateTime Rec_dthrCriacao
    {
        get { return rec_dthrCriacao; }
        set { rec_dthrCriacao = value; }
    }

    public global::Funcionario Fun_id
    {
        get { return fun_id; }
        set { fun_id = value; }
    }
}