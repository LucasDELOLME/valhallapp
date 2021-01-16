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
            await ReplyAsync($"Error: at least one user should be provided!\nExemple: {prefix}wrestler <@779648566057762826>");
        }
        [Command("wrestler")]
        public async Task OneParams([Remainder] string param)
        {
            DisplayCommandLine("fine", Context.Message.Author.Username, Context.Channel.Name);
            await ReplyAsync($"{param}");
        }
        [Command("wrestler")]
        public async Task OneParams([Remainder] string param, [Remainder] string param2)
        {
            DisplayCommandLine("fine", Context.Message.Author.Username, Context.Channel.Name);
            await ReplyAsync($"{param} {param2}");
        }
    }
}
