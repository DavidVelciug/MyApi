using MyFullstackApp.Domains.Models.Base;
using MyFullstackApp.Domains.Models.Category;

namespace MyFullstackApp.BusinessLogic.Interface;

public interface ICategory
{
    List<CategoryDto> GetAllCategoriesAction();
    CategoryDto? GetCategoryByIdAction(int id);
    ResponceMsg ResponceCategoryUpdateAction(CategoryDto category);
    ResponceMsg ResponceCategoryDeleteAction(int id);
    ResponceMsg ResponceCategoryCreateAction(CategoryDto category);
}
