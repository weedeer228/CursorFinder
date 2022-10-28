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

        public async Task AddCursorPositionAsync(int xPos, int YPos, MouseActionType actionType)
        {
            _dbContext.CursorPositions.Add(new CursorPosition()
            {
                XPos = xPos,
                YPos = YPos,
                DateTime = DateTime.Now,
                ActionType = actionType
            });
            await _dbContext.SaveChangesAsync();
            //System.Console.WriteLine($"Added: xPos: {xPos} yPos: {YPos}");
        }
        public async Task<IList<CursorPosition>> GetAllCursorPositionsAsync()
        {
            var cursorPositions = await _dbContext.CursorPositions.ToListAsync();
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

        public async Task<int> GetRecordsCountAsync() => await _dbContext.CursorPositions.CountAsync();


    }
}
