using System;
using System.Diagnostics.Contracts;
using Newtonsoft.Json;
using ShitterTwitter.Common.Objects;

namespace ShitterTwitter
{
    [Serializable]
    public class IShitterTwitterMessage : Common.Objects.IShitterTwitterMessage
    {
        public string id { get; set; }
        public string Message { get; set; }
        public string DateAdded { get; set; }
        public string DateLastUsed { get; set; }
        public int MessageType { get; set; }
    }
}