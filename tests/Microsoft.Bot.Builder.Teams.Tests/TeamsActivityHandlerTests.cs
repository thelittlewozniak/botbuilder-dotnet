﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Teams;
using Microsoft.Bot.Schema;
using Microsoft.Bot.Schema.Teams;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace Microsoft.Bot.Builder.Tests
{
    [TestClass]
    public class TeamsActivityHandlerTests
    {
        [TestMethod]
        public async Task TestMessageActivity()
        {
            // Arrange
            var activity = MessageFactory.Text("hello");
            var turnContext = new TurnContext(new NotImplementedAdapter(), activity);

            // Act
            var bot = new TestTeamsActivityHandler();
            await ((IBot)bot).OnTurnAsync(turnContext);

            // Assert
            Assert.AreEqual(1, bot.Record.Count);
            Assert.AreEqual("OnMessageActivityAsync", bot.Record[0]);
        }

        [TestMethod]
        public async Task TestMemberAdded1()
        {
            // Arrange
            var activity = new Activity
            {
                Type = ActivityTypes.ConversationUpdate,
                MembersAdded = new List<ChannelAccount>
                {
                    new ChannelAccount { Id = "b" },
                },
                Recipient = new ChannelAccount { Id = "b" },
            };
            var turnContext = new TurnContext(new NotImplementedAdapter(), activity);

            // Act
            var bot = new TestTeamsActivityHandler();
            await ((IBot)bot).OnTurnAsync(turnContext);

            // Assert
            Assert.AreEqual(1, bot.Record.Count);
            Assert.AreEqual("OnConversationUpdateActivityAsync", bot.Record[0]);
        }

        [TestMethod]
        public async Task TestMemberAdded2()
        {
            // Arrange
            var activity = new Activity
            {
                Type = ActivityTypes.ConversationUpdate,
                MembersAdded = new List<ChannelAccount>
                {
                    new ChannelAccount { Id = "a" },
                    new ChannelAccount { Id = "b" },
                },
                Recipient = new ChannelAccount { Id = "b" },
            };
            var turnContext = new TurnContext(new NotImplementedAdapter(), activity);

            // Act
            var bot = new TestTeamsActivityHandler();
            await ((IBot)bot).OnTurnAsync(turnContext);

            // Assert
            Assert.AreEqual(2, bot.Record.Count);
            Assert.AreEqual("OnConversationUpdateActivityAsync", bot.Record[0]);
            Assert.AreEqual("OnMembersAddedAsync", bot.Record[1]);
        }

        [TestMethod]
        public async Task TestMemberAdded3()
        {
            // Arrange
            var activity = new Activity
            {
                Type = ActivityTypes.ConversationUpdate,
                MembersAdded = new List<ChannelAccount>
                {
                    new ChannelAccount { Id = "a" },
                    new ChannelAccount { Id = "b" },
                    new ChannelAccount { Id = "c" },
                },
                Recipient = new ChannelAccount { Id = "b" },
            };
            var turnContext = new TurnContext(new NotImplementedAdapter(), activity);

            // Act
            var bot = new TestTeamsActivityHandler();
            await ((IBot)bot).OnTurnAsync(turnContext);

            // Assert
            Assert.AreEqual(2, bot.Record.Count);
            Assert.AreEqual("OnConversationUpdateActivityAsync", bot.Record[0]);
            Assert.AreEqual("OnMembersAddedAsync", bot.Record[1]);
        }

        [TestMethod]
        public async Task TestMemberRemoved1()
        {
            // Arrange
            var activity = new Activity
            {
                Type = ActivityTypes.ConversationUpdate,
                MembersAdded = new List<ChannelAccount>
                {
                    new ChannelAccount { Id = "c" },
                },
                Recipient = new ChannelAccount { Id = "c" },
            };
            var turnContext = new TurnContext(new NotImplementedAdapter(), activity);

            // Act
            var bot = new TestTeamsActivityHandler();
            await ((IBot)bot).OnTurnAsync(turnContext);

            // Assert
            Assert.AreEqual(1, bot.Record.Count);
            Assert.AreEqual("OnConversationUpdateActivityAsync", bot.Record[0]);
        }

        [TestMethod]
        public async Task TestMemberRemoved2()
        {
            // Arrange
            var activity = new Activity
            {
                Type = ActivityTypes.ConversationUpdate,
                MembersAdded = new List<ChannelAccount>
                {
                    new ChannelAccount { Id = "a" },
                    new ChannelAccount { Id = "c" },
                },
                Recipient = new ChannelAccount { Id = "c" },
            };
            var turnContext = new TurnContext(new NotImplementedAdapter(), activity);

            // Act
            var bot = new TestTeamsActivityHandler();
            await ((IBot)bot).OnTurnAsync(turnContext);

            // Assert
            Assert.AreEqual(2, bot.Record.Count);
            Assert.AreEqual("OnConversationUpdateActivityAsync", bot.Record[0]);
            Assert.AreEqual("OnMembersAddedAsync", bot.Record[1]);
        }

        [TestMethod]
        public async Task TestMemberRemoved3()
        {
            // Arrange
            var activity = new Activity
            {
                Type = ActivityTypes.ConversationUpdate,
                MembersAdded = new List<ChannelAccount>
                {
                    new ChannelAccount { Id = "a" },
                    new ChannelAccount { Id = "b" },
                    new ChannelAccount { Id = "c" },
                },
                Recipient = new ChannelAccount { Id = "c" },
            };
            var turnContext = new TurnContext(new NotImplementedAdapter(), activity);

            // Act
            var bot = new TestTeamsActivityHandler();
            await ((IBot)bot).OnTurnAsync(turnContext);

            // Assert
            Assert.AreEqual(2, bot.Record.Count);
            Assert.AreEqual("OnConversationUpdateActivityAsync", bot.Record[0]);
            Assert.AreEqual("OnMembersAddedAsync", bot.Record[1]);
        }

        [TestMethod]
        public async Task TestMemberAddedJustTheBot()
        {
            // Arrange
            var activity = new Activity
            {
                Type = ActivityTypes.ConversationUpdate,
                MembersAdded = new List<ChannelAccount>
                {
                    new ChannelAccount { Id = "b" },
                },
                Recipient = new ChannelAccount { Id = "b" },
            };
            var turnContext = new TurnContext(new NotImplementedAdapter(), activity);

            // Act
            var bot = new TestTeamsActivityHandler();
            await ((IBot)bot).OnTurnAsync(turnContext);

            // Assert
            Assert.AreEqual(1, bot.Record.Count);
            Assert.AreEqual("OnConversationUpdateActivityAsync", bot.Record[0]);
        }

        [TestMethod]
        public async Task TestMemberRemovedJustTheBot()
        {
            // Arrange
            var activity = new Activity
            {
                Type = ActivityTypes.ConversationUpdate,
                MembersAdded = new List<ChannelAccount>
                {
                    new ChannelAccount { Id = "c" },
                },
                Recipient = new ChannelAccount { Id = "c" },
            };
            var turnContext = new TurnContext(new NotImplementedAdapter(), activity);

            // Act
            var bot = new TestTeamsActivityHandler();
            await ((IBot)bot).OnTurnAsync(turnContext);

            // Assert
            Assert.AreEqual(1, bot.Record.Count);
            Assert.AreEqual("OnConversationUpdateActivityAsync", bot.Record[0]);
        }

        [TestMethod]
        public async Task TestMessageReaction()
        {
            // Note the code supports multiple adds and removes in the same activity though
            // a channel may decide to send separate activities for each. For example, Teams
            // sends separate activities each with a single add and a single remove.

            // Arrange
            var activity = new Activity
            {
                Type = ActivityTypes.MessageReaction,
                ReactionsAdded = new List<MessageReaction>
                {
                    new MessageReaction("sad"),
                },
                ReactionsRemoved = new List<MessageReaction>
                {
                    new MessageReaction("angry"),
                },
            };
            var turnContext = new TurnContext(new NotImplementedAdapter(), activity);

            // Act
            var bot = new TestTeamsActivityHandler();
            await ((IBot)bot).OnTurnAsync(turnContext);

            // Assert
            Assert.AreEqual(3, bot.Record.Count);
            Assert.AreEqual("OnMessageReactionActivityAsync", bot.Record[0]);
            Assert.AreEqual("OnReactionsAddedAsync", bot.Record[1]);
            Assert.AreEqual("OnReactionsRemovedAsync", bot.Record[2]);
        }

        [TestMethod]
        public async Task TestTokenResponseEventAsync()
        {
            // Arrange
            var activity = new Activity
            {
                Type = ActivityTypes.Event,
                Name = "tokens/response",
            };
            var turnContext = new TurnContext(new NotImplementedAdapter(), activity);

            // Act
            var bot = new TestTeamsActivityHandler();
            await ((IBot)bot).OnTurnAsync(turnContext);

            // Assert
            Assert.AreEqual(2, bot.Record.Count);
            Assert.AreEqual("OnEventActivityAsync", bot.Record[0]);
            Assert.AreEqual("OnTokenResponseEventAsync", bot.Record[1]);
        }

        [TestMethod]
        public async Task TestEventAsync()
        {
            // Arrange
            var activity = new Activity
            {
                Type = ActivityTypes.Event,
                Name = "some.random.event",
            };
            var turnContext = new TurnContext(new NotImplementedAdapter(), activity);

            // Act
            var bot = new TestTeamsActivityHandler();
            await ((IBot)bot).OnTurnAsync(turnContext);

            // Assert
            Assert.AreEqual(2, bot.Record.Count);
            Assert.AreEqual("OnEventActivityAsync", bot.Record[0]);
            Assert.AreEqual("OnEventAsync", bot.Record[1]);
        }

        [TestMethod]
        public async Task TestEventNullNameAsync()
        {
            // Arrange
            var activity = new Activity
            {
                Type = ActivityTypes.Event,
            };
            var turnContext = new TurnContext(new NotImplementedAdapter(), activity);

            // Act
            var bot = new TestTeamsActivityHandler();
            await ((IBot)bot).OnTurnAsync(turnContext);

            // Assert
            Assert.AreEqual(2, bot.Record.Count);
            Assert.AreEqual("OnEventActivityAsync", bot.Record[0]);
            Assert.AreEqual("OnEventAsync", bot.Record[1]);
        }

        [TestMethod]
        public async Task TestUnrecognizedActivityType()
        {
            // Arrange
            var activity = new Activity
            {
                Type = "shall.not.pass",
            };
            var turnContext = new TurnContext(new NotImplementedAdapter(), activity);

            // Act
            var bot = new TestTeamsActivityHandler();
            await ((IBot)bot).OnTurnAsync(turnContext);

            // Assert
            Assert.AreEqual(1, bot.Record.Count);
            Assert.AreEqual("OnUnrecognizedActivityTypeAsync", bot.Record[0]);
        }

        private class NotImplementedAdapter : BotAdapter
        {
            public override Task DeleteActivityAsync(ITurnContext turnContext, ConversationReference reference, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public override Task<ResourceResponse[]> SendActivitiesAsync(ITurnContext turnContext, Activity[] activities, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public override Task<ResourceResponse> UpdateActivityAsync(ITurnContext turnContext, Activity activity, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

        private class TestTeamsActivityHandler : TeamsActivityHandler
        {
            public List<string> Record { get; } = new List<string>();

            // ConversationUpdate
            protected override Task OnConversationUpdateActivityAsync(ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
            {
                Record.Add(MethodBase.GetCurrentMethod().Name);
                return base.OnConversationUpdateActivityAsync(turnContext, cancellationToken);
            }

            protected override Task OnChannelCreatedEventAsync(ChannelInfo channelInfo, TeamInfo teamInfo, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
            {
                Record.Add(MethodBase.GetCurrentMethod().Name);
                return base.OnChannelCreatedEventAsync(channelInfo, teamInfo, turnContext, cancellationToken);
            }

            protected override Task OnChannelDeletedEventAsync(ChannelInfo channelInfo, TeamInfo teamInfo, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
            {
                Record.Add(MethodBase.GetCurrentMethod().Name);
                return base.OnChannelDeletedEventAsync(channelInfo, teamInfo, turnContext, cancellationToken);
            }

            protected override Task OnChannelRenamedEventAsync(ChannelInfo channelInfo, TeamInfo teamInfo, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
            {
                Record.Add(MethodBase.GetCurrentMethod().Name);
                return base.OnChannelRenamedEventAsync(channelInfo, teamInfo, turnContext, cancellationToken);
            }

            protected override Task OnTeamRenamedEventAsync(TeamInfo teamInfo, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
            {
                Record.Add(MethodBase.GetCurrentMethod().Name);
                return base.OnTeamRenamedEventAsync(teamInfo, turnContext, cancellationToken);
            }

            protected override Task OnTeamMembersAddedEventAsync(IList<ChannelAccount> membersAdded, TeamInfo teamInfo, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
            {
                Record.Add(MethodBase.GetCurrentMethod().Name);
                return base.OnTeamMembersAddedEventAsync(membersAdded, teamInfo, turnContext, cancellationToken);
            }

            protected override Task OnTeamMembersRemovedEventAsync(IList<ChannelAccount> membersRemoved, TeamInfo teamInfo, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
            {
                Record.Add(MethodBase.GetCurrentMethod().Name);
                return base.OnTeamMembersRemovedEventAsync(membersRemoved, teamInfo, turnContext, cancellationToken);
            }

            protected override Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
            {
                Record.Add(MethodBase.GetCurrentMethod().Name);
                return base.OnMembersAddedAsync(membersAdded, turnContext, cancellationToken);
            }

            protected override Task OnMembersRemovedAsync(IList<ChannelAccount> membersRemoved, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
            {
                Record.Add(MethodBase.GetCurrentMethod().Name);
                return base.OnMembersRemovedAsync(membersRemoved, turnContext, cancellationToken);
            }

            // Invoke
            protected override Task<InvokeResponse> OnInvokeActivityAsync(ITurnContext<IInvokeActivity> turnContext, CancellationToken cancellationToken)
            {
                Record.Add(MethodBase.GetCurrentMethod().Name);
                return base.OnInvokeActivityAsync(turnContext, cancellationToken);
            }

            protected override Task<InvokeResponse> OnTeamsFileConsentAsync(ITurnContext<IInvokeActivity> turnContext, FileConsentCardResponse fileConsentCardResponse, CancellationToken cancellationToken)
            {
                Record.Add(MethodBase.GetCurrentMethod().Name);
                return base.OnTeamsFileConsentAsync(turnContext, fileConsentCardResponse, cancellationToken);
            }

            protected override Task OnTeamsFileConsentAcceptAsync(ITurnContext<IInvokeActivity> turnContext, FileConsentCardResponse fileConsentCardResponse, CancellationToken cancellationToken)
            {
                Record.Add(MethodBase.GetCurrentMethod().Name);
                return base.OnTeamsFileConsentAcceptAsync(turnContext, fileConsentCardResponse, cancellationToken);
            }

            protected override Task OnTeamsFileConsentDeclineAsync(ITurnContext<IInvokeActivity> turnContext, FileConsentCardResponse fileConsentCardResponse, CancellationToken cancellationToken)
            {
                Record.Add(MethodBase.GetCurrentMethod().Name);
                return base.OnTeamsFileConsentDeclineAsync(turnContext, fileConsentCardResponse, cancellationToken);
            }

            protected override Task<MessagingExtensionActionResponse> OnTeamsMessagingExtensionBotMessagePreviewEditAsync(ITurnContext<IInvokeActivity> turnContext, MessagingExtensionAction query, CancellationToken cancellationToken)
            {
                Record.Add(MethodBase.GetCurrentMethod().Name);
                return base.OnTeamsMessagingExtensionBotMessagePreviewEditAsync(turnContext, query, cancellationToken);
            }

            protected override Task<MessagingExtensionActionResponse> OnTeamsMessagingExtensionBotMessagePreviewSendAsync(ITurnContext<IInvokeActivity> turnContext, MessagingExtensionAction query, CancellationToken cancellationToken)
            {
                Record.Add(MethodBase.GetCurrentMethod().Name);
                return base.OnTeamsMessagingExtensionBotMessagePreviewSendAsync(turnContext, query, cancellationToken);
            }

            protected override Task<MessagingExtensionResponse> OnTeamsMessagingExtensionConfigurationSettingsAsync(ITurnContext<IInvokeActivity> turnContext, CancellationToken cancellationToken)
            {
                Record.Add(MethodBase.GetCurrentMethod().Name);
                return base.OnTeamsMessagingExtensionConfigurationSettingsAsync(turnContext, cancellationToken);
            }

            protected override Task<MessagingExtensionActionResponse> OnTeamsMessagingExtensionFetchTaskAsync(ITurnContext<IInvokeActivity> turnContext, CancellationToken cancellationToken)
            {
                Record.Add(MethodBase.GetCurrentMethod().Name);
                return base.OnTeamsMessagingExtensionFetchTaskAsync(turnContext, cancellationToken);
            }

            protected override Task<MessagingExtensionResponse> OnTeamsMessagingExtensionConfigurationSettingsUrlAsync(ITurnContext<IInvokeActivity> turnContext, CancellationToken cancellationToken)
            {
                Record.Add(MethodBase.GetCurrentMethod().Name);
                return base.OnTeamsMessagingExtensionConfigurationSettingsUrlAsync(turnContext, cancellationToken);
            }

            protected override Task<MessagingExtensionResponse> OnTeamsMessagingExtensionQueryAsync(ITurnContext<IInvokeActivity> turnContext, MessagingExtensionQuery query, CancellationToken cancellationToken)
            {
                Record.Add(MethodBase.GetCurrentMethod().Name);
                return base.OnTeamsMessagingExtensionQueryAsync(turnContext, query, cancellationToken);
            }

            protected override Task<MessagingExtensionResponse> OnTeamsMessagingExtensionSelectItemAsync(ITurnContext<IInvokeActivity> turnContext, JObject query, CancellationToken cancellationToken)
            {
                Record.Add(MethodBase.GetCurrentMethod().Name);
                return base.OnTeamsMessagingExtensionSelectItemAsync(turnContext, query, cancellationToken);
            }

            protected override Task<MessagingExtensionActionResponse> OnTeamsMessagingExtensionSubmitActionAsync(ITurnContext<IInvokeActivity> turnContext, MessagingExtensionAction query, CancellationToken cancellationToken)
            {
                Record.Add(MethodBase.GetCurrentMethod().Name);
                return base.OnTeamsMessagingExtensionSubmitActionAsync(turnContext, query, cancellationToken);
            }

            protected override Task<MessagingExtensionActionResponse> OnTeamsMessagingExtensionSubmitActionDispatchAsync(ITurnContext<IInvokeActivity> turnContext, MessagingExtensionAction query, CancellationToken cancellationToken)
            {
                Record.Add(MethodBase.GetCurrentMethod().Name);
                return base.OnTeamsMessagingExtensionSubmitActionDispatchAsync(turnContext, query, cancellationToken);
            }

            protected override Task<MessagingExtensionResponse> OnTeamsAppBasedLinkQueryAsync(ITurnContext<IInvokeActivity> turnContext, AppBasedLinkQuery query, CancellationToken cancellationToken)
            {
                Record.Add(MethodBase.GetCurrentMethod().Name);
                return base.OnTeamsAppBasedLinkQueryAsync(turnContext, query, cancellationToken);
            }

            protected override Task<InvokeResponse> OnTeamsCardActionInvokeAsync(ITurnContext<IInvokeActivity> turnContext, CancellationToken cancellationToken)
            {
                Record.Add(MethodBase.GetCurrentMethod().Name);
                return base.OnTeamsCardActionInvokeAsync(turnContext, cancellationToken);
            }

            protected override Task OnTeamsO365ConnectorCardActionAsync(ITurnContext<IInvokeActivity> turnContext, O365ConnectorCardActionQuery query, CancellationToken cancellationToken)
            {
                Record.Add(MethodBase.GetCurrentMethod().Name);
                return base.OnTeamsO365ConnectorCardActionAsync(turnContext, query, cancellationToken);
            }

            protected override Task<TaskModuleResponse> OnTeamsTaskModuleFetchAsync(ITurnContext<IInvokeActivity> turnContext, CancellationToken cancellationToken)
            {
                Record.Add(MethodBase.GetCurrentMethod().Name);
                return base.OnTeamsTaskModuleFetchAsync(turnContext, cancellationToken);
            }

            protected override Task<TaskModuleResponse> OnTeamsTaskModuleSubmitAsync(ITurnContext<IInvokeActivity> turnContext, CancellationToken cancellationToken)
            {
                Record.Add(MethodBase.GetCurrentMethod().Name);
                return base.OnTeamsTaskModuleSubmitAsync(turnContext, cancellationToken);
            }
        }
    }
}
