using Application.Interfaces.PlaceInterfaces;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.PlaceServices
{
    public class PlaceService : IPlaceServices
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public PlaceService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }


        public Task Addplace()
        {
            throw new NotImplementedException();
        }

        public void DeletePlace()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable> GetAllPlacesAsync()
        => await UnitOfWork.GetRepo<Place>().GetAllAsync(
            p => p.PlacePhotos,
            p => p.Rooms,
             p => p.PlaceReviews,
             p => p.PlaceUtilities
            );

        public async Task GetPlaceById(int id)
        => await UnitOfWork.GetRepo<Place>().GetById(id,
                p => p.PlacePhotos,
            p => p.Rooms,
             p => p.PlaceReviews,
             p => p.PlaceUtilities
                );

        public void UpdatePlace()
        {
            throw new NotImplementedException();
        }
    }
}
