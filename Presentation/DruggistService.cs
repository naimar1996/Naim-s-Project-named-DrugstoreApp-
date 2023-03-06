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
    public class DruggistService
    {
        private readonly DruggistRepository _druggistRepository;
        private readonly DrugstoreRepository _drugstoreRepository;

        public DruggistService()
        {
            _druggistRepository = new DruggistRepository();
            _drugstoreRepository = new DrugstoreRepository();
        }

        public void GetAll()
        {
            var druggists = _druggistRepository.GetAll();
            ConsoleHelper.WriteWithColor(" --- All Druggists --- ", ConsoleColor.Gray);
            foreach (var druggist in druggists)
            {
                ConsoleHelper.WriteWithColor($"Fullname:{druggist.Name} {druggist.Surname}, Age:{druggist.Age}, Experience{druggist.Experience}", ConsoleColor.Yellow);
            }

        }
        public void GetAllDruggistsByDrugstore()
        {
            var drugstores = _drugstoreRepository.GetAll();
            foreach(var drugstore in drugstores)
            {
                ConsoleHelper.WriteWithColor($" ID:{drugstore.ID}, Name:{drugstore.Name}, Address:{drugstore.Address}, Contact number:{drugstore.ContactNumber}, Email:{drugstore.Email}", ConsoleColor.Yellow);
            }
          DIDDesc: ConsoleHelper.WriteWithColor(" Enter an ID of the drugstore,please",ConsoleColor.Gray);
            int drugstoreID;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out drugstoreID);
            if(!isSucceeded )
            {
                ConsoleHelper.WriteWithColor(" The entered ID is not a correct format!", ConsoleColor.Red);
                goto DIDDesc;
            }

            var dbDrugstore = _drugstoreRepository.Get(drugstoreID);
            if( dbDrugstore is null ) 
            {
                ConsoleHelper.WriteWithColor(" This ID doesn't contain any drugstore!", ConsoleColor.Red);
            }
            else
            {
                foreach(var druggist in dbDrugstore.Druggists)
                {
                    ConsoleHelper.WriteWithColor($" ID:{druggist.ID}, Fullname:{druggist.Surname} {druggist.Name}, Age:{druggist.Age},Experience:{druggist.Experience}", ConsoleColor.Yellow);
                }

            }

        }
        public void Update()
        {
            GetAll();
        DruggistIDDesc: ConsoleHelper.WriteWithColor(" Enter a druggist's ID", ConsoleColor.Gray);
            int druggistID;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out druggistID);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor(" The entered ID is not a correct format!", ConsoleColor.Red);
                goto DruggistIDDesc;
            }
            var dbDruggist = _druggistRepository.Get(druggistID);
            if (dbDruggist == null)
            {
                ConsoleHelper.WriteWithColor(" This ID doesn't contain any duggist!", ConsoleColor.Red);
            }

            ConsoleHelper.WriteWithColor(" Enter a new druggist's name ", ConsoleColor.Gray);
            string name = Console.ReadLine();

            ConsoleHelper.WriteWithColor(" Enter a new druggist's surname ", ConsoleColor.Gray);
            string surname = Console.ReadLine();

        AgeDesc: ConsoleHelper.WriteWithColor(" Enter a new druggist's age ", ConsoleColor.Gray);
            byte druggistAge;
            isSucceeded = byte.TryParse(Console.ReadLine(), out druggistAge);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("The entered age is not a correct format!", ConsoleColor.Red);
                goto AgeDesc;
            }
        ExperienceDesc: ConsoleHelper.WriteWithColor(" Enter a new druggist's experience ", ConsoleColor.Gray);
            byte druggistExperience;
            isSucceeded = byte.TryParse(Console.ReadLine(), out druggistExperience);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("The entered age is not a correct format!", ConsoleColor.Red);
                goto ExperienceDesc;
            }
            if (druggistExperience <= druggistAge - 18)
            {
                ConsoleHelper.WriteWithColor(" The experience doesn't fit with the age!", ConsoleColor.Red);
                goto ExperienceDesc;
            }


            dbDruggist.Name = name;
            dbDruggist.Surname = surname;
            dbDruggist.Age = druggistAge;
            dbDruggist.Experience = druggistExperience;





        }
        public void Create()
        {

            if (_drugstoreRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor(" Create an drugstore firstly!", ConsoleColor.Red);
            }
            else
            {
                ConsoleHelper.WriteWithColor(" Enter the druggist's name,please", ConsoleColor.Gray);
                string name = Console.ReadLine();
                ConsoleHelper.WriteWithColor(" Enter the druggist's surname,please", ConsoleColor.Gray);
                string surname = Console.ReadLine();
            AgeDesc: ConsoleHelper.WriteWithColor(" Enter the druggest's age,please", ConsoleColor.Gray);
                byte age;
                bool isSucceded = byte.TryParse(Console.ReadLine(), out age);
                if (!isSucceded)
                {
                    ConsoleHelper.WriteWithColor(" The entered age is not a correct format!", ConsoleColor.Red);
                    goto AgeDesc;
                }

            ExpDesc: ConsoleHelper.WriteWithColor(" Enter the druggist's experience,please", ConsoleColor.Gray);
                byte experience;
                isSucceded = byte.TryParse(Console.ReadLine(), out experience);
                if (!isSucceded)
                {
                    ConsoleHelper.WriteWithColor(" The entered experience is not correct format!", ConsoleColor.Red);
                    goto ExpDesc;
                }
                if (experience <= age - 18)
                {
                    ConsoleHelper.WriteWithColor(" The experience doesn't fit with the age!", ConsoleColor.Red);
                    goto ExpDesc;
                }

                var drugstores = _drugstoreRepository.GetAll();
                foreach (var drugstore in drugstores)
                {
                    ConsoleHelper.WriteWithColor($" ID: {drugstore.ID},Name:{drugstore.Name},Address:{drugstore.Address},Contact number{drugstore.ContactNumber},Email:{drugstore.Email}", ConsoleColor.DarkYellow);
                }
            IdDesc: ConsoleHelper.WriteWithColor(" Enter an ID of the drugstore,please", ConsoleColor.Gray);
                int drugstoreID;
                bool isSucceeded = int.TryParse(Console.ReadLine(), out drugstoreID);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("The entered Id is not a correct format!", ConsoleColor.Red);
                    goto IdDesc;
                }
                var dbDrugstore = _drugstoreRepository.Get(drugstoreID);
                if (dbDrugstore == null)
                {
                    ConsoleHelper.WriteWithColor(" This ID doesn't contain any drugstore!", ConsoleColor.Red);
                }

                var druggist = new Druggist()
                {
                    Name = name,
                    Surname = surname,
                    Age = age,
                    Experience = experience,
                    Drugstore = dbDrugstore,
                };
                dbDrugstore.Druggists.Add(druggist);

                _druggistRepository.Add(druggist);
                ConsoleHelper.WriteWithColor($"The druggist is successfully created with \n Fullname: {druggist.Name} {druggist.Surname},Age:{druggist.Age}, Experience:{druggist.Experience}", ConsoleColor.White);
            }
        }
        public void Delete()
        {
            GetAll();
        EnterIDDesc: ConsoleHelper.WriteWithColor(" Enter the druggist's ID,please", ConsoleColor.Gray);
            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor(" The entered ID is not a correct format!", ConsoleColor.Red);
                goto EnterIDDesc;
            }
            var druggist = _druggistRepository.Get(id);
            if (druggist == null)
            {
                ConsoleHelper.WriteWithColor(" This ID doesn't contain any druggist!", ConsoleColor.Red);
            }
            _druggistRepository.Delete(druggist);
        }







    }
}
