using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static valhallappweb.PublicFunction;

namespace valhallappweb.Commands
{
    public class Wrestler : ModuleBase<SocketCommandContext>
    {
        [Command("wrestler")]
        public async Task NullParams()
        {
            DisplayCommandLine("wrestler 0", Context.Message.Author.Username, Context.Channel.Name);
            await ReplyAsync($"Error: at least one user should be provided!\nExemple: {}");
        }
        [Command("wrestler")]
        public async Task NullParams()
        {
            DisplayCommandLine("fine", Context.Message.Author.Username, Context.Channel.Name);
            await ReplyAsync("I guess you're my little PogChamp.\nCome here~");
        }
    }
}
