using Bogus;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using MongoDB.Driver;
using Person = Domain.Entities.Person;

namespace Infrastructure.Persistence.Common.Helpers;

public class DatabaseSeeder
{
    private readonly IMongoDatabase _mongoDatabase;
    private const int DefaultHugeNumber = 10000;
    private const int DefaultNormalNumber = 5000;
    private const int DefaultSmallNumber = 1000;

    public DatabaseSeeder(string connectionString, string databaseName)
    {
        var mongoClient = new MongoClient(connectionString);

        _mongoDatabase = mongoClient.GetDatabase(databaseName);
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
        var fakeVotes = GetFakeVotes();

        var fakeContents = new List<BaseEntity>();

        fakeContents.AddRange(fakeAnswers);
        fakeContents.AddRange(fakeComments);
        fakeContents.AddRange(fakeProducts);
        fakeContents.AddRange(fakeQuestions);

        foreach (var address in fakeAddresses)
        {
            address.UserId = fakeUsers.ElementAt(Random.Shared.Next(0, fakeUsers.Count)).InternalId;
        }

        foreach (var answer in fakeAnswers)
        {
            answer.QuestionId = fakeQuestions.ElementAt(Random.Shared.Next(0, fakeQuestions.Count)).InternalId;
            answer.UserId = fakeUsers.ElementAt(Random.Shared.Next(0, fakeUsers.Count)).InternalId;
        }

        foreach (var cart in fakeCarts)
        {
            cart.UserId = fakeUsers.ElementAt(Random.Shared.Next(0, fakeUsers.Count)).InternalId;
        }

        foreach (var cartItem in fakeCartItems)
        {
            cartItem.CartId = fakeCarts.ElementAt(Random.Shared.Next(0, fakeCarts.Count)).InternalId;
            cartItem.ProductId = fakeProducts.ElementAt(Random.Shared.Next(0, fakeProducts.Count)).InternalId;
        }

        foreach (var comment in fakeComments)
        {
            comment.UserId = fakeUsers.ElementAt(Random.Shared.Next(0, fakeUsers.Count)).InternalId;
            comment.ProductId = fakeProducts.ElementAt(Random.Shared.Next(0, fakeProducts.Count)).InternalId;
        }

        foreach (var order in fakeOrders)
        {
            order.UserId = fakeUsers.ElementAt(Random.Shared.Next(0, fakeUsers.Count)).InternalId;
            order.PaymentId = fakePayments.ElementAt(Random.Shared.Next(0, fakePayments.Count)).InternalId;
            order.ShipmentId = fakeShipments.ElementAt(Random.Shared.Next(0, fakeShipments.Count)).InternalId;
        }

        foreach (var orderItem in fakeOrderItems)
        {
            orderItem.OrderId = fakeOrders.ElementAt(Random.Shared.Next(0, fakeOrders.Count)).InternalId;
            orderItem.ProductId = fakeProducts.ElementAt(Random.Shared.Next(0, fakeProducts.Count)).InternalId;
        }

        foreach (var payment in fakePayments)
        {
            payment.OrderId = fakeOrders.ElementAt(Random.Shared.Next(0, fakeOrders.Count)).InternalId;
        }

        foreach (var person in fakePersons)
        {
            person.UserId = fakeUsers.ElementAt(Random.Shared.Next(0, fakeUsers.Count)).InternalId;
        }

        foreach (var product in fakeProducts)
        {
            product.CategoryId = fakeCategories.ElementAt(Random.Shared.Next(0, fakeCategories.Count)).InternalId;
        }

        foreach (var phoneNumber in fakePhoneNumbers)
        {
            phoneNumber.UserId = fakeUsers.ElementAt(Random.Shared.Next(0, fakeUsers.Count)).InternalId;
        }

        foreach (var question in fakeQuestions)
        {
            question.UserId = fakeUsers.ElementAt(Random.Shared.Next(0, fakeUsers.Count)).InternalId;
        }

        foreach (var shipment in fakeShipments)
        {
            shipment.OrderId = fakeOrders.ElementAt(Random.Shared.Next(0, fakeOrders.Count)).InternalId;
            shipment.DestinationAddressId = fakeAddresses.ElementAt(Random.Shared.Next(0, fakeAddresses.Count)).InternalId;
            shipment.OriginAddressId = fakeAddresses.ElementAt(Random.Shared.Next(0, fakeAddresses.Count)).InternalId;
        }

        foreach (var user in fakeUsers)
        {
            user.CartId = fakeCarts.ElementAt(Random.Shared.Next(0, fakeCarts.Count)).InternalId;
            user.PersonId = fakePersons.ElementAt(Random.Shared.Next(0, fakePersons.Count)).InternalId;
        }

        foreach (var userRole in fakeUserRoles)
        {
            userRole.UserId = fakeUsers.ElementAt(Random.Shared.Next(0, fakeUsers.Count)).InternalId;
            userRole.RoleId = fakeRoles.ElementAt(Random.Shared.Next(0, fakeRoles.Count)).InternalId;
        }

        foreach (var vote in fakeVotes)
        {
            vote.UserId = fakeUsers.ElementAt(Random.Shared.Next(0, fakeUsers.Count)).InternalId;
            vote.ContentId = fakeContents.ElementAt(Random.Shared.Next(0, fakeContents.Count)).InternalId;
        }

        // ------------------------------------------------

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
        var externalId = 0;

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

        for (var i = 0; i < DefaultHugeNumber; i++)
        {
            fakeAddresses.Add(addressFaker.Generate());
        }

        return fakeAddresses;
    }

