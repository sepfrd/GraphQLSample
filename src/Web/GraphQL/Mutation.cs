using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Addresses.Commands;
using Application.EntityManagement.Addresses.Dtos;
using Application.EntityManagement.Answers;
using Application.EntityManagement.Answers.Commands;
using Application.EntityManagement.Answers.Dtos;
using Application.EntityManagement.CartItems.Commands;
using Application.EntityManagement.CartItems.Dtos;
using Application.EntityManagement.Categories.Commands;
using Application.EntityManagement.Categories.Dtos;
using Application.EntityManagement.Comments;
using Application.EntityManagement.Comments.Commands;
using Application.EntityManagement.Comments.Dtos;
using Application.EntityManagement.Orders;
using Application.EntityManagement.Orders.Commands;
using Application.EntityManagement.Orders.Dtos;
using Application.EntityManagement.Payments.Commands;
using Application.EntityManagement.Payments.Dtos;
using Application.EntityManagement.Persons.Commands;
using Application.EntityManagement.Persons.Dtos;
using Application.EntityManagement.PhoneNumbers.Commands;
using Application.EntityManagement.PhoneNumbers.Dtos;
using Application.EntityManagement.Products;
using Application.EntityManagement.Products.Commands;
using Application.EntityManagement.Products.Dtos;
using Application.EntityManagement.Questions;
using Application.EntityManagement.Questions.Commands;
using Application.EntityManagement.Questions.Dtos;
using Application.EntityManagement.Shipments.Commands;
using Application.EntityManagement.Shipments.Dtos;
using Application.EntityManagement.Users;
using Application.EntityManagement.Users.Commands;
using Application.EntityManagement.Users.Dtos;
using Application.EntityManagement.Votes.Commands;
using Application.EntityManagement.Votes.Dtos;
using MediatR;
using System.Security.Claims;

namespace Web.GraphQL;

