using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;

namespace IOT_Backend
{
    public class TemperatureHub: Hub
    {
        [EnableCors("AllowLocalhost")]
        public async Task TemperatureUpdate(int temp){
            await Clients.All.SendAsync("TemperatureUpdate", temp);
        }
    }
}