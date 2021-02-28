using XCoffee.Core.Domain;

namespace XCoffee.Ordering.Domain.AggregatesModel.OrderAggregate
{
    public class Coin : Enumeration<int>
    {
        #region Static Fields

        public static Coin OneCent = new Coin(1, "1", 0.01M, "centavos");
        public static Coin FiveCents = new Coin(5, "5", 0.05M, "centavos");
        public static Coin TenCents = new Coin(10, "10", 0.10M, "centavos", true);
        public static Coin TwentyFiveCents = new Coin(25, "25", 0.25M, "centavos", true);
        public static Coin FiftyCents = new Coin(50, "50", 0.50M, "centavos", true);
        public static Coin OneReal = new Coin(100, "1", 1M, "real", true);

        #endregion

        #region Properties

        public decimal Value { get; private set; }
        public string Currency { get; private set; }
        public bool Accept { get; private set; }

        #endregion

        #region Constructors

        public Coin(int id, string name, decimal value, string currency, bool accept = false) : base(name)
        {
            Id = id;
            Value = value;
            Currency = currency;
            Accept = accept;
        }

        #endregion
    }
}
