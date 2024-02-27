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

        }

    }

}
