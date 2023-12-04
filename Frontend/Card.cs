using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectFront.Data
{
        public class Card
        {
            public int Id { get; set; }
            public string Value { get; set; }
            public string Suit { get; set; }

        public override string ToString()
            {
                return $"{Value} of {Suit}";
            }
        }
    

}
