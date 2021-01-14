using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;

public class AdminDB
{
    public static DataSet validarEmail(Funcionario fun)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "select * from fun_funcionario where fun_email = ?fun_email and fun_disabled=0";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?fun_email", fun.Fun_email));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        return ds;
    }

    public static void insertFuncionario(Funcionario fun)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "INSERT INTO fun_funcionario (fun_nome, fun_telefone, fun_email, fun_senha, fun_permissao) VALUES ( ?fun_nome, ?fun_telefone, ?fun_email, ?fun_senha, ?fun_permissao)";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?fun_nome", fun.Fun_nome));
        objCommand.Parameters.Add(Mapped.Parameter("?fun_telefone", fun.Fun_telefone));
        objCommand.Parameters.Add(Mapped.Parameter("?fun_email", fun.Fun_email));
        objCommand.Parameters.Add(Mapped.Parameter("?fun_senha", fun.Fun_senha));
        objCommand.Parameters.Add(Mapped.Parameter("?fun_permissao", fun.Fun_permissao));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();
    }

    public static DataSet selectFuncionario()
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "select * from fun_funcionario where fun_disabled=0";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        return ds;
    }

    public static void updateFun(Funcionario fun)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "UPDATE fun_funcionario SET fun_nome=?fun_nome, fun_telefone=?fun_telefone, fun_email=?fun_email, fun_senha=?fun_senha, fun_permissao=?fun_permissao WHERE fun_id=?fun_id and fun_disabled=0";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?fun_nome", fun.Fun_nome));
        objCommand.Parameters.Add(Mapped.Parameter("?fun_telefone", fun.Fun_telefone));
        objCommand.Parameters.Add(Mapped.Parameter("?fun_email", fun.Fun_email));
        objCommand.Parameters.Add(Mapped.Parameter("?fun_senha", fun.Fun_senha));
        objCommand.Parameters.Add(Mapped.Parameter("?fun_permissao", fun.Fun_permissao));
        objCommand.Parameters.Add(Mapped.Parameter("?fun_id", fun.Fun_id));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();
    }

    public static void updateFunSemTrocaSenha(Funcionario fun)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "UPDATE fun_funcionario SET fun_nome=?fun_nome, fun_telefone=?fun_telefone, fun_email=?fun_email, fun_permissao=?fun_permissao WHERE fun_id=?fun_id and fun_disabled=0";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?fun_nome", fun.Fun_nome));
        objCommand.Parameters.Add(Mapped.Parameter("?fun_telefone", fun.Fun_telefone));
        objCommand.Parameters.Add(Mapped.Parameter("?fun_email", fun.Fun_email));
        objCommand.Parameters.Add(Mapped.Parameter("?fun_permissao", fun.Fun_permissao));
        objCommand.Parameters.Add(Mapped.Parameter("?fun_id", fun.Fun_id));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();
    }

    public static DataSet validarEmailUpdate(Funcionario fun)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "select * from fun_funcionario where fun_email=?fun_email and fun_id<>?fun_id and fun_disabled=0";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?fun_email", fun.Fun_email));
        objCommand.Parameters.Add(Mapped.Parameter("?fun_id", fun.Fun_id));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        return ds;
    }

    public static void removerFuncionario(Funcionario fun)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "UPDATE fun_funcionario SET fun_disabled=1 WHERE fun_id=?fun_id";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?fun_id", fun.Fun_id));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();
    }

    /* pesquisas da página Dashboard */

    public static DataSet selectQuantidadeFuncionario()
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "SELECT COUNT(fun_id) FROM fun_funcionario WHERE fun_disabled = 0;";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        return ds;
    }

    public static DataSet selectReclamacao()
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "select * from rec_reclamacao join fun_funcionario using(fun_id) where rec_disabled = 0";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        return ds;
    }

    public static DataSet selectQuantidadeReclamacao()
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "SELECT COUNT(rec_id) FROM rec_reclamacao WHERE rec_disabled = 0;";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        return ds;
    }

    public static DataSet selectQuantidadeComandaPorMes(int ano)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "SELECT COUNT(com_id), month(com_dthrCriacao) FROM com_comanda WHERE com_disabled = 0 AND com_foiPaga = 1 AND YEAR(com_dthrCriacao) = ?ano GROUP BY month(com_dthrCriacao) ORDER BY month(com_dthrCriacao);";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?ano", ano));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        return ds;
    }

    public static DataSet selectQuantidadeComandaEfetivada(int ano)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "SELECT COUNT(com_id), com_disabled FROM com_comanda WHERE ((com_disabled = 0 AND com_foiPaga = 1) OR (com_disabled = 1)) AND YEAR(com_dthrCriacao) = ?ano GROUP BY com_disabled ORDER BY monthname(com_dthrCriacao);";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?ano", ano));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        return ds;
    }

    public static DataSet selectEntradaNesteDia(int dia, int ano)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "SELECT SUM(cai_valortotal) FROM cai_caixa WHERE cai_disabled = 0 AND DAY(cai_dthrPagamento) = ?dia  AND YEAR(cai_dthrPagamento) = ?ano;";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?dia", dia));
        objCommand.Parameters.Add(Mapped.Parameter("?ano", ano));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        return ds;
    }

    public static DataSet selectEntradaNesteMes(int mes, int ano)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "SELECT SUM(cai_valortotal) FROM cai_caixa WHERE cai_disabled = 0 AND MONTH(cai_dthrPagamento) = ?mes  AND YEAR(cai_dthrPagamento) = ?ano;";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?mes", mes));
        objCommand.Parameters.Add(Mapped.Parameter("?ano", ano));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        return ds;
    }

    public static DataSet selectEntradaNesteAno(int ano)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "SELECT SUM(cai_valortotal) FROM cai_caixa WHERE cai_disabled = 0 AND YEAR(cai_dthrPagamento) = ?ano GROUP BY YEAR(cai_dthrPagamento);";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?ano", ano));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        return ds;
    }

    public static DataSet selectMediaPedidosPorComanda(int ano)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "SELECT count(ped.ped_id) FROM com_comanda com JOIN ped_pedidos ped using(com_id) WHERE com_dthrCriacao = ?ano GROUP BY com.com_id;";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?ano", ano));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        return ds;
    }
    
    public static DataSet selectRendaPorDia(string data, int ano)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "SELECT cai_valorTotal as 'total_dia', day(cai_dthrPagamento) as 'dia', month(cai_dthrPagamento) as 'mes', year(cai_dthrPagamento) as 'ano' FROM cai_caixa WHERE cai_disabled = 0 AND week(cai_dthrPagamento) = week(?data) AND YEAR(cai_dthrPagamento) = ?ano GROUP BY dia ORDER BY mes, dia; ";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?data", data));
        objCommand.Parameters.Add(Mapped.Parameter("?ano", ano));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        return ds;
    }

    public static DataSet selectRendaPorMes(int ano)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "SELECT SUM(cai_valorTotal) as 'total_mes', month(cai_dthrPagamento) as 'mes', year(cai_dthrPagamento) as 'ano' FROM cai_caixa WHERE cai_disabled = 0 AND YEAR(cai_dthrPagamento) = ?ano GROUP BY mes ORDER BY month(cai_dthrPagamento); ";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?ano", ano));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        return ds;
    }

    public static DataSet selectRendaPorAno(int ano)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "SELECT SUM(cai_valorTotal) as 'total_ano', year(cai_dthrPagamento) as 'ano' FROM cai_caixa WHERE cai_disabled = 0 AND(YEAR(cai_dthrPagamento) > ?ano - 5 AND YEAR(cai_dthrPagamento) < ?ano + 5) GROUP BY ano ORDER BY year(cai_dthrPagamento); ";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?ano", ano));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        return ds;
    }

    public static DataSet selectTodosProdutos(int ano)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "SELECT pro_nome as 'Nome', IF(pnp_quantidade is NULL, 0, sum(pnp_quantidade)) as 'Quantidade', IF(pnp_id is NULL, 0, sum(pnp_quantidade * pnp_valor)) as 'Valor' FROM pro_produto pro LEFT JOIN pnp_produtosnopedido pnp ON pnp.pro_id = pro.pro_id AND pnp.pnp_foiFeito = 1 AND pnp.pnp_disabled = 0 AND year(pnp.pnp_dthrCozinha) = ?ano GROUP BY pro.pro_id ORDER BY(pnp_valor * pnp_quantidade) DESC; ";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?ano", ano));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        return ds;
    }

    public static DataSet selectTodosProdutos5Limit(int ano)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "SELECT pro_nome AS 'nome', sum(pnp_quantidade) AS 'quantidade', sum(pnp_valor * pnp_quantidade) as 'Valor' FROM pnp_produtosnopedido JOIN pro_produto using (pro_id) WHERE pnp_foiFeito = 1 AND pnp_disabled = 0 AND year(pnp_dthrCozinha) = ?ano GROUP BY pro_id ORDER BY quantidade DESC LIMIT 5; ";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?ano", ano));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        return ds;
    }

    public static DataSet selectTodosProdutos5LimitOffset(int ano)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "SELECT pro_nome AS 'nome', sum(pnp_quantidade) AS 'quantidade', sum(pnp_valor * pnp_quantidade) as 'Valor' FROM pnp_produtosnopedido JOIN pro_produto using (pro_id) WHERE pnp_foiFeito = 1 AND pnp_disabled = 0 AND year(pnp_dthrCozinha) = ?ano GROUP BY pro_id ORDER BY quantidade DESC LIMIT 5, 11111111; ";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?ano", ano));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        return ds;
    }

    public static DataSet selectTodosProdutosValor5Limit(int ano)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "SELECT pro_nome AS 'nome', sum(pnp_quantidade) AS 'quantidade', sum(pnp_valor * pnp_quantidade) as 'Valor' FROM pnp_produtosnopedido JOIN pro_produto using (pro_id) WHERE pnp_foiFeito = 1 AND pnp_disabled = 0 AND year(pnp_dthrCozinha) = ?ano GROUP BY pro_id ORDER BY quantidade DESC LIMIT 5; ";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?ano", ano));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        return ds;
    }

    public static DataSet selectTodosProdutosValor5LimitOffset(int ano)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "SELECT pro_nome AS 'nome', sum(pnp_quantidade) AS 'quantidade', sum(pnp_valor * pnp_quantidade) as 'Valor' FROM pnp_produtosnopedido JOIN pro_produto using (pro_id) WHERE pnp_foiFeito = 1 AND pnp_disabled = 0 AND year(pnp_dthrCozinha) = ?ano GROUP BY pro_id ORDER BY quantidade DESC LIMIT 5, 11111111; ";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?ano", ano));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        return ds;
    }

    /* Pesquisas para histórico */

    public static DataSet selectHistoricoPnp(int ano)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "select * from pnp_produtosnopedido pnp join pro_produto pro using (pro_id) where year(pnp_dthrCozinha) = ?ano;";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?ano", ano));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        return ds;
    }

    public static DataSet selectHistoricoProduto()
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "select * from pro_produto";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        return ds;
    }

    public static DataSet selectHistoricoPagamento(int ano)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "select * from cai_caixa cai where year(cai_dthrPagamento) = ?ano;";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?ano", ano));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        return ds;
    }

    public static DataSet selectHistoricoComanda(int ano)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "select * from com_comanda com where year(com_dthrCriacao) = ?ano;";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?ano", ano));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        return ds;
    }

    public static DataSet selectHistoricoComandaCancelada(int ano)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "select * from coc_comandaCancelada coc where year(coc_dthrCancelada) = ?ano;";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?ano", ano));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        return ds;
    }

    public static DataSet selectHistoricoPedido(int ano)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "select * from ped_pedidos ped where year(ped_dthrCriacao) = ?ano;";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?ano", ano));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();

        return ds;
    }
}