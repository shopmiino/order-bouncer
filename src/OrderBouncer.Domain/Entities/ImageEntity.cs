using System;
using OrderBouncer.Domain.Entities.Base;
using SharedKernel.Enums;

namespace OrderBouncer.Domain.Entities;

public class ImageEntity : BaseEntity
{
    public ImageTypeEnum ImageType { get;}
    public required string FilePath {get; set;}
    private bool _isAttached {get; set;} = false;
    private byte[]? _imageBytes {get; set;} = null;

    protected ImageEntity(){}
    public ImageEntity(ImageTypeEnum imageType, int? parentId = null, EntityTypeEnum? parentType = null) : base(parentId: parentId, parentType: parentType)
    {
        ImageType = imageType;
    }

    internal bool IsAttached() => _isAttached;
    internal void Attach(int parentId, EntityTypeEnum parentType){
        ParentId = parentId;
        ParentType = parentType;

        _isAttached = true;
    }

    internal void SetFilePath(string path){
        if (!_isAttached) throw new InvalidOperationException("Can't set filepath on unattached Image");
        ArgumentNullException.ThrowIfNullOrWhiteSpace(path);

        FilePath = path;
    }

    internal bool HasImageBytes(){
        if(_imageBytes is null) return false;
        return _imageBytes.Length > 0;
    }
    internal void SetImageBytes(byte[] imageBytes){
        if (!_isAttached) throw new InvalidOperationException("Can't set bytes to unattached Image");

        _imageBytes = imageBytes; 
    }
    internal byte[] GetImageBytes(){
        if (!_isAttached) throw new InvalidOperationException("Can't get bytes from unattached Image");

        ArgumentNullException.ThrowIfNull(_imageBytes);
        return _imageBytes;
    }
}
