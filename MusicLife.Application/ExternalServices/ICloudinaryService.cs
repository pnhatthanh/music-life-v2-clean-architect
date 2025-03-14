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
        Task<string> UploadFileImage(IFormFile fileImage);
        Task<string> UploadFileAudio(IFormFile fileAudio);
        Task DeleteImageFile(string fileName);
        Task DeleteAudioFile(string fileName);
    }
}
