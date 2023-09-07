using km56.VirtualStorage.Application.Dto;
using km56.VirtualStorage.Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace km56.VirtualStorage.Api.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IVirtualStorageRepository _virtualStorageRepository;
        private readonly ILogger<FilesController> _logger;

        public FilesController(IVirtualStorageRepository virtualStorageRepository, 
                               ILogger<FilesController> logger)
        {
            _virtualStorageRepository = virtualStorageRepository;
            _logger = logger;
        }

        // POST api/files/create-file
        [HttpPost("create-file")]
        public async Task<IActionResult> CreateFile([FromBody] CreateFileRequestDto requestDto)
        {
            try
            {
                var result = await _virtualStorageRepository.CreateFile(requestDto);
                var resultDto = new CreateFileResultDto { Successful = result.Successful, ErrorMessage = result.ErrorMessage };
                return Ok(resultDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Creating a File");

                var failResult = new CreateFileResultDto
                {
                    Successful = false,
                    ErrorMessage = ex.Message
                };

                return BadRequest(failResult);
            }
        }

        // POST api/files/download-file
        [HttpPost("download-file")]
        public async Task<IActionResult> DowloadFile([FromBody] DownloadFileRequestDto requestDto)
        {
            try
            {
                var result = await _virtualStorageRepository.DownloadFile(requestDto);
                var resultDto = new DownloadFileResultDto { FileContent = result };
                return Ok(resultDto);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error Downloading a File"); 

                var failResult = new DownloadFileResultDto
                {
                    FileContent = new byte[] { }
                };

                return BadRequest(failResult);
            }
        }

        // POST api/files/delete-file
        [HttpPost("delete-file")]
        public async Task<IActionResult> DeleteFile([FromBody] DeleteFileRequestDto requestDto)
        {
            try
            {
                var result = await _virtualStorageRepository.DeleteFile(requestDto);
                var resultDto = new DeleteFileResultDto { Successful = result };
                return Ok(resultDto);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error Deleting a File");

                var failResult = new DeleteFileResultDto
                {
                    Successful = false
                };

                return BadRequest(failResult);
            }
        }
    }
}
