using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Theater.Data
{
    public class Genre
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int MaximumAudience { get; set; }
        public decimal Cache { get; set; }
        public decimal AudienceAdditionalValue { get; set; }

        public virtual ICollection<Play> Plays { get; set; }
    }
}
