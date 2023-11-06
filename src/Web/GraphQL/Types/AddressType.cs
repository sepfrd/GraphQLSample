using Domain.Entities;
using HotChocolate;
using HotChocolate.Types;
using MongoDB.Driver;

namespace Web.GraphQL.Types;

public class AddressType : ObjectType<Address>
{
    protected override void Configure(IObjectTypeDescriptor<Address> descriptor)
    {
        descriptor.Description("Represents an address in its entirety.");

        descriptor
            .Field(address => address.InternalId)
            .Ignore();

        descriptor
            .Field(address => address.UserId)
            .Ignore();

        descriptor
            .Field(address => address.City)
            .Description("The City Name");

        descriptor
            .Field(address => address.Country)
            .Description("The Country Name");

        descriptor
            .Field(address => address.State)
            .Description("The State Name");

        descriptor
            .Field(address => address.Street)
            .Description("The Street Name");

        descriptor
            .Field(address => address.User)
            .Description("The Owner User");

        descriptor
            .Field(address => address.BuildingNumber)
            .Description("The Building Number");

        descriptor
            .Field(address => address.PostalCode)
            .Description("The Postal Code");

        descriptor
            .Field(address => address.UnitNumber)
            .Description("The Unit Number");

        descriptor
            .Field(address => address.DateCreated)
            .Description("The Creation Date");

        descriptor
            .Field(address => address.DateModified)
            .Description("The Last Modification Date");

        descriptor
            .Field(address => address.ExternalId)
            .Description("The External ID for User Interactions");
    }
}



/*
 *
 * public class PlatformType : ObjectType<Platform>
   {
   protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
   {
   descriptor.Description("Represents any software or service that has a command line interface.");

   descriptor
   .Field(p => p.Id)
   .Description("Represents the unique ID for the platform.");

   descriptor
   .Field(p => p.Name)
   .Description("Represents the name for the platform.");

   descriptor
   .Field(p => p.LicenseKey).Ignore();

   descriptor
   .Field(p => p.Commands)
   .ResolveWith<Resolvers>(p => p.GetCommands(default!, default!))
   .UseDbContext<AppDbContext>()
   .Description("This is the list of available commands for this platform.");
   }

   private class Resolvers
   {
   public IQueryable<Command> GetCommands(Platform platform, [ScopedService] AppDbContext context)
   {
   return context.Commands.Where(p => p.PlatformId == platform.Id);
   }
   }
   }
 *
 */