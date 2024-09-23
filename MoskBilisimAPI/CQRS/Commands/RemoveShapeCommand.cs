using MediatR;

namespace MoskBilisimAPI.CQRS.Commands
{
    public class RemoveShapeCommand : IRequest<bool>
    {
        public int Id { get; }

        public RemoveShapeCommand(int id)
        {
            Id = id;
        }
    }
}
