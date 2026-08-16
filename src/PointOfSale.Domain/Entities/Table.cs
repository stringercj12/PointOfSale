using PointOfSale.Domain.Enums;

namespace PointOfSale.Domain.Entities
{
    public class Table
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public TableStatus Status { get; private set; }

        public Table(string name, string description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Status = TableStatus.Available; 
        }

        public void UpdateDetails(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void Occupy()
        {
            Status = TableStatus.Occupied;
        }

        public void Reserve()
        {
            Status = TableStatus.Reserved;
        }

        public void Release()
        {
            Status = TableStatus.Available;
        }

        public void MakeUnavailable()
        {
            Status = TableStatus.Unavailable;
        }

        public void Close()
        {
            Status = TableStatus.Closed;
        }
    }
}