public class Mutation
{
    public async Task<CommandResult> AddAddressAsync([Service] ISender sender,
        AddressDto addressDto,
        CancellationToken cancellationToken)
    {
        var createCommand = new CreateAddressCommand(addressDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> UpdateAddressAsync([Service] ISender sender, int externalId, AddressDto addressDto, CancellationToken cancellationToken)
    {
        var updateCommand = new UpdateAddressCommand(externalId, addressDto);

        var result = await sender.Send(updateCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> DeleteAddressAsync([Service] ISender sender, int externalId, CancellationToken cancellationToken)
    {
        var deleteCommand = new DeleteAddressByExternalIdCommand(externalId);

        var result = await sender.Send(deleteCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> AddAnswerAsync([Service] ISender sender, CreateAnswerDto answerDto, CancellationToken cancellationToken)
    {
        var createCommand = new CreateAnswerCommand(answerDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> UpdateAnswerAsync([Service] ISender sender, int externalId, AnswerDto answerDto, CancellationToken cancellationToken)
    {
        var updateCommand = new UpdateAnswerCommand(externalId, answerDto);

        var result = await sender.Send(updateCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> DeleteAnswerAsync([Service] AnswerService answerService, int externalId, CancellationToken cancellationToken) =>
        await answerService.DeleteByExternalIdAsync(externalId, cancellationToken);

    public async Task<CommandResult> AddCartItemAsync([Service] ISender sender, CreateCartItemDto createCartItemDto, CancellationToken cancellationToken)
    {
        var createCommand = new CreateCartItemCommand(createCartItemDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> DeleteCartItemAsync([Service] ISender sender, int externalId, CancellationToken cancellationToken)
    {
        var deleteCommand = new DeleteCartItemByExternalICommand(externalId);

        var result = await sender.Send(deleteCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> AddCategoryAsync([Service] ISender sender, CategoryDto categoryDto, CancellationToken cancellationToken)
    {
        var createCommand = new CreateCategoryCommand(categoryDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> AddCommentAsync([Service] ISender sender, CommentDto commentDto, CancellationToken cancellationToken)
    {
        var createCommand = new CreateCommentCommand(commentDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> DeleteCommentAsync([Service] CommentService commentService, int externalId, CancellationToken cancellationToken) =>
        await commentService.DeleteByExternalIdAsync(externalId, cancellationToken);

    public async Task<CommandResult> AddOrderAsync([Service] ISender sender, CreateOrderDto createOrderDto, CancellationToken cancellationToken)
    {
        var createCommand = new CreateOrderCommand(createOrderDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> UpdateOrderAsync([Service] ISender sender, int externalId, CreateOrderDto createOrderDto, CancellationToken cancellationToken)
    {
        var updateCommand = new UpdateOrderCommand(externalId, createOrderDto);

        var result = await sender.Send(updateCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> DeleteOrderAsync([Service] OrderService orderService, int externalId, CancellationToken cancellationToken) =>
        await orderService.DeleteByExternalIdAsync(externalId, cancellationToken);

    public async Task<CommandResult> UpdatePaymentAsync([Service] ISender sender, int externalId, CreatePaymentDto createPaymentDto, CancellationToken cancellationToken)
    {
        var updateCommand = new UpdatePaymentCommand(externalId, createPaymentDto);

        var result = await sender.Send(updateCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> UpdatePersonAsync([Service] ISender sender, int externalId, UpdatePersonDto updatePersonDto, CancellationToken cancellationToken)
    {
        var updateCommand = new UpdatePersonCommand(externalId, updatePersonDto);

        var result = await sender.Send(updateCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> AddPhoneNumberAsync([Service] ISender sender, PhoneNumberDto phoneNumberDto, CancellationToken cancellationToken)
    {
        var createCommand = new CreatePhoneNumberCommand(phoneNumberDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> UpdatePhoneNumberAsync([Service] ISender sender, int externalId, PhoneNumberDto phoneNumberDto, CancellationToken cancellationToken)
    {
        var updateCommand = new UpdatePhoneNumberCommand(externalId, phoneNumberDto);

        var result = await sender.Send(updateCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> DeletePhoneNumberAsync([Service] ISender sender, int externalId, CancellationToken cancellationToken)
    {
        var deleteCommand = new DeletePhoneNumberByExternalIdCommand(externalId);

        var result = await sender.Send(deleteCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> AddProductAsync([Service] ISender sender, CreateProductDto createProductDto, CancellationToken cancellationToken)
    {
        var createCommand = new CreateProductCommand(createProductDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> UpdateProductAsync([Service] ISender sender, int externalId, ProductDto productDto, CancellationToken cancellationToken)
    {
        var updateCommand = new UpdateProductCommand(externalId, productDto);

        var result = await sender.Send(updateCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> DeleteProductAsync([Service] ProductService productService, int externalId, CancellationToken cancellationToken) =>
        await productService.DeleteByExternalIdAsync(externalId, cancellationToken);

    public async Task<CommandResult> AddQuestionAsync([Service] ISender sender, QuestionDto questionDto, CancellationToken cancellationToken)
    {
        var createCommand = new CreateQuestionCommand(questionDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> DeleteQuestionAsync([Service] QuestionService questionService, int externalId, CancellationToken cancellationToken) =>
        await questionService.DeleteByExternalIdAsync(externalId, cancellationToken);


    public async Task<CommandResult> UpdateShipmentAsync([Service] ISender sender, int externalId, ShipmentDto shipmentDto, CancellationToken cancellationToken)
    {
        var updateCommand = new UpdateShipmentCommand(externalId, shipmentDto);

        var result = await sender.Send(updateCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> AddUserAsync([Service] ISender sender, CreateUserDto userDto, CancellationToken cancellationToken)
    {
        var createCommand = new CreateUserCommand(userDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> UpdateUserAsync([Service] ISender sender, UserDto userDto, CancellationToken cancellationToken)
    {
        var updateCommand = new UpdateUserCommand(userDto);

        var result = await sender.Send(updateCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> DeleteUserAsync([Service] UserService userService, int externalId, CancellationToken cancellationToken) =>
        await userService.DeleteByExternalIdAsync(externalId, cancellationToken);

    public async Task<CommandResult> AddVoteAsync([Service] ISender sender, CreateVoteDto createVoteDto, CancellationToken cancellationToken)
    {
        var createVoteCommand = new CreateVoteCommand(createVoteDto);

        var result = await sender.Send(createVoteCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> DeleteVoteAsync([Service] ISender sender, int externalId, CancellationToken cancellationToken)
    {
        var deleteCommand = new DeleteVoteByExternalIdCommand(externalId);

        var result = await sender.Send(deleteCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> LoginAsync([Service] ISender sender, LoginDto loginDto, CancellationToken cancellationToken)
    {
        var loginCommand = new LoginCommand(loginDto);

        return await sender.Send(loginCommand, cancellationToken);
    }
}