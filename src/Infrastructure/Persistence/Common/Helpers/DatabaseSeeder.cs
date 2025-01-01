using System.Globalization;
using Application.Abstractions;
using Application.Common.Constants;
using Bogus;
using Domain.Entities;
using Domain.Enums;
using MongoDB.Driver;
using Person = Domain.Entities.Person;

namespace Infrastructure.Persistence.Common.Helpers;

public class DatabaseSeeder
{
    private const int DefaultNumber = 200;
    private const int DefaultLargeNumber = 100_000;
    private readonly IAuthenticationService _authenticationService;
    private readonly IMongoDatabase _mongoDatabase;
    private static readonly Guid AdminRoleInternalId = Guid.NewGuid();
    private static readonly Guid CustomerRoleInternalId = Guid.NewGuid();
    private static readonly Guid AdminUserInternalId = Guid.NewGuid();
    private static readonly Guid CustomerUserInternalId = Guid.NewGuid();

    public DatabaseSeeder(string connectionString, string databaseName, IAuthenticationService authenticationService)
    {
        var mongoClient = new MongoClient(connectionString);

        _mongoDatabase = mongoClient.GetDatabase(databaseName);

        _authenticationService = authenticationService;
    }

    public void SeedData()
    {
        var fakeAddresses = GetFakeAddresses();
        var fakeAnswers = GetFakeAnswers();
        var fakeCarts = GetFakeCarts();
        var fakeCartItems = GetFakeCartItems();
        var fakeCategories = GetFakeCategories();
        var fakeComments = GetFakeComments();
        var fakeOrders = GetFakeOrders();
        var fakeOrderItems = GetFakeOrderItems();
        var fakePayments = GetFakePayments();
        var fakePersons = GetFakePersons();
        var fakePhoneNumbers = GetFakePhoneNumbers();
        var fakeProducts = GetFakeProducts();
        var fakeQuestions = GetFakeQuestions();
        var fakeRoles = GetFakeRoles();
        var fakeShipments = GetFakeShipments();
        var fakeUsers = GetFakeUsers();
        var fakeUserRoles = GetFakeUserRoles();
        var fakeAnswerVotes = GetFakeAnswerVotes();
        var fakeCommentVotes = GetFakeCommentVotes();
        var fakeProductVotes = GetFakeProductVotes();
        var fakeQuestionVotes = GetFakeQuestionVotes();

        for (var i = 0; i < DefaultNumber; i++)
        {
            fakeAddresses[i].UserId = fakeUsers[i].InternalId;
            fakePhoneNumbers[i].UserId = fakeUsers[i].InternalId;
            fakeOrders[i].UserId = fakeUsers[i].InternalId;
            fakeQuestions[i].UserId = fakeUsers[i].InternalId;
            fakeAnswers[i].UserId = fakeUsers[i].InternalId;
            fakeComments[i].UserId = fakeUsers[i].InternalId;
            fakeAnswerVotes[i].UserId = fakeUsers[i].InternalId;
            fakeCommentVotes[i].UserId = fakeUsers[i].InternalId;
            fakeProductVotes[i].UserId = fakeUsers[i].InternalId;
            fakeQuestionVotes[i].UserId = fakeUsers[i].InternalId;

            fakePersons[i].UserId = fakeUsers[i].InternalId;
            fakeUsers[i].PersonId = fakePersons[i].InternalId;

            fakeCarts[i].UserId = fakeUsers[i].InternalId;
            fakeUsers[i].CartId = fakeCarts[i].InternalId;

            fakeCartItems[i].CartId = fakeCarts[i].InternalId;

            fakeShipments[i].DestinationAddressId = fakeAddresses[i].InternalId;
            fakeShipments[i].OriginAddressId = fakeAddresses[DefaultNumber - i - 1].InternalId;

            fakeUserRoles[i].UserId = fakeUsers[i].InternalId;
            fakeUserRoles[i].RoleId = fakeRoles.ElementAt(Random.Shared.Next(0, fakeRoles.Count)).InternalId;
        }

        for (var index = 0; index < DefaultLargeNumber; index++)
        {
            if (index < DefaultNumber)
            {
                fakeCartItems[index].ProductId = fakeProducts[index].InternalId;
            }

            fakeAnswers[index].QuestionId = fakeQuestions[index].InternalId;
            fakeQuestions[index].ProductId = fakeProducts[index].InternalId;
            fakeComments[index].ProductId = fakeProducts[index].InternalId;

            fakeOrders[index].PaymentId = fakePayments[index].InternalId;
            fakePayments[index].OrderId = fakeOrders[index].InternalId;
            fakeOrders[index].ShipmentId = fakeShipments[index].InternalId;
            fakeShipments[index].OrderId = fakeOrders[index].InternalId;

            fakeOrderItems[index].OrderId = fakeOrders[index].InternalId;
            fakeOrderItems[index].ProductId = fakeProducts[index].InternalId;

            fakeProducts[index].CategoryId =
                fakeCategories.ElementAt(Random.Shared.Next(0, fakeCategories.Length)).InternalId;

            fakeAnswerVotes[index].ContentId = fakeAnswers[index].InternalId;
            fakeCommentVotes[index].ContentId = fakeComments[index].InternalId;
            fakeProductVotes[index].ContentId = fakeProducts[index].InternalId;
            fakeQuestionVotes[index].ContentId = fakeQuestions[index].InternalId;
        }

        fakeUserRoles[0] = new UserRole
        {
            ExternalId = 1,
            UserId = AdminUserInternalId,
            RoleId = AdminRoleInternalId
        };

        fakeUserRoles[1] = new UserRole
        {
            ExternalId = 2,
            UserId = CustomerUserInternalId,
            RoleId = CustomerRoleInternalId
        };

        var fakeVotes = new List<Vote>();

        fakeVotes.AddRange(fakeAnswerVotes);
        fakeVotes.AddRange(fakeCommentVotes);
        fakeVotes.AddRange(fakeProductVotes);
        fakeVotes.AddRange(fakeQuestionVotes);

        _mongoDatabase.GetCollection<Address>("Addresses").InsertMany(fakeAddresses);
        _mongoDatabase.GetCollection<Answer>("Answers").InsertMany(fakeAnswers);
        _mongoDatabase.GetCollection<Cart>("Carts").InsertMany(fakeCarts);
        _mongoDatabase.GetCollection<CartItem>("CartItems").InsertMany(fakeCartItems);
        _mongoDatabase.GetCollection<Category>("Categories").InsertMany(fakeCategories);
        _mongoDatabase.GetCollection<Comment>("Comments").InsertMany(fakeComments);
        _mongoDatabase.GetCollection<Order>("Orders").InsertMany(fakeOrders);
        _mongoDatabase.GetCollection<OrderItem>("OrderItems").InsertMany(fakeOrderItems);
        _mongoDatabase.GetCollection<Payment>("Payments").InsertMany(fakePayments);
        _mongoDatabase.GetCollection<Person>("Persons").InsertMany(fakePersons);
        _mongoDatabase.GetCollection<PhoneNumber>("PhoneNumbers").InsertMany(fakePhoneNumbers);
        _mongoDatabase.GetCollection<Product>("Products").InsertMany(fakeProducts);
        _mongoDatabase.GetCollection<Question>("Questions").InsertMany(fakeQuestions);
        _mongoDatabase.GetCollection<Role>("Roles").InsertMany(fakeRoles);
        _mongoDatabase.GetCollection<Shipment>("Shipments").InsertMany(fakeShipments);
        _mongoDatabase.GetCollection<User>("Users").InsertMany(fakeUsers);
        _mongoDatabase.GetCollection<UserRole>("UserRoles").InsertMany(fakeUserRoles);
        _mongoDatabase.GetCollection<Vote>("Votes").InsertMany(fakeVotes);
    }

