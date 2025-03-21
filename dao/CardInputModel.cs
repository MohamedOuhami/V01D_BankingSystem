public class CardInputModel {
    public int Id {get;set;}
    public string CardNumber {get;set;}
    public DateTime ExpiryDate {get;set;}
    public string Type {get;set;}
    public string PIN {get;set;}
    public string CVV {get;set;}
    public string Status {get;set;}
    public List<string> AccountsNumbers {get;set;}
}