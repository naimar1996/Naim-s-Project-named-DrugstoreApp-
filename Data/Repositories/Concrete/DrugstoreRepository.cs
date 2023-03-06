using Core.Entities;
using Data.Contexts;
using Data.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Concrete
{
    public class DrugstoreRepository : IDrugstoreRepository
    {
        static int id;
        public List<Drugstore> GetAll()
        {
            return DbContext.Drugstores;
        }
        public Drugstore Get(int id)
        {
            return DbContext.Drugstores.FirstOrDefault(s => s.ID == id);
        }
        public List<Drugstore> GetAllDrugstoresByOwner(int id)
        {
            return DbContext.Drugstores.Where(o => o.ID == id).ToList();
        }

      
        public void Add(Drugstore drugstore)
        {
            id++;
            drugstore.ID = id;
            DbContext.Drugstores.Add(drugstore);

        }
        public void Update(Drugstore drugstore)
        {
          var dbDrugstore = DbContext.Drugstores.FirstOrDefault(s => s.ID == drugstore.ID);
            if (drugstore != null) 
            {
                dbDrugstore.Name = drugstore.Name;
                dbDrugstore.Address = drugstore.Address;    
                dbDrugstore.ContactNumber = drugstore.ContactNumber;
                dbDrugstore.Email = drugstore.Email;
            }
        }
        public void Delete(Drugstore drugstore)
        {
            DbContext.Drugstores.Remove(drugstore);
        }

        public bool IsDublicatedEmail(string email)
        {
            return DbContext.Drugstores.Any(d => d.Email == email);
        }
        public bool IsValidatedContactNumber(string number)
        {
            return DbContext.Drugstores.Any(n => n.ContactNumber == number);
        }
    }
}







        
