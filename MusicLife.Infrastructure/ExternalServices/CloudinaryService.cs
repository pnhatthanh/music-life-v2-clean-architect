using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MusicLife.Application.Exceptions;
using MusicLife.Application.ExternalServices;
using MusicLife.Infrastructure.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Infrastructure.ExternalServices
{
    public class CloudinaryService : ICloudinaryService
    {
        private Cloudinary _cloudinary;
        public CloudinaryService(IOptions<CloudinarySetting> options) 
        {
            Account account = new Account(
                options.Value.CloudName,
                options.Value.ApiKey,
                options.Value.ApiSecret);
            _cloudinary = new Cloudinary(account);
        }  
        public async Task DeleteAudioFile(string fileName)
        {
            var uri = new Uri(fileName);
            var segments = uri.Segments;
            var publicId = Path.Combine(segments[^2], segments[^1]);
            var deleteParams = new DeletionParams(publicId)
            {
                ResourceType = ResourceType.Raw
            };
            await _cloudinary.DestroyAsync(deleteParams);
        }

        public async Task DeleteImageFile(string fileName)
        {
            var uri = new Uri(fileName);
            var segments = uri.Segments;
            var publicId = Path.Combine(segments[^2], Path.GetFileNameWithoutExtension(segments[^1]));
            var deleteParams = new DeletionParams(publicId);
            await _cloudinary.DestroyAsync(deleteParams);
        }

        public async Task<string> UploadFileAudio(IFormFile fileAudio)
        {
            if (fileAudio.Length == 0)
            {
                throw new BadRequestException("File image cannot empty");
            }
            if (fileAudio.Length > 20 * 1024 * 1024)
            {
                throw new BadRequestException("File is too large. Maximum allowed size is 20Mb");
            }
            var validExtension = new[] { ".mp3", ".wav" };
            var fileExtension = Path.GetExtension(fileAudio.FileName).ToLowerInvariant();
            if (!Array.Exists(validExtension, extention => extention == fileExtension))
            {
                throw new BadRequestException("Invalid file extension. Only .mp3, .wav are allowed");
            }
            var uploadParams = new RawUploadParams
            {
                File = new FileDescription(Guid.NewGuid().ToString() + fileExtension
                                           , fileAudio.OpenReadStream()),
                Folder = "Audios"
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.SecureUrl.ToString();
        }

        public async Task<string> UploadFileImage(IFormFile fileImage)
        {
            if (fileImage.Length == 0)
            {
                throw new BadRequestException("File image cannot empty");
            }
            if (fileImage.Length > 10 * 1024 * 1024)
            {
                throw new BadRequestException("File is too large. Maximum allowed size is 10Mb");
            }
            var validExtension = new[] { ".png", ".jpg" };
            var fileExtension = Path.GetExtension(fileImage.FileName).ToLowerInvariant();
            if (!Array.Exists(validExtension, extention => extention == fileExtension))
            {
                throw new BadRequestException("Invalid file extension. Only .png, .jpg are allowed");
            }
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(Guid.NewGuid().ToString() + fileExtension
                                           , fileImage.OpenReadStream()),
                Folder = "Images"
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.SecureUrl.ToString();
        }
    }
}
