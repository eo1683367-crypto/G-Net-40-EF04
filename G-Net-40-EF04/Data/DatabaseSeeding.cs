using System;
using System.Collections.Generic;
using System.Text;
using G_Net_40_EF04.Models;

namespace G_Net_40_EF04.Data
{
    public static class DatabaseSeeding
    {
        public static void Seed(BankDbContext bankDbContext)
        {

            if (!bankDbContext.Managers.Any())
            {
               List<Manager> managerlist = new ()
               {
                   new()
                   {
                       FullName = "Soly omar",
                       Email = "Se200444@nationalbank.com",
                       PhoneNumber = "01234567890",
                       HireDate = new DateOnly(2020, 1, 13)
                   },
                   new()
                   {
                       FullName = "Youssef Omar",
                       Email = "Jooo@nationalbank.com",
                       PhoneNumber = "01036272141",
                       HireDate = new DateOnly(2024, 4, 10)
                   }
               };

                bankDbContext.Managers.AddRange(managerlist);
                bankDbContext.SaveChanges();    
            }

            //-------------------------------------------------------------------

            if (!bankDbContext.Branches.Any())
            {
                List<Branch> branchlist = new()
               {
                   new()
                   {
                       Name = "Master Branch",
                       Code = "CA01",
                       Address = "Cairo",
                       PhoneNumber = "01010445630",
                       ManagerId = 1
                   },
                   new()
                   {
                       Name = "Alex Branch",
                       Code = "ALEX",
                       Address = "Alexandria",
                       PhoneNumber = "01018845630",
                       ManagerId = 2
                   }
               };

                bankDbContext.Branches.AddRange(branchlist);
                bankDbContext.SaveChanges();
            }
            //---------------------------------------------------------------------
        }
    }
}
