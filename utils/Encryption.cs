using System.Security.Cryptography;

static class  Encryption {
    public static string publicKey {get;private set;}
    public static string privateKey {get;private set;}

    public static void generateKeyPair(){
        using (RSA rsa = RSA.Create(2048)) {
            publicKey = Convert.ToBase64String(rsa.ExportSubjectPublicKeyInfo());
            privateKey = Convert.ToBase64String(rsa.ExportPkcs8PrivateKey());
        }
    }
}