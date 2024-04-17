
using Microsoft.AspNetCore.Http;

namespace E_CommerceAPI.Application.Services
{
    public interface IFileService
    {
        Task<List<(string fileName,string path)>> UploadAsync(string path, IFormFileCollection files); //todo //  Task<List<(string fileName,string path)>>  burdaki (string fileName,string path)  bu methodum geriye retrn olaraq bunu dondurur bunda Tupple deyilir 

      

        Task<bool> CopyFileAsync(string path,IFormFile file);
    }
}
