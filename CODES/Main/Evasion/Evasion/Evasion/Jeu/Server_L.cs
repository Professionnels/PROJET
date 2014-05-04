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
    public class Server_L
    {
      /*  private TcpListener tcpListener;
        private Thread listenThread;

        public Server_L()
        {
            this.tcpListener = new TcpListener(IPAddress.Any, 3000);
            this.listenThread = new Thread(new ThreadStart(ListenForClients));
            this.listenThread.Start();
        }

        private void ListenForClients()
        {
            this.tcpListener.Start();

            while (true)
            {
                // bloque jusqu'à ce qu'un client s'est connecté au serveur
                TcpClient client = this.tcpListener.AcceptTcpClient();

                // création d'un thread pour gérer la communication avec le client connecté
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                Thread clientThreadSend = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clientThread.Start(client);
                clientThreadSend.Start(client);
            }
        }
        private void HandleClientComm(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            TcpClient sendClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();


            byte[] message = new byte[4096];
            int bytesRead;

            while (true)
            {
                bytesRead = 0;

                try
                {
                    // bloque jusqu'à ce qu'un client envoie un message
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch
                {
                    // une erreur de socket s'est produite
                    break;
                }

                if (bytesRead == 0)
                {
                    // le client s'est déconnecté du serveur
                    break;
                }

                // le message a bien été reçu
                ASCIIEncoding encoder = new ASCIIEncoding();
                System.Diagnostics.Debug.WriteLine(encoder.GetString(message, 0, bytesRead));

                Console.WriteLine("To: " + tcpClient.Client.LocalEndPoint);
                Console.WriteLine("From: " + tcpClient.Client.RemoteEndPoint);
                Console.WriteLine(encoder.GetString(message, 0, bytesRead));
                clientStream.Flush();
                sendToClient("Test back", sendClient);
            }

            tcpClient.Close();
        }
        public void sendToClient(String line, TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                ASCIIEncoding encoder = new ASCIIEncoding();
                byte[] buffer = encoder.GetBytes(line + "\0");

                stream.Write(buffer, 0, buffer.Length);
                stream.Flush();
            }
            catch
            {
                Console.WriteLine("Client Disconnected." + Environment.NewLine);

            }
        }

        public void broadcast(String line, TcpClient thisClient)
        {
            sendToClient(line, thisClient);
        }*/
    }
}