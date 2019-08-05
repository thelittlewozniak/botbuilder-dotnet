﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Xml.Serialization;

namespace Microsoft.Bot.Builder.Adapters.WeChat.Schema.Request.Event.Common
{
    /// <summary>
    /// Enter a conversation.
    /// </summary>
    [XmlRoot("xml")]
    public class EnterEvent : RequestEvent
    {
        /// <summary>
        /// Gets event, EventType: ENTER.
        /// </summary>
        /// <value>
        /// EventType: ENTER.
        /// </value>
        public override string Event
        {
            get { return EventType.Enter; }
        }
    }
}
