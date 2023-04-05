using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientCases.Models.Entities;

public class StatusEntity
{


    
    public int Id { get; set; }
    public string StatusName { get; set; } = null!;

    public ICollection<CaseEntity> Cases { get; set; } = new HashSet<CaseEntity>();
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StatusEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.StatusName).IsRequired().HasMaxLength(50);
            
        });
    }
}
