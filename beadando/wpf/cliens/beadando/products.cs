using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beadando
{
    [Serializable]
    internal class products
    {
        public int id;
        public string category;
        public string name;
        public string description;
        public string price;
        public string stock;
        public override string ToString()
        {
            return (id + " ; " + category + " ; " + name + " ; " + description + " ; " + price + " ; " + stock);
        }
    }
}
