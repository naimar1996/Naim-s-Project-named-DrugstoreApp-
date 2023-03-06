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
    public class DrugRepository : IDrugRepository
    {
        static int id;
        public List<Drug> GetAll()
        {
            return DbContext.Drugs;
        }
       public List<Drug>  GetAllDrugsByDrugstore(int id) 
        {
        return DbContext.Drugs.Where(f => f.ID == id).ToList();
        }
        public Drug Get(int id)
        {
            return DbContext.Drugs.FirstOrDefault(l => l.ID == id);
        }
        public void Add(Drug drug)
        {
            id++;
            drug.ID = id;
            DbContext.Drugs.Add(drug);
        }
        public void Update(Drug drug)
        {
            var dbDrug = DbContext.Drugs.FirstOrDefault(l => l.ID == drug.ID);
            if(drug != null)
            {
                dbDrug.Name = drug.Name;
                dbDrug.Price = drug.Price;
                dbDrug.Count = drug.Count;
            }
        }
        public void Delete(Drug drug)
        {
         DbContext.Drugs.Remove(drug);
        }
        
        public List<Drug> Filter(decimal price)
        {
            return DbContext.Drugs.Where(f => f.Price <= price).ToList();
        }



    }
}
