using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Goods.Social.Network.Web.API
{
    [Authorize]
    public class ChatHub : Hub
    {
        string groupName = "";
        public async Task Enter(string username, string groupname)
        {
            groupName = groupname;
            if (String.IsNullOrEmpty(username))
            {
                await Clients.Caller.SendAsync("Notify", "Авторизуйтесь для входа в чат");
            }
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, groupname);
                await Clients.Group(groupname).SendAsync("Notify", $"{username} вошел в чат");
            }
        }
        public async Task Send(string message, string username, string groupname)
        {

            await this.Clients.Group(groupname).SendAsync("Receive", message, username);
        }
    }
}