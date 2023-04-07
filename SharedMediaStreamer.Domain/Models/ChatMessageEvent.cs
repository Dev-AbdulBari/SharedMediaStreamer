namespace SharedMediaStreamer.Domain.Models
{
    public class ChatMessageEvent
    {
        public string User {get;set;}
        public string Message {get;set;}
        public MessageEventType MessageEventType {get;set;}
    }
}
