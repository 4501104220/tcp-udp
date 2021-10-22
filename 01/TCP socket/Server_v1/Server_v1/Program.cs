using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Server_v1

{
    class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2008);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(iep);
            server.Listen(10);
            Console.WriteLine("Cho ket noi tu Client ");

            Socket client = server.Accept();
            Console.WriteLine("Chap nhan ket noi tu: {0} ", client.RemoteEndPoint.ToString());

            string s = "Chao ban den voi Server ";
            // Chuyen chuoi s thanh mang byte 
            byte[] data = new byte[1024];
            data = Encoding.ASCII.GetBytes(s);

            // Gui nhan du lieu theo giao thuc da thiet ke 
            client.Send(data, data.Length, SocketFlags.None);

            while (true)
            {
                data = new byte[1024];
                int recv = client.Receive(data);
                if (recv == 0) break;

                // Chuyen mang byte Data thanh chuoi va in ra man hinh 
                s = Encoding.ASCII.GetString(data, 0, recv);
                Console.WriteLine("Client gui len: {0} ", s);

                // Neu chuoi nhan duoc la Quit thi thoat
                if (s.ToUpper().Equals("QUIT")) break;

                // Gui tra lai cho client chuoi s        
                data = new byte[1024];
                data = Encoding.ASCII.GetBytes(s);
                client.Send(data, data.Length, SocketFlags.None);
            }
            client.Shutdown(SocketShutdown.Both);
            client.Close();
            server.Close();
        }
    }
}
