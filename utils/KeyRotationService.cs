

public class KeyRotationService : BackgroundService {

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested){
            Encryption.generateKeyPair();
            await Task.Delay(TimeSpan.FromMinutes(1),stoppingToken);
        }
    }
}