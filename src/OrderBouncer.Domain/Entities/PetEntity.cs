using System;
using OrderBouncer.Domain.ValueObjects;
using SharedKernel;
using SharedKernel.Enums;

namespace OrderBouncer.Domain.Entities;

public class PetEntity: BaseEntity
{
    public int ParentId {get; protected set;}
    public EntityTypeEnum ParentType {get; protected set;}
    public ICollection<ImageEntity> Images {get; protected set;} = [];
    public string? CustomerNote {get; protected set;} = null;
    public Money Cost {get; private set;}
    public PetEntity(int parentId, EntityTypeEnum parentType, Money cost){
        
        ArgumentOutOfRangeException.ThrowIfNegative(parentId);


        ParentId = parentId;
        ParentType = parentType;
        Cost = cost;
    }

    public bool HasCustomerNote(){
        return CustomerNote is not null;
    }

    public void SetCustomerNote(string note){
        ArgumentOutOfRangeException.ThrowIfLessThan(note.Length, Constants.MinCustomerNoteLength);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(note.Length, Constants.MaxCustomerNoteLength);

        CustomerNote = note;
    }

    public int GetImageCount(){
        return Images.Count;
    }

    public void AddImage(ImageEntity image){
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(Images.Count, Constants.MaxPhotoCount);

        ImageEntity? exists = Images.FirstOrDefault(img => image.Id == img.Id); 
        exists ??= Images.FirstOrDefault(img => img == image);

        if(exists is not null){
            throw new InvalidOperationException("Cannot add already contained image to the list");
        }

        Images.Add(image);
    }

    public void RemoveImage(ImageEntity image){
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(Images.Count, 0);

        ImageEntity? exists = Images.FirstOrDefault(img => image.Id == img.Id); 
        exists ??= Images.FirstOrDefault(img => img == image);
        
        if(exists is null){
            throw new InvalidOperationException("Image must be exist in order to remove");
        }

        Images.Remove(image);
    }
}
