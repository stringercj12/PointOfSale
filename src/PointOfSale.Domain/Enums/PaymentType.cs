namespace PointOfSale.Domain.Enums
{
    public enum PaymentType
    {
        Cash = 1,              // Dinheiro
        CreditCard = 2,        // Cartão de Crédito
        DebitCard = 3,         // Cartão de Débito
        Pix = 4,               // PIX
        BankTransfer = 5,      // Transferência Bancária
        Voucher = 6,           // Vale/Refeição/Alimentação
        GiftCard = 7,          // Cartão Presente
        Check = 8,             // Cheque
        Other = 99             // Outro
    }
}
