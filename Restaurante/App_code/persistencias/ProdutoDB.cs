using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;

public class ProdutoDB
{
    public static void inserirProduto(Produto pro)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "INSERT INTO pro_produto (pro_nome, pro_valor, pro_descricao, pro_complemento, pro_disponivel) VALUES ( ?pro_nome, ?pro_valor, ?pro_descricao, ?pro_complemento, ?pro_disponivel )";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?pro_nome", pro.Pro_nome));
        objCommand.Parameters.Add(Mapped.Parameter("?pro_valor", pro.Pro_valor));
        objCommand.Parameters.Add(Mapped.Parameter("?pro_descricao", pro.Pro_descricao));
        objCommand.Parameters.Add(Mapped.Parameter("?pro_complemento", pro.Pro_complemento));
        objCommand.Parameters.Add(Mapped.Parameter("?pro_disponivel", pro.Pro_disponivel));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();
    }

    public static DataSet selectProduto()
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "select * from pro_produto where pro_disabled=0";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        return ds;
    }

    public static void updateProduto(Produto pro)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "UPDATE pro_produto SET pro_nome = ?pro_nome, pro_descricao = ?pro_descricao, pro_valor = ?pro_valor, pro_complemento = ?pro_complemento, pro_disponivel = ?pro_disponivel WHERE pro_id = ?pro_id and pro_disabled = 0";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?pro_id", pro.Pro_id));
        objCommand.Parameters.Add(Mapped.Parameter("?pro_nome", pro.Pro_nome));
        objCommand.Parameters.Add(Mapped.Parameter("?pro_valor", pro.Pro_valor));
        objCommand.Parameters.Add(Mapped.Parameter("?pro_descricao", pro.Pro_descricao));
        objCommand.Parameters.Add(Mapped.Parameter("?pro_complemento", pro.Pro_complemento));
        objCommand.Parameters.Add(Mapped.Parameter("?pro_disponivel", pro.Pro_disponivel));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();
    }

    public static void removerProduto(Produto pro)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "UPDATE pro_produto SET pro_disabled=1 WHERE pro_id = ?pro_id";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?pro_id", pro.Pro_id));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();
    }
}