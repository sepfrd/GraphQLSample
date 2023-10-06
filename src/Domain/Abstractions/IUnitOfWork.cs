using Domain.Common;
using Domain.Entities;

namespace Domain.Abstractions;

public interface IUnitOfWork
{
    IRepository<Address> AddressRepository { get; set; }

    IRepository<Answer> AnswerRepository { get; set; }

    IRepository<Cart> CartRepository { get; set; }

    IRepository<CartItem> CartItemRepository { get; set; }

    IRepository<Category> CategoryRepository { get; set; }

    IRepository<Comment> CommentRepository { get; set; }

    IRepository<Order> OrderRepository { get; set; }

    IRepository<OrderItem> OrderItemRepository { get; set; }

    IRepository<Payment> PaymentRepository { get; set; }

    IRepository<Person> PersonRepository { get; set; }

    IRepository<PhoneNumber> PhoneNumberRepository { get; set; }

    IRepository<Product> ProductRepository { get; set; }

    IRepository<Question> QuestionRepository { get; set; }

    IRepository<Shipment> ShipmentRepository { get; set; }

    IRepository<User> UserRepository { get; set; }

    IRepository<Vote> VoteRepository { get; set; }

    ICollection<IRepository<BaseEntity>> Repositories { get; set; }
    
    int SaveChanges();
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}