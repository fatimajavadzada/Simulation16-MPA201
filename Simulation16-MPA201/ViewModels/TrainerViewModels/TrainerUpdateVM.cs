using System.ComponentModel.DataAnnotations;

namespace Simulation16_MPA201.ViewModels.TrainerViewModels;

public class TrainerUpdateVM
{
    public int Id { get; set; }
    [Required, MaxLength(256)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(1024)]
    public string Description { get; set; } = string.Empty;
    [Required]
    public int BranchId { get; set; }
    public IFormFile? ImagePath { get; set; }
}
