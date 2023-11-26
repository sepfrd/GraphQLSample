#region

using Application.Common;
using Application.EntityManagement.Addresses.Commands;
using Application.EntityManagement.Addresses.Dtos;
using Application.EntityManagement.Answers.Commands;
using Application.EntityManagement.Answers.Dtos;
using Application.EntityManagement.Answers.Queries;
using Application.EntityManagement.CartItems.Commands;
using Application.EntityManagement.CartItems.Dtos;
using Application.EntityManagement.Categories.Commands;
using Application.EntityManagement.Categories.Dtos;
using Application.EntityManagement.Comments.Commands;
using Application.EntityManagement.Comments.Dtos;
using Application.EntityManagement.Comments.Queries;
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
using Application.EntityManagement.Products.Queries;
using Application.EntityManagement.Questions.Commands;
using Application.EntityManagement.Questions.Dtos;
using Application.EntityManagement.Questions.Queries;
using Application.EntityManagement.Shipments.Commands;
using Application.EntityManagement.Shipments.Dtos;
using Application.EntityManagement.Users.Commands;
using Application.EntityManagement.Users.Dtos;
using Application.EntityManagement.Votes.Commands;
using Application.EntityManagement.Votes.Dtos;
using Domain.Common;
using MediatR;
using Web.GraphQL.Types.InputTypes;

#endregion

namespace Web.GraphQL;

public class Mutation
{
    // ------------------------------------------ Address --------------------------------------->

