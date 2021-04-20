using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Statmath.Application.Models
{
    [Table(TableName)]
    public class JobDto
    {
        public const string TableName = "Jobs";

        public JobDto()
        {
            Id = Guid.NewGuid();
        }
        public Guid MachineId { get; set; }
        public MachineDto Machine { get; set; }
        public Guid Id { get; set; }
        public int Job { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
    }
}