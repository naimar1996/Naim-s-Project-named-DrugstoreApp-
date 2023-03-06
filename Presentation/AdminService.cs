using Core.ConsoleHelper;
using Core.Entities;
using Data.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public class AdminService
    {
        private readonly AdminRepository _adminRepository;

        public AdminService()
        {
            _adminRepository = new AdminRepository();
        }
        public Admin Authorize()
        {
        LoginDesc: ConsoleHelper.WriteWithColor(" --- Login --- ", ConsoleColor.DarkGray);
            Console.WriteLine();
            ConsoleHelper.WriteWithColor(" Enter username, please", ConsoleColor.Gray);
            string username = Console.ReadLine();
            ConsoleHelper.WriteWithColor(" Enter password, please", ConsoleColor.Gray);
            string password = Console.ReadLine();
            var admin = _adminRepository.GetByUsernameAndPassword(username, password);
            if(admin == null)
            {
                ConsoleHelper.WriteWithColor(" The entered username or password is not a correct format!", ConsoleColor.Red);
                goto LoginDesc;
            }
            return admin;

        }




    }
}
