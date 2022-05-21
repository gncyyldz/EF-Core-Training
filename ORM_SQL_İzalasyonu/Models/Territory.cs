using System;
using System.Collections.Generic;

namespace ORM_SQL_İzalasyonu.Models
{
    public partial class Territory
    {
        public Territory()
        {
            Employees = new HashSet<Employee>();
        }

        public string TerritoryId { get; set; } = null!;
        public string TerritoryDescription { get; set; } = null!;
        public int RegionId { get; set; }

        public virtual Region Region { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
