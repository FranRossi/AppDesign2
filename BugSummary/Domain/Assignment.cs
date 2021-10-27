using Domain.DomainUtilities;
using Domain.DomainUtilities.CustomExceptions;

namespace Domain
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HourlyRate { get; set; }
        public double Duration { get; set; }
        public Project Project { get; set; }
        public int ProjectId { get; set; }
    }
}