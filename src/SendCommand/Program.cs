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

namespace SendCommand {

    enum Command {
        Execute,
        Query
    }

    class Program {

        static bool Query(NpgsqlConnection connection, string command) {
            var data = connection.Query<dynamic>(command).ToList();
            DynamicTable.From(data).Write();
            return true;
        }

        static bool Execute(NpgsqlConnection connection, string command) {
            var data = connection.Execute(command);
            Console.WriteLine("Affect {0} rows", data);
            return true;
        }

        static void Main(
            FileInfo file,
            string host = "localhost",
            string database = "postgres",
            int port = 5432,
            string user = "postgres",
            string sql = "",
            Command command = Command.Query,
            string password = "1234") {

            var conn = $"Host={host};Database={database};User Id={user};Password={password}";

            using (var connection = new NpgsqlConnection(conn)) {
                connection.Open();

                if (command == Command.Query)
                    if (file != null && file.Exists) {
                        var text = File.ReadAllText(file.FullName);
                        Query(connection, text);
                    } else {
                        Query(connection, sql);
                    }

                if (command == Command.Execute) {
                    if (file != null && file.Exists) {
                        var text = File.ReadAllText(file.FullName);
                        Execute(connection, text);
                    } else {
                        Execute(connection, sql);
                    }
                }
            }
        }
    }
}