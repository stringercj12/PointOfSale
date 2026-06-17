namespace PointOfSale.Application.Exceptions
{
    public abstract class PointOfSaleBaseException : SystemException
    {
        protected PointOfSaleBaseException(string message) : base(message)
        {
        }

        public abstract int StatusCode { get; }
        public abstract List<string> GetErrors();
    }
}
