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
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            server.Bind(iep);
            Console.WriteLine("Cho ket noi tu Client ");

            // Tao ra mot Endpoint tu xa de nhan du lieu ve 
            IPEndPoint RemoteEp = new IPEndPoint(IPAddress.Any, 0);
            EndPoint remote = (EndPoint)RemoteEp;

            byte[] data = new byte[1024];
            int recv = server.ReceiveFrom(data, ref remote);
            string s = Encoding.ASCII.GetString(data, 0, recv);

            Console.WriteLine("Nhan ve tu Client: {0}", s);
            data = Encoding.ASCII.GetBytes(" Chao ban den voi Server");
            server.SendTo(data, remote);

            while (true)
            {
                data = new byte[1024];
                recv = server.ReceiveFrom(data, ref remote);

                // Chuyen mang byte Data thanh chuoi va in ra man hinh 
                s = Encoding.ASCII.GetString(data, 0, recv);           
                Console.WriteLine("Client gui len: {0} ", s);

                // Neu chuoi nhan duoc la Quit thi thoat
                if (s.ToUpper().Equals("QUIT")) break;

                
                // Gui tra lai cho client chuoi s 
                data = new byte[1024];
                data = Encoding.ASCII.GetBytes(s);
                server.SendTo(data, 0, data.Length, SocketFlags.None, remote);
            }
            server.Close();
        }
    }
}
