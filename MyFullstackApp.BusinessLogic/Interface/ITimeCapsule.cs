using MyFullstackApp.Domains.Models.Base;
using MyFullstackApp.Domains.Models.Capsule;

namespace MyFullstackApp.BusinessLogic.Interface;

public interface ITimeCapsule
{
    List<TimeCapsuleDto> GetAllTimeCapsulesAction();
    TimeCapsuleDto? GetTimeCapsuleByIdAction(int id);
    TimeCapsuleDto? GetTimeCapsuleByIdForUserAction(int id, int viewerUserId);
    List<TimeCapsuleDto> GetTimeCapsulesByOwnerAction(int ownerUserId);
    List<TimeCapsuleDto> GetTimeCapsulesByRecipientAction(int recipientUserId);
    List<TimeCapsuleDto> GetOpenedCapsulesForUserAction(int userId);
    List<TimeCapsuleDto> GetPublicFeedAction();
    ResponceMsg ResponceTimeCapsuleCreateAction(TimeCapsuleDto capsule);
    ResponceMsg ResponceTimeCapsuleUpdateAction(TimeCapsuleDto capsule);
    ResponceMsg ResponceTimeCapsuleDeleteAction(int id);
    ResponceMsg ResponceTimeCapsuleDeleteByOwnerAction(int id, int ownerUserId);
}
