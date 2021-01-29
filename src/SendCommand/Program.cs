using System;
using Dapper;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using DynamicTables;
using System.Dynamic;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace SendCommand {

    enum Command {
        Execute,
        Query
    }

    enum Dbms {
        PosgreSQL,
        MySQL
    }

    class Program {

        static bool Query(DbConnection connection, string command, int limit) {
            var data = connection.Query<dynamic>(command).Take(limit);
            var format = Format.Minimal;
            DynamicTable.From(data).Write(format);
            return true;
        }

        static bool Execute(DbConnection connection, string command) {
            var data = connection.Execute(command);
            Console.WriteLine("Affect {0} rows", data);
            return true;
        }

        static DbConnection CreateConnection(string connecitonString, Dbms dbms) {
            return dbms switch {
                Dbms.PosgreSQL => new NpgsqlConnection(connecitonString),
                Dbms.MySQL => new MySqlConnection(connecitonString)
            };
        }

        /// <summary>
        ///  wk-send-command
        /// </summary>
        /// <param name="file"></param>
        /// <param name="host"></param>
        /// <param name="database"></param>
        /// <param name="port"></param>
        /// <param name="user"></param>
        /// <param name="limit"></param>
        /// <param name="sql"></param>
        /// <param name="command"></param>
        /// <param name="dbms"></param>
        /// <param name="password"></param>
        static void Main(
            FileInfo file,
            string host = "localhost",
            string database = "postgres",
            int port = 5432,
            string user = "postgres",
            int limit = 20,
            string sql = "",
            Command command = Command.Query,
            Dbms dbms = Dbms.MySQL,
            string password = "1234") {

            var conn = $"Host={host};Database={database};Port={port};User Id={user};Password={password}";

            using var connection = CreateConnection(conn, dbms);
            connection.Open();

            if (command == Command.Query) {
                Console.WriteLine();
                if (file?.Exists == true) {
                    var text = File.ReadAllText(file.FullName);
                    Query(connection, text, limit);
                } else {
                    Query(connection, sql, limit);
                }
            }
            if (command == Command.Execute) {
                Console.WriteLine();
                if (file?.Exists == true) {
                    var text = File.ReadAllText(file.FullName);
                    Execute(connection, text);
                } else {
                    Execute(connection, sql);
                }
            }

            connection.Close();
        }
    }
}