using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoyalEventServer
{
    public class ListedNode
    {
        public string Name { get; set; }

        public string Ip { get; set; }

        public int Port { get; set; }

        public int NodeNum { get; set; }

        public override string ToString()
        {
            return "[" + Name + "] " + Ip + ":" + Port + " /" + NodeNum;
        }
    }
}
