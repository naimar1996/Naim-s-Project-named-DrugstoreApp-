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
    public class OwnerRepository : IOwnerRepository
    {
        static int id;
        public List<Owner> GetAll()
        {
            return DbContext.Owners;
        }
        public Owner Get(int id)
        {
            return DbContext.Owners.FirstOrDefault(o => o.ID == id);
        }
        public void Add(Owner owner)
        {
            id++;
            owner.ID = id;
            owner.CreatedAt = DateTime.Now;
            DbContext.Owners.Add(owner);
        }
        public void Update(Owner owner)
        {
            var dbOwner = DbContext.Owners.FirstOrDefault(o => o.ID == owner.ID);
            if (owner != null)
            {
                dbOwner.Name = owner.Name;
                dbOwner.Surname = owner.Surname;
            }
        }
        public void Delete(Owner owner)
        {
            DbContext.Owners.Remove(owner);
        }
    }
}
         