    private static List<Answer> GetFakeAnswers()
    {
        var externalId = 0;

        var answerFaker = new Faker<Answer>()
            .RuleFor(answer => answer.ExternalId, _ => externalId++)
            .RuleFor(answer => answer.Title, faker => faker.Lorem.Sentence(5))
            .RuleFor(answer => answer.Description, faker => faker.Lorem.Paragraph());

        var fakeAnswers = new List<Answer>();

        for (var i = 0; i < DefaultSmallNumber; i++)
        {
            fakeAnswers.Add(answerFaker.Generate());
        }

        return fakeAnswers;
    }

    private static List<Cart> GetFakeCarts()
    {
        var externalId = 0;

        var cartFaker = new Faker<Cart>()
            .RuleFor(cart => cart.ExternalId, _ => externalId++);

        var fakeCarts = new List<Cart>();

        for (var i = 0; i < DefaultHugeNumber; i++)
        {
            fakeCarts.Add(cartFaker.Generate());
        }

        return fakeCarts;
    }

    private static List<CartItem> GetFakeCartItems()
    {
        var externalId = 0;

        var cartItemFaker = new Faker<CartItem>()
            .RuleFor(cartItem => cartItem.ExternalId, _ => externalId++)
            .RuleFor(cartItem => cartItem.Quantity, faker => faker.Random.Number(10))
            .RuleFor(cartItem => cartItem.UnitPrice, faker => faker.Random.Decimal());

        var fakeCartItems = new List<CartItem>();

        for (var i = 0; i < DefaultHugeNumber; i++)
        {
            fakeCartItems.Add(cartItemFaker.Generate());
        }

        return fakeCartItems;
    }

    private static List<Category> GetFakeCategories()
    {
        var externalId = 0;

        var categoryFaker = new Faker<Category>()
            .RuleFor(category => category.ExternalId, _ => externalId++)
            .RuleFor(category => category.Description, faker => faker.Lorem.Sentences(5))
            .RuleFor(category => category.Name, faker => faker.Lorem.Word())
            .RuleFor(category => category.IconUrl, faker => faker.Image.PicsumUrl())
            .RuleFor(category => category.ImageUrl, faker => faker.Image.PicsumUrl());

        var fakeCategories = new List<Category>();

        for (var i = 0; i < 20; i++)
        {
            fakeCategories.Add(categoryFaker.Generate());
        }

        return fakeCategories;
    }

    private static List<Comment> GetFakeComments()
    {
        var externalId = 0;

        var commentFaker = new Faker<Comment>()
            .RuleFor(comment => comment.ExternalId, _ => externalId++)
            .RuleFor(comment => comment.Description, faker => faker.Lorem.Sentences(5));

        var fakeComments = new List<Comment>();

        for (var i = 0; i < DefaultSmallNumber; i++)
        {
            fakeComments.Add(commentFaker.Generate());
        }

        return fakeComments;
    }

