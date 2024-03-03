using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookthree
{
    internal class PhonebookManager
    {

        private DataAccess dataAccess;

        public PhonebookManager()
        {
            dataAccess = new DataAccess();
        }
        public async Task AddContactAsync(Contact contact)
        {
            await dataAccess.InsertContactAsync(contact);
            Console.WriteLine($"Контакт: {contact.Name}, успешно добавлен в базу данных.");
        }
        public async Task PrintALL()
        {
            var contacts = await dataAccess.DisplayAllContactsAsync();
            if (contacts.Count == 0)
            {
                Console.WriteLine("Контакты не найден");

            }
            foreach (var contact in contacts)
            {
                string name = contact.Name ?? "N/A";
                string pnoNumber = contact.PhoneNumber ??"N/A";
                string email = contact.Email ??"N/A";
                Console.WriteLine($"Имя:{name},PhoneNumber: {pnoNumber},Email:{email}");
            }
        }
        public async Task SearchNameAsync(string name)
        {
            var contacts = await dataAccess.SearchNameAsync(name);
            if (contacts.Count == 0)
            {
                Console.WriteLine("Контакт не найден");

            }
            foreach (var contact in contacts)
            {
                Console.WriteLine($"Имя:{contact.Name},PhoneNumber:{contact.PhoneNumber}, Email:{contact.Email}");
            }
        }

        public async Task SearchPhoneAsync(string phoneNamber)
        {
            var contacts = await dataAccess.SearchPhoneNumberAsync(phoneNamber);
            if (contacts.Count == 0)
            {
                Console.WriteLine("Контакт не найден");

            }
            foreach (var contact in contacts)
            {
                Console.WriteLine($"Имя:{contact.Name},PhoneNumber:{contact.PhoneNumber}, Email:{contact.Email}");
            }
        }

        public async Task AddInsertAsync(string name, string updatedEmail)
        {
            var contacts = await dataAccess.AddInsertAsync(name,updatedEmail);
            if (contacts.Count == 0)
            {
                Console.WriteLine("Контакт не найден");

            }
            foreach (var contact in contacts)
            {
                Console.WriteLine($"Контакт с именем '{name}' обновлен.");
                Console.WriteLine($"Имя:{contact.Name}, Email:{contact.Email}");
            }
        }

}   } 
