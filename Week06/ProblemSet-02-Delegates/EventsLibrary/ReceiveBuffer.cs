using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsLibrary
{
    public class ReceiveBufferEventArgs : EventArgs
    {
        private readonly string message;
        public string Message { get { return message; } }
        public ReceiveBufferEventArgs(string message)
        {
            this.message = message;
        }
    }

    public class ReceiveBuffer
    {
        private bool startedRecievingMessage;
        private ushort remainingMessageLenght;
        private LinkedList<byte> bytes;
        private Encoding messageEncoding;

        public event EventHandler MessageReceived;

        public ReceiveBuffer(EventHandler recievedMessageHandler)
        {
            startedRecievingMessage = false;
            remainingMessageLenght = 0;
            bytes = new LinkedList<byte>();
            messageEncoding = Encoding.UTF8;
            MessageReceived = recievedMessageHandler;
        }

        public void BytesReceived(byte[] data)
        {
            ushort startIndex = 0;
            if (!startedRecievingMessage)
            {
                remainingMessageLenght = BitConverter.ToUInt16(data, 0);
                startedRecievingMessage = true;
                startIndex = 2;
            }

            for (int i = startIndex; i < data.Length; i++)
            {
                bytes.AddLast(data[i]);
            }
            remainingMessageLenght -= (ushort)(data.Length - startIndex);

            if (remainingMessageLenght == 0)
            {
                startedRecievingMessage = false;
                MessageReceived(this, new ReceiveBufferEventArgs(messageEncoding.GetString(bytes.ToArray())));
                bytes = new LinkedList<byte>();
            }
        }
    }
}
