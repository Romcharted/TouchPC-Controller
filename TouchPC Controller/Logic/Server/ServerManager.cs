using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TouchPC_Controller.Logic.Server {

    /// <summary>
    /// Gestionnaire du serveur UDP pour la réception et le traitement des données.
    /// </summary>
    public class ServerManager {
        private UdpClient udpServer;

        private DataProcessor dataProcessor;

        /// <summary>
        /// Initialise une nouvelle instance du gestionnaire de serveur.
        /// </summary>
        public ServerManager() {
            dataProcessor = new DataProcessor();
        }

        /// <summary>
        /// Lance le serveur UDP en écoutant sur le port spécifié dans la configuration.
        /// </summary>
        public void LaunchServer() {
            udpServer = new UdpClient(ServerConfig.Instance.UdpPort);
            Debug.WriteLine("Serveur UDP en écoute sur le port " + ServerConfig.Instance.UdpPort);
        }

        /// <summary>
        /// Reçoit et traite continuellement les données entrantes.
        /// </summary>
        public void ReceiveAndProcessData() {
            try {
                while (true) {
                    string receivedData = this.ReceiveData();

                    if (receivedData != null) {
                        dataProcessor.ProcessReceivedData(receivedData);
                    }
                }
            } catch (Exception ex) {
                Debug.WriteLine("Erreur lors de la réception de données : " + ex.Message);
            }
        }

        /// <summary>
        /// Reçoit les données de la socket UDP et les retourne sous forme de chaîne UTF-8.
        /// </summary>
        /// <returns>Les données reçues sous forme de chaîne.</returns>
        public string ReceiveData() {
            try {
                IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] receivedBytes = udpServer.Receive(ref clientEndPoint);
                return Encoding.UTF8.GetString(receivedBytes);
            } catch (SocketException ex) {
                Debug.WriteLine("Erreur de réception : " + ex.Message);
                return "";
            }
        }

        /// <summary>
        /// Arrête le serveur UDP en fermant la socket.
        /// </summary>
        public void StopServer() {
            udpServer.Close();
        }
    }
}
