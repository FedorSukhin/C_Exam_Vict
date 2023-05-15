﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C_Exam_Vict.Model;
using C_Exam_Vict.Repositories;

namespace C_Exam_Vict.Services;

internal class UserService
{
    private static UserModel? _currentUser;
    private UserRepos _userRepos;
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
        return _currentUser;
    }
    public UserModel? GetCurrentUser() => _currentUser;
}

