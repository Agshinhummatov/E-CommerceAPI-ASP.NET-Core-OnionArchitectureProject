using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Domain.Entities.Common
{
    public abstract class  BaseEntity
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

       virtual public DateTime UpdatedDate { get; set; }

    }
}
