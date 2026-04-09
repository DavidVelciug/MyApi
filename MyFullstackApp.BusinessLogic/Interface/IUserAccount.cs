using MyFullstackApp.Domains.Models.Base;
using MyFullstackApp.Domains.Models.User;

namespace MyFullstackApp.BusinessLogic.Interface;

public interface IUserAccount
{
    List<UserAccountDto> GetAllUserAccountsAction();
    UserAccountDto? GetUserAccountByIdAction(int id);
    ResponceMsg ResponceUserAccountCreateAction(UserAccountDto user);
    ResponceMsg ResponceUserAccountUpdateAction(UserAccountDto user);
    ResponceMsg ResponceUserAccountDeleteAction(int id);
}
