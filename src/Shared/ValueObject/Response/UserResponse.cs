namespace Shared.ValueObject.Response;

public class UserResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string PhotoUrl { get; set; }

    public DateTime CreatedAt { get; set; }
}
