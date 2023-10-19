namespace Application.EntityManagement.Votes.Dtos;

public record GetAllVotesByContentExternalIdResponseDto(int HatesCount, int DislikesCount, int LikesCount, int SuperLikesCount);