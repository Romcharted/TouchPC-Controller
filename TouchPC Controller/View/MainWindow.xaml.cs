using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using TouchPC_Controller.Logic.Server;

namespace TouchPC_Controller {

    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : Window {

        private ServerManager serverManager;
        private const int PortMin = 1024;
        private const int PortMax = 65535;

        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenu;

        /// <summary>
        /// Initialisation de Main Widow et du serveur
        /// </summary>
        public MainWindow() {
            InitializeComponent();

            txtPort.Text = ServerConfig.Instance.UdpPort.ToString();

            serverManager = new ServerManager();
            serverManager.LaunchServer();
            ReceiveData();

            // Initialisation de l'icône de notification
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = new System.Drawing.Icon("Ressources\\logoFavicon.ico");
            notifyIcon.Visible = false;

            // Créer le menu contextuel
            contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Ouvrir l'application", null, OpenApplication_Click);
            contextMenu.Items.Add("Quitter l'application", null, QuitApplication_Click);

            // Associer le menu contextuel à l'icône
            notifyIcon.ContextMenuStrip = contextMenu;

            // Événement pour restaurer l'application lorsque l'icône est double-cliquée
            notifyIcon.MouseDoubleClick += NotifyIcon_MouseDoubleClick;

            // Événement pour cacher l'application dans la zone de notification lorsque la fenêtre est minimisée
            this.StateChanged += Window_StateChanged;
            this.Closed += Window_Closed;
        }

        private void NotifyIcon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e) {
            // Restaure l'application lorsque l'icône est double-cliquée
            this.Show();
            this.WindowState = WindowState.Normal;
        }

        private void OpenApplication_Click(object sender, EventArgs e) {
            // Ouvrir l'application depuis le menu contextuel
            this.Show();
            this.WindowState = WindowState.Normal;
        }

        private void QuitApplication_Click(object sender, EventArgs e) {
            // Quitte l'application depuis le menu contextuel
            this.Close();
        }

        private void Window_StateChanged(object sender, EventArgs e) {
            // Cache l'application dans la zone de notification lorsque la fenêtre est minimisée
            if (this.WindowState == WindowState.Minimized) {
                this.Hide();
                notifyIcon.Visible = true;
            }
        }

        private void Window_Closed(object sender, EventArgs e) {
            notifyIcon.Dispose();
        }

        /// <summary>
        /// Tâche asynchrone pour la réception de données
        /// </summary>
        private async void ReceiveData() {
            await Task.Run(() => serverManager.ReceiveAndProcessData());
        }

        /// <summary>
        /// Permet de changer le port
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplyPort_Click(object sender, RoutedEventArgs e) {
            if (int.TryParse(txtPort.Text, out int newPort)) {

                // Vérifie si le port est dans la plage autorisée
                if (newPort >= PortMin && newPort <= PortMax) {

                    // Modifie le port dans ServerConfig
                    ServerConfig.Instance.UdpPort = newPort;
                    ServerConfig.Instance.SaveDataServer();

                    // Redémarre le serveur avec le nouveau port
                    serverManager.StopServer();
                    serverManager.LaunchServer();
                } else {
                    System.Windows.MessageBox.Show($"Veuillez entrer un numéro de port entre {PortMin} et {PortMax}.");
                }
            } else {
                System.Windows.MessageBox.Show("Veuillez entrer un numéro de port valide.");
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            // Utilise une expression régulière pour vérifier si le texte est numérique
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "[0-9]")) {
                // Si le texte n'est pas numérique, annulation l'événement
                e.Handled = true;
            }
        }
    }
}