    private static List<Address> GetFakeAddresses()
    {
        var externalId = 1;

        var addressFaker = new Faker<Address>()
            .RuleFor(address => address.ExternalId, _ => externalId++)
            .RuleFor(address => address.City, faker => faker.Address.City())
            .RuleFor(address => address.Country, faker => faker.Address.Country())
            .RuleFor(address => address.State, faker => faker.Address.State())
            .RuleFor(address => address.Street, faker => faker.Address.StreetName())
            .RuleFor(address => address.BuildingNumber, faker => faker.Address.BuildingNumber())
            .RuleFor(address => address.PostalCode, faker => faker.Address.ZipCode())
            .RuleFor(address => address.UnitNumber, faker => faker.Address.BuildingNumber());

        var fakeAddresses = new List<Address>();

        for (var i = 0; i < DefaultNumber; i++)
        {
            fakeAddresses.Add(addressFaker.Generate());
        }

        return fakeAddresses;
    }

    private static List<Answer> GetFakeAnswers()
    {
        var externalId = 1;

        var answerFaker = new Faker<Answer>()
            .RuleFor(answer => answer.ExternalId, _ => externalId++)
            .RuleFor(answer => answer.Title, faker => faker.Lorem.Sentence(5))
            .RuleFor(answer => answer.Description, faker => faker.Lorem.Paragraph());

        var fakeAnswers = new List<Answer>();

        for (var i = 0; i < DefaultLargeNumber; i++)
        {
            fakeAnswers.Add(answerFaker.Generate());
        }

        return fakeAnswers;
    }

