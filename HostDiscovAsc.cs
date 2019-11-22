using System;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.ComponentModel;
using System.Threading;
using System.Collections.Generic;

namespace System
{
    public class HostDiscovAsc
    {
        public static List<string> cevapVerenIp = new List<string>();
        public static void Main(string[] args)
        {
            StringBuilder sp = new StringBuilder("192.168.1.");
            int i;
            for (i = 0; i <= 255; i++)
            {
                sp.Append(i);
                string ip = sp.ToString();
                AutoResetEvent ResBek = new AutoResetEvent(false);
                Ping pingAt = new Ping();
                pingAt.PingCompleted += new PingCompletedEventHandler(PingBitis);
                string data = "karsikidaglarcendermecendermeeee";
                byte[] tampon = Encoding.ASCII.GetBytes(data);
                int kesim = 1000;
                PingOptions PingAyar = new PingOptions(64, true);

                pingAt.SendAsync(ip, kesim, tampon, PingAyar, ResBek);

                //Console.Write("\r{0} Scanning ", i);
                //ResBek.WaitOne();
                if (i <= 9)
                {
                    sp.Remove(10, 1);
                }
                else if (i > 9 && i <= 99)
                {
                    sp.Remove(10, 2);
                }
                else if (i > 99 && i <= 255)
                {
                    sp.Remove(10, 3);
                }
            }
            for (int j = 0; j < cevapVerenIp.Count; j++)
            {
                Console.WriteLine(cevapVerenIp[j]);

            }
            //Console.WriteLine("Scaning completed. Press enter for exit!");
            Console.ReadLine();
            //Environment.Exit(0);

        }

        private static void PingBitis(object sender, PingCompletedEventArgs e)
        {
            // If the operation was canceled, display a message to the user.
            if (e.Cancelled)
            {
                Console.WriteLine("Ping Addaya gitti.");
                ((AutoResetEvent)e.UserState).Set();
            }

            if (e.Error != null)
            {
                Console.WriteLine("Acayip isler oluyor necati:");
                Console.WriteLine(e.Error.ToString());

                ((AutoResetEvent)e.UserState).Set();
            }

            PingReply reply = e.Reply;

            Donus(reply);

            ((AutoResetEvent)e.UserState).Set();
        }

        public static void Donus(PingReply reply)
        {
            if (reply == null) { return; }

            if (reply.Status == IPStatus.Success)
            {
                //Console.WriteLine("Address: {0} up {1} ms", reply.Address.ToString(), reply.RoundtripTime.ToString());
                cevapVerenIp.Add(reply.Address.ToString());
            }
        }
    }
}