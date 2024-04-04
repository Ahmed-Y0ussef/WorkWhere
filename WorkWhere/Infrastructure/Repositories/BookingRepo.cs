using Core.Entities;
using Core.Interfaces;
using Infrastructure.Dbcontext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BookingRepo : IBookingRepo
    {
        ApplicationDbContext AppContext;
       
        public BookingRepo(ApplicationDbContext _AppContext)
        {
            AppContext = _AppContext;

        }
        public async Task AddBooking(RoomBooking roomBooking)
        {
            await AppContext.AddAsync(roomBooking);
           await AppContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<RoomBooking>> GetAvailableBooking()
        => await AppContext.Set<RoomBooking>().ToListAsync();
    }
}
