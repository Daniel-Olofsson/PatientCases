using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientCases.Models.Entities;

public class StatusEntity
{
    public int Id { get; set; }
    public string StatusName { get; set; } = null!;

    public ICollection<CaseEntity> Cases { get; set; } = new HashSet<CaseEntity>();
}
