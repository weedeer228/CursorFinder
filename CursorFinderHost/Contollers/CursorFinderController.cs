using CursorFinder.Models;
using CursorFinder.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CursorFinder.Contollers
{
    /// <summary>
    /// Контроллера для работы с БД
    /// </summary>
    public class CursorFinderController
    {
        private readonly CursorFinderDbContext _dbContext;
        public CursorFinderController()
        {
            _dbContext = new CursorFinderDbContext();
        }

        public async Task AddCursorPositionAsync(int xPos, int YPos, MouseActionType actionType, int userToken)
        {
            _dbContext.CursorPositions.Add(new CursorPosition()
            {
                XPos = xPos,
                YPos = YPos,
                DateTime = DateTime.Now,
                ActionType = actionType,
                UserAuthToken = userToken
            });
            await _dbContext.SaveChangesAsync();
            //System.Console.WriteLine($"Added: xPos: {xPos} yPos: {YPos}");
        }
        internal async Task<IList<CursorPosition>> GetAllCursorPositionsAsync()
        {
            var cursorPositions = await _dbContext.CursorPositions.ToListAsync();
            return cursorPositions.Skip(cursorPositions.Count() - 1000).ToList();
        }
        public async Task<IList<CursorPosition>> GetCursorPositionsByUserTokenAsync(int userToken)
        {
            var cursorPositions = (await _dbContext.CursorPositions.ToListAsync()).Where(e=>e.UserAuthToken.Equals(userToken));
            return cursorPositions.Skip(cursorPositions.Count() - 1000).ToList();
        }


        public async Task ClearDbAsync()
        {
            foreach (var position in _dbContext.CursorPositions)
            {
                _dbContext.CursorPositions.Remove(position);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveAll(Func<CursorPosition, bool> predicate = default)
        {
            if (predicate is null)
                await ClearDbAsync();
            else
            {
                var elemetsForDelete = _dbContext.CursorPositions.Where(predicate);
                foreach (var elemet in elemetsForDelete)
                    _dbContext.CursorPositions.Remove(elemet);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> GetRecordsCountAsync(int userToken) => await _dbContext.CursorPositions.CountAsync(e => e.UserAuthToken.Equals(userToken));

    }
}
