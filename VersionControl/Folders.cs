using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersionControl
{
    class Folders
    {
        private string name;

        private Boolean initialized;

        private List<Finfo> files;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Boolean Initialized
        {
            get { return initialized; }
            set { initialized = value; }
        }

        public List<Finfo> Files
        {
            get { return files; }
            set { files = value; }
        }
    }
}
