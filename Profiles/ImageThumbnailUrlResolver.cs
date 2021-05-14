using System;
using AutoMapper;
using Microsoft.Extensions.Options;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Profiles
{
    public class ImageThumbnailUrlResolver : IValueResolver<Image, ImageDto, string>
    {
        private readonly AzureStorageConfig storageConfig = null;

        public ImageThumbnailUrlResolver(IOptions<AzureStorageConfig> config)
        {
            storageConfig = config.Value;
        }

        public string Resolve(Image source, ImageDto destination, string destMember, ResolutionContext context)
        {
            var blobUri = new Uri("https://" +
                                  storageConfig.AccountName +
                                  ".blob.core.windows.net/" +
                                  storageConfig.ThumbnailContainer +
                                  "/" + source.FileName());

            return blobUri.ToString();
        }
    }
}