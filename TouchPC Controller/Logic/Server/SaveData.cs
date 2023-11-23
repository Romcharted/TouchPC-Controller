using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;

namespace TouchPC_Controller.Logic.Server {

    /// <summary>
    /// Classe statique pour la sauvegarde et le chargement des données de l'application TouchPC Controller.
    /// </summary>
    public static class SaveData {

        private static string saveFileName = "SaveData.json";

        /// <summary>
        /// Sauvegarde les données spécifiées dans un fichier JSON.
        /// </summary>
        /// <param name="data">Les données à sauvegarder.</param>
        public static void SaveDataTouchPC(Data data) {
            try {
                // Convertir les données en format JSON
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);

                // Permet d'obtenir le chemin complet du dossier de sauvegarde
                string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TouchPC Controller");

                // Créer le dossier s'il n'existe pas déjà
                Directory.CreateDirectory(folderPath);

                // Combinez le chemin complet du fichier de sauvegarde
                string filePath = Path.Combine(folderPath, saveFileName);

                // Écrire le JSON dans le fichier
                File.WriteAllText(filePath, json);
            } catch (Exception ex) {

                MessageBox.Show($"Erreur lors de la sauvegarde des données : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Charge les données sauvegardées depuis le fichier JSON.
        /// </summary>
        /// <returns>Les données chargées ou une nouvelle instance si le fichier n'existe pas.</returns>
        public static Data LoadSaveData() {
            try {
                // Obtenir le chemin complet du dossier de sauvegarde
                string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TouchPC Controller");

                // Combinez le chemin complet du fichier de sauvegarde
                string filePath = Path.Combine(folderPath, saveFileName);

                if (File.Exists(filePath)) {
                    // Lire le contenu JSON du fichier
                    string json = File.ReadAllText(filePath);

                    // Désérialiser le JSON en objets de données
                    return JsonConvert.DeserializeObject<Data>(json);
                } else {
                    // Retourner une nouvelle instance si le fichier n'existe pas
                    return new Data();
                }
            } catch (Exception ex) {
                MessageBox.Show($"Erreur lors du chargement des données : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return new Data();
            }
        }
    }
}
