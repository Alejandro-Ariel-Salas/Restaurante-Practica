using Aplication.Interfaces.IQuery;
using Domain.Entityes;
using Infraestructure.Perssistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Query
{
    public class StatusQuery: IStatusQuery
    {
        private readonly AppDbContext _Context;

        public StatusQuery(AppDbContext context)
        {
            _Context = context;
        }

        public async Task<List<Status>> GetAllStatuses()
        {
            return await _Context.Statuses.ToListAsync();
        }
    }
}
