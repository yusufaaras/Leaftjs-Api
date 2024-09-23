using MediatR;
using MoskBilisimAPI.Context;
using MoskBilisimAPI.Core;
using MoskBilisimAPI.CQRS.Commands;
using MoskBilisimAPI.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MoskBilisimAPI.CQRS.Handlers
{
    public class CreateShapeCommandHandler : IRequestHandler<CreateShapeCommand, int>
    {
        private readonly MoskBilisimDbContext _context;

        public CreateShapeCommandHandler(MoskBilisimDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateShapeCommand request, CancellationToken cancellationToken)
        {
            _context.MapData.Add(request.MapData);
            await _context.SaveChangesAsync(cancellationToken);
            return request.MapData.Id;
        }

    }
}
