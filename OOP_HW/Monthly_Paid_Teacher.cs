using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_HW
{
    class Monthly_Paid_Teacher : Teacher
    {
        public decimal Additional_Work_Houers { set; get; }

        public decimal base_Salary;

        public decimal GetBase_Salary()
        {
            return base_Salary;
        }

        public void SetBase_Salary(decimal value)
        {
            base_Salary = value;
        }

        public override decimal calcSalary()
        {
          return this.GetBase_Salary() + (Additional_Work_Houers*5);
        }
    }
}
