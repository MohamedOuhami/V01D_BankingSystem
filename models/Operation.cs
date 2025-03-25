using System.Text.Json.Serialization;

public class Operation
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime Timestamp { get; set; }
    public string Type { get; set; }

    [JsonIgnore]
    public int AccountId { get; set; }
    [JsonIgnore]
    public Account? Account { get; set; }
}