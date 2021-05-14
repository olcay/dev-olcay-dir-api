using System;

namespace WebApi.Models
{
    public class ImageDto
    {
        public Guid Id { get; set; }

        public string ThumbnailUrl { get; set; }

        public string Url { get; set; }
    }
}
