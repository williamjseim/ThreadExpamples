
namespace jakob
{
    static internal class MessageSender
    {
        static public void sendMessageToAll(string[] to, Message m, bool isHTML)
        {
            if (isHTML)
                m.Body = ConvertBodyToHTML(m.Body);
        }

        static public void sendMessage(Message m, bool isHTML)
        {
            if (isHTML)
                m.Body = ConvertBodyToHTML(m.Body);
        }

        static public string ConvertBodyToHTML(string plainText)
        {
            return "" + plainText + "";
        }
    }
}
