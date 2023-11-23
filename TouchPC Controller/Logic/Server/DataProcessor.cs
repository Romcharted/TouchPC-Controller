using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using TouchPC_Controller.Logic.Enum;

namespace TouchPC_Controller.Logic.Server {

    /// <summary>
    /// Classe responsable du traitement des données reçues du client.
    /// </summary>
    public class DataProcessor {

        private int isWheel = 0;

        private ActionManager actionManager;

        /// <summary>
        /// Constructeur de la classe DataProcessor.
        /// </summary>
        public DataProcessor() {
            this.actionManager = new ActionManager();
        }

        /// <summary>
        /// Méthode pour traiter les données JSON reçues.
        /// </summary>
        /// <param name="receivedData">Les données JSON reçues du client.</param>
        public void ProcessReceivedData(string receivedData) {
            try {
                // Debug.WriteLine("Received JSON: " + receivedData); // Affiche le JSON reçu dans le débogage
                JObject jsonObject = JObject.Parse(receivedData);

                string action = jsonObject["action"].Value<string>();

                switch (action) {
                    case "MouseMove":
                        MouseMove(jsonObject);
                        break;
                    case "Scroll":
                        Scroll(jsonObject);
                        break;
                    case "LeftClick":
                        LeftClick();
                        break;
                    case "RightClick":
                        RightClick();
                        break;
                    case "DoubleLeftClick":
                        DoubleLeftClick();
                        break;
                    case "Keyboard":
                        KeyboardTextAction(jsonObject);
                        break;
                    case "MediaControl":
                        MediaControlAction(jsonObject);
                        break;
                }

            } catch (Exception ex) {
                Debug.WriteLine("Erreur lors du traitement des données JSON : " + ex.Message);
            }
        }

        /// <summary>
        /// Méthode pour traiter les données liées au mouvement de la souris.
        /// </summary>
        /// <param name="jsonObject">Objet JSON contenant les coordonnées du mouvement de la souris.</param>
        private void MouseMove(JObject jsonObject) {
            JObject coords = jsonObject["coords"] as JObject;

            int x = coords["x"].Value<int>();
            int y = coords["y"].Value<int>();

            this.actionManager.MouseMove(x, y);
        }

        /// <summary>
        /// Méthode pour traiter les données liées au défilement.
        /// </summary>
        /// <param name="jsonObject">Objet JSON contenant la direction du défilement.</param>
        private void Scroll(JObject jsonObject) {
            string direction = jsonObject["scroll"].Value<string>();

            Directions dir = Directions.Left;
            switch (direction) {
                case "right":
                    dir = Directions.Right;
                    break;
                case "up":
                    dir = Directions.Up;
                    break;
                case "down":
                    dir = Directions.Down;
                    break;
            }

            if (isWheel < 4) {
                isWheel++;
            } else {
                isWheel = 0;
                this.actionManager.Scroll(dir);
            }
        }

        /// <summary>
        /// Méthode pour traiter les données liées au clic gauche.
        /// </summary>
        private void LeftClick() {
            this.actionManager.LeftClick();
        }

        /// <summary>
        /// Méthode pour traiter les données liées au clic droit.
        /// </summary>
        private void RightClick() {
            this.actionManager.RightClick();
        }

        /// <summary>
        /// Méthode pour traiter les données liées au double clic gauche.
        /// </summary>
        private void DoubleLeftClick() {
            this.actionManager.DoubleLeftClick();
        }

        /// <summary>
        /// Méthode pour traiter les données liées à l'action du clavier.
        /// </summary>
        /// <param name="jsonObject">Objet JSON contenant la valeur associée à l'action du clavier.</param>
        private void KeyboardTextAction(JObject jsonObject) {

            string unicode = jsonObject["value"].Value<string>();
            this.actionManager.KeyboardTextAction(unicode);

        }

        /// <summary>
        /// Méthode pour traiter les données liées à l'action de contrôle multimédia.
        /// </summary>
        /// <param name="jsonObject">Objet JSON contenant la valeur associée à l'action de contrôle multimédia.</param>
        private void MediaControlAction(JObject jsonObject) {
            string action = jsonObject["value"].Value<string>();
            this.actionManager.MediaControlAction(action);
        }
    }
}
