using System.ComponentModel.DataAnnotations;

namespace PustokBackEnd.ViewModels.SliderVM;

public class CreateSliderItemVM
{
    public string ImageUrl { get; set; }
    [Required, MinLength(3), MaxLength(100)]
    public string Title { get; set; }
    [Required, MinLength(3), MaxLength(100)]
    public string Text { get; set; }
    public sbyte IsLeft { get; set; }
    public sbyte IsRightText { get; set; }
}
