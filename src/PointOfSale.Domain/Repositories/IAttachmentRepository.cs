using PointOfSale.Domain.Entities;

namespace PointOfSale.Domain.Repositories
{
    public interface IAttachmentRepository
    {
        Task<IEnumerable<Attachment>> GetAll();
        Task<Attachment?> GetById(Guid id);
        Task<IReadOnlyCollection<Attachment>> GetByIds(IEnumerable<Guid> ids);
        Task Add(Attachment attachment);
        void Update(Attachment attachment);
        void Delete(Attachment attachment);
    }
}
