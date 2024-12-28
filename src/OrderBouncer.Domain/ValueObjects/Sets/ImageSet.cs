using System;
using OrderBouncer.Domain.Entities;
using SharedKernel.Constants;

namespace OrderBouncer.Domain.ValueObjects.Sets;

public class ImageSet
{
    public ICollection<ImageEntity>? Images { get; private set; } = null;
    public bool Required = false;

    public ImageSet(bool required = false)
    {
        Required = required;
        if (Required) Images = [];
    }
    protected internal bool HasImage()
    {
        if (Images is null) return false;

        if (Required) return true;

        return Images.Count > 0;
    }
    protected internal void AddImage(ImageEntity image)
    {
        Images ??= [];

        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(Images.Count, Limits.MaxPhotoCount);

        ImageEntity? exists = Images.FirstOrDefault(img => image.Id == img.Id);
        exists ??= Images.FirstOrDefault(img => img == image);

        if (exists is not null)
        {
            throw new InvalidOperationException("Cannot add already contained image to the list");
        }

        Images.Add(image);
    }

    protected internal void RemoveImage(ImageEntity image)
    {
        ArgumentNullException.ThrowIfNull(Images);

        if (Required && Images.Count <= Limits.MinPhotoCount)
        {
            throw new InvalidOperationException($"Cannot remove image when it is in minimum ({Limits.MinPhotoCount}) required");
        }

        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(Images.Count, 0);


        ImageEntity? exists = Images.FirstOrDefault(img => image.Id == img.Id);
        exists ??= Images.FirstOrDefault(img => img == image);

        if (exists is null)
        {
            throw new InvalidOperationException("Image must be exist to be removed");
        }

        Images.Remove(image);
    }
}
