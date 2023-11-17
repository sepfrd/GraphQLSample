using Application.Common;
using Application.EntityManagement.Addresses.Commands;
using Application.EntityManagement.Addresses.Dtos;
using Application.EntityManagement.Answers.Commands;
using Application.EntityManagement.Answers.Dtos;
using Application.EntityManagement.CartItems.Commands;
using Application.EntityManagement.CartItems.Dtos;
using Application.EntityManagement.Categories.Commands;
using Application.EntityManagement.Categories.Dtos;
using Application.EntityManagement.Comments.Commands;
using Application.EntityManagement.Comments.Dtos;
using Application.EntityManagement.OrderItems.Commands;
using Application.EntityManagement.OrderItems.Dtos;
using Application.EntityManagement.Orders.Commands;
using Application.EntityManagement.Orders.Dtos;
using Application.EntityManagement.Payments.Commands;
using Application.EntityManagement.Payments.Dtos;
using Application.EntityManagement.Persons.Commands;
using Application.EntityManagement.Persons.Dtos;
using Application.EntityManagement.PhoneNumbers.Commands;
using Application.EntityManagement.PhoneNumbers.Dtos;
using Application.EntityManagement.Products.Commands;
using Application.EntityManagement.Products.Dtos;
using Application.EntityManagement.Questions.Commands;
using Application.EntityManagement.Questions.Dtos;
using Application.EntityManagement.Shipments.Commands;
using Application.EntityManagement.Shipments.Dtos;
using Application.EntityManagement.Users.Commands;
using Application.EntityManagement.Users.Dtos;
using Application.EntityManagement.Votes.Commands;
using Application.EntityManagement.Votes.Dtos;
using MediatR;

namespace Web.GraphQL;

