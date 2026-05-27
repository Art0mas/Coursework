using AsyncDataLibrary.Infrastructure;
using AsyncDataLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDataLibrary.Repositories
{
    public class JsonRepository<T> : IRepository<T> where T : IEntity
    {
        private string _filePath;
        private IDataSerializer _serializer;
        private FileStorageProvider _fileProvider;
        public JsonRepository(string filePath, IDataSerializer serializer, FileStorageProvider fileProvider)
        {
            _filePath = filePath;
            _serializer = serializer;
            _fileProvider = fileProvider;
        }

        public async Task<List<T>> GetAllAsync()
        {
            if (!_fileProvider.FileExists(_filePath)) return new List<T>();
            using (Stream stream = _fileProvider.GetReadStream(_filePath))
            {
                var data = await _serializer.DeserializeAsync<List<T>>(stream);
                return data ?? new List<T>();
            }
        }
        public async Task AddAsync(T entity)
        {
            var items = await GetAllAsync();

            if (items.Count == 0) entity.Id = 1;
            else entity.Id = items.Max(x => x.Id) + 1;

            items.Add(entity);
            using (Stream stream = _fileProvider.GetWriteStream(_filePath))
            {
                await _serializer.SerializeAsync(stream, items);
            }
        }
    }
}
