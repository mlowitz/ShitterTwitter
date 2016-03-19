﻿using System;
using System.Diagnostics.Contracts;
using ShitterTwitter.Common.Objects;

namespace ShitterTwitter
{
    [Serializable]
    public class ShitterTwitterMessage : IShitterTwitterMessage
    {
        public string id { get; set; }
        public string Message { get; set; }
        public string DateAdded { get; set; }
        public string DateLastUsed { get; set; }
        public int MessageType { get; set; }
    }
}