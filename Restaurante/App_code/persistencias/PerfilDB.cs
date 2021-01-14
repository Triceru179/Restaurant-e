using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Text;
using System.Security.Cryptography;

public class PerfilDB
{
    public static DataSet validarLogin(Funcionario fun)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objDataAdapter;
        string sql = "select * from fun_funcionario where fun_email = ?fun_email and fun_senha = ?fun_senha and fun_disabled=0";
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command(sql, objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?fun_email", fun.Fun_email));
        objCommand.Parameters.Add(Mapped.Parameter("?fun_senha", fun.Fun_senha));
        objDataAdapter = Mapped.Adapter(objCommand);
        objDataAdapter.Fill(ds);
        objConexao.Close();
        objConexao.Dispose();
        objCommand.Dispose();
        
        return ds;
    }
}