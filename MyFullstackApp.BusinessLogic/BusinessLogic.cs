using AutoMapper;
using MyFullstackApp.BusinessLogic.Functions.Admin;
using MyFullstackApp.BusinessLogic.Functions.Capsules;
using MyFullstackApp.BusinessLogic.Functions.Categories;
using MyFullstackApp.BusinessLogic.Functions.Moderation;
using MyFullstackApp.BusinessLogic.Functions.Products;
using MyFullstackApp.BusinessLogic.Functions.Users;
using MyFullstackApp.BusinessLogic.Interface;

namespace MyFullstackApp.BusinessLogic;

public class BusinessLogic
{
    private readonly IMapper _mapper;

    public BusinessLogic(IMapper mapper)
    {
        _mapper = mapper;
    }

    public IProduct GetProductActions() => new ProductFlow(_mapper);

    public ICategory GetCategoryActions() => new CategoryFlow(_mapper);

    public IUserAccount GetUserAccountActions() => new UserFlow(_mapper);

    public ITimeCapsule GetTimeCapsuleActions() => new TimeCapsuleFlow(_mapper);

    public ICapsuleLocation GetCapsuleLocationActions() => new CapsuleLocationFlow(_mapper);

    public IModerationReport GetModerationReportActions() => new ModerationReportFlow(_mapper);

    public IAdminAnalytics GetAdminAnalyticsActions() => new AdminAnalyticsFlow();
}
