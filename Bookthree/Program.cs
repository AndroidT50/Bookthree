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

            //await AddContactAsync(phonebookManager);

            //static async Task AddContactAsync(PhonebookManager phonebookManager)
            //{

            //    Console.WriteLine("Введите имя контакта:");
            //    string name = Console.ReadLine();

            //    Console.WriteLine("Введите email'ы контакта (через запятую):");
            //    string emailsInput = Console.ReadLine();
            //    List<string> emails = new List<string>(emailsInput.Split(','));
            //    Contact contact = new Contact
            //    {
            //        Name = name,
            //        Email = emails
            //    };
            //    await phonebookManager.AddContactAsync(contact);
            //}
            //await phonebookManager.PrintALL();
            await SearchByNameAsync(phonebookManager);
            static async Task SearchByNameAsync (PhonebookManager phonebookManager)
            {
                Console.WriteLine("Введите Имя для поиска");
                string name = Console.ReadLine();
                await phonebookManager.SearchNameAsync(name);

            }













            //Contact contact = new Contact();



            ////Console.WriteLine("Введите имя:");
            ////contact.Name = Console.ReadLine();

            ////contact.Email = new List<string>();

            //Console.WriteLine("Введите имя для поиска");
            //string nameserchre = Console.ReadLine();
            //Console.WriteLine("Введите email:");
            //contact.Email.Add(Console.ReadLine());
            //var emails = string.Join(",", contact.Email);

            ////await PhoneBook.AddAsync(contact, emails);
            ////await PhoneBook.DisplayAllContactsAsync();

            //await PhoneBook.AddEmail(nameserchre, emails);
            //await PhoneBook.SearchNameAsync(nameserchre);
            //await PhoneBook.AddEmail();
            //Contact contact = new Contact();

            //Console.WriteLine("Введите Email для поиска");
            //contact.Email = new List<string> { Console.ReadLine() };
            //var personEmail = string.Join(",", contact.Email);


        }
    }

}
