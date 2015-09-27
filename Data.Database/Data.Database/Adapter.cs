using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;


namespace Data.Database
{
    public class Adapter
    {
        const string consKeyDefaultCnnString = "ConnStringLocal"; //Para uso local
//       const string consKeyDefaultCnnString = "ConnStringExpress"; //Para uso con con sql express
//        const string consKeyDefaultCnnString = "ConnStringSereverISI"; //Para usar en clases
        public SqlConnection sqlConn;

        protected void OpenConnection()
        {
            String conn = ConfigurationManager.ConnectionStrings[consKeyDefaultCnnString].ConnectionString;  
            sqlConn = new SqlConnection(conn);
            sqlConn.Open();
        }

        protected void CloseConnection()
        {
            sqlConn.Close();
            sqlConn = null;
        }

        protected SqlDataReader ExecuteReader(String commandText)
        {
            throw new Exception("Metodo no implementado");
        }       
    }
}
