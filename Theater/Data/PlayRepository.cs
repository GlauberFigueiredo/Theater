using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Theater.Data
{
    public class PlayRepository
    {
        private readonly PlayContext Context;


        public PlayRepository(PlayContext context)
        {
            Context = context;
        }
    }
}
