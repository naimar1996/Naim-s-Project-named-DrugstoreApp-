using Core.ConsoleHelper;
using Core.Entities;
using Core.Extensions;
using Data.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Presentation
{
    public class DrugstoreService
    {
        private readonly DrugstoreRepository _drugstoreRepository;
        private readonly OwnerRepository _ownerRepository;
        private readonly DrugRepository _drugRepository;

        public DrugstoreService()
        {
            _drugstoreRepository = new DrugstoreRepository(); 
            _ownerRepository = new OwnerRepository();
            _drugRepository = new DrugRepository();
        }

        public void GetAll()
        {
            var drugstores = _drugstoreRepository.GetAll();
            ConsoleHelper.WriteWithColor(" --- All Drugstores --- ", ConsoleColor.Green);

            foreach (var drugstore in drugstores)
            {
                ConsoleHelper.WriteWithColor($"ID:{drugstore.ID},Name:{drugstore.Name}, Address:{drugstore.Address}, Contact number:{drugstore.ContactNumber}, Email:{drugstore.Email}", ConsoleColor.Gray);
            }
        }
        public void GetAllDrugstoresByOwner()
        {
            var owners = _ownerRepository.GetAll();
            foreach (var owner in owners)
            {
                ConsoleHelper.WriteWithColor($" ID:{owner.ID}, Name:{owner.Name}, Surname:{owner.Surname}", ConsoleColor.Yellow);
            }

           OwnerIDDesc: ConsoleHelper.WriteWithColor(" Enter an ID of the owner", ConsoleColor.Gray);
            int ownerID;
            bool isSucceeded = int.TryParse(Console.ReadLine(),out ownerID);
            if(!isSucceeded)
            {
                ConsoleHelper.WriteWithColor(" The entered ID is not a correct format!", ConsoleColor.Red);
                goto OwnerIDDesc;
            }

            var dbOwner = _ownerRepository.Get(ownerID);
            if(dbOwner == null)
            {
                ConsoleHelper.WriteWithColor(" This ID doesn't contain any owner!",ConsoleColor.Red);
            }
            else
            {
                foreach (var drugstore in dbOwner.Drugstores)
                {
                    ConsoleHelper.WriteWithColor($" Name:{drugstore.Name}, Address:{drugstore.Address},Contact number:{drugstore.ContactNumber}, Email:{drugstore.Email}", ConsoleColor.Yellow);
                }
            }
        }
        public void Create()
        {
            if (_ownerRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor(" Create an owner firstly,please!", ConsoleColor.Red);
            }
            else
            {
                ConsoleHelper.WriteWithColor(" Enter a name of the drugstore,please", ConsoleColor.White);
                string name = Console.ReadLine();

                ConsoleHelper.WriteWithColor(" Enter an address of the drugstore,please", ConsoleColor.White);
                string address = Console.ReadLine();

            ContactNumberDesc: ConsoleHelper.WriteWithColor(" Enter a contact number of the drugstore", ConsoleColor.White);
                string contactNumber = Console.ReadLine();
                if (!contactNumber.IsContactNumber()) 
                {
                    ConsoleHelper.WriteWithColor(" The entered contact number is not a correct format!", ConsoleColor.Red);
                    goto ContactNumberDesc;
                }

            EmailDesc: ConsoleHelper.WriteWithColor(" Enter an email of the drugstore,please", ConsoleColor.White);
                string email = Console.ReadLine();
                if (!email.IsEmail())
                {
                    ConsoleHelper.WriteWithColor(" The entered email is not a correct format!", ConsoleColor.Red);
                    goto EmailDesc;
                }
                if (_drugstoreRepository.IsDublicatedEmail(email))
                {
                    ConsoleHelper.WriteWithColor(" The entered email is already used!", ConsoleColor.Red);
                    goto EmailDesc;
                }

                var owners = _ownerRepository.GetAll();
                foreach (var owner in owners)
                {
                    ConsoleHelper.WriteWithColor($" ID: {owner.ID},Fullname: {owner.Name} {owner.Surname}");

                }
            OwnerIDDesc: ConsoleHelper.WriteWithColor(" Enter owner's ID,please", ConsoleColor.Green);
                int ownerID;
               bool isSucceeded = int.TryParse(Console.ReadLine(), out ownerID);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor(" The entered owner's Id is not a correct format", ConsoleColor.Red);
                    goto OwnerIDDesc;
                }
                var dbOwner = _ownerRepository.Get(ownerID);
                if (dbOwner == null)
                {
                    ConsoleHelper.WriteWithColor("This ID doesn't contain any owne", ConsoleColor.Red);
                }


                var drugstore = new Drugstore()
                {
                    Name = name,
                    Address = address,
                    ContactNumber = contactNumber,
                    Email = email,
                    Owner = dbOwner
                };
                dbOwner.Drugstores.Add(drugstore);

                _drugstoreRepository.Add(drugstore);
                ConsoleHelper.WriteWithColor($" The drugstore succesfully created with \n Name:{drugstore.Name}, Address:{drugstore.Address}, Contact number:{drugstore.ContactNumber}, Email:{drugstore.Email}", ConsoleColor.Green);

            }
        }
        public void Update()
        {
            GetAll();
        IDDesc: ConsoleHelper.WriteWithColor(" Enter an ID of the drugstore,please", ConsoleColor.Gray);
            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor(" The entered ID is not a correct format!", ConsoleColor.Red);
                goto IDDesc;
            }
            var drugstore = _drugstoreRepository.Get(id);
            if (drugstore == null)
            {
                ConsoleHelper.WriteWithColor(" The entered ID doesn't contain any drugstore!", ConsoleColor.Red);
            }

            ConsoleHelper.WriteWithColor(" Enter a new name of the drugstore,please", ConsoleColor.White);
            string name = Console.ReadLine();

            ConsoleHelper.WriteWithColor(" Enter a new address of the drugstore,please", ConsoleColor.White);
            string address = Console.ReadLine();

           ContactNumberDesc: ConsoleHelper.WriteWithColor(" Enter a new contact number of the drugstore,please", ConsoleColor.White);
            string contactNumber = Console.ReadLine();
            if (!contactNumber.IsContactNumber())
            {
                ConsoleHelper.WriteWithColor(" The entered contact number is not a correct format!", ConsoleColor.Red);
                goto ContactNumberDesc;
            }


            drugstore.Name = name;
            drugstore.Address = address;
            drugstore.ContactNumber = contactNumber;

            _drugstoreRepository.Update(drugstore);




        }
        public void Delete()
        {
            GetAll();
        IDDesc: ConsoleHelper.WriteWithColor(" Enter an ID of the drugstore", ConsoleColor.Gray);
            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor(" The entered ID is not a correct format!", ConsoleColor.Red);
                goto IDDesc;
            }
            var drugstore = _drugstoreRepository.Get(id);
            if (drugstore == null)
            {
                ConsoleHelper.WriteWithColor(" This ID doesn't contain any drugstore!", ConsoleColor.Red);
            }
            _drugstoreRepository.Delete(drugstore);
            ConsoleHelper.WriteWithColor(" The drugstore is successfully deleted", ConsoleColor.Green);

        }
        public void Sale()
        {
            var drugstores = _drugstoreRepository.GetAll();
            foreach (var drugstore in drugstores)
            {
                ConsoleHelper.WriteWithColor($"ID:{drugstore.ID},Name:{drugstore.Name}, Address:{drugstore.Address}, Contact number:{drugstore.ContactNumber}, Email:{drugstore.Email}", ConsoleColor.Gray);
            }
        IDDesc: ConsoleHelper.WriteWithColor(" Enter an ID of the drugstore", ConsoleColor.Gray);
            int drugstoreID;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out drugstoreID);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor(" The entered ID is not a correct format!", ConsoleColor.Red);
                goto IDDesc;
            }
            var dbDrugstore = _drugstoreRepository.Get(drugstoreID);
            if (dbDrugstore == null)
            {
                ConsoleHelper.WriteWithColor("This ID doesn't contain any drugstore!", ConsoleColor.Red);
            }

            var drugs = _drugRepository.GetAllDrugsByDrugstore(drugstoreID);
            foreach (var drug in drugs)
            {
                ConsoleHelper.WriteWithColor($"ID:{drug.ID}, Name:{drug.Name},Price:{drug.Price}, Count:{drug.Count}", ConsoleColor.Gray);
            }
        DrugIdDesc: ConsoleHelper.WriteWithColor(" Enter an ID of the drug,please", ConsoleColor.Cyan);
            int drugID;
            isSucceeded = int.TryParse(Console.ReadLine(), out drugID);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor(" The entered ID is not a correct format!", ConsoleColor.Red);
                goto DrugIdDesc;
            }
            var dbDrug = _drugRepository.Get(drugID);
            if (dbDrug == null)
            {
                ConsoleHelper.WriteWithColor(" This ID doesn't contain any drug!", ConsoleColor.Red);
            }
        EnterCountDesc: ConsoleHelper.WriteWithColor(" Enter the count of the drug,please", ConsoleColor.Gray);
            int count;
            isSucceeded = int.TryParse(Console.ReadLine(), out count);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor(" The entered count is not a correct format!", ConsoleColor.Red);
                goto EnterCountDesc;
            }

            if (dbDrug.Count < count)
            {
                ConsoleHelper.WriteWithColor(" There is not the count of what you want in this drugstore!", ConsoleColor.Red);
                goto EnterCountDesc;
            }
            else
            {
                var totalPrice = count * dbDrug.Price;
                ConsoleHelper.WriteWithColor($"Total price:{totalPrice}", ConsoleColor.Gray);
                dbDrug.Price = totalPrice;
            }

            ConsoleHelper.WriteWithColor($"Count:{dbDrug.Count}", ConsoleColor.Gray);
            var drugsInDrugstore = dbDrug.Count - count;
            dbDrug.Count = drugsInDrugstore;

           
           










        }
    }
}

























