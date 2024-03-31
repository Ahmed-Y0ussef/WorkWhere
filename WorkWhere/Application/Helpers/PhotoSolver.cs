using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public static class PhotoSolver
    { 
        public static async Task<byte[]> ConvertToArrayOfBytes( this IFormFile formfile)
        {
            if (formfile == null)
                return null;

            using (var photo = new MemoryStream())
            {
                await formfile.CopyToAsync(photo);
                return photo.ToArray();
            }
        }
    } 
}