public class Mutation(ISender sender)
{
    public async Task<CommandResult> AddAddressAsync(AddressDto addressDto, CancellationToken cancellationToken)
    {
        var createCommand = new CreateAddressCommand(addressDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> UpdateAddressAsync(int externalId, AddressDto addressDto, CancellationToken cancellationToken)
    {
        var updateCommand = new UpdateAddressCommand(externalId, addressDto);

        var result = await sender.Send(updateCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> DeleteAddressAsync(int externalId, CancellationToken cancellationToken)
    {
        var deleteCommand = new DeleteAddressByExternalIdCommand(externalId);

        var result = await sender.Send(deleteCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> AddAnswerAsync(AnswerDto answerDto, CancellationToken cancellationToken)
    {
        var createCommand = new CreateAnswerCommand(answerDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> UpdateAnswerAsync(int externalId, AnswerDto answerDto, CancellationToken cancellationToken)
    {
        var updateCommand = new UpdateAnswerCommand(externalId, answerDto);

        var result = await sender.Send(updateCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> DeleteAnswerAsync(int externalId, CancellationToken cancellationToken)
    {
        var deleteCommand = new DeleteAnswerByExternalIdCommand(externalId);

        var result = await sender.Send(deleteCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> AddCartItemAsync(CartItemDto cartItemDto, CancellationToken cancellationToken)
    {
        var createCommand = new CreateCartItemCommand(cartItemDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> AddCategoryAsync(CategoryDto categoryDto, CancellationToken cancellationToken)
    {
        var createCommand = new CreateCategoryCommand(categoryDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> AddCommentAsync(CommentDto commentDto, CancellationToken cancellationToken)
    {
        var createCommand = new CreateCommentCommand(commentDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> DeleteCommentAsync(int externalId, CancellationToken cancellationToken)
    {
        var deleteCommand = new DeleteCommentByExternalIdCommand(externalId);

        var result = await sender.Send(deleteCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> AddOrderItemAsync(OrderItemDto orderItemDto, CancellationToken cancellationToken)
    {
        var createCommand = new CreateOrderItemCommand(orderItemDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> UpdateOrderItemAsync(int externalId, OrderItemDto orderItemDto, CancellationToken cancellationToken)
    {
        var updateCommand = new UpdateOrderItemCommand(externalId, orderItemDto);

        var result = await sender.Send(updateCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> DeleteOrderItemAsync(int externalId, CancellationToken cancellationToken)
    {
        var deleteCommand = new DeleteOrderItemByExternalIdCommand(externalId);

        var result = await sender.Send(deleteCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> AddOrderAsync(OrderDto orderDto, CancellationToken cancellationToken)
    {
        var createCommand = new CreateOrderCommand(orderDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> UpdateOrderAsync(int externalId, OrderDto orderDto, CancellationToken cancellationToken)
    {
        var updateCommand = new UpdateOrderCommand(externalId, orderDto);

        var result = await sender.Send(updateCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> DeleteOrderAsync(int externalId, CancellationToken cancellationToken)
    {
        var deleteCommand = new DeleteOrderByExternalIdCommand(externalId);

        var result = await sender.Send(deleteCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> AddPaymentAsync(PaymentDto paymentDto, CancellationToken cancellationToken)
    {
        var createCommand = new CreatePaymentCommand(paymentDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> UpdatePaymentAsync(int externalId, PaymentDto paymentDto, CancellationToken cancellationToken)
    {
        var updateCommand = new UpdatePaymentCommand(externalId, paymentDto);

        var result = await sender.Send(updateCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> DeletePaymentAsync(int externalId, CancellationToken cancellationToken)
    {
        var deleteCommand = new DeletePaymentByExternalIdCommand(externalId);

        var result = await sender.Send(deleteCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> AddPersonAsync(PersonDto personDto, CancellationToken cancellationToken)
    {
        var createCommand = new CreatePersonCommand(personDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> UpdatePersonAsync(int externalId, PersonDto personDto, CancellationToken cancellationToken)
    {
        var updateCommand = new UpdatePersonCommand(externalId, personDto);

        var result = await sender.Send(updateCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> DeletePersonAsync(int externalId, CancellationToken cancellationToken)
    {
        var deleteCommand = new DeletePersonByExternalIdCommand(externalId);

        var result = await sender.Send(deleteCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> AddPhoneNumberAsync(PhoneNumberDto phoneNumberDto, CancellationToken cancellationToken)
    {
        var createCommand = new CreatePhoneNumberCommand(phoneNumberDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> UpdatePhoneNumberAsync(int externalId, PhoneNumberDto phoneNumberDto, CancellationToken cancellationToken)
    {
        var updateCommand = new UpdatePhoneNumberCommand(externalId, phoneNumberDto);

        var result = await sender.Send(updateCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> DeletePhoneNumberAsync(int externalId, CancellationToken cancellationToken)
    {
        var deleteCommand = new DeletePhoneNumberByExternalIdCommand(externalId);

        var result = await sender.Send(deleteCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> AddProductAsync(ProductDto productDto, CancellationToken cancellationToken)
    {
        var createCommand = new CreateProductCommand(productDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> UpdateProductAsync(int externalId, ProductDto productDto, CancellationToken cancellationToken)
    {
        var updateCommand = new UpdateProductCommand(externalId, productDto);

        var result = await sender.Send(updateCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> DeleteProductAsync(int externalId, CancellationToken cancellationToken)
    {
        var deleteCommand = new DeleteProductByExternalIdCommand(externalId);

        var result = await sender.Send(deleteCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> AddQuestionAsync(QuestionDto questionDto, CancellationToken cancellationToken)
    {
        var createCommand = new CreateQuestionCommand(questionDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> DeleteQuestionAsync(int externalId, CancellationToken cancellationToken)
    {
        var deleteCommand = new DeleteQuestionByExternalIdCommand(externalId);

        var result = await sender.Send(deleteCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> AddShipmentAsync(ShipmentDto shipmentDto, CancellationToken cancellationToken)
    {
        var createCommand = new CreateShipmentCommand(shipmentDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> UpdateShipmentAsync(int externalId, ShipmentDto shipmentDto, CancellationToken cancellationToken)
    {
        var updateCommand = new UpdateShipmentCommand(externalId, shipmentDto);

        var result = await sender.Send(updateCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> DeleteShipmentAsync(int externalId, CancellationToken cancellationToken)
    {
        var deleteCommand = new DeleteShipmentByExternalIdCommand(externalId);

        var result = await sender.Send(deleteCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> AddUserAsync(CreateUserDto userDto, CancellationToken cancellationToken)
    {
        var createCommand = new CreateUserCommand(userDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> UpdateUserAsync(UserDto userDto, CancellationToken cancellationToken)
    {
        var updateCommand = new UpdateUserCommand(userDto);

        var result = await sender.Send(updateCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> DeleteUserAsync(int externalId, CancellationToken cancellationToken)
    {
        var deleteCommand = new DeleteUserByExternalIdCommand(externalId);

        var result = await sender.Send(deleteCommand, cancellationToken);

        return result;
    }

    // TODO: Figure it out. 
    
    public async Task<CommandResult> AddVoteAsync(VoteDto voteDto, CancellationToken cancellationToken)
    {
        var createCommand = new CreateVoteCommand(voteDto);
    
        var result = await sender.Send(createCommand, cancellationToken);
    
        return result;
    }

    public async Task<CommandResult> DeleteVoteAsync(int externalId, CancellationToken cancellationToken)
    {
        var deleteCommand = new DeleteVoteByExternalIdCommand(externalId);

        var result = await sender.Send(deleteCommand, cancellationToken);

        return result;
    }
}