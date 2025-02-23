namespace OrderBouncer.Domain.DTOs.Base;

public record class BaseDto
{
    public ICollection<string>? ImagePaths {get; init;}
    public string? Note {get; init;}

    public BaseDto(){}
    public BaseDto(ICollection<string>? imagePaths = null, string? note = null){
        ImagePaths = imagePaths;
        Note = note;
    }
}

public static class BaseDtoExtensions{
    public static PetDto ToPetDto(this BaseDto dto){
        return new(
            imagePaths: dto.ImagePaths,
            note: dto.Note
        );
    }

    public static AccessoryDto ToAccessoryDto(this BaseDto dto){
        return new(
            imagePaths: dto.ImagePaths,
            note: dto.Note
        );
    }

    public static KeychainDto ToKeychainDto(this BaseDto dto){
        return new(
            imagePaths: dto.ImagePaths,
            note: dto.Note
        );
    }
    
}
