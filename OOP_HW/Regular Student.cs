using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_HW
{
    class Regular_Student : Student
    {
        public void calculateFees()
        {
            double fees = 100;
            for(int i =1; i < this.Grade; i++)
            {
                fees += fees * 0.1;
                fees += 100;
            }
            Console.WriteLine(fees);
        }
    }
}
