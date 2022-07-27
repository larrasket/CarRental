namespace Services.Interfaces;

public interface INotifierManagement
{
    public int NotifierBeforeRent(int n);
    public int MaintenanceNotifier(int n);
    public int MaintenanceDelayNotifier(int n);
}