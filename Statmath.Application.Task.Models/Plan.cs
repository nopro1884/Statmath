using System;

namespace Statmath.Application.Task.Models
{
    public class Plan
    {
        public Plan()
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
