using gyak6_WEYEWU.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gyak6_WEYEWU.Entities
{
    public class BallFactory : IToyFactory
    {
        public Abstractions.Toy CreateNew()
        {
            return new Toy();
        }
    }
}