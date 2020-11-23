// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with EchoBot .NET Template version v4.11.1

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Teams;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Bot.Schema;
using Microsoft.Bot.Schema.Teams;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace EchoBot.Bots
{
    public class EchoBot : ActivityHandler
    {
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            //var replyText = $"Echo: {turnContext.Activity.Text}";
            //await turnContext.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken);
            turnContext.Activity.RemoveRecipientMention();

            var text = turnContext.Activity.Text.Trim().ToLower();
            if(text.Contains("Hi"))
                await turnContext.SendActivityAsync("Hi Welcome to Edelweiss!!!", cancellationToken: cancellationToken);
            else
                await CardActivityAsync(turnContext, cancellationToken);
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "Hello and welcome!";
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                }
            }
        }

         private async Task CardActivityAsync(ITurnContext<IMessageActivity> turnContext,CancellationToken cancellationToken)
        {

            var card = new HeroCard
            {
                Buttons = new List<CardAction>
                        {
                            new CardAction
                            {
                                Type = ActionTypes.MessageBack,
                                Title = "Pending Account Opening Cases",
                                Text = "PendingAccountOpeningCases"
                            },
                            new CardAction
                            {
                                Type = ActionTypes.MessageBack,
                                Title = "Status Of Your Case",
                                Text = "StatusOfYourCase"
                            },
                            new CardAction
                            {
                                Type = ActionTypes.MessageBack,
                                Title = "Reports and Dashbords",
                                Text = "ReportsandDashbords"
                            },
                             new CardAction
                            {
                                Type = ActionTypes.MessageBack,
                                Title = "Order Entry Query",
                                Text = "OrderEntryQuery"
                            },
                              new CardAction
                            {
                                Type = ActionTypes.MessageBack,
                                Title = "Client AUM",
                                Text = "Client AUM"
                            }
                        }
            };

                await SendWelcomeCard(turnContext, card, cancellationToken);
        }
        private static async Task SendWelcomeCard(ITurnContext<IMessageActivity> turnContext, HeroCard card, CancellationToken cancellationToken)
        {
            var initialValue = new JObject { { "count", 0 } };
            card.Title = "Welcome To Edelwessis Bot!";
            card.Text = "Please tell me what do you want to know?";
            card.Buttons.Add(new CardAction
            {
                Type = ActionTypes.MessageBack,
                Title = "Update Card",
                Text = "UpdateCardAction",
                Value = initialValue
            });

            var activity = MessageFactory.Attachment(card.ToAttachment());

            await turnContext.SendActivityAsync(activity, cancellationToken);
        }

    }
}
