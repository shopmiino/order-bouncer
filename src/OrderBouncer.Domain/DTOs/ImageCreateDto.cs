using SharedKernel.Enums;

namespace OrderBouncer.Domain.DTOs;

public record class ImageCreateDto
{
    public ImageTypeEnum ImageType {get; protected set;}
    public string FilePath {get; protected set;}
    
    public ImageCreateDto(ImageTypeEnum imageType, string filePath){
        ImageType = imageType;
        FilePath = filePath;
    }
}
