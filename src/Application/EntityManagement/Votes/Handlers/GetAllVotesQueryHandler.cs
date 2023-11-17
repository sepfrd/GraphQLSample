using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.EntityManagement.Votes.Handlers;

public class GetAllVotesQueryHandler(IRepository<Vote> voteRepository)
    : BaseGetAllQueryHandler<Vote>(voteRepository);