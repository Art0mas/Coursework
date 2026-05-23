using AsyncDataLibrary.Interfaces;
using AsyncDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDataLibrary.Services
{
    public class GameSaveService
    {
        private IRepository<GameSave> _repository;
        public GameSaveService(IRepository<GameSave> repository)
        {
            _repository = repository;
        }
        public async Task<List<GameSave>> GetBooksAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task AddBookAsync(GameSave gameSave)
        {
            if (gameSave.CurrentHealth <= 0 || string.IsNullOrWhiteSpace(gameSave.LastActivatedCheckpointId))
                throw new ArgumentException("Здоров'я не може бути збережено нульове.");

            await _repository.AddAsync(gameSave);
        }
    }
}
