using System;

using XCoffee.Core.Domain;

namespace XCoffee.Ordering.Domain.AggregatesModel.ProductAggregate
{
    public class Product : Enumeration<Guid>, IAggregateRoot
    {
        #region Static Fields

        public static Product Mocha = new Product(Guid.Parse("22865E17-ADB2-4F15-9E35-AFB2D3BAC296"), "Mocha", 4.0M, "Café, chocolate, leite vaporizado e finalizado com chantilly.", "coffee_2.png");
        public static Product Cappuccino = new Product(Guid.Parse("25855FF4-B3DD-46BE-AEF0-689B44319FF2"), "Cappuccino", 3.5M, "Bebida italiana preparada com café expresso e leite.", "coffee_1.png");
        public static Product CoffeWithMilk = new Product(Guid.Parse("949905D3-99CA-4D3A-A3F7-4C88D3132ABC"), "Café com Leite", 3.0M, "Bebida matinal mais consumida no mundo.", "coffee_3.png");

        #endregion

        #region Properties

        public decimal Price { get; private set; }
        public string Thumbnail { get; private set; }
        public string Description { get; private set; }

        #endregion

        #region Constructors

        public Product(Guid id, string name, decimal price, string description, string thumbnail) : base(name)
        {
            Guard.AssertStateFalse(id == Guid.Empty, "The id is invalid.");
            Guard.AssertArgumentNotEmpty(thumbnail, "The thumbnail is required.");
            Guard.AssertArgumentNotEmpty(description, "The description is required.");
            Guard.AssertArgumentTrue(price > 0M, "The price must be greater than zero.");

            Id = id;
            Price = price;
            Thumbnail = thumbnail;
            Description = description;
        }

        #endregion
    }
}
