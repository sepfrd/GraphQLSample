using Domain.Entities;

namespace Web.GraphQL.Types;

public class AddressType : ObjectType<Address>
{
    protected override void Configure(IObjectTypeDescriptor<Address> descriptor)
    {
        descriptor
            .Description(
                "Represents a physical address with details such as street, city, state, postal code, country, unit number and building number.");

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
            .Description("The External ID for Client Interactions");
    }
}