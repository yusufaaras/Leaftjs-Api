using MediatR;

namespace MoskBilisimAPI.CQRS.Commands
{
    public class CreateShapeCommand : IRequest<int>
    {
        public MapData MapData { get; set; }
    }

}
