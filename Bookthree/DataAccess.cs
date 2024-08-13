//Слой доступа к данным (DAL)
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using System.Xml.Linq;

namespace Bookthree
{
    public class Contact
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string ? PhoneNumber { get; set; }
        public List<string> Email { get; set; } = new List<string>();
    }
    internal class DataAccess
    {
        public static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=dbtest;Trusted_Connection=True;";// строка подключения

        public async Task InsertContactAsync(Contact contact)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                        string emails = string.Join(" ", contact.Email);
                        await connection.OpenAsync();

                        string sqlExpression = $"INSERT INTO dbbtest (Name, Email, PhoneNumber) VALUES ('{contact.Name}','{emails}','{contact.PhoneNumber}')";

                        SqlCommand command = new SqlCommand(sqlExpression, connection);

                        // создаем параметр для имени
                        SqlParameter nameParam = new SqlParameter("@Name", contact.Name);

                        command.Parameters.Add(nameParam);
                        SqlParameter phoneParam = new SqlParameter("PhoneNumber",contact.PhoneNumber);
                        command.Parameters.Add(phoneParam);

                        SqlParameter emailParam = new SqlParameter("@Email", emails);

                        command.Parameters.Add(emailParam);

                        int number = await command.ExecuteNonQueryAsync();
                        Console.WriteLine($"Добавлено объектов: {number}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async Task<List<(string Name,string PhoneNumber, string Email)>> DisplayAllContactsAsync()
        {
            List<(string Name, string PhoneNumber, string Email)>contacts = new List<(string, string, string)>();
            try
            {
                string sqlExpression = "SELECT * FROM dbbtest";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                  
                    while (await reader.ReadAsync()) 
                    {
                        contacts.Add((reader["Name"].ToString(),reader["PhoneNumber"].ToString(),reader["Email"].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
              Console.WriteLine(ex.Message);
            }
            return contacts;
        }
        public async Task<List<(string Name,string Email,string PhoneNumber)>> SearchPhoneNumberAsync(string phoneNumber)
        {

            List<(string Name, string Email,string PhoneNumber)> contacts = new List<(string, string, string)>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string sqlExpression = "SELECT Id, Name, PhoneNumber,Email FROM dbbtest WHERE PhoneNumber LIKE @PhoneNumber";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.Parameters.AddWithValue("@PhoneNumber", "%" + phoneNumber + "%");
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        contacts.Add((reader["Name"].ToString(), reader["Email"].ToString(), reader["PhoneNumber"].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка {ex.Message}");
            }
            return contacts;
        }
        public async Task<List <(string Name, string PhoneNumber,string Email)>> SearchNameAsync(string name)
        {
            List<(string Name, string PhoneNumber, string Email)> contacts = new List<(string, string,string)>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string sqlExpression = "SELECT Id, Name, Email FROM dbbtest WHERE Name LIKE @Name";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.Parameters.AddWithValue("@Name", "%" + name + "%");
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    
                    while (await reader.ReadAsync())
                    {
                        contacts.Add((reader["Name"].ToString(),reader["PhoneNamber"].ToString(),reader["Email"].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка :{ex.Message}");
            }
            return contacts;
        }
        public async  Task<List<(string Name, string Email)>> AddInsertAsync(string name, string updatedEmail)
        {
            List<(string Name, string Email)> contacts = new List<(string, string)>();
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
                            if( await reader.ReadAsync())
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
                                    await updateCommand.ExecuteNonQueryAsync();
                                    contacts.Add((name, updatedEmail));
                                }

                            }
                            
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка:{ex.Message}");
            }
            return contacts;
        }
    }
  
}
