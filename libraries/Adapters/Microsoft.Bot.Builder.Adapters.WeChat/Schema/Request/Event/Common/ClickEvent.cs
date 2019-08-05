﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Xml.Serialization;

namespace Microsoft.Bot.Builder.Adapters.WeChat.Schema.Request.Event.Common
{
    /// <summary>
    /// Click.
    /// </summary>
    [XmlRoot("xml")]
    public class ClickEvent : RequestEventWithEventKey
    {
        /// <summary>
        /// Gets Event, eventType: CLICK.
        /// </summary>
        /// <value>
        /// Event type click.
        /// </value>
        public override string Event
        {
            get { return EventType.Click; }
        }
    }
}
