using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.Servicios
{
    public interface IFileStorage
    {
        Task<string> EditArchive(byte[] content, string extension, string container, string route,
            string contentType);
        Task DeleteArchive(string route, string container);
        Task<string> SaveArchive(byte[] content, string extension, string container, string contentType);
    }
}