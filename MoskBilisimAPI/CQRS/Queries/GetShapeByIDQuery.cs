using MediatR;

namespace MoskBilisimAPI.CQRS.Queries
{
    public class GetShapeByIDQuery : IRequest<MapData>
    {
        public GetShapeByIDQuery(int id)
        {
            this.id = id;
        }

        public int id { get; set; }

    }
}
