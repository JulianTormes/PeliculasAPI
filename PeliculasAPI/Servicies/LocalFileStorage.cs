using Microsoft.AspNetCore.Http;
using PeliculasAPI.Servicios;

namespace PeliculasAPI.Servicies
{
    public class LocalFileStorage : IFileStorage
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LocalFileStorage(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }
        public Task DeleteArchive(string route, string container)
        {
            if (route != null)
            {
                var nameArchive = Path.GetFileName(route);
                string directoryArchive = Path.Combine(_env.WebRootPath, container, nameArchive);

                if (File.Exists(directoryArchive))
                {
                    File.Delete(directoryArchive);
                }
            }

            return Task.FromResult(0);
        }

        public async Task<string> EditArchive(byte[] content, string extension, string container, string route, string contentType)
        {
            await DeleteArchive(route, container);
            return await SaveArchive(content, extension, container, contentType);
        }

        public async Task<string> SaveArchive(byte[] content, string extension, string container, string contentType)
        {
            var nameArchive = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(_env.WebRootPath, container);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            string ruta = Path.Combine(folder, nameArchive);
            await File.WriteAllBytesAsync(ruta, content);
            var urlActual = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            var urlParaBD = Path.Combine(urlActual, container, nameArchive).Replace("\\", "/");
            return urlParaBD;

        }
    }
}
