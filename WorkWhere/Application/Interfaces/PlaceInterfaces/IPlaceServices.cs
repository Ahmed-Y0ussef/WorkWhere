using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;


namespace Application.Interfaces.PlaceInterfaces
{
    public interface IPlaceServices
    {
        public Task<IEnumerable> GetAllPlacesAsync();

        public Task Addplace();
        public void UpdatePlace();
        public void DeletePlace();
        public Task GetPlaceById(int id);
    }
}
