
namespace jakob
{
    public enum MessageCarrier { Smtp, VMessage }
    internal class Message
    {
        string to, from, body, subject, cc;

        public Message(string to, string from, string body, string subject, string cc)
        {
            this.to = to;
            this.from = from;
            this.body = body;
            this.subject = subject;
            this.cc = cc;
        }

        public string To { get { return to; } set { to = value; } }
        public string From { get { return from; } set { from = value; } }
        public string Body { get { return body; } set { body = value; } }
        public string Subject { get { return subject; } set { subject = value; } }
        public string Cc { get { return cc; } set { cc = value; } }
    }
}
