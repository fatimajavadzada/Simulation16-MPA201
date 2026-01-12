using Simulation16_MPA201.Models.Common;

namespace Simulation16_MPA201.Models;

public class Branch : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public ICollection<Trainer> Trainers { get; set; } = [];
}
