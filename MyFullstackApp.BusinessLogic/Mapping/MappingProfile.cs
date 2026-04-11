using AutoMapper;
using MyFullstackApp.Domains.Entities.Capsule;
using MyFullstackApp.Domains.Entities.Category;
using MyFullstackApp.Domains.Entities.Moderation;
using MyFullstackApp.Domains.Entities.Product;
using MyFullstackApp.Domains.Entities.User;
using MyFullstackApp.Domains.Models.Capsule;
using MyFullstackApp.Domains.Models.Category;
using MyFullstackApp.Domains.Models.Moderation;
using MyFullstackApp.Domains.Models.Product;
using MyFullstackApp.Domains.Models.User;

namespace MyFullstackApp.BusinessLogic.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CategoryData, CategoryDto>().ReverseMap();

        CreateMap<ProductData, ProductDto>()
            .ForMember(d => d.Category, o => o.MapFrom(s => s.Category != null ? s.Category.Name : null));

        CreateMap<ProductDto, ProductData>()
            .ForMember(d => d.Category, o => o.Ignore());

        CreateMap<UserAccountData, UserAccountDto>().ReverseMap();
        CreateMap<UserAccountDto, UserAccountData>()
            .ForMember(d => d.Capsules, o => o.Ignore());

        CreateMap<TimeCapsuleData, TimeCapsuleDto>()
            .ForMember(d => d.OwnerDisplayName, o => o.Ignore());
        CreateMap<TimeCapsuleDto, TimeCapsuleData>()
            .ForMember(d => d.Owner, o => o.Ignore())
            .ForMember(d => d.Location, o => o.Ignore())
            .ForMember(d => d.Reports, o => o.Ignore());

        CreateMap<CapsuleLocationData, CapsuleLocationDto>().ReverseMap();
        CreateMap<CapsuleLocationDto, CapsuleLocationData>()
            .ForMember(d => d.Capsule, o => o.Ignore());

        CreateMap<ModerationReportData, ModerationReportDto>().ReverseMap();
        CreateMap<ModerationReportDto, ModerationReportData>()
            .ForMember(d => d.Capsule, o => o.Ignore());
    }
}
