using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;


namespace AccountManagement.Application;

public class AccountApplication:IAccountApplication
{
    private readonly IAccountRepository _accountRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IFileUploader _fileUploader;
    private readonly IAuthHelper _authHelper;
     
    public AccountApplication(IAccountRepository accountRepository, IPasswordHasher passwordHasher,
        IFileUploader fileUploader, IAuthHelper authHelper)
    {
        _accountRepository = accountRepository;
        _passwordHasher = passwordHasher;
        _fileUploader = fileUploader;
        _authHelper = authHelper;
    }

    public OperationResult Create(CreateAccount command)
    {
        var operation=new OperationResult();
        if (_accountRepository.Exists(x => x.Username == command.Username || x.Mobile == command.Mobile))
        {
            return operation.Failed(ApplicationMessage.DuplicatedRecord);
        }

        if (command.Password != command.RePassword)
        {
            return operation.Failed(ApplicationMessage.PasswordNotMatch);
        }
        var password = _passwordHasher.Hash(command.Password);
        var picturePath = "ProfilePhotos";
        var fileName = _fileUploader.Upload(command.ProfilePhoto, picturePath);
        var account = new Account(command.Fullname,command.Username,password,command.Mobile,command.RoleId,
            fileName);
        _accountRepository.Create(account);
        _accountRepository.SaveChanges();
        return operation.Succedded();
    }

    public OperationResult Edit(EditAccount command)
    {
        var operation = new OperationResult();
        var account = _accountRepository.Get(command.Id);
        if (account == null)
        {
            return operation.Failed(ApplicationMessage.RecordNotFound);
        }
        if(_accountRepository.Exists(x=>(x.Username==command.Username || x.Mobile==command.Mobile) && 
               x.Id != command.Id))
        {
            return operation.Failed(ApplicationMessage.DuplicatedRecord);
        }
        var picturePath = "ProfilePhotos";
        var fileName = _fileUploader.Upload(command.ProfilePhoto, picturePath);
        account.Edit(command.Fullname,command.Username,command.Mobile,command.RoleId,fileName);
        _accountRepository.SaveChanges();
        return operation.Succedded();
    }

    public OperationResult ChangePassword(ChangePassword command)
    {
        var operation=new OperationResult();
        var account = _accountRepository.Get(command.Id);
        if (account == null)
        {
            return operation.Failed(ApplicationMessage.RecordNotFound);
        }

        if (command.Password != command.RePassword)
        {
            return operation.Failed(ApplicationMessage.PasswordNotMatch);
        }

        var password = _passwordHasher.Hash(command.Password);
        account.ChangePassword(password);
        _accountRepository.SaveChanges();
        return operation.Succedded(); 

    }

    public OperationResult Login(Login command)
    {
        var operation = new OperationResult();
        var account = _accountRepository.GetBy(command.UserName);
        if (account == null)
        {
            return operation.Failed(ApplicationMessage.WrongUserPass);
        }

        (bool Verified,bool NeedsUpgrade) result = _passwordHasher.Check(account.Password, command.Password);

        if (!result.Verified)
        {
            return operation.Failed(ApplicationMessage.WrongUserPass);
        }

        var authViewModel = new AuthViewModel(account.Id, account.RoleId, account.Fullname, account.Username,account.ProfilePhoto);
        _authHelper.Signin(authViewModel);
        return operation.Succedded();
    }

    public EditAccount GetDetails(long id)
    {
        return _accountRepository.GetDetails(id);
    }

    public List<AccountViewModel> Search(AccountSearchModel searchModel)
    {
        return _accountRepository.Search(searchModel);
    }

    public void Logout()
    {
        _authHelper.SignOut();
    }
}