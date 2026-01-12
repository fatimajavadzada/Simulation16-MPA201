using Simulation16_MPA201.Models;
using System.ComponentModel.DataAnnotations;

namespace Simulation16_MPA201.ViewModels.TrainerViewModels
{
    public class TrainerCreateVM
    {
        [Required,MaxLength(256)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(1024)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public IFormFile ImagePath { get; set; } = null!;
        [Required]
        public int BranchId { get; set; }
    }
}
