using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_HW
{
    abstract class Student
    {
        public int ID { set; get; }

        public string fName;

        public string GetfName()
        {
            return fName;
        }

        public void SetfName(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new Exception("Name cannot be Empty ");
            fName = value;
        }

        public string lName { set; get; }
        public DateTime birthDate { set; get; }
        public string Major { set; get; }
        public DateTime RegisterationDate { set; get; }
        public DateTime graduationDate { set; get; }
        public int Grade { set; get; }
        

        public  decimal CalcAverage(List<Subject_Student> l)
        {
            var i = l.Where(s => s.std_ID == this.ID).Select(a => a.Mark).Average();
            return i;
        }

    }
}
