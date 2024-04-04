using Azure;
using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public class Params
    {
        public int PageSize { get; set; } = 0;
        public int PageIndex { get; set; } = 0;
        public Status Status { get; set; }
        public List<int>? UtilityIds { get; set; } = null;
        public int MinPrice { get; set; } = 0;
        public int MaxPrice { get; set; } = 0;
        public string? Search {  get; set; }=null;
        public int UserId { get; set; } = 0;
    }
}
