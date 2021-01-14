using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;

public class GerenteDB
{
    public static void insertMesa(Mesa mes)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "INSERT INTO mes_mesa (mes_identificacao) VALUES ( ?mes_identificacao )";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?mes_identificacao", mes.Mes_identificacao));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();
    }

    public static DataSet selectMesa()
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "select * from mes_mesa where mes_disabled=0";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        return ds;
    }

    public static void updateMesa(Mesa mes)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "UPDATE mes_Mesa SET mes_Identificacao=?mes_identificacao WHERE mes_id=?mes_id and mes_disabled=0";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?mes_id", mes.Mes_id));
        objCommand.Parameters.Add(Mapped.Parameter("?mes_identificacao", mes.Mes_identificacao));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();
    }

    public static void removerMesa(Mesa mes)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "UPDATE mes_mesa SET mes_disabled=1 WHERE mes_id = ?mes_id";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?mes_id", mes.Mes_id));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();
    }

    public static void insertReclamacao(Reclamacao rec)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "INSERT INTO rec_reclamacao (rec_descricao, rec_categoria, rec_dthrCriacao, fun_id) VALUES ( ?rec_descricao, ?rec_categoria, ?rec_dthrCriacao, ?fun_id )";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?rec_descricao", rec.Rec_descricao));
        objCommand.Parameters.Add(Mapped.Parameter("?rec_categoria", rec.Rec_categoria));
        objCommand.Parameters.Add(Mapped.Parameter("?rec_dthrCriacao", rec.Rec_dthrCriacao));
        objCommand.Parameters.Add(Mapped.Parameter("?fun_id", rec.Fun_id.Fun_id));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();
    }

    public static DataSet selectReclamacao()
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "select * from rec_reclamacao where rec_disabled=0";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        return ds;
    }

    public static void updateReclamacao(Reclamacao rec)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "UPDATE rec_reclamacao SET rec_descricao = ?rec_descricao, rec_categoria = ?rec_categoria, fun_id = ?fun_id  WHERE rec_id = ?rec_id";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?rec_descricao", rec.Rec_descricao));
        objCommand.Parameters.Add(Mapped.Parameter("?rec_categoria", rec.Rec_categoria));
        objCommand.Parameters.Add(Mapped.Parameter("?fun_id", rec.Fun_id.Fun_id));
        objCommand.Parameters.Add(Mapped.Parameter("?rec_id", rec.Rec_id));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();
    }

    public static void deleteReclamacao(Reclamacao rec)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "UPDATE rec_reclamacao SET rec_disabled = 1 WHERE rec_id = ?rec_id";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?rec_id", rec.Rec_id));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();
    }
}