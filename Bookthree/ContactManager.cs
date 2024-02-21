using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookthree
{
    internal class ContactManager
    {
        public readonly ContactRepository _contactRepository;
        ContactManager() {  _contactRepository = new ContactRepository(); }

        public void AddContact(Contact contact)
        {
            contact.Email = new List<string>();
            Console.WriteLine("Введите имя:");
            string name = Console.ReadLine();
            Console.WriteLine("ВВедите email:");
            contact.Email.Add(Console.ReadLine());
            var email = string.Join(",", contact.Email);


        }


    }
}
