
namespace Services.DateTimeProvider;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now() => DateTime.UtcNow;
}