    private static List<Cart> GetFakeCarts()
    {
        var externalId = 1;

        var cartFaker = new Faker<Cart>()
            .RuleFor(cart => cart.ExternalId, _ => externalId++);

        var fakeCarts = new List<Cart>();

        for (var i = 0; i < DefaultNumber; i++)
        {
            fakeCarts.Add(cartFaker.Generate());
        }

        return fakeCarts;
    }

    private static List<CartItem> GetFakeCartItems()
    {
        var externalId = 1;

        var cartItemFaker = new Faker<CartItem>()
            .RuleFor(cartItem => cartItem.ExternalId, _ => externalId++)
            .RuleFor(cartItem => cartItem.Quantity, faker => faker.Random.Number(10))
            .RuleFor(cartItem => cartItem.UnitPrice, faker => faker.Random.Decimal());

        var fakeCartItems = new List<CartItem>();

        for (var i = 0; i < DefaultNumber; i++)
        {
            fakeCartItems.Add(cartItemFaker.Generate());
        }

        return fakeCartItems;
    }

    private static Category[] GetFakeCategories()
    {
        var externalId = 1;

        Category[] fakeCategories =
        [
            new Category
            {
                ExternalId = externalId++,
                Name = "Electronics",
                Description = "Cutting-edge gadgets and devices for tech enthusiasts",
                ImageUrl = "categories/electronics.png",
                IconUrl = "categories/electronics.png"
            },
            new Category
            {
                ExternalId = externalId++,
                Name = "Clothing",
                Description = "Fashionable apparel and accessories for all occasions",
                ImageUrl = "categories/clothing.png",
                IconUrl = "categories/clothing.png"
            },
            new Category
            {
                ExternalId = externalId++,
                Name = "Books",
                Description = "Engaging reads for bookworms of all ages",
                ImageUrl = "categories/books.png",
                IconUrl = "categories/books.png"
            },
            new Category
            {
                ExternalId = externalId++,
                Name = "Home & Garden",
                Description = "Decor and essentials to spruce up your living space",
                ImageUrl = "categories/garden.png",
                IconUrl = "categories/garden.png"
            },
            new Category
            {
                ExternalId = externalId++,
                Name = "Toys & Games",
                Description = "Fun and entertaining toys and games for kids and adults alike",
                ImageUrl = "categories/games.png",
                IconUrl = "categories/games.png"
            },
            new Category
            {
                ExternalId = externalId++,
                Name = "Health & Beauty",
                Description = "Products to enhance your well-being and beauty regimen",
                ImageUrl = "categories/health.png",
                IconUrl = "categories/health.png"
            },
            new Category
            {
                ExternalId = externalId++,
                Name = "Sports & Outdoors",
                Description = "Gear and equipment for outdoor adventures and fitness pursuits",
                ImageUrl = "categories/sports.png",
                IconUrl = "categories/sports.png"
            },
            new Category
            {
                ExternalId = externalId++,
                Name = "Pet Supplies",
                Description = "Essentials and treats for your furry friends",
                ImageUrl = "categories/pet.png",
                IconUrl = "categories/pet.png"
            },
            new Category
            {
                ExternalId = externalId++,
                Name = "Jewelry & Accessories",
                Description = "Elegant adornments and stylish accessories to complement any outfit",
                ImageUrl = "categories/jewelry.png",
                IconUrl = "categories/jewelry.png"
            },
            new Category
            {
                ExternalId = externalId,
                Name = "Food & Beverages",
                Description = "Delicious treats and beverages for every palate",
                ImageUrl = "categories/food.png",
                IconUrl = "categories/food.png"
            }
        ];

        return fakeCategories;
    }