    public async Task<CommandResult> AddAddressAsync([Service] ISender sender, CreateAddressDto createAddressDto, CancellationToken cancellationToken)
    {
        var createCommand = new CreateAddressCommand(createAddressDto);

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

    // <-------------------------------------------------------------------------------------------

    // ------------------------------------------ Answer --------------------------------------->

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

    public async Task<CommandResult> DeleteAnswerAsync([Service] ISender sender, int externalId, CancellationToken cancellationToken)
    {
        var deleteCommand = new DeleteAnswerByExternalIdCommand(externalId);

        var result = await sender.Send(deleteCommand, cancellationToken);

        return result;
    }

    // <-------------------------------------------------------------------------------------------

    // ------------------------------------------ CartItem --------------------------------------->

    public async Task<CommandResult> AddCartItemAsync([Service] ISender sender, CreateCartItemDto createCartItemDto, CancellationToken cancellationToken)
    {
        var createCommand = new CreateCartItemCommand(createCartItemDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    // <-------------------------------------------------------------------------------------------

    // ------------------------------------------ Category --------------------------------------->

    public async Task<CommandResult> AddCategoryAsync([Service] ISender sender, CategoryDto categoryDto, CancellationToken cancellationToken)
    {
        var createCommand = new CreateCategoryCommand(categoryDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    // <-------------------------------------------------------------------------------------------

    // ------------------------------------------ Comment --------------------------------------->

    public async Task<CommandResult> AddCommentAsync([Service] ISender sender, CommentDto commentDto, CancellationToken cancellationToken)
    {
        var createCommand = new CreateCommentCommand(commentDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> DeleteCommentAsync([Service] ISender sender, int externalId, CancellationToken cancellationToken)
    {
        var deleteCommand = new DeleteCommentByExternalIdCommand(externalId);

        var result = await sender.Send(deleteCommand, cancellationToken);

        return result;
    }

    // <-------------------------------------------------------------------------------------------

    // ------------------------------------------ Order --------------------------------------->

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

    public async Task<CommandResult> DeleteOrderAsync([Service] ISender sender, int externalId, CancellationToken cancellationToken)
    {
        var deleteCommand = new DeleteOrderByExternalIdCommand(externalId);

        var result = await sender.Send(deleteCommand, cancellationToken);

        return result;
    }

    // <-------------------------------------------------------------------------------------------

    // ------------------------------------------ Payment --------------------------------------->

    public async Task<CommandResult> UpdatePaymentAsync([Service] ISender sender, int externalId, CreatePaymentDto createPaymentDto, CancellationToken cancellationToken)
    {
        var updateCommand = new UpdatePaymentCommand(externalId, createPaymentDto);

        var result = await sender.Send(updateCommand, cancellationToken);

        return result;
    }

    // <-------------------------------------------------------------------------------------------

    // ------------------------------------------ Person --------------------------------------->

    public async Task<CommandResult> UpdatePersonAsync([Service] ISender sender, int externalId, PersonDto personDto, CancellationToken cancellationToken)
    {
        var updateCommand = new UpdatePersonCommand(externalId, personDto);

        var result = await sender.Send(updateCommand, cancellationToken);

        return result;
    }

    // <-------------------------------------------------------------------------------------------

    // ------------------------------------------ PhoneNumber --------------------------------------->

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

    // <-------------------------------------------------------------------------------------------

    // ------------------------------------------ Product --------------------------------------->
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

    public async Task<CommandResult> DeleteProductAsync([Service] ISender sender, int externalId, CancellationToken cancellationToken)
    {
        var deleteCommand = new DeleteProductByExternalIdCommand(externalId);

        var result = await sender.Send(deleteCommand, cancellationToken);

        return result;
    }

    // <-------------------------------------------------------------------------------------------

    // ------------------------------------------ Question --------------------------------------->
    public async Task<CommandResult> AddQuestionAsync([Service] ISender sender, QuestionDto questionDto, CancellationToken cancellationToken)
    {
        var createCommand = new CreateQuestionCommand(questionDto);

        var result = await sender.Send(createCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> DeleteQuestionAsync([Service] ISender sender, int externalId, CancellationToken cancellationToken)
    {
        var deleteCommand = new DeleteQuestionByExternalIdCommand(externalId);

        var result = await sender.Send(deleteCommand, cancellationToken);

        return result;
    }

    // <-------------------------------------------------------------------------------------------

    // ------------------------------------------ Shipment --------------------------------------->

    public async Task<CommandResult> UpdateShipmentAsync([Service] ISender sender, int externalId, ShipmentDto shipmentDto, CancellationToken cancellationToken)
    {
        var updateCommand = new UpdateShipmentCommand(externalId, shipmentDto);

        var result = await sender.Send(updateCommand, cancellationToken);

        return result;
    }

    // <-------------------------------------------------------------------------------------------

    // ------------------------------------------ User --------------------------------------->

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

    public async Task<CommandResult> DeleteUserAsync([Service] ISender sender, int externalId, CancellationToken cancellationToken)
    {
        var deleteCommand = new DeleteUserByExternalIdCommand(externalId);

        var result = await sender.Send(deleteCommand, cancellationToken);

        return result;
    }

    // <-------------------------------------------------------------------------------------------

    // ------------------------------------------ Vote --------------------------------------->

    public async Task<CommandResult> AddAnswerVoteAsync([Service] ISender sender, AddAnswerVoteInputType answerVoteInput, CancellationToken cancellationToken)
    {
        var answerQuery = new GetAllAnswersQuery(
            new Pagination(),
            answer => answer.ExternalId == answerVoteInput.AnswerExternalId);

        var answerResult = await sender.Send(answerQuery, cancellationToken);

        var answer = answerResult.Data?.FirstOrDefault();

        if (answer is null)
        {
            return CommandResult.Failure(Messages.NotFound);
        }

        var voteDto = new VoteDto(answerVoteInput.VoteType, answer);

        var createVoteCommand = new CreateVoteCommand(voteDto);

        var result = await sender.Send(createVoteCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> AddCommentVoteAsync([Service] ISender sender, AddCommentVoteInputType commentVoteInput, CancellationToken cancellationToken)
    {
        var commentQuery = new GetAllCommentsQuery(
            new Pagination(),
            comment => comment.ExternalId == commentVoteInput.CommentExternalId);

        var commentResult = await sender.Send(commentQuery, cancellationToken);

        var comment = commentResult.Data?.FirstOrDefault();

        if (comment is null)
        {
            return CommandResult.Failure(Messages.NotFound);
        }

        var voteDto = new VoteDto(commentVoteInput.VoteType, comment);

        var createVoteCommand = new CreateVoteCommand(voteDto);

        var result = await sender.Send(createVoteCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> AddProductVoteAsync([Service] ISender sender, AddProductVoteInputType productVoteInput, CancellationToken cancellationToken)
    {
        var productQuery = new GetAllProductsQuery(
            new Pagination(),
            product => product.ExternalId == productVoteInput.ProductExternalId);

        var productResult = await sender.Send(productQuery, cancellationToken);

        var product = productResult.Data?.FirstOrDefault();

        if (product is null)
        {
            return CommandResult.Failure(Messages.NotFound);
        }

        var voteDto = new VoteDto(productVoteInput.VoteType, product);

        var createVoteCommand = new CreateVoteCommand(voteDto);

        var result = await sender.Send(createVoteCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> AddQuestionVoteAsync([Service] ISender sender, AddQuestionVoteInputType questionVoteInput, CancellationToken cancellationToken)
    {
        var questionQuery = new GetAllQuestionsQuery(
            new Pagination(),
            question => question.ExternalId == questionVoteInput.QuestionExternalId);

        var questionResult = await sender.Send(questionQuery, cancellationToken);

        var question = questionResult.Data?.FirstOrDefault();

        if (question is null)
        {
            return CommandResult.Failure(Messages.NotFound);
        }

        var voteDto = new VoteDto(questionVoteInput.VoteType, question);

        var createVoteCommand = new CreateVoteCommand(voteDto);

        var result = await sender.Send(createVoteCommand, cancellationToken);

        return result;
    }

    public async Task<CommandResult> DeleteVoteAsync([Service] ISender sender, int externalId, CancellationToken cancellationToken)
    {
        var deleteCommand = new DeleteVoteByExternalIdCommand(externalId);

        var result = await sender.Send(deleteCommand, cancellationToken);

        return result;
    }

    // <-------------------------------------------------------------------------------------------
}