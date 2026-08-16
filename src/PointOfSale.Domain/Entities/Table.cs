using PointOfSale.Domain.Enums;

namespace PointOfSale.Domain.Entities
{
    public class Table
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public int Number { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public int Capacity { get; private set; }
        public TableStatus Status { get; private set; } = TableStatus.Free;
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        private Table() { }

        public static Table Create(string name, int number, string description, int capacity = 4)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("O nome da mesa é obrigatório.", nameof(name));

            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "A capacidade deve ser maior que zero.");

            return new Table
            {
                Id = Guid.NewGuid(),
                Name = name,
                Number = number,
                Description = description,
                Capacity = capacity,
                Status = TableStatus.Free,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void UpdateDetails(string name, int number, string description, int capacity)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("O nome da mesa é obrigatório.", nameof(name));

            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "A capacidade deve ser maior que zero.");

            Name = name;
            Number = number;
            Description = description;
            Capacity = capacity;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Occupy()
        {
            if (Status == TableStatus.Unavailable)
                throw new InvalidOperationException("Mesa indisponível não pode ser ocupada.");

            Status = TableStatus.Occupied;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Reserve()
        {
            if (Status != TableStatus.Free)
                throw new InvalidOperationException("Apenas mesas disponíveis podem ser reservadas.");

            Status = TableStatus.Reserved;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Release()
        {
            Status = TableStatus.Free;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MakeUnavailable()
        {
            if (Status == TableStatus.Occupied)
                throw new InvalidOperationException("Não é possível tornar uma mesa ocupada indisponível.");

            Status = TableStatus.Unavailable;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Close()
        {
            if (Status != TableStatus.Occupied)
                throw new InvalidOperationException("Apenas mesas ocupadas podem entrar em fase de fechamento.");

            Status = TableStatus.Closed;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}