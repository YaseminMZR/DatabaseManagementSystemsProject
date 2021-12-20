using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Odbc;
using Npgsql;


namespace SHOE_S
{
    class PostgreSQL
    {
        public static readonly NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Database=SHOE'S;Username=postgres;Password=postgres;");
        public static NpgsqlCommand command = new NpgsqlCommand();
        public static NpgsqlDataAdapter adapter;

        public PostgreSQL()
        {
            command.Connection = connection;
        }

        public void sqlIslem(String sql)
        {
            command.CommandText = sql;
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public String sqlString(String sql)
        {
            command.CommandText = sql;
            connection.Open();
            sql = command.ExecuteScalar().ToString();
            connection.Close();
            return sql;
        }

        public DataTable sqlTablo(String sql)
        {
            DataTable tablo = new DataTable();
            command.CommandText = sql;
            connection.Open();
            adapter = new NpgsqlDataAdapter(command);
            adapter.Fill(tablo);
            connection.Close();
            return tablo;
        }
    }
}
