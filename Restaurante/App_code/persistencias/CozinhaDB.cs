using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;

public class CozinhaDB
{
    public static DataSet selectPedidoPendente()
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "select * from ped_pedidos ped join pnp_produtosnopedido pnp using (ped_id) join pro_produto pro using (pro_id) join com_comanda using (com_id) join mes_mesa using (mes_id) where ped_solicitarPreparo = 1 and pnp_foiFeito = 0 and ped.ped_disabled = 0 and com_disabled = 0 and pnp.pnp_disabled = 0 order by ped_dthrCriacao asc; ";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        return ds;
    }

    public static void prepararProduto(ProdutosNoPedido pnp)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "UPDATE pnp_produtosnopedido SET pnp_foiFeito=1, pnp_dthrCozinha=?pnp_dthrCozinha WHERE pnp_id=?pnp_id";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?pnp_dthrCozinha", pnp.Pnp_dthrCozinha));
        objCommand.Parameters.Add(Mapped.Parameter("?pnp_id", pnp.Pnp_id));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();
    }

    public static void cancelarProduto(ProdutosNoPedido pnp, Produto pro)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "UPDATE pnp_produtosnopedido SET pnp_disabled=2, pnp_dthrCozinha=?pnp_dthrCozinha WHERE pnp_id=?pnp_id";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?pnp_dthrCozinha", pnp.Pnp_dthrCozinha));
        objCommand.Parameters.Add(Mapped.Parameter("?pnp_id", pnp.Pnp_id));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();
        
        subtrairValorPedido(pnp, pro);
    }

    public static void subtrairValorPedido(ProdutosNoPedido pnp, Produto pro)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "UPDATE ped_pedidos SET ped_valor = (ped_valor - ?pro_valor) WHERE ped_id = ?ped_id;";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?ped_id", pnp.Ped_id.Ped_id));
        objCommand.Parameters.Add(Mapped.Parameter("?pro_valor", (pnp.Pnp_quantidade * pro.Pro_valor)));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();
    }

    public static DataSet verificarEntregarPedido(Pedidos ped)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "select * from ped_pedidos ped left join(select count(pnp.pnp_id), pnp.ped_id, sum(pnp.pnp_foiFeito), sum(pnp.pnp_disabled)/2 from pnp_produtosnopedido pnp where (pnp.pnp_disabled = 0 OR pnp.pnp_disabled = 2) group by ped_id) pn using (ped_id) where ped.ped_id = ?ped_id and ped.ped_disabled = 0;";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?ped_id", ped.Ped_id));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        return ds;
    }

    public static void entregarPedido(Pedidos ped)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "UPDATE ped_pedidos SET ped_foiEntregue = 1 WHERE ped_id = ?ped_id";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?ped_id", ped.Ped_id));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();
    }
}