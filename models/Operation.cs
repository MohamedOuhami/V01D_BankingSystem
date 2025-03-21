public class Operation {
    public int Id {get;set;}
    public float Amount {get;set;}
    public DateTime Timestamp {get;set;}
    public string Type {get;set;}

    public int AccountId {get;set;}
    public Account? Account {get;set;}
}