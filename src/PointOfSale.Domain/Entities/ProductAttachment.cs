namespace PointOfSale.Domain.Entities
{
    public class ProductAttachment
    {
        private ProductAttachment()
        {
        }

        public ProductAttachment(Product product, Attachment attachment, bool isPrimary, int displayOrder)
        {
            Product = product;
            ProductId = product.Id;

            Attachment = attachment;
            AttachmentId = attachment.Id;

            IsPrimary = isPrimary;
            DisplayOrder = displayOrder;
        }


        public Guid ProductId { get; private set; }
        public Guid AttachmentId { get; private set; }

        public bool IsPrimary { get; private set; }

        public int DisplayOrder { get; private set; }

        public Attachment Attachment { get; private set; } = null!;
        public Product Product { get; private set; } = null!;

        public void SetPrimary(bool isPrimary)
        {
            IsPrimary = isPrimary;
        }
    }
}
