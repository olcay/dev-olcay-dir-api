using WebApi.Entities;
using WebApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Persistence.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly DataContext _context;

        public ImageRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(Image image)
        {
            if (image == null)
            {
                throw new AppException(nameof(image));
            }

            _context.Images.Add(image);
        }

        public Image Get(Guid imageId)
        {
            if (imageId == Guid.Empty)
            {
                throw new AppException(nameof(imageId));
            }

            var image = _context.Images
                            .SingleOrDefault(a => a.Id == imageId);

            if (image == null)
            {
                throw new KeyNotFoundException("Image not found");
            }

            return image;
        }

        public void Delete(Image image)
        {
            if (image == null)
            {
                throw new AppException(nameof(image));
            }

            _context.Images.Remove(image);
        }
    }
}
