using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netwise
{
    public class CatFact
    {
        public string? Fact { get; set; }
        public int Length { get; set; }

        public override string ToString()
        {
            return $"{{fact: {Fact}, length: {Length}}}";
        }
    }
}
