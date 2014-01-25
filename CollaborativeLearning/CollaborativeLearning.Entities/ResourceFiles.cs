using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaborativeLearning.Entities
{
    class ResourceFiles:Base
    {
        public int resourceID { get; set; }
        public virtual ResourceList ResourceList { get; set; }

        public int fileID { get; set; }
        public virtual Files Files { get; set; }

    }
}
