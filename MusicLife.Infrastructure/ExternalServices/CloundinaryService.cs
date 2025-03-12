using Microsoft.AspNetCore.Http;
using MusicLife.Application.ExternalServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Infrastructure.ExternalServices
{
    public class CloundinaryService : ICloundinaryService
    {
        public Task DeleteAudioFile(string fileName)
        {
            throw new NotImplementedException();
        }

        public Task DeleteImageFile(string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<string> UploadFileAudio(IFormFile fileAudio)
        {
            throw new NotImplementedException();
        }

        public Task<string> UploadFileImage(IFormFile fileImage)
        {
            throw new NotImplementedException();
        }
    }
}
