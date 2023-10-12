using Domain.Entities;

namespace Domain.Abstractions;

public interface IDbContext
{
    ICollection<Address> Addresses { get; set; }

    ICollection<Answer> Answers { get; set; }

    ICollection<Cart> Carts { get; set; }

    ICollection<CartItem> CartItems { get; set; }

    ICollection<Category> Categories { get; set; }

    ICollection<Comment> Comments { get; set; }

    ICollection<Order> Orders { get; set; }

    ICollection<OrderItem> OrderItems { get; set; }

    ICollection<Payment> Payments { get; set; }

    ICollection<Person> Persons { get; set; }

    ICollection<PhoneNumber> PhoneNumbers { get; set; }

    ICollection<Product> Products { get; set; }

    ICollection<Question> Questions { get; set; }

    ICollection<Shipment> Shipments { get; set; }

    ICollection<User> Users { get; set; }

    ICollection<Vote> Votes { get; set; }
}