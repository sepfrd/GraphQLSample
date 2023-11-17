// using Domain.Common;
// using Domain.Entities;
//
// namespace Domain.Abstractions;
//
// public interface IUnitOfWork
// {
//     IRepository<Address> AddressRepository { get;  }
//
//     IRepository<Answer> AnswerRepository { get;  }
//
//     IRepository<Cart> CartRepository { get;  }
//
//     IRepository<CartItem> CartItemRepository { get;  }
//
//     IRepository<Category> CategoryRepository { get;  }
//
//     IRepository<Comment> CommentRepository { get;  }
//
//     IRepository<Order> OrderRepository { get;  }
//
//     IRepository<OrderItem> OrderItemRepository { get;  }
//
//     IRepository<Payment> PaymentRepository { get;  }
//
//     IRepository<Person> PersonRepository { get;  }
//
//     IRepository<PhoneNumber> PhoneNumberRepository { get;  }
//
//     IRepository<Product> ProductRepository { get;  }
//
//     IRepository<Question> QuestionRepository { get;  }
//
//     IRepository<Shipment> ShipmentRepository { get;  }
//
//     IRepository<User> UserRepository { get;  }
//
//     IRepository<Vote> VoteRepository { get;  }
//
//     IEnumerable<IRepository<BaseEntity>> Repositories { get;  }
//     
//     int SaveChanges();
//     
//     Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
// }

