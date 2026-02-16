using Aplication.Interfaces.IQuery;
using Domain.Entityes;
using Infraestructure.Perssistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Query
{
    public class DeliveryTypeQuery : IDeliveryTypeQuery
    {
        private readonly AppDbContext _context;

        public DeliveryTypeQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<DeliveryType>> GetDeliveryTypes()
        {
            return await _context.DeliveryTypes.ToListAsync();
        }
    }
}
