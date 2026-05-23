using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDataLibrary.Interfaces
{
    public interface IDataSerializer
    {
        Task SerializeAsync<T>(Stream stream, T data);
        Task<T> DeserializeAsync<T>(Stream stream);
    }
}
