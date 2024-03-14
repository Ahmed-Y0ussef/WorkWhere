using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class PlaceUtilities
    {
        public int Id { get; set; }
        public string UtilityName {  get; set; }
        public int PlaceId { get; set; }
        public Place Place { get; set; }

    }
}
