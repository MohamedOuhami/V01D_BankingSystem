using System.Text.Json.Serialization;

public class Account
{
    public int Id { get; set; }
    public string? AccountNumber { get; set; }
    public float Balance { get; set; }
    public string? Type { get; set; }
    public string? Currency { get; set; }
    public DateTime OpeningDate { get; set; }
    public string? Status { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }

    [JsonIgnore]
    public List<Card> Cards { get; set; } = new List<Card>();

    public List<Operation> Operations { get; set; } = new List<Operation>();

    public List<Transaction> FromTransactions { get; set; } = new List<Transaction>();
    public List<Transaction> ToTransactions { get; set; } = new List<Transaction>();
}