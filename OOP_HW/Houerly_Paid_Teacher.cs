using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_HW
{
    class Houerly_Paid_Teacher : Teacher
    {
        public decimal WorksHouers { set; get; }

        public override decimal calcSalary()
        {
            return this.WorksHouers * 5;
           
        }
    }
    
}
