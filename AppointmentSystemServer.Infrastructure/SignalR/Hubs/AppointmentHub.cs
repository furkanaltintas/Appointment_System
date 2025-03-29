using Microsoft.AspNetCore.SignalR;

namespace AppointmentSystemServer.Infrastructure.SignalR.Hubs;

public class AppointmentHub : Hub
{
    // Kullanıcıya sinyal gönderme örneği
    public async Task NotifyAppointmentDeleted(string appointmentId)
    {
        await Clients.All.SendAsync("ReceiveAppointmentDeleted", appointmentId);
    }
}