    private static List<Order> GetFakeOrders()
    {
        var externalId = 0;

        var orderFaker = new Faker<Order>()
            .RuleFor(order => order.ExternalId, _ => externalId++)
            .RuleFor(order => order.TotalPrice, faker => faker.Random.Decimal());

        var fakeOrders = new List<Order>();

        for (var i = 0; i < DefaultNormalNumber; i++)
        {
            fakeOrders.Add(orderFaker.Generate());
        }

        return fakeOrders;
    }

    private static List<OrderItem> GetFakeOrderItems()
    {
        var externalId = 0;

        var orderItemFaker = new Faker<OrderItem>()
            .RuleFor(orderItem => orderItem.ExternalId, _ => externalId++)
            .RuleFor(orderItem => orderItem.Quantity, faker => faker.Random.Number(20))
            .RuleFor(orderItem => orderItem.UnitPrice, faker => faker.Random.Decimal());

        var fakeOrderItems = new List<OrderItem>();

        for (var i = 0; i < DefaultHugeNumber; i++)
        {
            fakeOrderItems.Add(orderItemFaker.Generate());
        }

        return fakeOrderItems;
    }

    private static List<Payment> GetFakePayments()
    {
        var externalId = 0;

        var paymentFaker = new Faker<Payment>()
            .RuleFor(payment => payment.ExternalId, _ => externalId++)
            .RuleFor(payment => payment.Amount, faker => faker.Random.Decimal())
            .RuleFor(payment => payment.PaymentMethod, faker => Enum.GetValues<PaymentMethod>()[faker.Random.Number(9)])
            .RuleFor(payment => payment.PaymentStatus, faker => Enum.GetValues<PaymentStatus>()[faker.Random.Number(9)]);

        var fakePayments = new List<Payment>();

        for (var i = 0; i < DefaultNormalNumber; i++)
        {
            fakePayments.Add(paymentFaker.Generate());
        }

        return fakePayments;
    }

    private static List<PhoneNumber> GetFakePhoneNumbers()
    {
        var externalId = 0;

        var phoneNumberFaker = new Faker<PhoneNumber>()
            .RuleFor(phoneNumber => phoneNumber.ExternalId, _ => externalId++)
            .RuleFor(phoneNumber => phoneNumber.Number, faker => faker.Phone.PhoneNumber())
            .RuleFor(phoneNumber => phoneNumber.Type, faker => Enum.GetValues<PhoneNumberType>()[faker.Random.Number(1)])
            .RuleFor(phoneNumber => phoneNumber.IsConfirmed, faker => faker.Random.Bool());

        var fakePhoneNumbers = new List<PhoneNumber>();

        for (var i = 0; i < DefaultHugeNumber; i++)
        {
            fakePhoneNumbers.Add(phoneNumberFaker.Generate());
        }

        return fakePhoneNumbers;
    }

