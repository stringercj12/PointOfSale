using Microsoft.EntityFrameworkCore;
using PointOfSale.Domain.Entities;
using PointOfSale.Domain.Repositories;
using PointOfSale.Infra.DataAccess;

namespace PointOfSale.Infra.Persistence.Repositories
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly PointOfSaleDbContext _context;

        public AttachmentRepository(PointOfSaleDbContext context)
        {
            _context = context;
        }

        public async Task Add(Attachment attachment)
        {
             await _context.Attachments.AddAsync(attachment);
        }

        public void Delete(Attachment attachment)
        {
            _context.Attachments.Remove(attachment);
        }

        public async Task<IEnumerable<Attachment>> GetAll()
        {
            return await _context.Attachments.AsNoTracking().ToListAsync();
        }

        public async Task<Attachment?> GetById(Guid id)
        {
            return await _context.Attachments.AsNoTracking().Where(attachment => attachment.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyCollection<Attachment>> GetByIds(IEnumerable<Guid> ids)
        {
            if (ids == null || !ids.Any()) return Array.Empty<Attachment>();

            var attachmentsIds = ids.ToList();

            return await _context.Attachments
                .AsNoTracking()
                .Where(c => attachmentsIds.Contains(c.Id))
                .ToListAsync();
        }

        public void Update(Attachment attachment)
        {
            _context.Attachments.Update(attachment);
        }
    }
}
