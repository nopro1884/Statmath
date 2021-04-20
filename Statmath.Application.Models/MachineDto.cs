using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Statmath.Application.Models
{

    [Table(TableName)]
    public class MachineDto
    {
        public const string TableName = "Machines";

        public MachineDto()
        {
            Id = new Guid();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<JobDto> Jobs { get; set; }
    }
}
