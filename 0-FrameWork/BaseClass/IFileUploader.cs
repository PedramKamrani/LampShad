

using Microsoft.AspNetCore.Http;

namespace _0_FrameWork.BaseClass
{
   public interface IFileUploader
    {
        string Upload(IFormFile file,string path);
    }
}
