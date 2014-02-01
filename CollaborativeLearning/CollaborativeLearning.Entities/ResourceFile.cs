using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class ResourceFile:BaseEntity
    {
        public int resourceID { get; set; }
        public virtual ResourceList ResourceList { get; set; }

        public int fileID { get; set; }
        public virtual File Files { get; set; }
    }
}
