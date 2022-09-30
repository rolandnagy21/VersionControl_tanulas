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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return string.Format("{0} {1}", LastName, FirstName); }
            //.Format szintaxis új
            //propfull-ból a private változó rész törlése, set ág törlése, majd get ág kibontása
            //internal class helyett public class, vagy UserMaintenance.Entities névtér behivatkozása máshol?
        }

    }
}
