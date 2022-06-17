using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Networks_Lab_assignment
{
    class Client
    {

        static void Main(string[] args)
        {
            IPAddress host = IPAddress.Parse("127.0.0.1");

            IPEndPoint hostEndpoint = new IPEndPoint(host, 8000);

            Socket clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            clientSock.Connect(hostEndpoint);

            byte[] receivedData = new byte[1024];
            int receivedBytesLen = clientSock.Receive(receivedData);
            int fileNameLength = BitConverter.ToInt32(receivedData, 0);
            String fileName = Encoding.ASCII.GetString(receivedData, 4, fileNameLength);
            BinaryWriter binaryWriter = new BinaryWriter(File.Open(fileName, FileMode.Create));
            binaryWriter.Write(receivedData, 4 + fileNameLength, receivedBytesLen);
           binaryWriter.Close();
            Console.Write(">>File Name : ");
            Console.WriteLine(fileName);
            Console.WriteLine(">>File Has been Recieved, Saved at 'Client Debug' Folder........");
            Console.ReadLine();
            clientSock.Shutdown(SocketShutdown.Both);
            clientSock.Close();

        }

    }
}