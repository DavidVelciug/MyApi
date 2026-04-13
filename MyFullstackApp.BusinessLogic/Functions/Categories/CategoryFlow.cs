using AutoMapper;
using MyFullstackApp.BusinessLogic.Core.Categories;
using MyFullstackApp.BusinessLogic.Interface;
using MyFullstackApp.Domains.Models.Base;
using MyFullstackApp.Domains.Models.Category;

namespace MyFullstackApp.BusinessLogic.Functions.Categories;

public class CategoryFlow : CategoryAction, ICategory
{
    public CategoryFlow(IMapper mapper) : base(mapper) { }

    public List<CategoryDto> GetAllCategoriesAction()
    {
        return ExecuteGetAllCategoriesAction();
    }

    public CategoryDto? GetCategoryByIdAction(int id)
    {
        return GetCategoryDataByIdAction(id);
    }

    public ResponceMsg ResponceCategoryUpdateAction(CategoryDto category)
    {
        return ExecuteCategoryUpdateAction(category);
    }

    public ResponceMsg ResponceCategoryDeleteAction(int id)
    {
        return ExecuteCategoryDeleteAction(id);
    }

    public ResponceMsg ResponceCategoryCreateAction(CategoryDto category)
    {
        return ExecuteCategoryCreateAction(category);
    }
}
