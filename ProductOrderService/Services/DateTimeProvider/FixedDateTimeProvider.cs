namespace Services.DateTimeProvider;

/// <summary>
/// For testing purposes
/// </summary>
public class FixedDateTimeProvider : IDateTimeProvider
{
    public DateTime DateTime { get; set; } = DateTime.MinValue;

    public DateTime Now() => DateTime;
}
