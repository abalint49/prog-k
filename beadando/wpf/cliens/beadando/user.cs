using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beadando
{
    internal class user
    {
        public int id;
        public string name;
        public string admin;

        public user(string name, string admin)
        {
            this.name = name;
            this.admin = admin;
        }
        public user()
        {
            this.name = null;
            this.admin = null;
        }

        public override string ToString()
        {
            return (name + ";" + admin);
        }
    }
}
