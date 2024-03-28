using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public class PlaceParams
    {
        public int PageSize { get; set; } = 0;
        public int PageIndex { get; set; } = 0;
        public List<int>? UtilityIds { get; set; } = null;
        public int MinPrice { get; set; } = 0;
        public int MaxPrice { get; set; } = 0;
    }
}
