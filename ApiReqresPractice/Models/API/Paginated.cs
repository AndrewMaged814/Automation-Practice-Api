namespace ApiReqresPractice.Models;

public record Paginated<T>(
    int Page,
    int Total,
    List<T> Data
);