    private static List<Product> GetFakeProducts()
    {
        var externalId = 0;

        var productFaker = new Faker<Product>()
            .RuleFor(product => product.ExternalId, _ => externalId++)
            .RuleFor(product => product.StockQuantity, faker => faker.Random.Number(10_000))
            .RuleFor(product => product.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(product => product.Name, faker => faker.Commerce.ProductName())
            .RuleFor(product => product.Price, faker => faker.Random.Decimal())
            .RuleFor(product => product.ImageUrls, faker => new List<string>
            {
                faker.Image.PicsumUrl(),
                faker.Image.PicsumUrl(),
                faker.Image.PicsumUrl(),
                faker.Image.PicsumUrl(),
                faker.Image.PicsumUrl()
            });

        var fakeProducts = new List<Product>();

        for (var i = 0; i < DefaultHugeNumber; i++)
        {
            fakeProducts.Add(productFaker.Generate());
        }

        return fakeProducts;
    }

    private static List<Question> GetFakeQuestions()
    {
        var externalId = 0;

        var questionFaker = new Faker<Question>()
            .RuleFor(question => question.ExternalId, _ => externalId++)
            .RuleFor(question => question.Title, faker => faker.Lorem.Sentence(5))
            .RuleFor(question => question.Description, faker => faker.Lorem.Paragraph());

        var fakeQuestions = new List<Question>();

        for (var i = 0; i < DefaultNormalNumber; i++)
        {
            fakeQuestions.Add(questionFaker.Generate());
        }

        return fakeQuestions;
    }

    private static List<Role> GetFakeRoles()
    {
        var externalId = 0;

        var roleFaker = new Faker<Role>()
            .RuleFor(role => role.ExternalId, _ => externalId++)
            .RuleFor(role => role.Description, faker => faker.Name.JobDescriptor())
            .RuleFor(role => role.Title, faker => faker.Name.JobTitle());

        var fakeRoles = new List<Role>();

        for (var i = 0; i < 5; i++)
        {
            fakeRoles.Add(roleFaker.Generate());
        }

        return fakeRoles;
    }

    private static List<Shipment> GetFakeShipments()
    {
        var externalId = 0;

        var shipmentFaker = new Faker<Shipment>()
            .RuleFor(shipment => shipment.ExternalId, _ => externalId++)
            .RuleFor(shipment => shipment.DateToBeDelivered, faker => faker.Date.Soon())
            .RuleFor(shipment => shipment.DateToBeShipped, faker => faker.Date.Recent())
            .RuleFor(shipment => shipment.ShipmentStatus, faker => Enum.GetValues<ShipmentStatus>()[faker.Random.Number(9)])
            .RuleFor(shipment => shipment.ShippingMethod, faker => Enum.GetValues<ShippingMethod>()[faker.Random.Number(9)])
            .RuleFor(shipment => shipment.ShippingCost, faker => faker.Random.Decimal())
            .RuleFor(shipment => shipment.TraceId, _ => Guid.NewGuid());

        var fakeShipments = new List<Shipment>();

        for (var i = 0; i < DefaultNormalNumber; i++)
        {
            fakeShipments.Add(shipmentFaker.Generate());
        }

        return fakeShipments;
    }

    private static List<Vote> GetFakeVotes()
    {
        var externalId = 0;

        var voteFaker = new Faker<Vote>()
            .RuleFor(vote => vote.ExternalId, _ => externalId++)
            .RuleFor(vote => vote.Type, faker => Enum.GetValues<VoteType>()[faker.Random.Number(3)]);

        var fakeVotes = new List<Vote>();

        for (var i = 0; i < DefaultNormalNumber; i++)
        {
            fakeVotes.Add(voteFaker.Generate());
        }

        return fakeVotes;
    }

    private static List<Person> GetFakePersons()
    {
        var externalId = 0;

        var personFaker = new Faker<Person>()
            .RuleFor(person => person.ExternalId, _ => externalId++)
            .RuleFor(person => person.BirthDate, faker => faker.Date.Between(DateTime.Now.AddYears(-100), DateTime.Now))
            .RuleFor(person => person.FirstName, faker => faker.Name.FirstName())
            .RuleFor(person => person.LastName, faker => faker.Name.LastName());

        var fakePeople = new List<Person>();

        for (var i = 0; i < DefaultHugeNumber; i++)
        {
            fakePeople.Add(personFaker.Generate());
        }

        return fakePeople;
    }

    private static List<User> GetFakeUsers()
    {
        var externalId = 0;

        var userFaker = new Faker<User>()
            .RuleFor(user => user.ExternalId, _ => externalId++)
            .RuleFor(user => user.Username, faker => faker.Internet.UserName())
            .RuleFor(user => user.Password, faker => faker.Internet.Password())
            .RuleFor(user => user.Score, faker => faker.Random.Number(50))
            .RuleFor(user => user.Email, faker => faker.Internet.Email())
            .RuleFor(user => user.IsEmailConfirmed, faker => faker.Random.Bool());

        var fakeUsers = new List<User>();

        for (var i = 0; i < DefaultHugeNumber; i++)
        {
            fakeUsers.Add(userFaker.Generate());
        }

        return fakeUsers;
    }

    private static List<UserRole> GetFakeUserRoles()
    {
        var externalId = 0;

        var userRoleFaker = new Faker<UserRole>()
            .RuleFor(user => user.ExternalId, _ => externalId++);

        var fakeUserRoles = new List<UserRole>();

        for (var i = 0; i < DefaultHugeNumber; i++)
        {
            fakeUserRoles.Add(userRoleFaker.Generate());
        }

        return fakeUserRoles;
    }
}