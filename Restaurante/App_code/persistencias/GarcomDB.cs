using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

using System.Data;

public class GarcomDB
{
    static double i = 0;
    static string t = "";
    static DateTime now;

    public static void resetI()
    {
        now = DateTime.Now;
        t = "";
        i = 0;
    }

    public static double getI()
    {
        return i;
    }

    public static string getT()
    {
        return t;
    }

    static void incI()
    {
        DateTime last = now;
        now = DateTime.Now;
        StackTrace st = new StackTrace();
        i += now.Subtract(last).TotalMilliseconds;
        t += now.Subtract(last).TotalMilliseconds + " " + st.GetFrame(1).GetMethod().Name + "<br>";
        now = DateTime.Now;
    }

    public static DataSet selectMesa()
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "select * from mes_mesa mes left join (select c2.* from (select max(cc.com_id) as max_id, cc.mes_id from com_comanda cc group by cc.mes_id) c inner join com_comanda c2 on c2.com_id = c.max_id) com on com.mes_id = mes.mes_id and com.com_disabled = 0 where mes.mes_disabled = 0 group by mes.mes_id; ";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        incI();
        return ds;
    }

    public static Comanda selectComanda(Mesa mes)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "select * from com_comanda where mes_id = ?mes_id and com_disabled = 0 order by com_id desc limit 1";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?mes_id", mes.Mes_id));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        Comanda c = new Comanda();
        c.Com_id = -1;
        if (ds.Tables[0].Rows.Count == 1 && ds.Tables[0].Rows[0]["com_id"] != null)
        {
            c.Com_id = Convert.ToInt32(ds.Tables[0].Rows[0]["com_id"]);
            c.Com_foiFinalizada = Convert.ToInt32(ds.Tables[0].Rows[0]["com_foiFinalizada"]);
            c.Com_foiPaga = Convert.ToInt32(ds.Tables[0].Rows[0]["com_foiPaga"]);
        }

        incI();
        return c;
    }

    public static void createComanda(Comanda com)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "INSERT INTO com_comanda (com_dthrCriacao, com_foiPaga, com_foiFinalizada, fun_id, mes_id) VALUES (?com_dthrCriacao, ?com_foiPaga, ?com_foiFinalizada, ?fun_id, ?mes_id);";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?com_dthrCriacao", com.DthrCriacao));
        objCommand.Parameters.Add(Mapped.Parameter("?com_foiPaga", com.Com_foiPaga));
        objCommand.Parameters.Add(Mapped.Parameter("?com_foiFinalizada", com.Com_foiFinalizada));
        objCommand.Parameters.Add(Mapped.Parameter("?fun_id", com.Fun_id.Fun_id));
        objCommand.Parameters.Add(Mapped.Parameter("?mes_id", com.Mes_id.Mes_id));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        incI();
    }

    public static void finalizarComanda(Comanda com)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "UPDATE com_comanda SET com_foiFinalizada = 1 WHERE com_id = ?com_id";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?com_id", com.Com_id));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        incI();
    }

    public static void deleteComanda(Comanda com, ComandaCancelada coc, Funcionario fun)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "UPDATE com_comanda SET com_disabled = 1 WHERE com_id = ?com_id";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?com_id", com.Com_id));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();
        
        coc.Com_id = com;

        deleteComandaMotivo(coc, fun);

        incI();
    }

    public static void deleteComandaMotivo(ComandaCancelada coc, Funcionario fun)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "INSERT INTO coc_comandaCancelada (coc_motivo, coc_dthrCancelada, com_id, fun_id) VALUES (?coc_motivo, ?coc_dthrCancelada, ?com_id, ?fun_id)";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?coc_motivo", coc.Coc_motivo));
        objCommand.Parameters.Add(Mapped.Parameter("?coc_dthrCancelada", coc.Coc_dthrCancelada));
        objCommand.Parameters.Add(Mapped.Parameter("?com_id", coc.Com_id.Com_id));
        objCommand.Parameters.Add(Mapped.Parameter("?fun_id", fun.Fun_id));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        incI();
    }

    public static void createPedido(Comanda com)
    {
        DateTime dt = new DateTime();
        dt = DateTime.Now;

        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "INSERT INTO ped_pedidos (ped_dthrCriacao, ped_foiEntregue, ped_valor, com_id) VALUES (?ped_dthrCriacao, 0, 0, ?com_id);";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?ped_dthrCriacao", dt));
        objCommand.Parameters.Add(Mapped.Parameter("?com_id", com.Com_id));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        incI();
    }

    public static void deletePedido(Pedidos ped)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "UPDATE ped_pedidos SET ped_disabled = 1 WHERE ped_id = ?ped_id";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?ped_id", ped.Ped_id));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        incI();
    }

    public static DataSet selectAllPedido(Comanda com)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "select * from ped_pedidos where ped_disabled = 0 and com_id = ?com_id; ";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?com_id", com.Com_id));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        incI();
        return ds;
    }

    public static DataSet selectPedido(Comanda com)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "select * from ped_pedidos where com_id = ?com_id and ped_disabled = 0";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?com_id", com.Com_id));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        incI();
        return ds;
    }

    public static DataSet selectItensNoPedido(Comanda com, Pedidos ped)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "select * from pnp_produtosnopedido join ped_pedidos using (ped_id) join pro_produto using (pro_id) where (pnp_disabled = 0 or pnp_disabled = 2) and com_id = ?com_id and ped_id = ?ped_id";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?com_id", com.Com_id));
        objCommand.Parameters.Add(Mapped.Parameter("?ped_id", ped.Ped_id));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        incI();
        return ds;
    }

    public static void createPedidoCancelado(Pedidos ped, PedidoCancelado pec)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "INSERT INTO pec_pedidoCancelado (pec_motivo, pec_dthrCancelado , ped_id) VALUES (?pec_motivo, ?pec_dthrCancelado, ?ped_id);";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?pec_motivo", pec.Pec_motivo));
        objCommand.Parameters.Add(Mapped.Parameter("?pec_dthrCancelado", pec.Pec_dthrCancelado));
        objCommand.Parameters.Add(Mapped.Parameter("?ped_id", ped.Ped_id));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        deletePedido(ped);

        incI();
    }

    public static void solicitarPreparo(Pedidos ped)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "UPDATE ped_pedidos SET ped_solicitarPreparo = 1, ped_valor = ?ped_valor, ped_dthrCriacao = ?ped_dthrCriacao WHERE ped_id = ?ped_id";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?ped_id", ped.Ped_id));
        objCommand.Parameters.Add(Mapped.Parameter("?ped_valor", ped.Ped_valor));
        objCommand.Parameters.Add(Mapped.Parameter("?ped_dthrCriacao", ped.Ped_dthrCriacao));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        incI();
    }

    public static void insertProdutoNoPedido(ProdutosNoPedido pnp, Pedidos ped, Produto pro)
    {
        DateTime dt = new DateTime();
        dt = DateTime.Now;

        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "INSERT INTO pnp_produtosNoPedido (pnp_dthrCozinha, pnp_quantidade, pnp_valor, pnp_observacao, ped_id, pro_id) VALUES (?pnp_dthrCozinha, ?pnp_quantidade, ?pnp_valor, ?pnp_observacao, ?ped_id, ?pro_id);";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?pnp_dthrCozinha", pnp.Pnp_dthrCozinha));
        objCommand.Parameters.Add(Mapped.Parameter("?pnp_quantidade", pnp.Pnp_quantidade));
        objCommand.Parameters.Add(Mapped.Parameter("?pnp_valor", pnp.Pnp_valor));
        objCommand.Parameters.Add(Mapped.Parameter("?pnp_observacao", pnp.Pnp_observacao));
        objCommand.Parameters.Add(Mapped.Parameter("?ped_id", ped.Ped_id));
        objCommand.Parameters.Add(Mapped.Parameter("?pro_id", pro.Pro_id));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        incI();
    }

    public static void removerProdutoNoPedido(ProdutosNoPedido pnp)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "UPDATE pnp_produtosNoPedido SET pnp_disabled = 1 WHERE pnp_id = ?pnp_id";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?pnp_id", pnp.Pnp_id));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        incI();
    }

    public static DataSet selectIfComandaPodeFinalizar(Comanda com)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "select * from ped_pedidos ped left join(select count(pnp.pnp_id), sum(pnp.pnp_foiFeito), pnp.ped_id, sum(pnp.pnp_disabled)/2 from pnp_produtosnopedido pnp where (pnp.pnp_disabled = 0 OR pnp.pnp_disabled = 2) group by ped_id) pn using (ped_id) where ped.com_id = ?com_id and ped.ped_disabled = 0;";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?com_id", com.Com_id));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        incI();
        return ds;
    }

    public static DataSet selectIfPodeSolicitarPedido(Pedidos ped)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "select count(pnp.pnp_id) from pnp_produtosnopedido pnp where pnp.ped_id = ?ped_id and pnp.pnp_disabled = 0; ";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?ped_id", ped.Ped_id));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        incI();
        return ds;
    }
}