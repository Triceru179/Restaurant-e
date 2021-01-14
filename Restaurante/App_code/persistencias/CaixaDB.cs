using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;

public class CaixaDB
{
    public static DataSet selectPagamentoPendente()
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "select * from ped_pedidos ped join com_comanda com on ped.com_id = com.com_id and com.com_foiFinalizada = 1 join mes_mesa mes on com.mes_id = mes.mes_id where ped.ped_foiPago = 0 and ped.ped_foiEntregue = 1 and com_disabled = 0 and ped.ped_disabled = 0;";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        return ds;
    }

    public static void realizarPagamento(Caixa cai)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "UPDATE ped_pedidos SET ped_foiPago = 1 WHERE ped_id = ?ped_id";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?ped_id", cai.Ped_id.Ped_id));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        inserirPagamento(cai);
    }

    public static void cancelarPagamento(Caixa cai)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "UPDATE ped_pedidos SET ped_disabled = 2 WHERE ped_id = ?ped_id";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?ped_id", cai.Ped_id.Ped_id));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();
    }

    public static DataSet validarConcluir(Caixa cai)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "select * from com_comanda com left join(select count(ped.ped_id), sum(ped.ped_foiPago), ped.com_id, ped.ped_disabled from ped_pedidos ped where (ped.ped_disabled = 0 or ped.ped_disabled = 2) group by ped_id) pn using (com_id) where com.com_id = ?com_id and com.com_disabled = 0; ";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?com_id", cai.Com_id.Com_id));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        return ds;
    }

    public static void concluirComanda(Caixa cai)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "UPDATE com_comanda SET com_foiPaga = 1 WHERE com_id = ?com_id";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?com_id", cai.Com_id.Com_id));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();
    }

    public static DataSet selectItensNoPedido(Caixa cai)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "select * from pnp_produtosnopedido join ped_pedidos using (ped_id) join pro_produto using (pro_id) where (pnp_disabled = 0 or pnp_disabled = 2) and com_id = ?com_id and ped_id = ?ped_id";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?com_id", cai.Com_id.Com_id));
        objCommand.Parameters.Add(Mapped.Parameter("?ped_id", cai.Ped_id.Ped_id));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();
        
        return ds;
    }

    public static void inserirPagamento(Caixa cai)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "INSERT INTO cai_caixa (cai_dthrPagamento, cai_valorTotal, cai_gorjeta, cai_descricao, fun_id, ped_id, com_id) VALUES (?cai_dthrPagamento, ?cai_valorTotal, ?cai_gorjeta, ?cai_descricao, ?fun_id, ?ped_id, ?com_id)";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?cai_dthrPagamento", DateTime.Now));
        objCommand.Parameters.Add(Mapped.Parameter("?cai_valorTotal", cai.Cai_valorTotal));
        objCommand.Parameters.Add(Mapped.Parameter("?cai_gorjeta", 0));
        objCommand.Parameters.Add(Mapped.Parameter("?cai_descricao", "Pagamento de pedido"));
        objCommand.Parameters.Add(Mapped.Parameter("?fun_id", cai.Fun_id.Fun_id));
        objCommand.Parameters.Add(Mapped.Parameter("?ped_id", cai.Ped_id.Ped_id));
        objCommand.Parameters.Add(Mapped.Parameter("?com_id", cai.Com_id.Com_id));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();
    }

    public static void inserirPagamentoNaoAgendado(Caixa cai)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "INSERT INTO cai_caixa (cai_dthrPagamento, cai_valorTotal, cai_gorjeta, cai_descricao, fun_id, ped_id, com_id) VALUES (?cai_dthrPagamento, ?cai_valorTotal, ?cai_gorjeta, ?cai_descricao, ?fun_id, ?ped_id, ?com_id)";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?cai_dthrPagamento", DateTime.Now));
        objCommand.Parameters.Add(Mapped.Parameter("?cai_valorTotal", cai.Cai_valorTotal));
        objCommand.Parameters.Add(Mapped.Parameter("?cai_gorjeta", cai.Cai_gorjeta));
        objCommand.Parameters.Add(Mapped.Parameter("?cai_descricao", cai.Cai_descricao));
        objCommand.Parameters.Add(Mapped.Parameter("?fun_id", cai.Fun_id.Fun_id));
        objCommand.Parameters.Add(Mapped.Parameter("?ped_id", null));
        objCommand.Parameters.Add(Mapped.Parameter("?com_id", null));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();
    }
}