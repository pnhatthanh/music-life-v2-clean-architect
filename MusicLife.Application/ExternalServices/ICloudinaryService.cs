using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.ExternalServices
{
    public interface ICloudinaryService
    {
        Task<string> UploadFileImageAsync(IFormFile fileImage);
        Task<string> UploadFileAudioAsync(IFormFile fileAudio);
        Task DeleteImageFileAsync(string fileName);
        Task DeleteAudioFileAsync(string fileName);
    }
}
