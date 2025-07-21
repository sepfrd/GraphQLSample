namespace Application.Common.Dtos;

public record PaginationDto(uint PageNumber = 1, uint PageSize = 10);