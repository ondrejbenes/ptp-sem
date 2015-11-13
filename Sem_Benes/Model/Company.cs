using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem_Benes.Model
{
    class Company
    {
        public long Id { get; set; }
        public int ICO { get; set; }
        public int DIC { get; set; }
        public Address Address { get; set; }
        public string Name { get; set; }
        public string BusinessType { get; set; }


    }
}
