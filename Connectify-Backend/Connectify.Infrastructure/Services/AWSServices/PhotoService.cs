using Amazon.S3;
using Amazon.S3.Model;
using Connectify.Application.Interfaces.AWSServicesInterfaces;
using Connectify.Domain.Entities;
using Connectify.Infrastructure.Configurations.AWSConfigurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Infrastructure.Services.AWSService
{
    public class PhotoService : IPhotoService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly AWSConfigurations _awsConfigurations;

        public PhotoService(IAmazonS3 s3Client, IOptions<AWSConfigurations> awsConfigurations)
        {
            _s3Client = s3Client;
            _awsConfigurations = awsConfigurations.Value;
        }
        public Task<bool> DeletePhoto(string photoPath)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UploadGroupPhoto(IFormFile photo)
        {
            var key = $"groupsPhotos/{Guid.NewGuid()}{Path.GetExtension(photo.FileName)}";
            using (var stream = photo.OpenReadStream())
            {
                var putRequest = new PutObjectRequest()
                {
                    BucketName = _awsConfigurations.BucketName,
                    Key = key,
                    InputStream = stream,
                    ContentType = photo.ContentType
                };

                var response = await _s3Client.PutObjectAsync(putRequest);
                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    return "https://connectify-real-time-bucket.s3.eu-north-1.amazonaws.com/" + key;
                 
                throw new Exception("File upload failed");
            }
        }

        public async Task<string> UploadMessageAttachment(IFormFile photo)
        {
            var key = $"messagesAttachments/{Guid.NewGuid()}{Path.GetExtension(photo.FileName)}";
            using (var stream = photo.OpenReadStream())
            {
                var putRequest = new PutObjectRequest()
                {
                    BucketName = _awsConfigurations.BucketName,
                    Key = key,
                    InputStream = stream,
                    ContentType = photo.ContentType
                };

                var response = await _s3Client.PutObjectAsync(putRequest);
                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    return "https://connectify-real-time-bucket.s3.eu-north-1.amazonaws.com/" + key;

                throw new Exception("File upload failed");
            }
        }

        public async Task<string> UploadPhoto(IFormFile photo, Guid userId)
        {
            var key = $"profilePhotos/{userId}{Path.GetExtension(photo.FileName)}";
            using (var stream = photo.OpenReadStream())
            {
                var putRequest = new PutObjectRequest()
                {
                    BucketName = _awsConfigurations.BucketName,
                    Key = key,
                    InputStream = stream,
                    ContentType = photo.ContentType
                };

                var response = await _s3Client.PutObjectAsync(putRequest);
                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    return "https://connectify-real-time-bucket.s3.eu-north-1.amazonaws.com/" + key;

                throw new Exception("File upload failed");
            }
        }
    }
}
