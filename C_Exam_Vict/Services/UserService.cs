using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C_Exam_Vict.Model;
using C_Exam_Vict.Repositories;

namespace C_Exam_Vict.Services;

public class UserService
{
    private static UserModel? _currentUser;
    private UserRepos _userRepos;
    public event EventHandler OnUserChange;
    public UserService()
    {
        _userRepos = new UserRepos();
        
    }
    public UserModel SingIn(string login, string password)
    {
        var user = _userRepos.GetUserByLogin(login);

        if (user == null)
        {
            throw new Exception("Login not found");
        }
        if (user.Password != password)
        {
            throw new Exception("Password not corect"); 
        }
        _currentUser = new UserModel()
        {
            Id = user.Id,
            Login = login,
            Password = password
        };
        OnUserChange(null,null);
        return _currentUser;
    }
    public void SingOut(string login, string password) 
    {
        var user =_userRepos.SetUserDB(login,password);
        if (user != null)
        {
            throw new Exception("Login alredy exist");
        }
        else 
        {
            throw new Exception("Acount successful create");
        }
    }
    public UserModel? GetCurrentUser() => _currentUser;

    public void LogOut()
    {
        _currentUser = null;
        OnUserChange(this, EventArgs.Empty);

    }

}
   

