using CursorFinder.Models;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace CursorFinder.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICursorFinderService" in both code and config file together.
    [ServiceContract]
    public interface ICursorFinderService
    {
        [OperationContract]
        Task AddNewCursorPositionAsync(int xPos, int Ypos, MouseActionType actionType, int userToken);
        [OperationContract]
        bool StartRecord();
        [OperationContract]
        bool StopRecord();
        [OperationContract]
        Task<IList<CursorPosition>> GetAllCursorPositions();
        [OperationContract]
        int Auth(bool isAdmin);
        [OperationContract]
        Task<bool> ClearDb(int userAuthToken);
        [OperationContract]
        bool IsUSerAdmin(int userAuthToken);
        [OperationContract]
        Task<int> GetDbRecordsCount(int userToken);
        [OperationContract]
        void EnableNotification();
        [OperationContract]
        void DisableNotification();
    }
}
