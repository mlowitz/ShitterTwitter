namespace ShitterTwitter.Common.Objects
{
    
    public interface IShitterTwitterMessage
    {
        string id { get; set; }
        string Message { get; set; }

        string DateAdded { get; set; }

        string DateLastUsed { get; set; }

        int MessageType { get; set; }

        }
}