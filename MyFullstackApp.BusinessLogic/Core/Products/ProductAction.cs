using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyFullstackApp.BusinessLogic.Core.Common;
using MyFullstackApp.DataAccess.Context;
using MyFullstackApp.Domains.Entities.Capsule;
using MyFullstackApp.Domains.Entities.Product;
using MyFullstackApp.Domains.Models.Base;
using MyFullstackApp.Domains.Models.Product;

namespace MyFullstackApp.BusinessLogic.Core.Products;

public class ProductAction
{
    protected readonly IMapper Mapper;

    protected ProductAction(IMapper mapper)
    {
        Mapper = mapper;
    }

    protected List<ProductDto> ExecuteGetAllProductsAction()
    {
        using var db = new AppDbContext();
        var pData = db.Products
            .Include(p => p.Category)
            .Where(p => p.CapsuleId != null && db.TimeCapsules.Any(tc => tc.Id == p.CapsuleId))
            .ToList();

        var capsuleOwnerIds = pData
            .Where(p => p.CapsuleId != null)
            .Select(p => p.CapsuleId!.Value)
            .Distinct()
            .ToList();

        var ownersByCapsuleId = db.TimeCapsules
            .Include(c => c.Owner)
            .Where(c => capsuleOwnerIds.Contains(c.Id))
            .ToDictionary(c => c.Id, c => c.Owner);

        return pData.Select(p =>
        {
            var dto = Mapper.Map<ProductDto>(p);
            if (p.CapsuleId != null && ownersByCapsuleId.TryGetValue(p.CapsuleId.Value, out var owner))
            {
                dto.CreatorName = owner.DisplayName;
                dto.CreatorEmail = owner.Email;
            }
            return dto;
        }).ToList();
    }

    protected ProductDto? GetProductDataByIdAction(int id)
    {
        using var db = new AppDbContext();
        var pData = db.Products.Include(p => p.Category).FirstOrDefault(x => x.Id == id);
        return pData == null ? null : Mapper.Map<ProductDto>(pData);
    }

    protected ResponceMsg ExecuteProductUpdateAction(ProductDto product)
    {
        using var db = new AppDbContext();
        var pData = db.Products.FirstOrDefault(x => x.Id == product.Id);
        if (pData == null)
        {
            return new ResponceMsg { IsSuccess = false, Message = "Product not found." };
        }

        var categoryExists = db.Categories.Any(c => c.Id == product.CategoryId);
        if (!categoryExists)
        {
            return new ResponceMsg { IsSuccess = false, Message = "Category not found." };
        }

        pData.Name = product.Name;
        pData.Description = product.Description;
        pData.Image = ImageStorage.SaveDataUrlIfNeeded(product.Image, "products");
        pData.Price = product.Price;
        pData.CategoryId = product.CategoryId;

        db.SaveChanges();

        return new ResponceMsg { IsSuccess = true, Message = "Product updated successfully." };
    }

    protected ResponceMsg ExecuteProductDeleteAction(int id)
    {
        using var db = new AppDbContext();
        var pData = db.Products.FirstOrDefault(x => x.Id == id);
        if (pData == null)
        {
            return new ResponceMsg { IsSuccess = false, Message = "Product not found." };
        }

        db.Products.Remove(pData);
        db.SaveChanges();

        return new ResponceMsg { IsSuccess = true, Message = "Product deleted successfully." };
    }

    protected ResponceMsg ExecuteProductCreateAction(ProductDto product)
    {
        using var db = new AppDbContext();
        var duplicate = db.Products.FirstOrDefault(x => x.Name.Equals(product.Name));
        if (duplicate != null)
        {
            return new ResponceMsg
            {
                IsSuccess = false,
                Message = "A product with this name already exists in the system."
            };
        }

        var categoryExists = db.Categories.Any(c => c.Id == product.CategoryId);
        if (!categoryExists)
        {
            return new ResponceMsg { IsSuccess = false, Message = "Category not found." };
        }

        var entity = Mapper.Map<ProductData>(product);
        entity.Id = 0;
        entity.Category = null!;
        entity.Image = ImageStorage.SaveDataUrlIfNeeded(entity.Image, "products");

        db.Products.Add(entity);
        db.SaveChanges();

        return new ResponceMsg
        {
            IsSuccess = true,
            Message = "Product was successfully added."
        };
    }
}
