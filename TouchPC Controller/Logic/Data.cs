namespace TouchPC_Controller.Logic {

    /// <summary>
    /// Classe qui va etre sauvegardé
    /// </summary>
    public class Data {

        /// <summary>
        /// Le port UDP
        /// </summary>
        public int UdpPort { get; set; }

        /// <summary>
        /// Constructeur de Data
        /// </summary>
        public Data() {
            UdpPort = 12345;
        }
    }
}
