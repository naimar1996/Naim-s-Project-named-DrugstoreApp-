using Core.Entities;
using Data.Contexts;
using Data.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Concrete
{
    public class DruggistRepository : IDruggistRepository
    {
        static int id;
        public List<Druggist> GetAll()
        {
            return DbContext.Druggists;
        }
        public Druggist Get(int id)
        {
            return DbContext.Druggists.FirstOrDefault(d => d.ID == id);
        }
        public void Add(Druggist druggist)
        {
            id++;
            druggist.ID = id;
            DbContext.Druggists.Add(druggist);

        }
        public void Update(Druggist druggist)
        {
          var dbDruggist = DbContext.Druggists.FirstOrDefault(d => d.ID == druggist.ID);
            if(druggist != null)
            {
                dbDruggist.Name = druggist.Name;
                dbDruggist.Surname = druggist.Surname;
                dbDruggist.Age = druggist.Age;
                dbDruggist.Experience = druggist.Experience;
            }
        }
        public void Delete(Druggist druggist)
        {
         DbContext.Druggists.Remove(druggist);

        }




    }
}
