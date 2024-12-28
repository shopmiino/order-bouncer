using System;
using OrderBouncer.Domain.Entities.Base;
using SharedKernel.Enums;

namespace OrderBouncer.Domain.Entities;

public class ImageEntity : BaseEntity
{
    public ImageTypeEnum ImageType { get;}
    public required string FilePath {get; set;}
    private byte[]? _imageBytes {get; set;} = null;

    protected ImageEntity(){}
    public ImageEntity(int parentId, EntityTypeEnum parentType, ImageTypeEnum imageType) : base(parentId: parentId, parentType: parentType)
    {
        ImageType = imageType;
    }

    internal void SetFilePath(string path){
        ArgumentNullException.ThrowIfNullOrWhiteSpace(path);

        FilePath = path;
    }

    internal bool HasImageBytes(){
        if(_imageBytes is null) return false;
        return _imageBytes.Length > 0;
    }
    internal void SetImageBytes(byte[] imageBytes){
        _imageBytes = imageBytes; 
    }
    internal byte[] GetImageBytes(){
        ArgumentNullException.ThrowIfNull(_imageBytes);
        return _imageBytes;
    }
}
