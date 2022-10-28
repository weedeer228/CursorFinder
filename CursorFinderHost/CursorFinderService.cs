using CursorFinder.Contollers;
using CursorFinder.Models;
using CursorFinderHost.Contollers;
using CursorFinderHost.Models;
using System.Collections.Generic;
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

        public async Task AddNewCursorPositionAsync(int xPos, int Ypos, MouseActionType actionType)
        {
            await CursorFinderController.AddCursorPositionAsync(xPos, Ypos, actionType);
            SendNotificationIfRequired();
        }

        public int Auth(bool isAdmin) => AuthController.Auth(isAdmin ? UserRole.Admin : UserRole.User);

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

        private async void SendNotification(string message) =>   await MailController.SendMessageAsync(message);

        /// <summary>
        /// Отправляет уведомление если выполнено условие
        /// </summary>
        private async void SendNotificationIfRequired()
        {
            var recordsCount = await _cursorFinderController.GetRecordsCountAsync();
            if (_isNotificationEnabled && recordsCount > 0 && recordsCount % 50 == 0)
                SendNotification("total db record count: " + recordsCount.ToString());
        }

        public async Task<IList<CursorPosition>> GetAllCursorPositions() => await CursorFinderController.GetAllCursorPositionsAsync();

        public async Task<int> GetDbRecordsCount() => await CursorFinderController.GetRecordsCountAsync();

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
