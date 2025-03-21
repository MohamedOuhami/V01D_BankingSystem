public class User
{

    public int Id { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public DateTime Dob { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    public List<Account> Accounts { get; set; } = new List<Account>();
}