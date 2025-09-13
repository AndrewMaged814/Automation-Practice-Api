using System.Collections.Generic;

namespace AutomationPractice_api.Models.API;

public record Paginated<T>(
    int Page,
    int Total,
    List<T> Data
);