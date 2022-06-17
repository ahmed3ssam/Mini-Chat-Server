using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace Networks_Lab_assignment
{
    class Server
    {
        static void Main()
        {
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Any, 8000);
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            sock.Bind(ipEnd);

            sock.Listen(100);

            Console.WriteLine("Ahmed Essam Sayed Mohamed\nCyber Security Student\n");

            while (true)
            {
                Socket clientSock = sock.Accept();
                Console.WriteLine(">> Client Connection is on");

                Console.Write(">> Enter File Name, File type(.txt / .exe): ");
                string fileName = Console.ReadLine();

                Console.WriteLine(">> Sending File....");
                byte[] wholeFile = new byte[1000];
                byte[] fileNameByte = Encoding.ASCII.GetBytes(fileName);
                byte[] fileNameLength = BitConverter.GetBytes(fileNameByte.Length);
                byte[] fileData = File.ReadAllBytes(fileName);

                fileNameLength.CopyTo(wholeFile, 0);
                fileNameByte.CopyTo(wholeFile, 4);
                fileData.CopyTo(wholeFile, 4 + fileNameByte.Length);

                clientSock.Send(wholeFile);
                Console.WriteLine(">> File Sent....");

                clientSock.Shutdown(SocketShutdown.Both);
                clientSock.Close();
            }

        }
    }
}