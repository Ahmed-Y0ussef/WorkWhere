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
        public static byte[] ConverToIArrayOfByes(this string photo)
            => photo == null ? null : Convert.FromBase64String(photo.Split(',')[1]);
       
        public static string ConvertToPhoto(this byte[] data)
            => $"data:image/jpg;base64,{Convert.ToBase64String(data)}";

    }
}
