using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ScreenplayApp.Application.Commands;
using ScreenplayApp.Application.Mapper;
using ScreenplayApp.Application.Responses;
using ScreenplayApp.Core.Entities;
using ScreenplayApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScreenplayApp.Application.Handlers.CommandHandlers
{
    public class CreateBookingHandler : IRequestHandler<CreateBookingCommand, BookingResponse>
    {
        private readonly IBookingRepository _bookingRepo;
        private readonly ITicketRepository _ticketRepo;
        private readonly UserManager<AppUser> _userManager;

        public CreateBookingHandler(IBookingRepository bookingRepo, ITicketRepository ticketRepo, UserManager<AppUser> userManager)
        {
            _bookingRepo = bookingRepo;
            _ticketRepo = ticketRepo;
            _userManager = userManager;
        }

        public async Task<BookingResponse> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            if (request.Date <= DateTime.Now) throw new ApplicationException("Can not book this ticket");

            // Get available tickets
            var ticketQuery = await _ticketRepo.GetAllTicketsQueryAsync();
            List<Ticket> tickets = await ticketQuery.Include(t => t.Screenplay).Where( x=> x.Date == request.Date
                && x.Date > DateTime.Now && x.Location == request.Location
                && x.IsAvailable == true && x.Screenplay.Id == request.ScreenplayId).Take(request.NumberOfTickets).ToListAsync();

            if(tickets.Count() < request.NumberOfTickets) throw new ApplicationException("Not enough tickets");

            // Get user
            var userEntitiy = await _userManager.Users
                .SingleOrDefaultAsync(x => x.Id == request.AppUserId);

            // Create new booking
            var newBooking = new Booking
            {
                AppUser = userEntitiy,
            };

            await _bookingRepo.AddAsync(newBooking);

            foreach (Ticket ticket in tickets)
            {
                var t = await _ticketRepo.GetByIdAsync(ticket.Id);
                t.IsAvailable = false;
                t.Booking = newBooking;
                await _ticketRepo.UpdateAsync(t);
            }

            var bookingResponse = ScreenplayAppMapper.Mapper.Map<BookingResponse>(newBooking);
            return bookingResponse;
        }
    }
}
