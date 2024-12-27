using System;
using System.Reflection;
using Microsoft.VisualBasic;
using OrderBouncer.Domain.Aggregates;
using SharedKernel.Enums;

namespace OrderBouncer.Domain.Entities;

public class ImageEntity : BaseEntity
{
    public int ParentId {get; protected set;}
    public EntityTypeEnum ParentType {get; protected set; }
    public ImageTypeEnum ImageType { get; protected set; }

    protected ImageEntity()
    {
    }
    // public ImageEntity()
    // {
    // }
}
