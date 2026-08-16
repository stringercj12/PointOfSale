using PointOfSale.Domain.Exceptions;

namespace PointOfSale.Domain.Entities
{
    public class OrderItem
    {
        private OrderItem() { }

        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal SubTotal => Quantity * UnitPrice;

        public Product Product { get; private set; } = null!;
        public Order Order { get; private set; } = null!;


        public static OrderItem CreateItem(Guid orderId, Guid productId, int quantity, decimal unitPrice)
        {
            if (orderId == Guid.Empty)
                throw new DomainException("Informe o pedido.");

            if (productId == Guid.Empty)
                throw new DomainException("Informe o produto.");

            if (quantity <= 0)
                throw new DomainException("A quantidade deve ser maior que zero.");

            if (unitPrice <= 0)
                throw new DomainException("O preço unitário deve ser maior que zero.");



            return new OrderItem()
            {
                OrderId = orderId,
                ProductId = productId,
                Quantity = quantity,
                UnitPrice = unitPrice
            };

        }

        public void IncreaseQuantity(int quantity)
        {
            Quantity += quantity;
        }


        public void ChangeQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new DomainException("A quantidade deve ser maior que zero.");

            Quantity = quantity;
        }
    }
}
