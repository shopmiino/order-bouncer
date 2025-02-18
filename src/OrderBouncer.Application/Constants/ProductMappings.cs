using System;
using OrderBouncer.Domain.DTOs.Base;
using SharedKernel.Enums;

namespace OrderBouncer.Application.Constants;

public static class ProductMappings
{
    public static readonly Dictionary<ShopifyProductsEnum, Type> ProductDtoPairs = new() {
        {ShopifyProductsEnum.Miino_Pop, typeof(FigureDto)},
        {ShopifyProductsEnum.Cift_Miino_Popu, typeof(FigureDto)},
        {ShopifyProductsEnum.Miino_Pop_Aksesuar, typeof(AccessoryDto)},
        {ShopifyProductsEnum.Miino_Pop_Anahtarlik, typeof(KeychainDto)},
        {ShopifyProductsEnum.Miino_Pop_Evcil_Hayvan, typeof(PetDto)},
    };
}
