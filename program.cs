using System;
using System.Data.SqlClient; // Подключаем пространство имен для работы с SQL Server

namespace AdoNetLesson3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 4. Создайте строку подключения и соединение с помощью SqlConnection.
            // ВАЖНО: Замените Server, Database, User ID и Password на свои значения.
            // Если у вас Windows Authentication, используйте "Integrated Security=True" вместо User ID/Password.
            string connectionString = @"Server=localhost;Database=MyTestDB;Integrated Security=True;TrustServerCertificate=True;";

            // Используем конструкцию using, чтобы соединение автоматически закрылось после завершения блока
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Открываем соединение
                    connection.Open();
                    Console.WriteLine("Соединение успешно установлено!");

                    // 5. Создайте команду с помощью SqlCommand на языке sql с запросом SELECT.
                    // Замените 'Users' на имя вашей таблицы
                    string sqlQuery = "SELECT Id, Name FROM Users";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        // 6. Выведите в консоль результат запроса с помощью SqlDataReader.
                        // ExecuteReader() возвращает объект SqlDataReader для чтения данных построчно
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("\nРезультат запроса:");
                            Console.WriteLine("--------------------------------");

                            // Цикл while читает строки, пока они есть (метод Read() возвращает true, пока есть данные)
                            while (reader.Read())
                            {
                                // Получаем данные по имени колонки или по индексу
                                // reader.GetInt32(0) - получить первое поле как int
                                // reader.GetString(1) - получить второе поле как string
                                int id = reader.GetInt32(0);
                                string name = reader.GetString(1);

                                Console.WriteLine($"ID: {id}, Name: {name}");
                            }
                            Console.WriteLine("--------------------------------");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка: " + ex.Message);
                }
            } // Здесь соединение закроется автоматически

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}