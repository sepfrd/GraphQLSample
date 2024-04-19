using Application.Common;
using Application.EntityManagement.Addresses.Commands;
using Application.EntityManagement.Addresses.Dtos.AddressDto;
using Application.EntityManagement.Answers;
using Application.EntityManagement.Answers.Commands;
using Application.EntityManagement.Answers.Dtos.AnswerDto;
using Application.EntityManagement.CartItems.Commands;
using Application.EntityManagement.CartItems.Dtos.CartItemDto;
using Application.EntityManagement.Categories.Commands;
using Application.EntityManagement.Categories.Dtos.CategoryDto;
using Application.EntityManagement.Comments;
using Application.EntityManagement.Comments.Commands;
using Application.EntityManagement.Comments.Dtos.CommentDto;
using Application.EntityManagement.Orders;
using Application.EntityManagement.Orders.Commands;
using Application.EntityManagement.Orders.Dtos.CreateOrderDto;
using Application.EntityManagement.Payments.Commands;
using Application.EntityManagement.Payments.Dtos.PaymentDto;
using Application.EntityManagement.Persons.Commands;
using Application.EntityManagement.Persons.Dtos.PersonDto;
using Application.EntityManagement.PhoneNumbers.Commands;
using Application.EntityManagement.PhoneNumbers.Dtos.PhoneNumberDto;
using Application.EntityManagement.Products;
using Application.EntityManagement.Products.Commands;
using Application.EntityManagement.Products.Dtos.ProductDto;
using Application.EntityManagement.Questions;
using Application.EntityManagement.Questions.Commands;
using Application.EntityManagement.Questions.Dtos.QuestionDto;
using Application.EntityManagement.Roles;
using Application.EntityManagement.Roles.Commands;
using Application.EntityManagement.Roles.Dtos.RoleDto;
using Application.EntityManagement.Shipments.Commands;
using Application.EntityManagement.Shipments.Dtos.ShipmentDto;
using Application.EntityManagement.UserRoles.Commands;
using Application.EntityManagement.UserRoles.Dtos.UserRoleDto;
using Application.EntityManagement.Users;
using Application.EntityManagement.Users.Commands;
using Application.EntityManagement.Users.Dtos.CreateUserDto;
using Application.EntityManagement.Users.Dtos.LoginDto;
using Application.EntityManagement.Users.Dtos.UserDto;
using Application.EntityManagement.Votes.Commands;
using Application.EntityManagement.Votes.Dtos.VoteDto;
using MediatR;

namespace Web.GraphQL;

