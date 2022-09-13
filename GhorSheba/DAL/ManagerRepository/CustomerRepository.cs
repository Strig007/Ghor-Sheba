using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ManagerRepository
{
    public class CustomerRepository : ManagerCustomerServiceInterface<Customer, int>
    {
        ShebaDbEntities db;

        public CustomerRepository(ShebaDbEntities db)
        {
            this.db = db;
        }

        public void AddC(Customer e)
        {
            throw new NotImplementedException();
        }

        public void DeleteC(int id)
        {
            throw new NotImplementedException();
        }

        public void EditC(Customer e)
        {
            throw new NotImplementedException();
        }

        public List<Customer> GetC()
        {
            return db.Customers.ToList();
        }

        public Customer GetC(int id)
        {
            throw new NotImplementedException();
        }
    }
}
