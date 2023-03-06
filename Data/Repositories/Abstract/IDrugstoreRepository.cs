using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Abstract
{
    public interface IDrugstoreRepository: IRepository<Drugstore>
    {
        bool IsDublicatedEmail(string email);
        bool IsValidatedContactNumber(string number);
        List<Drugstore> GetAllDrugstoresByOwner(int owner);
       

    }



}
