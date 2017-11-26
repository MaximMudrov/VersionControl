using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersionControl
{
    class Finfo
    {
        private string name, size, crtime, modtime, note;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Size
        {
            get { return size; }
            set { size = value; }
        }

        public string Crtime
        {
            get { return crtime; }
            set { crtime = value; }
        }

        public string Modtime
        {
            get { return modtime; }
            set { modtime = value; }
        }

        public string Note
        {
            get { return note; }
            set { note = value; }
        }
    }
}
