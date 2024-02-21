//Слой доступа к данным (DAL)
using System.Data.SqlClient;

namespace Bookthree
{
    internal class DataAccess
    {
        public static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=dbtest;Trusted_Connection=True;";// строка подключения

        public static async Task InsertContactAsync(Contact contact, string emails)
        {
            try
            {
                string sqlExpression = $"INSERT INTO dbbtest (Name, Email) VALUES ('{contact.Name}', '{emails}')";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand(sqlExpression, connection);

                    // создаем параметр для имени
                    SqlParameter nameParam = new SqlParameter("@name", contact.Name);

                    command.Parameters.Add(nameParam);

                    SqlParameter emailParam = new SqlParameter("@email", emails);

                    command.Parameters.Add(emailParam);

                    int number = await command.ExecuteNonQueryAsync();
                    Console.WriteLine($"Добавлено объектов: {number}");
                }
                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static async Task DisplayAllContactsAsync()
        {
            try
            {
                string sqlExpression = "SELECT * FROM dbbtest";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows) // если есть данные
                    {
                        // выводим названия столбцов
                        string columnName1 = reader.GetName(0);
                        string columnName2 = reader.GetName(1);
                        string columnName3 = reader.GetName(2);

                        Console.WriteLine($"{columnName1}\t{columnName2}\t{columnName3}");
                    }
                    while (await reader.ReadAsync()) // построчно считываем данные
                    {
                        object id = reader.GetValue(0);
                        object name = reader.GetValue(1);
                        object mail = reader.GetValue(2);

                        Console.WriteLine($"{id} \t{name} \t{mail}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static async Task SearchPhoneNumberAsync(string phoneNumber)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string sqlExpression = "SELECT Id, Name, PhoneNumber, City, Email FROM dbbtest WHERE Name LIKE @phoneNumber";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.Parameters.AddWithValue("@PhoneNumber", "%" + phoneNumber + "%");
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    bool found = false; //флаг чтобы отследить найден ли контакт или нет
                    while (await reader.ReadAsync())
                    {
                        found = true;
                        Console.WriteLine($"Id: {reader.GetValue(0)}, Name: {reader.GetValue(1)}, Email: {reader.GetValue(2)}");
                    }
                    if (!found)
                    {
                        Console.WriteLine("Контакт не найден в базе данных");
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка {ex.Message}");
            }
        }
        public static async Task SearchNameAsync(string name)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string sqlExpression = "SELECT Id, Name, Email FROM dbbtest WHERE Name LIKE @Name";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.Parameters.AddWithValue("@Name", "%" + name + "%");
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    bool found = false; //флаг чтобы отследить найден ли контакт или нет
                    while (await reader.ReadAsync())
                    {
                        found = true;
                        Console.WriteLine($"Id: {reader.GetValue(0)}, Name: {reader.GetValue(1)}, Email: {reader.GetValue(2)}");
                    }
                    if (!found)
                    {
                        Console.WriteLine("Имя пользователя не найдено в базе данных");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка :{ex.Message}");
            }
        }
        public static async Task AddEmail(string name, string updatedEmail)
        {
            try
            {
                // Создаем подключение к базе данных
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Открываем подключение
                    await connection.OpenAsync();

                    // запрос для поиска имени человека
                    string searchQuery = "SELECT * FROM dbbtest WHERE Name = @Name";
                    using (SqlCommand command = new SqlCommand(searchQuery, connection))
                    {
                        //  параметр для имени человека
                        command.Parameters.AddWithValue("@Name", name);

                        // запрос и считываем результаты
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                // Если имя найдено,можно обновить запись
                                int personId = reader.GetInt32(reader.GetOrdinal("Id")); // Поиск  столбца Id

                                await reader.CloseAsync();

                                // метод добавления емэила
                                string updateQuery = "UPDATE dbbtest SET Email= @updatedEmail WHERE Id = @PersonId";
                                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                                {
                                    updateCommand.Parameters.AddWithValue("@UpdatedEmail", updatedEmail);
                                    updateCommand.Parameters.AddWithValue("@PersonId", personId);
                                    updateCommand.ExecuteNonQuery();
                                    Console.WriteLine($"Контакт с именем '{name}' обновлен.");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Контакт с именем '{name}' не найден в базе данных.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка:{ex.Message}");
            }
        }
    }

    internal class Contact
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        //public string PhoneNumber { get; set; }
        public List<string> Email { get; set; } = new List<string>();
    }
}
