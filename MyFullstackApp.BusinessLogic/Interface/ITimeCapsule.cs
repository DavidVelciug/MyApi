using MyFullstackApp.Domains.Models.Base;
using MyFullstackApp.Domains.Models.Capsule;

namespace MyFullstackApp.BusinessLogic.Interface;

public interface ITimeCapsule
{
    List<TimeCapsuleDto> GetAllTimeCapsulesAction();
    TimeCapsuleDto? GetTimeCapsuleByIdAction(int id);
    List<TimeCapsuleDto> GetTimeCapsulesByOwnerAction(int ownerUserId);
    List<TimeCapsuleDto> GetPublicFeedAction();
    ResponceMsg ResponceTimeCapsuleCreateAction(TimeCapsuleDto capsule);
    ResponceMsg ResponceTimeCapsuleUpdateAction(TimeCapsuleDto capsule);
    ResponceMsg ResponceTimeCapsuleDeleteAction(int id);
}
