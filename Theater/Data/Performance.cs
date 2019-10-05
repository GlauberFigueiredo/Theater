using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Theater.Data
{
    public class Performance
    {
        public int Id { get; set; }
        public int PlayId { get; set; }
        public int Audience { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalPerformance { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("PlayId")]
        public virtual Play Play { get; set; }


        public Performance()
        {
        }
        public void CalculateParcialPerformance()
        {
            //TODO Organizar variaveis
            decimal total;
            decimal cache = this.Play.Genre.Cache;
            int maximumAudience = this.Play.Genre.MaximumAudience;
            decimal additional = this.Play.Genre.AudienceAdditionalValue;
            int audience = this.Audience;

            total = cache + ((audience - maximumAudience) * additional);

            this.TotalPerformance = total;
        }
    }
}
