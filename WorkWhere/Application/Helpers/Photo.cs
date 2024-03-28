using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public static class Photo
    {
        public static byte[] ConverToIArrayOfByes(this IFormFile formFile)
        {
            if (formFile == null)
                return null;
            using (var ms  = new MemoryStream())
            {
                formFile.CopyTo(ms);
                return ms.ToArray();
            }
        }

    }
}
