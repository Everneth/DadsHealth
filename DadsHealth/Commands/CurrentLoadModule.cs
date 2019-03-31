using DadsHealth.Utils;
using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DadsHealth.Commands
{
    public class CurrentLoadModule : ModuleBase<SocketCommandContext>
    {
        [Command("currentload")]
        public async Task CurrentLoadAsync()
        {
            var output = "top -b -n 1 -d 1 -U mc1 | awk '{print $9}' | tail -1".Bash();
            var mem = "top -b -n 1 -d 1 -U mc1 | awk '{print $10}' | tail -1".Bash();
            var totalmem = Double.Parse(mem) / 100 * 9.1;

            double load = Double.Parse(output);
            /*
            if(load < 75)
            {
                var role = Context.Guild.Roles.SingleOrDefault(r => r.Name == Context.Client.CurrentUser.Username);
                await Context.Guild.GetRole(role.Id).ModifyAsync(r => r.Color = Color.Green);
            }
            */

            await ReplyAsync("CPU%: " + output + "MEM: " + totalmem + " GB");
        }
    }
}