    private static List<Comment> GetFakeComments()
    {
        var externalId = 1;

        var commentFaker = new Faker<Comment>()
            .RuleFor(comment => comment.ExternalId, _ => externalId++)
            .RuleFor(comment => comment.Description, faker => faker.Lorem.Sentences(5));

        var fakeComments = new List<Comment>();

        for (var i = 0; i < DefaultLargeNumber; i++)
        {
            fakeComments.Add(commentFaker.Generate());
        }

        return fakeComments;
    }

    private static List<Order> GetFakeOrders()
    {
        var externalId = 1;

        var orderFaker = new Faker<Order>()
            .RuleFor(order => order.ExternalId, _ => externalId++)
            .RuleFor(order => order.TotalPrice, faker => faker.Random.Decimal());

        var fakeOrders = new List<Order>();

        for (var i = 0; i < DefaultLargeNumber; i++)
        {
            fakeOrders.Add(orderFaker.Generate());
        }

        return fakeOrders;
    }

    private static List<OrderItem> GetFakeOrderItems()
    {
        var externalId = 1;

        var orderItemFaker = new Faker<OrderItem>()
            .RuleFor(orderItem => orderItem.ExternalId, _ => externalId++)
            .RuleFor(orderItem => orderItem.Quantity, faker => faker.Random.Number(20))
            .RuleFor(orderItem => orderItem.UnitPrice, faker => faker.Random.Decimal());

        var fakeOrderItems = new List<OrderItem>();

        for (var i = 0; i < DefaultLargeNumber; i++)
        {
            fakeOrderItems.Add(orderItemFaker.Generate());
        }

        return fakeOrderItems;
    }

    private static List<Payment> GetFakePayments()
    {
        var externalId = 1;

        var paymentFaker = new Faker<Payment>()
            .RuleFor(payment => payment.ExternalId, _ => externalId++)
            .RuleFor(payment => payment.Amount, faker => faker.Random.Decimal())
            .RuleFor(payment => payment.PaymentMethod, faker => Enum.GetValues<PaymentMethod>()[faker.Random.Number(9)])
            .RuleFor(payment => payment.PaymentStatus,
                faker => Enum.GetValues<PaymentStatus>()[faker.Random.Number(9)]);

        var fakePayments = new List<Payment>();

        for (var i = 0; i < DefaultLargeNumber; i++)
        {
            fakePayments.Add(paymentFaker.Generate());
        }

        return fakePayments;
    }

    private static List<PhoneNumber> GetFakePhoneNumbers()
    {
        var externalId = 1;

        var phoneNumberFaker = new Faker<PhoneNumber>()
            .RuleFor(phoneNumber => phoneNumber.ExternalId, _ => externalId++)
            .RuleFor(phoneNumber => phoneNumber.Number, faker => faker.Phone.PhoneNumber())
            .RuleFor(phoneNumber => phoneNumber.Type,
                faker => Enum.GetValues<PhoneNumberType>()[faker.Random.Number(1)])
            .RuleFor(phoneNumber => phoneNumber.IsConfirmed, faker => faker.Random.Bool());

        var fakePhoneNumbers = new List<PhoneNumber>();

        for (var i = 0; i < DefaultLargeNumber; i++)
        {
            fakePhoneNumbers.Add(phoneNumberFaker.Generate());
        }

        return fakePhoneNumbers;
    }

