using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Domaine.Entities
{
    public class BasicEntity
    {
        public BasicEntity() // EF
        {
        }

        // gestion
        public bool IsEnable { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }

  
        public void Touch()
        {
            UpdatedAt = DateTime.Now;
        }

        public void Disabled()
        {
            IsEnable = false;
            Touch();
        }

        public void Enable()
        {
            IsEnable = true;
            Touch();
        }

    }
}