public class Mutation
{
    public async static Task<CommandResult> AddAddressAsync([Service] ISender sender,
        AddressDto addressDto,
        CancellationToken cancellationToken)
    {
        var createCommand = new CreateAddressCommand(addressDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async static Task<CommandResult> UpdateAddressAsync([Service] ISender sender, int externalId,
        AddressDto addressDto, CancellationToken cancellationToken)
    {
        var updateCommand = new UpdateAddressCommand(externalId, addressDto);

        var result = await sender.Send(updateCommand, cancellationToken);

        return result;
    }

    public async static Task<CommandResult> DeleteAddressAsync([Service] ISender sender, int externalId,
        CancellationToken cancellationToken)
    {
        var deleteCommand = new DeleteAddressByExternalIdCommand(externalId);

        var result = await sender.Send(deleteCommand, cancellationToken);

        return result;
    }

    public async static Task<CommandResult> AddAnswerAsync([Service] ISender sender, AnswerDto answerDto,
        CancellationToken cancellationToken)
    {
        var createCommand = new CreateAnswerCommand(answerDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async static Task<CommandResult> UpdateAnswerAsync([Service] ISender sender, int externalId,
        AnswerDto answerDto, CancellationToken cancellationToken)
    {
        var updateCommand = new UpdateAnswerCommand(externalId, answerDto);

        var result = await sender.Send(updateCommand, cancellationToken);

        return result;
    }

    public async static Task<CommandResult> DeleteAnswerAsync([Service] AnswerService answerService, int externalId,
        CancellationToken cancellationToken) =>
        await answerService.DeleteByExternalIdAsync(externalId, cancellationToken);

    public async static Task<CommandResult> AddCartItemAsync([Service] ISender sender, CartItemDto cartItemDto,
        CancellationToken cancellationToken)
    {
        var createCommand = new CreateCartItemCommand(cartItemDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async static Task<CommandResult> DeleteCartItemAsync([Service] ISender sender, int externalId,
        CancellationToken cancellationToken)
    {
        var deleteCommand = new DeleteCartItemByExternalIdCommand(externalId);

        var result = await sender.Send(deleteCommand, cancellationToken);

        return result;
    }

    public async static Task<CommandResult> AddCategoryAsync([Service] ISender sender, CategoryDto categoryDto,
        CancellationToken cancellationToken)
    {
        var createCommand = new CreateCategoryCommand(categoryDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async static Task<CommandResult> AddCommentAsync([Service] ISender sender, CommentDto commentDto,
        CancellationToken cancellationToken)
    {
        var createCommand = new CreateCommentCommand(commentDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async static Task<CommandResult> DeleteCommentAsync([Service] CommentService commentService, int externalId,
        CancellationToken cancellationToken) =>
        await commentService.DeleteByExternalIdAsync(externalId, cancellationToken);

    public async static Task<CommandResult> AddOrderAsync([Service] ISender sender, CreateOrderDto createOrderDto,
        CancellationToken cancellationToken)
    {
        var createCommand = new CreateOrderCommand(createOrderDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async static Task<CommandResult> UpdateOrderAsync([Service] ISender sender, int externalId,
        CreateOrderDto createOrderDto, CancellationToken cancellationToken)
    {
        var updateCommand = new UpdateOrderCommand(externalId, createOrderDto);

        var result = await sender.Send(updateCommand, cancellationToken);

        return result;
    }

    public async static Task<CommandResult> DeleteOrderAsync([Service] OrderService orderService, int externalId,
        CancellationToken cancellationToken) =>
        await orderService.DeleteByExternalIdAsync(externalId, cancellationToken);

    public async static Task<CommandResult> UpdatePaymentAsync([Service] ISender sender, int externalId,
        PaymentDto paymentDto, CancellationToken cancellationToken)
    {
        var updateCommand = new UpdatePaymentCommand(externalId, paymentDto);

        var result = await sender.Send(updateCommand, cancellationToken);

        return result;
    }

    public async static Task<CommandResult> UpdatePersonAsync([Service] ISender sender, int externalId,
        PersonDto personDto, CancellationToken cancellationToken)
    {
        var updateCommand = new UpdatePersonCommand(externalId, personDto);

        var result = await sender.Send(updateCommand, cancellationToken);

        return result;
    }

    public async static Task<CommandResult> AddPhoneNumberAsync([Service] ISender sender, PhoneNumberDto phoneNumberDto,
        CancellationToken cancellationToken)
    {
        var createCommand = new CreatePhoneNumberCommand(phoneNumberDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async static Task<CommandResult> UpdatePhoneNumberAsync([Service] ISender sender, int externalId,
        PhoneNumberDto phoneNumberDto, CancellationToken cancellationToken)
    {
        var updateCommand = new UpdatePhoneNumberCommand(externalId, phoneNumberDto);

        var result = await sender.Send(updateCommand, cancellationToken);

        return result;
    }

    public async static Task<CommandResult> DeletePhoneNumberAsync([Service] ISender sender, int externalId,
        CancellationToken cancellationToken)
    {
        var deleteCommand = new DeletePhoneNumberByExternalIdCommand(externalId);

        var result = await sender.Send(deleteCommand, cancellationToken);

        return result;
    }

    public async static Task<CommandResult> AddProductAsync([Service] ISender sender, ProductDto productDto,
        CancellationToken cancellationToken)
    {
        var createCommand = new CreateProductCommand(productDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async static Task<CommandResult> UpdateProductAsync([Service] ISender sender, int externalId,
        ProductDto productDto, CancellationToken cancellationToken)
    {
        var updateCommand = new UpdateProductCommand(externalId, productDto);

        var result = await sender.Send(updateCommand, cancellationToken);

        return result;
    }

    public async static Task<CommandResult> DeleteProductAsync([Service] ProductService productService, int externalId,
        CancellationToken cancellationToken) =>
        await productService.DeleteByExternalIdAsync(externalId, cancellationToken);

    public async static Task<CommandResult> AddQuestionAsync([Service] ISender sender, QuestionDto questionDto,
        CancellationToken cancellationToken)
    {
        var createCommand = new CreateQuestionCommand(questionDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async static Task<CommandResult> DeleteQuestionAsync([Service] QuestionService questionService,
        int externalId, CancellationToken cancellationToken) =>
        await questionService.DeleteByExternalIdAsync(externalId, cancellationToken);

    public async static Task<CommandResult> AddRoleAsync([Service] ISender sender,
        RoleDto roleDto,
        CancellationToken cancellationToken)
    {
        var createCommand = new CreateRoleCommand(roleDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async static Task<CommandResult> UpdateRoleAsync([Service] ISender sender, int externalId, RoleDto roleDto,
        CancellationToken cancellationToken)
    {
        var updateCommand = new UpdateRoleCommand(externalId, roleDto);

        var result = await sender.Send(updateCommand, cancellationToken);

        return result;
    }

    public async static Task<CommandResult> DeleteRoleAsync([Service] RoleService roleService, int externalId,
        CancellationToken cancellationToken) =>
        await roleService.DeleteByExternalIdAsync(externalId, cancellationToken);

    public async static Task<CommandResult> UpdateShipmentAsync([Service] ISender sender, int externalId,
        ShipmentDto shipmentDto, CancellationToken cancellationToken)
    {
        var updateCommand = new UpdateShipmentCommand(externalId, shipmentDto);

        var result = await sender.Send(updateCommand, cancellationToken);

        return result;
    }

    public async static Task<CommandResult> AddUserRoleAsync([Service] ISender sender, UserRoleDto userRoleDto,
        CancellationToken cancellationToken)
    {
        var createCommand = new CreateUserRoleCommand(userRoleDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async static Task<CommandResult> DeleteUserRoleAsync([Service] ISender sender, int externalId,
        CancellationToken cancellationToken)
    {
        var deleteCommand = new DeleteUserRoleByExternalIdCommand(externalId);

        var result = await sender.Send(deleteCommand, cancellationToken);

        return result;
    }

    public async static Task<CommandResult> UpdateUserAsync([Service] ISender sender, UserDto userDto,
        CancellationToken cancellationToken)
    {
        var updateCommand = new UpdateUserCommand(userDto);

        var result = await sender.Send(updateCommand, cancellationToken);

        return result;
    }

    public async static Task<CommandResult> DeleteUserAsync([Service] UserService userService, int externalId,
        CancellationToken cancellationToken) =>
        await userService.DeleteByExternalIdAsync(externalId, cancellationToken);

    public async static Task<CommandResult> AddVoteAsync([Service] ISender sender, VoteDto voteDto,
        CancellationToken cancellationToken)
    {
        var createVoteCommand = new CreateVoteCommand(voteDto);

        var result = await sender.Send(createVoteCommand, cancellationToken);

        return result;
    }

    public async static Task<CommandResult> DeleteVoteAsync([Service] ISender sender, int externalId,
        CancellationToken cancellationToken)
    {
        var deleteCommand = new DeleteVoteByExternalIdCommand(externalId);

        var result = await sender.Send(deleteCommand, cancellationToken);

        return result;
    }

    public async static Task<CommandResult> SignUpAsync([Service] ISender sender, CreateUserDto userDto,
        CancellationToken cancellationToken)
    {
        var createCommand = new CreateUserCommand(userDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async static Task<CommandResult> LoginAsync([Service] ISender sender, LoginDto loginDto,
        CancellationToken cancellationToken)
    {
        var loginCommand = new LoginCommand(loginDto);

        return await sender.Send(loginCommand, cancellationToken);
    }
}