    private static List<Product> GetFakeProducts()
    {
        var externalId = 1;

        var productFaker = new Faker<Product>()
            .RuleFor(product => product.ExternalId, _ => externalId++)
            .RuleFor(product => product.StockQuantity, faker => faker.Random.Number(10_000))
            .RuleFor(product => product.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(product => product.Name, faker => faker.Commerce.ProductName())
            .RuleFor(product => product.Price, faker => decimal.Parse(faker.Commerce.Price(), CultureInfo.InvariantCulture))
            .RuleFor(product => product.ImageUrls, faker => new List<string>
            {
                faker.Image.PicsumUrl(),
                faker.Image.PicsumUrl(),
                faker.Image.PicsumUrl(),
                faker.Image.PicsumUrl(),
                faker.Image.PicsumUrl()
            });

        var fakeProducts = new List<Product>();

        for (var i = 0; i < DefaultLargeNumber; i++)
        {
            fakeProducts.Add(productFaker.Generate());
        }

        return fakeProducts;
    }

    private static List<Question> GetFakeQuestions()
    {
        var externalId = 1;

        var questionFaker = new Faker<Question>()
            .RuleFor(question => question.ExternalId, _ => externalId++)
            .RuleFor(question => question.Title, faker => faker.Lorem.Sentence(5))
            .RuleFor(question => question.Description, faker => faker.Lorem.Paragraph());

        var fakeQuestions = new List<Question>();

        for (var i = 0; i < DefaultLargeNumber; i++)
        {
            fakeQuestions.Add(questionFaker.Generate());
        }

        return fakeQuestions;
    }

    private static List<Role> GetFakeRoles() =>
    [
        new Role
        {
            InternalId = AdminRoleInternalId,
            ExternalId = 1,
            Title = RoleConstants.Admin,
            Description = "The Highest Role in the Application Role Hierarchy"
        },

        new Role
        {
            InternalId = CustomerRoleInternalId,
            ExternalId = 2,
            Title = RoleConstants.Customer,
            Description = "The Basic Role in the Application Role Hierarchy"
        }
    ];

    private static List<Shipment> GetFakeShipments()
    {
        var externalId = 1;

        var shipmentFaker = new Faker<Shipment>()
            .RuleFor(shipment => shipment.ExternalId, _ => externalId++)
            .RuleFor(shipment => shipment.DateToBeDelivered, faker => faker.Date.Soon())
            .RuleFor(shipment => shipment.DateToBeShipped, faker => faker.Date.Recent())
            .RuleFor(shipment => shipment.ShipmentStatus,
                faker => Enum.GetValues<ShipmentStatus>()[faker.Random.Number(9)])
            .RuleFor(shipment => shipment.ShippingMethod,
                faker => Enum.GetValues<ShippingMethod>()[faker.Random.Number(9)])
            .RuleFor(shipment => shipment.ShippingCost, faker => faker.Random.Decimal())
            .RuleFor(shipment => shipment.TraceId, _ => Guid.NewGuid());

        var fakeShipments = new List<Shipment>();

        for (var i = 0; i < DefaultLargeNumber; i++)
        {
            fakeShipments.Add(shipmentFaker.Generate());
        }

        return fakeShipments;
    }

    private static List<Vote> GetFakeAnswerVotes()
    {
        var externalId = 1;

        var voteFaker = new Faker<Vote>()
            .RuleFor(vote => vote.ExternalId, _ => externalId++)
            .RuleFor(vote => vote.Type, faker => Enum.GetValues<VoteType>()[faker.Random.Number(3)])
            .RuleFor(vote => vote.ContentType, _ => VotableContentType.Answer);

        var fakeAnswerVotes = new List<Vote>();

        for (var i = 0; i < DefaultLargeNumber; i++)
        {
            fakeAnswerVotes.Add(voteFaker.Generate());
        }

        return fakeAnswerVotes;
    }

    private static List<Vote> GetFakeCommentVotes()
    {
        var externalId = DefaultLargeNumber;

        var voteFaker = new Faker<Vote>()
            .RuleFor(vote => vote.ExternalId, _ => externalId++)
            .RuleFor(vote => vote.Type, faker => Enum.GetValues<VoteType>()[faker.Random.Number(3)])
            .RuleFor(vote => vote.ContentType, _ => VotableContentType.Comment);

        var fakeCommentVotes = new List<Vote>();

        for (var i = 0; i < DefaultLargeNumber; i++)
        {
            fakeCommentVotes.Add(voteFaker.Generate());
        }

        return fakeCommentVotes;
    }

    private static List<Vote> GetFakeProductVotes()
    {
        var externalId = DefaultLargeNumber * 2;

        var voteFaker = new Faker<Vote>()
            .RuleFor(vote => vote.ExternalId, _ => externalId++)
            .RuleFor(vote => vote.Type, faker => Enum.GetValues<VoteType>()[faker.Random.Number(3)])
            .RuleFor(vote => vote.ContentType, _ => VotableContentType.Product);

        var fakeProductVotes = new List<Vote>();

        for (var i = 0; i < DefaultLargeNumber; i++)
        {
            fakeProductVotes.Add(voteFaker.Generate());
        }

        return fakeProductVotes;
    }

    private static List<Vote> GetFakeQuestionVotes()
    {
        var externalId = DefaultLargeNumber * 3;

        var voteFaker = new Faker<Vote>()
            .RuleFor(vote => vote.ExternalId, _ => externalId++)
            .RuleFor(vote => vote.Type, faker => Enum.GetValues<VoteType>()[faker.Random.Number(3)])
            .RuleFor(vote => vote.ContentType, _ => VotableContentType.Question);

        var fakeQuestionVotes = new List<Vote>();

        for (var i = 0; i < DefaultLargeNumber; i++)
        {
            fakeQuestionVotes.Add(voteFaker.Generate());
        }

        return fakeQuestionVotes;
    }

    private static List<Person> GetFakePersons()
    {
        var externalId = 1;

        var personFaker = new Faker<Person>()
            .RuleFor(person => person.ExternalId, _ => externalId++)
            .RuleFor(person => person.BirthDate, faker => faker.Date.Between(DateTime.Now.AddYears(-100), DateTime.Now))
            .RuleFor(person => person.FirstName, faker => faker.Name.FirstName())
            .RuleFor(person => person.LastName, faker => faker.Name.LastName());

        var fakePeople = new List<Person>();

        for (var i = 0; i < DefaultNumber; i++)
        {
            fakePeople.Add(personFaker.Generate());
        }

        return fakePeople;
    }

    private List<User> GetFakeUsers()
    {
        var adminUser = new User
        {
            InternalId = AdminUserInternalId,
            ExternalId = 1,
            Username = "sepehr_frd",
            Password = _authenticationService.HashPassword("Correct_p0"),
            Score = 0,
            Email = "sepfrd@outlook.com",
            IsEmailConfirmed = true
        };

        var customerUser = new User
        {
            InternalId = CustomerUserInternalId,
            ExternalId = 2,
            Username = "customer",
            Password = _authenticationService.HashPassword("Correct_p0"),
            Score = 0,
            Email = "customer@outlook.com",
            IsEmailConfirmed = true
        };

        var externalId = 3;

        var userFaker = new Faker<User>()
            .RuleFor(user => user.ExternalId, _ => externalId++)
            .RuleFor(user => user.Username, faker => faker.Internet.UserName())
            .RuleFor(user => user.Password,
                faker => _authenticationService.HashPassword(faker.Internet.Password()))
            .RuleFor(user => user.Score, faker => faker.Random.Number(50))
            .RuleFor(user => user.Email, faker => faker.Internet.Email())
            .RuleFor(user => user.IsEmailConfirmed, faker => faker.Random.Bool());

        var fakeUsers = new List<User>
        {
            adminUser,
            customerUser
        };

        for (var i = 0; i < DefaultNumber - 2; i++)
        {
            fakeUsers.Add(userFaker.Generate());
        }

        return fakeUsers;
    }

    private static List<UserRole> GetFakeUserRoles()
    {
        var externalId = 3;

        var userRoleFaker = new Faker<UserRole>()
            .RuleFor(userRole => userRole.ExternalId, _ => externalId++);

        var fakeUserRoles = new List<UserRole>();

        for (var i = 0; i < DefaultNumber; i++)
        {
            fakeUserRoles.Add(userRoleFaker.Generate());
        }

        return fakeUserRoles;
    }
}