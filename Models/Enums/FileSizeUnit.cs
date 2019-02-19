using System.ComponentModel.DataAnnotations;

namespace Dani551.Open.FileService
{
    public enum FileSizeUnit
    {
        [Display(Name ="Byte")]
        Byte = 0,
        [Display(Name = "Kilo Byte")]
        KB = 1,
        [Display(Name = "Mega Byte")]
        MB = 2,
        [Display(Name = "Giga Byte")]
        GB = 3
    }
}
