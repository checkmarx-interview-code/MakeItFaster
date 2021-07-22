using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MakeItFaster
{

    class Program
    {
        static void Main(string[] args)
        {

            var connectionString = @"Server=127.0.0.1\sqlexpress;Database=MakeItFaster;Trusted_Connection=True;";

            var messages = new MessagesGenerator().GenerateMessagesList(10000);


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var command = new SqlCommand(@"
                    IF NOT object_id('Messages') IS NULL
                        DROP TABLE Messages;
                    CREATE TABLE Messages(ID uniqueidentifier PRIMARY KEY, [From] varchar(100), [To] varchar(100), Subject varchar(4000), TimeStamp datetime);
                ", connection);
                command.ExecuteNonQuery();

                var started = DateTime.Now;

                foreach (var message in messages)
                {
                    command = new SqlCommand(@"
                        INSERT INTO Messages(ID, [From], [To], Subject, TimeStamp) 
                        VALUES (@ID, @From, @To, @Subject, @TimeStamp);
                    ", connection);
                    command.Parameters.AddRange(new SqlParameter[]
                    {
                        new SqlParameter("@ID", message.Id),
                        new SqlParameter("@From", message.From),
                        new SqlParameter("@To", message.To),
                        new SqlParameter("@Subject", message.Subject),
                        new SqlParameter("@TimeStamp", message.TimeStamp),
                    });
                    command.ExecuteNonQuery();
                }

                var t = (DateTime.Now - started);
                Console.WriteLine($"Time it took: {t.Hours:00}:{t.Minutes:00}:{t.Seconds:00}.{t.Milliseconds:000}");

            }
        }

      
    }
}