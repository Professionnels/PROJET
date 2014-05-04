using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text;
namespace Evasion.Jeu
{
    public class Client_L
    {
        TcpClient _tcpClient;

        // Le client se connecte à un serveur à l'adresse IP et au port indiqués.
        public void Connect(IPAddress address, int port)
        {
            _tcpClient = new TcpClient();

            IPEndPoint serverEndPoint =
               new IPEndPoint(address, port);

            _tcpClient.Connect(serverEndPoint);

            // Création d'un fil pour lire les données envoyées par le serveur.
            ThreadPool.QueueUserWorkItem(
               delegate
               {
                   Read();
               });
        }

        // Envoie des octets sur le serveur.
        public void Send(byte[] buffer)
        {
            _tcpClient.GetStream().Write(
               buffer, 0, buffer.Length);

            _tcpClient.GetStream().Flush();
        }

        private void Read()
        {
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] buffer = new byte[4096];
            int bytesRead;
            while (true)
            {
                bytesRead =
                   _tcpClient.GetStream().Read(buffer, 0, 4096);
                Console.WriteLine(encoder.GetString(buffer, 0, bytesRead));
            }
        }

        public void Dispose()
        {
            _tcpClient.Close();
        }
    }
}
