using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_HW
{
    abstract class Teacher
    {
        public int ID { set; get; }
        public string fName { set; get; }
        public string lName { set; get; }
        
        

        public abstract decimal calcSalary();
    }
}
