using System.ComponentModel.DataAnnotations;

namespace km56.VirtualStorage.Application.Dto
{
    public class FileRequestBaseDto
    {
        [Required(ErrorMessage = "Connection Name is required")]
        public string? ConnectionName { get; set; }

        [Required(ErrorMessage = "File Path is required")]
        public string? FilePath { get; set; }

        [Required(ErrorMessage = "File Name is required")]
        public string? FileName { get; set; }
    }
}
