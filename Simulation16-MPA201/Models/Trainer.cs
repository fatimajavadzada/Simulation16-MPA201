using Simulation16_MPA201.Models.Common;

namespace Simulation16_MPA201.Models
{
    public class Trainer : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public int BranchId { get; set; }
        public Branch Branch { get; set; } = null!;
    }
}
