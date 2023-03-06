using Data.Repositories.Concrete;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.ConsoleHelper;
using System.Collections.Immutable;

namespace Presentation
{
    public class DrugService
    {
        private readonly DrugRepository _drugRepository; 
        private readonly DrugstoreRepository _drugstoreRepository;
        public DrugService()
        {
            _drugRepository = new DrugRepository();
            _drugstoreRepository = new DrugstoreRepository();
        }

        public void GetAll()
        {
            var drugs = _drugRepository.GetAll();
            ConsoleHelper.WriteWithColor(" --- All Drugs --- ", ConsoleColor.Green);
            foreach (var drug in drugs)
            {
                ConsoleHelper.WriteWithColor($" Name:{drug.Name}, Price:{drug.Price},Count:{drug.Count}", ConsoleColor.Gray);
            }
        }
        public void GetAllDrugsByDrugstore()
        {
            var drugstores = _drugstoreRepository.GetAll();
            foreach (var drugstore in drugstores)
            {
                ConsoleHelper.WriteWithColor($"ID:{drugstore.ID}, Name:{drugstore.Name}, Address:{drugstore.Address},Contact number:{drugstore.ContactNumber},Email:{drugstore.Email}", ConsoleColor.Gray);
            }
        IDDe: ConsoleHelper.WriteWithColor(" Enter an ID of The drugstore,please", ConsoleColor.Gray);
            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor(" The entered ID is not a correct format!", ConsoleColor.Red);
                goto IDDe;
            }
            var dbDrugstore = _drugstoreRepository.Get(id);
            if (dbDrugstore == null)
            {
                ConsoleHelper.WriteWithColor(" This ID doesn't contain any drugstore!", ConsoleColor.Red);
            }
            else
            {
                foreach (var drug in dbDrugstore.Drugs)
                {
                    ConsoleHelper.WriteWithColor($"ID: {drug.ID},Name: {drug.Name}, Price: {drug.Price},Count:{drug.Count}", ConsoleColor.Green);
                }
                if (dbDrugstore.Drugs.Count == 0)
                {
                    ConsoleHelper.WriteWithColor("This drugstore doesn't contain any drug!", ConsoleColor.Red);
                }
            }
        }
        public void Filter()
        {
            GetAllDrugsByDrugstore();
           FetchPriceDesc: ConsoleHelper.WriteWithColor(" Fetch drugs that cost less than the entered price", ConsoleColor.Gray);
            decimal price;
            bool isSucceeded = decimal.TryParse(Console.ReadLine(), out price);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor(" The entered price is not a correct format!",ConsoleColor.Red);
                goto FetchPriceDesc;
            }
            var dbPrice = _drugRepository.Filter(price);
            {
                foreach(var drugPrice in dbPrice)
                {
                    ConsoleHelper.WriteWithColor($"ID:{drugPrice.ID}, Name:{drugPrice.Name},Price:{drugPrice.Price},Count:{drugPrice.Count}", ConsoleColor.Yellow);
                }
            }

            
        }
        public void Update()
        {
            GetAll();
        EnterIDDesc: ConsoleHelper.WriteWithColor(" Enter the ID of the drug", ConsoleColor.Gray);
            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("The entered ID is not a correct format!", ConsoleColor.Red);
                goto EnterIDDesc;
            }

            var drug = _drugRepository.Get(id);
            if (drug == null)
            {
                ConsoleHelper.WriteWithColor(" This ID doesn't contain any drug!", ConsoleColor.Red);
            }

            ConsoleHelper.WriteWithColor(" Enter a new name of the drug,please", ConsoleColor.Gray);
            string name = Console.ReadLine();

        PriceDesc: ConsoleHelper.WriteWithColor(" Enter a new price of the drug,please", ConsoleColor.Gray);
            decimal price;
            isSucceeded = decimal.TryParse(Console.ReadLine(), out price);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor(" The entered price is not a correct format!", ConsoleColor.Red);
                goto PriceDesc;
            }

        CountDesc: ConsoleHelper.WriteWithColor(" Enter a count of the drug,please", ConsoleColor.Gray);
            int count;
            isSucceeded = int.TryParse(Console.ReadLine(), out count);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor(" The entered count is not correct format!", ConsoleColor.Red);
                goto CountDesc;
            }
            if (count <= 0)
            {
                ConsoleHelper.WriteWithColor(" The count must not be less than or equaled to zero!", ConsoleColor.Red);
                goto CountDesc;
            }

            drug.Name = name;
            drug.Price = price;
            drug.Count = count;
            _drugRepository.Update(drug);





        }
        public void Create()
        {

            if (_drugstoreRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor(" Create a drugstore firstly,please!", ConsoleColor.Red);
            }
            else
            {
                ConsoleHelper.WriteWithColor(" Enter a name of the drug,please", ConsoleColor.White);
                string name = Console.ReadLine();
            PriceDesc: ConsoleHelper.WriteWithColor(" Enter a price of the drug,please", ConsoleColor.White);
                decimal price;
                bool isSucceeded = decimal.TryParse(Console.ReadLine(), out price);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor(" The entered price is not a correct format!", ConsoleColor.Red);
                    goto PriceDesc;
                }
            EnterDrugDesc: ConsoleHelper.WriteWithColor(" Enter the count of the drug,please", ConsoleColor.White);
                int count;
                isSucceeded = int.TryParse(Console.ReadLine(), out count);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor(" The entered count is not a correct format!", ConsoleColor.Red);
                }
                if (count <= 0)
                {
                    ConsoleHelper.WriteWithColor(" The count must not be less than or equaled to zero!", ConsoleColor.Red);
                    goto EnterDrugDesc;
                }

                var drugstores = _drugstoreRepository.GetAll();

                foreach (var drugstore in drugstores)
                {
                    ConsoleHelper.WriteWithColor($"ID:{drugstore.ID},Name:{drugstore.Name},Address:{drugstore.Address},Contact number:{drugstore.ContactNumber},Email:{drugstore.Email}", ConsoleColor.White);
                }
            DrgIDDesc: ConsoleHelper.WriteWithColor(" Enter the ID of the Drugstore,please", ConsoleColor.Gray);
                int drugstoreID;
                isSucceeded = int.TryParse(Console.ReadLine(), out drugstoreID);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor(" The entered drugstoreID is not a correct format!", ConsoleColor.Red);
                    goto DrgIDDesc;
                }
                var dbDrugstore = _drugstoreRepository.Get(drugstoreID);
                if (dbDrugstore == null)
                {
                    ConsoleHelper.WriteWithColor(" This ID doesn't contain any drugstore!", ConsoleColor.Red);
                    goto DrgIDDesc;
                }

                var drug = new Drug
                {
                    Name = name,
                    Price = price,
                    Count = count,
                    Drugstore = dbDrugstore,
                };
                dbDrugstore.Drugs.Add(drug);
                _drugRepository.Add(drug);
                ConsoleHelper.WriteWithColor($"The group succesfully created with \n Name:{drug.Name},Price:{drug.Price},Count:{drug.Count}", ConsoleColor.DarkYellow);
            }
        }
        public void Delete()
        {
            GetAll();
          IDde: ConsoleHelper.WriteWithColor(" Enter an ID of the drug",ConsoleColor.Gray);
            int drugID;
            bool isSucceeded = int.TryParse(Console.ReadLine(),out drugID);
            if(!isSucceeded)
            {
                ConsoleHelper.WriteWithColor(" The entered ID is not a correct format!", ConsoleColor.Red);
                goto IDde;
            }
            var dbDrug = _drugRepository.Get(drugID);
            if ( dbDrug == null )
            {
                ConsoleHelper.WriteWithColor(" This ID doesn't contain any drug!", ConsoleColor.Red);
            }

            _drugRepository.Delete(dbDrug);
            ConsoleHelper.WriteWithColor(" The drug is successfully deleted", ConsoleColor.Green);









        }

    }
}






















