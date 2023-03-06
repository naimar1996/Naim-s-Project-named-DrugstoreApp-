using Core.ConsoleHelper;
using Core.Constants;
using Core.Helper;
using Data;
using Data.Contexts;

namespace Presentation
{
    public static class Program
    {
        private readonly static OwnerService _ownerService;
        private readonly static DrugstoreService _drugstoreService;
        private readonly static DrugService _drugService;
        private readonly static DruggistService _druggistService;
        private readonly static AdminService _adminService;

        static Program()
        {
            _ownerService = new OwnerService();
            _drugstoreService = new DrugstoreService();
            _drugService = new DrugService();
            _druggistService = new DruggistService();
            _adminService = new AdminService();
            DbInitializer.SeedAdmins();
        }
        static void Main()
        {

        AuthorizeDesc: var admin = _adminService.Authorize();
            if (admin != null)
            {
                ConsoleHelper.WriteWithColor($" --- Welcome, {admin.Username}", ConsoleColor.Cyan);
                while (true)
                {

                MainMenuDesc: ConsoleHelper.WriteWithColor(" 1 - Owners ", ConsoleColor.Yellow);
                    ConsoleHelper.WriteWithColor(" 2 - Drugstores ", ConsoleColor.Yellow);
                    ConsoleHelper.WriteWithColor(" 3 - Druggists ", ConsoleColor.Yellow);
                    ConsoleHelper.WriteWithColor(" 4 - Drugs ", ConsoleColor.Yellow);
                    ConsoleHelper.WriteWithColor(" 0 - Log out ", ConsoleColor.Yellow);

                    int number;
                    bool isSucceeded = int.TryParse(Console.ReadLine(), out number);
                    if (!isSucceeded)
                    {
                        ConsoleHelper.WriteWithColor(" The entered number is not a corret format!", ConsoleColor.Red);
                        goto MainMenuDesc;
                    }
                    else
                    {
                        switch (number)
                        {

                            case (int)MainMenuOptions.Owners:
                                while (true)
                                {
                                OwnerListDesc: ConsoleHelper.WriteWithColor(" 1 - Create Owner ", ConsoleColor.Gray);
                                    ConsoleHelper.WriteWithColor(" 2 - Update Owner ", ConsoleColor.Gray);
                                    ConsoleHelper.WriteWithColor(" 3 - Delete Owner ", ConsoleColor.Gray);
                                    ConsoleHelper.WriteWithColor(" 4 - Get All Owners ", ConsoleColor.Gray);
                                    ConsoleHelper.WriteWithColor(" 0 - Back to Main Menu ", ConsoleColor.Gray);
                                    isSucceeded = int.TryParse(Console.ReadLine(), out number);
                                    if (!isSucceeded)
                                    {
                                        ConsoleHelper.WriteWithColor(" The entered number is not a correct format!", ConsoleColor.Red);
                                    }
                                    switch (number)
                                    {
                                        case (int)OwnerOptions.CreateOwner:
                                            _ownerService.Create();
                                            break;
                                        case (int)OwnerOptions.UpdateOwner:
                                            _ownerService.Update();
                                            break;
                                        case (int)OwnerOptions.DeleteOwner:
                                            _ownerService.Delete();
                                            break;
                                        case (int)OwnerOptions.GetAllOwners:
                                            _ownerService.GetAll();
                                            break;
                                        case (int)OwnerOptions.BacktoMainMenu:
                                            goto MainMenuDesc;

                                        default:
                                            ConsoleHelper.WriteWithColor("The List doesn't contain this number!", ConsoleColor.Red);
                                            goto OwnerListDesc;


                                    }
                                }

                            case (int)MainMenuOptions.Drugstores:
                                while (true)
                                {
                                DrugStoreDesc: ConsoleHelper.WriteWithColor(" 1 - Create Drugstore ", ConsoleColor.Gray);
                                    ConsoleHelper.WriteWithColor(" 2 - Update Drugstore ", ConsoleColor.Gray);
                                    ConsoleHelper.WriteWithColor(" 3 - Get All Drugstores ", ConsoleColor.Gray);
                                    ConsoleHelper.WriteWithColor(" 4 - Get All Drugstores by Owner ", ConsoleColor.Gray);
                                    ConsoleHelper.WriteWithColor(" 5 - Sale", ConsoleColor.Gray);
                                    ConsoleHelper.WriteWithColor(" 6 - Delete Drugstore ", ConsoleColor.Gray);
                                    ConsoleHelper.WriteWithColor(" 0 - Back to Main menu ", ConsoleColor.Gray);
                                    isSucceeded = int.TryParse(Console.ReadLine(), out number);
                                    if (!isSucceeded)
                                    {
                                        ConsoleHelper.WriteWithColor(" The entered ID is not a correct format!", ConsoleColor.Red);
                                    }
                                    else
                                    {
                                        switch (number)
                                        {
                                            case (int)DrugstoreOptions.CreateDrugstore:
                                                _drugstoreService.Create();
                                                break;
                                            case (int)DrugstoreOptions.UpdateDrugstore:
                                                _drugstoreService.Update();
                                                break;
                                            case (int)DrugstoreOptions.GetAllDrugstores:
                                                _drugstoreService.GetAll();
                                                break;
                                            case (int)DrugstoreOptions.GetAllDrugstoresByOwner:
                                                _drugstoreService.GetAllDrugstoresByOwner();
                                                break;
                                            case (int)DrugstoreOptions.Sale:
                                                _drugstoreService.Sale();
                                                break;
                                            case (int)DrugstoreOptions.DeleteDrugstore:
                                                _drugstoreService.Delete();
                                                break;
                                            case (int)DrugstoreOptions.BacktoMainMenu:
                                                goto MainMenuDesc;




                                        }
                                    }

                                }

                            case (int)MainMenuOptions.Drugs:
                                while (true)
                                {
                                DrugListDesc: ConsoleHelper.WriteWithColor(" 1 - Create Drug ", ConsoleColor.Gray);
                                    ConsoleHelper.WriteWithColor(" 2 - Update Drug ", ConsoleColor.Gray);
                                    ConsoleHelper.WriteWithColor(" 3 - Delete Drug ", ConsoleColor.Gray);
                                    ConsoleHelper.WriteWithColor(" 4 - Get All Drugs ", ConsoleColor.Gray);
                                    ConsoleHelper.WriteWithColor(" 5 - Get All Drugs By Drugstore ", ConsoleColor.Gray);
                                    ConsoleHelper.WriteWithColor(" 6 - Filter ", ConsoleColor.Gray);
                                    ConsoleHelper.WriteWithColor(" 0 - Back to Main Menu ", ConsoleColor.Gray);
                                    isSucceeded = int.TryParse(Console.ReadLine(), out number);
                                    if (!isSucceeded)
                                    {
                                        ConsoleHelper.WriteWithColor(" The entered number is not a correct format!", ConsoleColor.Red);
                                    }
                                    switch (number)
                                    {
                                        case (int)DrugOptions.CreateDrug:
                                            _drugService.Create();
                                            break;
                                        case (int)DrugOptions.UpdateDrug:
                                            _drugService.Update();
                                            break;
                                        case (int)DrugOptions.DeleteDrug:
                                            _drugService.Delete();
                                            break;
                                        case (int)DrugOptions.GetAllDrugs:
                                            _drugService.GetAll();
                                            break;
                                        case (int)DrugOptions.GetAllDrugsByDrugstore:
                                            _drugService.GetAllDrugsByDrugstore();
                                            break;
                                        case (int)DrugOptions.Filter:
                                            _drugService.Filter();
                                            break;
                                        case (int)DrugOptions.BacktoMainMenu:
                                            goto MainMenuDesc;
                                        default:
                                            ConsoleHelper.WriteWithColor("The List doesn't contain this number!", ConsoleColor.Red);
                                            goto DrugListDesc;

                                    }
                                }

                            case (int)MainMenuOptions.Druggists:
                                while (true)
                                {
                                DruggistListDesc: ConsoleHelper.WriteWithColor(" 1 - Create Druggist ", ConsoleColor.Gray);
                                    ConsoleHelper.WriteWithColor(" 2 - Update Druggist ", ConsoleColor.Gray);
                                    ConsoleHelper.WriteWithColor(" 3 - Get All Druggist ", ConsoleColor.Gray);
                                    ConsoleHelper.WriteWithColor(" 4 - Get All Druggists By Drugstore ", ConsoleColor.Gray);
                                    ConsoleHelper.WriteWithColor(" 5 - Delete Druggist ", ConsoleColor.Gray);
                                    ConsoleHelper.WriteWithColor(" 0 - Back to Main Menu ", ConsoleColor.Gray);
                                    isSucceeded = int.TryParse(Console.ReadLine(), out number);
                                    if (!isSucceeded)
                                    {
                                        ConsoleHelper.WriteWithColor(" The entered number is not a correct format!", ConsoleColor.Red);
                                    }

                                    switch (number)
                                    {
                                        case (int)DruggistOptions.Create:
                                            _druggistService.Create();
                                            break;
                                        case (int)DruggistOptions.Update:
                                            _druggistService.Update();
                                            break;
                                        case (int)DruggistOptions.GetAll:
                                            _druggistService.GetAll();
                                            break;
                                        case (int)DruggistOptions.GetAllDruggistsByDrugstore:
                                            _druggistService.GetAllDruggistsByDrugstore();
                                            break;
                                        case (int)DruggistOptions.Delete:
                                            _druggistService.Delete();
                                            break;
                                        case (int)DruggistOptions.BacktoMainMenu:
                                            goto MainMenuDesc;
                                        default:
                                            ConsoleHelper.WriteWithColor("The List doesn't contain this number!", ConsoleColor.Red);
                                            goto DruggistListDesc;

                                    }
                                }

                            case (int)MainMenuOptions.Logout:
                                goto AuthorizeDesc;
                            default:
                                ConsoleHelper.WriteWithColor(" The Main menu doesn't contain this number!", ConsoleColor.Red);
                                break;



                        }
                    }
                }
            }

        }
    }
}








