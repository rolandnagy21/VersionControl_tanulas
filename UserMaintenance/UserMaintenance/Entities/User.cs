using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMaintenance.Entities
{
    internal class User
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        private string _fullname;

        public string FullName
        {
            get { return _fullname; }
            set { _fullname = value; }
        }


    }
}
