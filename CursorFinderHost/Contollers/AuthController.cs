using CursorFinderHost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace CursorFinderHost.Contollers
{
    internal class AuthController
    {
        /// <summary>
        /// Контроллер авторизованных пользователей
        /// </summary>
        private readonly IList<User> _users;

        public AuthController()
        {
            _users = new List<User>();
        }

        public bool IsUserExist(int userToken)
        {
            return _users.Any(u => u.Token == userToken);
        }
        private bool IsUserExist(string name, UserRole role)
        {
            return _users.Any(u => u.Name.Equals(name) && u.Role == role);
        }
        /// <summary>
        /// При добавлении нового пользователя задается уникальный токен, по которому далее определяется роль пользователя
        /// </summary>
        /// <param name="name"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        private User GetOrCreateUser(string name, UserRole role)
        {
            if (IsUserExist(name, role))
                return _users.First(u => u.Name.Equals(name) && u.Role == role);
            var user = new User(name, Guid.NewGuid().GetHashCode(), role);
            AddUser(user);
            return user;
        }
        private User GetUserByToken(int userToken)
        {
            if (IsUserExist(userToken))
                return _users.Single(u => u.Token == userToken);
            throw new System.Exception("User not found");
        }

        public bool IsUserAdmin(int userToken)
        {
            return GetUserByToken(userToken).Role == UserRole.Admin;
        }
        public void AddUser(User user)
        {
            if (IsUserExist(user.Token))
                throw new System.Exception("User with same token already exist!!!");
            _users.Add(user);
        }

        public void ChageUserRole(int userToken, bool isAdmin) => GetUserByToken(userToken).Role = isAdmin ? UserRole.Admin : UserRole.User;
        public int Auth(UserRole role)
        {
            if (!ServiceSecurityContext.Current.PrimaryIdentity.IsAuthenticated)
            {
                Console.WriteLine("User not defined Auth failed");
                return -1;
            }
            var name = ServiceSecurityContext.Current.PrimaryIdentity.Name;
            Console.WriteLine($"Auth Succes\nuser name: {name}\nuser role: {role}");
            return GetOrCreateUser(name, role).Token;
        }
    }
}
