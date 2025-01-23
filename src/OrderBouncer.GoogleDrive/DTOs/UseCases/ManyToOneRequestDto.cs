using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleDrive.Constants;

namespace OrderBouncer.GoogleDrive.DTOs.UseCases;

public record class ManyToOneRequestDto<T> where T : BaseDto
{
    FolderNamesEnum name; 
    ICollection<T> Collection;
    string parentId;

    public ManyToOneRequestDto(ICollection<T> collection){
        Collection = collection;
    }
}
