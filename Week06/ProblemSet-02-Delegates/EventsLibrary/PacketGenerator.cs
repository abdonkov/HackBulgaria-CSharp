using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsLibrary
{
    public class PacketGenerator
    {
        private Encoding messageEncoding;
        private Random rand;
        private ReceiveBuffer receiveBuffer;

        public PacketGenerator(ReceiveBuffer receiveBuffer)
        {
            messageEncoding = Encoding.UTF8;
            rand = new Random();
            this.receiveBuffer = receiveBuffer;
        }

        public void SendMessage(string message)
        {
            byte[] messageBytes = messageEncoding.GetBytes(message);
            ushort length = (ushort)messageBytes.Length;
            byte[] lengthBytes = BitConverter.GetBytes(length);

            byte[] messagePackage = new byte[length + 2];
            messagePackage[0] = lengthBytes[0];
            messagePackage[1] = lengthBytes[1];
            for (int i = 0; i < messageBytes.Length; i++)
            {
                messagePackage[i + 2] = messageBytes[i];
            }

            int fullMessageLength = messagePackage.Length;
            if (fullMessageLength > 8)
            {
                int maxBytesInChunk = fullMessageLength / 3;
                int chunkStartIndex = 0;
                int chunkEndIndex = 0;
                while (chunkEndIndex < fullMessageLength - 1)
                {
                    int numberOfBytesInChunk = rand.Next(3, maxBytesInChunk);
                    chunkEndIndex = chunkStartIndex + numberOfBytesInChunk - 1;
                    if (chunkEndIndex > messagePackage.Length - 1)
                    {
                        chunkEndIndex = messagePackage.Length - 1;
                        numberOfBytesInChunk = chunkEndIndex - chunkStartIndex + 1;
                    }
                    byte[] curPackage = new byte[numberOfBytesInChunk];
                    for (int i = 0; i < numberOfBytesInChunk; i++)
                    {
                        curPackage[i] = messagePackage[chunkStartIndex + i];
                    }
                    chunkStartIndex = chunkEndIndex + 1;
                    receiveBuffer.BytesReceived(curPackage);

                }
            }
            else if (fullMessageLength > 5)
            {
                int middle = fullMessageLength / 2;
                byte[] curPackage = new byte[middle];
                for (int i = 0; i < middle; i++)
                {
                    curPackage[i] = messagePackage[i];
                }
                receiveBuffer.BytesReceived(curPackage);
                byte[] curPackage2 = new byte[fullMessageLength - middle];
                for (int i = 0; i < fullMessageLength - middle; i++)
                {
                    curPackage2[i] = messagePackage[middle + i];
                }
                receiveBuffer.BytesReceived(curPackage2);
            }
            else
            {
                receiveBuffer.BytesReceived(messagePackage);
            }
        }
    }
}
