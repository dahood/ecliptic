namespace ShoppingCartApp.Domain.Currency
{
    public class Money
    {
        public decimal Amount { get; private set; }

        public Money(decimal amount)
        {
            Amount = amount;
        }
        public Money(double amount)
        {
            Amount = (decimal)amount;
        }

        public static string Symbol => "£";

        public static Money Zero => new Money(0M);

        public override string ToString()
        {
            return Amount.ToString("0.00");
        }

        public string ToStringWithSymbol()
        {
            return Amount.ToString(Symbol + "0.00");
        }

        public override int GetHashCode()
        {
            return Amount.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var otherMoney = obj as Money;
            return otherMoney != null && Amount.Equals(otherMoney.Amount);
        }

        public bool Equals(Money otherMoney)
        {
            return otherMoney != null && Amount.Equals(otherMoney.Amount);
        }

        public static Money operator +(Money a, Money b)
        {
            return new Money(a.Amount + b.Amount);
        }

        public static Money operator *(Money a, int units)
        {
            return new Money(a.Amount*units);
        }

        public static bool operator ==(Money a, Money b)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object) a == null) || ((object) b == null))
            {
                return false;
            }

            return a.Amount == b.Amount;
        }

        public static bool operator !=(Money a, Money b)
        {
            return !(a == b);
        }

        public static implicit operator Money(decimal m)
        {
            return new Money(m);
        }
    }
}
