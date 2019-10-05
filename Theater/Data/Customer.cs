using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Theater.Data
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Performance> Performances { get; set; }

        public decimal CalculateTotalPerformances()
        {
            decimal total = 0;

            foreach (var performance in Performances)
            {
                performance.CalculateParcialPerformance();
                total += performance.TotalPerformance;
            }
            
            return total;

        }

        public decimal CalculateTotalPerformances(Customer customer, List<Performance> performances)
        {
            decimal total = 0;

            foreach (var performance in Performances)
            {
                performance.CalculateParcialPerformance();
                total += performance.TotalPerformance;
            }

            return total;
        }


    }
}
