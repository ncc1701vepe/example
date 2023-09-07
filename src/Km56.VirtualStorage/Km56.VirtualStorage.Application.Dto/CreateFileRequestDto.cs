using System.ComponentModel.DataAnnotations;

namespace km56.VirtualStorage.Application.Dto
{
    public class CreateFileRequestDto : FileRequestBaseDto
    {
        [Required(ErrorMessage = "File Content is required")]
        public byte[]? FileContent { get; set; }
    }
}
