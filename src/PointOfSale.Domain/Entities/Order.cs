using PointOfSale.Domain.Enums;
using PointOfSale.Domain.Exceptions;

namespace PointOfSale.Domain.Entities
{
    public class Order
    {
        private readonly List<OrderItem> _items = [];
        private Order()
        {
        }


        public Guid Id { get; private set; }
        public DateTime DateOfSale { get; private set; }
        public OrderStatus OrderStatus { get; private set; }
        public decimal TotalGeneral { get; private set; }
        public PaymentType PaymentType { get; private set; }
        public Guid UserId { get; private set; }
        public User User { get; private set; } = null!;
        public Guid? CustomerId { get; private set; }
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

        public static Order Create(Guid userId, Guid? customerId, PaymentType paymentType)
        {
            if (userId == Guid.Empty)
                throw new DomainException("Usuário inválido.");

            if (!Enum.IsDefined(paymentType))
                throw new DomainException("Forma de pagamento inválida.");


            return new Order
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CustomerId = customerId == Guid.Empty ? null : customerId,
                PaymentType = paymentType,
                OrderStatus = OrderStatus.Pending,
                DateOfSale = DateTime.UtcNow,
                TotalGeneral = 0
            };
        }

        public void AddItem(OrderItem item)
        {
            if (item is null)
                throw new DomainException("Item inválido.");

            if (OrderStatus != OrderStatus.Pending)
                throw new DomainException(
                    "Somente pedidos pendentes podem ser alterados.");

            var existing = _items.FirstOrDefault(i => i.ProductId == item.ProductId);

            if (existing != null)
            {
                existing.IncreaseQuantity(item.Quantity);
            }
            else
            {
                _items.Add(item);
            }

            CalculateTotal();
        }

        private void CalculateTotal()
        {
            TotalGeneral = _items.Sum(i => i.SubTotal);
        }

        public void RemoveItem(Guid productId)
        {
            var existing = _items.FirstOrDefault(i => i.ProductId == productId);

            if (existing != null)
            {
                _items.Remove(existing);
            }
        }

        public void MarkAsPaid()
        {
            if (OrderStatus != OrderStatus.Pending)
                throw new DomainException("Somente pedidos pendentes podem ser pagos.");

            OrderStatus = OrderStatus.Paid;
        }


        public void Cancel()
        {
            if (OrderStatus != OrderStatus.Pending)
                throw new DomainException("Somente pedidos pendentes podem ser cancelados.");

            OrderStatus = OrderStatus.Cancelled;
        }

        public void MarkAsPreparing()
        {
            if (OrderStatus != OrderStatus.Paid)
                throw new DomainException("Somente pedidos pagos podem ser enviados para preparar.");

            OrderStatus = OrderStatus.Preparing;
        }


        public void MarkAsShipped()
        {
            if (OrderStatus != OrderStatus.Preparing)
                throw new DomainException("Somente pedidos sendo preparados podem ser marcados como enviados.");

            OrderStatus = OrderStatus.Shipped;
        }

        public void Complete()
        {
            if (OrderStatus != OrderStatus.Shipped)
                throw new DomainException("Somente pedidos enviados podem ser marcados como entregue.");

            OrderStatus = OrderStatus.Delivered;
        }

    }
}

