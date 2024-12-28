using System;
using OrderBouncer.Domain.Entities;
using SharedKernel.Constants;
using SharedKernel.Enums;

namespace OrderBouncer.Domain.ValueObjects.Sets;

public class FigureSet
{
    public ICollection<FigureEntity>? Figures {get; private set;}
    private ProductTypeEnum _parentProductType;
    public FigureSet(ProductTypeEnum parentProductType){
        _parentProductType = parentProductType;
    }
    protected internal bool HasFigure()
    {
        if (Figures is null) return false;
        return Figures.Count > 0;
    }

    private int MaxLimit() => _parentProductType switch {
        ProductTypeEnum.PersonalizedSingle => Limits.MaxFiguresInStandard,
        ProductTypeEnum.PersonalizedCouple => Limits.MaxFiguresInCouple,
        ProductTypeEnum.PersonalizedFamily => Limits.MaxFiguresInFamily,
        _ => Limits.MaxFiguresForNonFigureProduct
    };

    protected internal void AddFigure(FigureEntity figure)
    {
        Figures ??= [];
        ArgumentOutOfRangeException.ThrowIfGreaterThan(Figures.Count, MaxLimit());

        FigureEntity? exists = Figures.FirstOrDefault(f => figure.Id == f.Id);
        exists ??= Figures.FirstOrDefault(f => f == figure);

        if (exists is not null)
        {
            throw new InvalidOperationException("You cannot add already existing item to Figures list");
        }

        Figures.Add(figure);
    }

    protected internal void RemoveFigure(FigureEntity figure)
    {
        if (Figures is null) throw new InvalidOperationException("Cannot remove from null list");
        if (Figures.Count <= 0) throw new InvalidOperationException("Figures doesn't have any members in it. Attempting to remove from empty collection");

        FigureEntity? exists = Figures.FirstOrDefault(f => figure.Id == f.Id);
        exists ??= Figures.FirstOrDefault(f => f == figure);

        if (exists is null)
        {
            throw new InvalidOperationException("Figure must be exist in order to remove");
        }

        Figures.Remove(figure);
    }
}
