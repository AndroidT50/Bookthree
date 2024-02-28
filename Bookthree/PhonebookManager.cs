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
            Console.WriteLine("Контакт успешно добавлен в базу данных.");
        }

        public async Task PrintALL()
        {
            var contacts = await dataAccess.DisplayAllContactsAsync();
            foreach (var item in contacts)
            {
                Console.WriteLine($"Имя: {item.Name}, Email: {item.Email}"); 
            }
        }

        public async Task SearchPhoneAsync(string name)
        {
            var contacts = await dataAccess.SearchPhoneNumberAsync(name);
            if(contacts.Count == 0)
            {
                Console.WriteLine("Контакт не найден");
                return;
            }
            foreach (var contact in contacts)
            {
                Console.WriteLine($"{contact.Name},{contact.Email}");
            }

        }

    }

}
