using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Customer
    {

        public int idCustomer { get; set; }
        public string curp { get; set; }

        public bool? blackList { get; set; }

        public string address { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string identification { get; set; }

        public int telephonNumber { get; set; } 

        public Customer()
        {
            this.idCustomer = 0;
            this.curp = "";
            this.blackList = false;
            this.address = "";
            this.firstName = "";
            this.lastName = "";
            this.identification = "";
            this.telephonNumber = 0;
        }

        public Customer(int idCustomer, string curp, bool blackList, string address, 
            string firstName, string lastName, string identification, int telephonNumber)
        {
            this.idCustomer = idCustomer;
            this.curp = curp;
            this.blackList = blackList;
            this.address = address;
            this.firstName = firstName;
            this.lastName = lastName;
            this.identification = identification;
            this.telephonNumber = telephonNumber;
        }
    }
}
