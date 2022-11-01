using CursorFinder.Contollers;
using CursorFinder.Models;
using CursorFinderHost.Contollers;
using CursorFinderHost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursorFinder.Services
{
    /// <summary>
    /// Сервис для сообщения с клиентом
    /// </summary>
    public class CursorFinderService : ICursorFinderService
    {
        private CursorFinderController _cursorFinderController;
        private AuthController _authController;
        private MailController _mailController;
        private bool _isNotificationEnabled;

        /// <summary>
        /// Ленивая подгрузка контроллеров
        /// </summary>
        private CursorFinderController CursorFinderController
        {
            get
            {
                if (_cursorFinderController is null)
                {
                    _cursorFinderController = new CursorFinderController();

                    System.Console.WriteLine("Connected to database...");
                }
                return _cursorFinderController;
            }
        }
        private AuthController AuthController
        {
            get
            {
                if (_authController is null)
                    _authController = new AuthController();
                return _authController;
            }
        }
        private MailController MailController
        {
            get
            {
                if (_mailController is null)
                    _mailController = new MailController();
                return _mailController;
            }
        }
        public async Task AddNewCursorPositionAsync(int xPos, int Ypos, MouseActionType actionType, int userToken)
        {
            await CursorFinderController.AddCursorPositionAsync(xPos, Ypos, actionType, userToken);
            SendNotificationIfRequired(userToken);
        }

        public int Auth(bool isAdmin, int? token = default) => AuthController.Auth(isAdmin ? UserRole.Admin : UserRole.User, token);

        public async Task<bool> ClearDb(int userAuthToken)
        {
            if (!AuthController.IsUserAdmin(userAuthToken)) return false;
            await _cursorFinderController.ClearDbAsync();
            return true;
        }

        public void DisableNotification()
        {
            _isNotificationEnabled = false;
            System.Console.WriteLine("Notifications Disabled");
        }

        public void EnableNotification()
        {
            _isNotificationEnabled = true;
            System.Console.WriteLine("Notifications Enabled");
        }

        private async void SendNotification(string message) => await MailController.SendMessageAsync(message);

        /// <summary>
        /// Отправляет уведомление если выполнено условие
        /// </summary>
        private async void SendNotificationIfRequired(int userToken)
        {
            var recordsCount = await _cursorFinderController.GetRecordsCountAsync(userToken);
            if (_isNotificationEnabled && recordsCount > 0 && recordsCount % 50 == 0)
                SendNotification("total db record count: " + recordsCount.ToString());
        }
        /// <summary>
        /// Удаляет записи пользователей, которых нет
        /// (Нужно чтобы не добавлять полноценную систему авторизации с логином и паролем и при этом добавить сессионность)
        /// </summary>
        /// <returns></returns>
        private async Task ClearRecordsOfUsersThatDontExist()
        {
            var usersTokens = (await CursorFinderController.GetAllCursorPositionsAsync()).Select(r => r.UserAuthToken).Distinct();
            foreach (var token in usersTokens)
                if (!AuthController.IsUserExist(token))
                    await CursorFinderController.RemoveAll(e => e.UserAuthToken.Equals(token));
        }

        public async Task<IList<CursorPosition>> GetAllCursorPositions() => await CursorFinderController.GetAllCursorPositionsAsync();

        public async Task<int> GetDbRecordsCount(int userToken)
        {
            try
            {
                var result = await CursorFinderController.GetRecordsCountAsync(userToken);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        public bool IsUSerAdmin(int userToken) => AuthController.IsUserAdmin(userToken);

        public bool StartRecord()
        {
            System.Console.WriteLine("Starting Recording");
            return true;
        }

        public bool StopRecord()
        {
            System.Console.WriteLine("Stopping Recording...");
            return true;
        }
    }
}
