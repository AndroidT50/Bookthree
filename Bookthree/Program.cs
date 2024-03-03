using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Bookthree;

namespace ConsoleApp14
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            PhonebookManager phonebookManager = new PhonebookManager();

            while (true)
            {
                Console.WriteLine("Введите цифру от 1-5 где:");
                Console.WriteLine("1: Добавить запись\n2: Найти контакт по имени\n3: найти контакт по номеру телефона\n4: Отредактировать Email\n5: Вывести все контакты");
                Console.WriteLine("Для выхода введите 5 ");
                int namber = int.Parse(Console.ReadLine());
                Console.Clear();
                switch (namber)
                {
                    case 1:
                        await AddContactAsync(phonebookManager); break;
                    case 2:
                        await SearchByNameAsync(phonebookManager); break;
                    case 3:
                        await SearchPhoneAsync(phonebookManager); break;
                    case 4:
                        await AddInsertAsync(phonebookManager); break;
                    case 5:
                        await phonebookManager.PrintALL(); break;
                    case 6:
                        return;
                        default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                        
                }
               
            }
            static async Task AddContactAsync(PhonebookManager phonebookManager)
            {
                Console.WriteLine("Введите имя контакта:");
                string ? name = Console.ReadLine();
                Console.WriteLine("Введите номер тефона ");
                string namberPhone = Console.ReadLine();
                Console.WriteLine("Введите email'ы контакта (через запятую):");
                string ? emailsInput = Console.ReadLine();
                List<string> emails = new List<string>(emailsInput.Split(','));
                Contact contact = new Contact
                {
                    Name = name,
                    PhoneNumber = namberPhone,
                    Email = emails
                };
                await phonebookManager.AddContactAsync(contact);
            }
            static async Task SearchByNameAsync(PhonebookManager phonebookManager)
            {
                Console.WriteLine("Введите Имя для поиска");
                string ? name = Console.ReadLine();
                await phonebookManager.SearchNameAsync(name);
            }
            static async Task SearchPhoneAsync(PhonebookManager phonebookManager)
            {
                Console.WriteLine("Введите номер телефона для поиска");
                string ? phoneNam = Console.ReadLine();
                await phonebookManager.SearchPhoneAsync(phoneNam);
            }
            static async Task AddInsertAsync(PhonebookManager phonebookManager)
            {
                Console.WriteLine("Ввести имя для вставки");
                string ? name =Console.ReadLine();
                Console.WriteLine("Введите email'ы контакта (через запятую):");
                string ? email = Console.ReadLine();
                await phonebookManager.AddInsertAsync(name, email);
            }
        }
    }

}
