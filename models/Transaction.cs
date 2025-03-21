public class Transaction {
    public int Id {get;set;}
    public float Amount {get;set;}
    public DateTime TimeStamp {get;set;}

    // From Account
    public int FromAccountId {get;set;}
    public Account? FromAccount {get;set;}

    public int ToAccountId {get;set;}
    public Account? ToAccount {get;set;}
}