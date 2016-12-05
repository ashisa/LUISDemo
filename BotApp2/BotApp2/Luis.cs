using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

namespace BotApp2
{
    [LuisModel("<YOUR LUIS APP ID>", "<YOUR LUIS APP KEY>")]
    [Serializable]
    public class Luis: LuisDialog<object>
    {
        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = "I'm sorry I didn't understand. Try asking about stocks or inventory.";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }

        [LuisIntent("ListItems")]
        public async Task ListInventory(IDialogContext context, LuisResult result)
        {
            string message = "";
            if (result.Entities.Count != 0 && result.Intents.Count != 0 ) message = $"I detected the intent \"{result.Intents[0].Intent}\" for \"{result.Entities[0].Entity}\". Was that right?";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }


        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceived);
        }

    }
}