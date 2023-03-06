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
    public class OwnerService
    {
        private readonly OwnerRepository _ownerRepository;

        public OwnerService()
        {
            _ownerRepository = new OwnerRepository();
        }
        public void GetAll()
        {
            var owners = _ownerRepository.GetAll();
            ConsoleHelper.WriteWithColor(" --- All Owners ---", ConsoleColor.DarkGray);

            foreach (var owner in owners)
            {
                ConsoleHelper.WriteWithColor($"ID:{owner.ID},Fullname: {owner.Name} {owner.Surname}", ConsoleColor.Gray);
            }
        } 
        public void Update()
        {
            GetAll();
        IDDesc: ConsoleHelper.WriteWithColor(" Enter an owner's ID,please", ConsoleColor.Cyan);
            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (isSucceeded == null)
            {
                ConsoleHelper.WriteWithColor(" The entered ID is not a correct format!", ConsoleColor.Red);
                goto IDDesc;
            }
            var owner = _ownerRepository.Get(id);
            if (owner == null)
            {
                ConsoleHelper.WriteWithColor(" This ID doesn't contain any owner!", ConsoleColor.Red);
            }

             ConsoleHelper.WriteWithColor(" Enter a new owner's name,please", ConsoleColor.Cyan);
                string name = Console.ReadLine();
                ConsoleHelper.WriteWithColor(" Enter a new owner's surname,please", ConsoleColor.Cyan);
                string surname = Console.ReadLine();

                owner.Name = name;
                owner.Surname = surname;
                _ownerRepository.Update(owner);
            



        }
        public void Create()
        {
            ConsoleHelper.WriteWithColor(" Enter owner's name,please", ConsoleColor.White);
            string name = Console.ReadLine();
            ConsoleHelper.WriteWithColor(" Enter owner's surname,please", ConsoleColor.White);
            string surname = Console.ReadLine();
            var owner = new Owner()
            {
                Name = name,
                Surname = surname,
                CreatedAt = DateTime.Now,
            };
            _ownerRepository.Add(owner);
            ConsoleHelper.WriteWithColor($" Name: {owner.Name}, Surname: {owner.Surname}", ConsoleColor.Green);
        }
        public void Delete()
        {
            GetAll();
        IDDesc: ConsoleHelper.WriteWithColor(" Enter an owner's ID,please", ConsoleColor.Cyan);
            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor(" The entered ID is not a correct format!", ConsoleColor.Red);
                goto IDDesc;
            }
            var owner = _ownerRepository.Get(id);
            if (owner == null)
            {
                ConsoleHelper.WriteWithColor(" This ID doesn't contain any owner!", ConsoleColor.Red);
            }
            var teacher = _ownerRepository.Get(id);
            if (teacher == null)
            {
                ConsoleHelper.WriteWithColor(" The entered id doesn't contain any owner!", ConsoleColor.Red);
            }
            _ownerRepository.Delete(owner);
            ConsoleHelper.WriteWithColor(" The owner is successfully deleted ", ConsoleColor.Green);

        }
    }

}
