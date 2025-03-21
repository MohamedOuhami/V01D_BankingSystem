using System.Text.Json.Serialization;

public class Card {
    public int Id {get;set;}
    public string CardNumber {get;set;}
    public DateTime ExpiryDate {get;set;}
    public string Type {get;set;}
    public string PIN {get;set;}
    public string CVV {get;set;}
    public string Status {get;set;}

    public List<Account> Accounts {get;set;} = new List<Account>();
}