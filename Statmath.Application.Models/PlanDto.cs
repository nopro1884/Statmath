using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Statmath.Application.Models
{
    [Table(TableName)]
    public class PlanDto
    {
        public const string TableName = "Plans";

        public PlanDto()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Machine { get; set; }
        public int Job { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
    }
}