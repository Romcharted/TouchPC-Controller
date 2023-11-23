namespace TouchPC_Controller.Logic.Server {
    /// <summary>
    /// Classe qui contient les différents paramètres pour le serveur
    /// </summary>
    public class ServerConfig {
        // Port d'écoute du serveur UDP
        public int UdpPort { get; set; }

        private static ServerConfig instance;

        /// <summary>
        /// Constructeur privé pour empêcher l'instanciation directe
        /// </summary>
        private ServerConfig() {
            UdpPort = SaveData.LoadSaveData().UdpPort;
        }

        /// <summary>
        /// Méthode pour obtenir l'instance unique de ServerConfig
        /// </summary>
        public static ServerConfig Instance {
            get {
                if (instance == null) {
                    instance = new ServerConfig();
                }
                return instance;
            }
        }

        public void SaveDataServer() {
            Data data = new Data();
            data.UdpPort = Instance.UdpPort;

            SaveData.SaveDataTouchPC(data);
        }
    }
}
