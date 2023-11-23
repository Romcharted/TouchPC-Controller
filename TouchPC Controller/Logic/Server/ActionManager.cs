using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using TouchPC_Controller.Logic.Enum;
using WindowsInput;

namespace TouchPC_Controller.Logic.Server {

    /// <summary>
    /// Gère les actions liées à l'entrée utilisateur, telles que le déplacement de la souris, les clics de souris, etc.
    /// </summary>
    public class ActionManager {

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int x, int y);

        private InputSimulator inputSimulator;

        /// <summary>
        /// Initialise une nouvelle instance de la classe ActionManager.
        /// </summary>
        public ActionManager() {
            inputSimulator = new InputSimulator();
        }

        /// <summary>
        /// Déplace la souris aux coordonnées spécifiées.
        /// </summary>
        /// <param name="x">Coordonnée X</param>
        /// <param name="y">Coordonnée Y</param>
        public void MouseMove(int x, int y) {
            SetCursorPos(x, y);
        }

        /// <summary>
        /// Fait défiler la souris dans la direction spécifiée.
        /// </summary>
        /// <param name="direction">Direction du défilement</param>
        public void Scroll(Directions direction) {
            const int scrollAmount = 1;

            switch (direction) {
                case Directions.Up:
                    MouseWheelVertical(-scrollAmount);
                    break;
                case Directions.Down:
                    MouseWheelVertical(scrollAmount);
                    break;
                case Directions.Left:
                    MouseWheelHorizontal(-scrollAmount);
                    break;
                case Directions.Right:
                    MouseWheelHorizontal(scrollAmount);
                    break;
            }
        }

        /// <summary>
        /// Fait défiler la souris verticalement.
        /// </summary>
        /// <param name="delta">Quantité de défilement</param>
        private void MouseWheelVertical(int delta) {
            try {
                inputSimulator.Mouse.VerticalScroll(delta);
            } catch (Exception exception) {
                Debug.WriteLine(exception);
            }
        }

        /// <summary>
        /// Fait défiler la souris horizontalement.
        /// </summary>
        /// <param name="delta">Quantité de défilement</param>
        private void MouseWheelHorizontal(int delta) {
            try {
                inputSimulator.Mouse.HorizontalScroll(delta);
            } catch (Exception exception) {
                Debug.WriteLine(exception);
            }
        }

        /// <summary>
        /// Effectue un clic gauche de la souris.
        /// </summary>
        public void LeftClick() {
            inputSimulator.Mouse.LeftButtonClick();
        }

        /// <summary>
        /// Effectue un clic droit de la souris.
        /// </summary>
        public void RightClick() {
            inputSimulator.Mouse.RightButtonClick();
        }

        /// <summary>
        /// Effectue un double-clic gauche de la souris.
        /// </summary>
        public void DoubleLeftClick() {
            inputSimulator.Mouse.LeftButtonDoubleClick();
        }

        /// <summary>
        /// Effectue un double-clic droit de la souris.
        /// </summary>
        public void DoubleRightClick() {
            inputSimulator.Mouse.RightButtonDoubleClick();
        }

        /// <summary>
        /// Effectue une action de texte au clavier en fonction du texte Unicode spécifié.
        /// </summary>
        /// <param name="unicode">Texte Unicode</param>
        public void KeyboardTextAction(string unicode) {
            try {
                switch (unicode) {
                    case "delete":
                        inputSimulator.Keyboard.KeyPress(VirtualKeyCode.BACK);
                        break;
                    case "enter":
                        inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
                        break;
                    case "left":
                        inputSimulator.Keyboard.KeyPress(VirtualKeyCode.LEFT);
                        break;
                    case "right":
                        inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                        break;
                    case "up":
                        inputSimulator.Keyboard.KeyPress(VirtualKeyCode.UP);
                        break;
                    case "down":
                        inputSimulator.Keyboard.KeyPress(VirtualKeyCode.DOWN);
                        break;
                    default:
                        inputSimulator.Keyboard.TextEntry(unicode);
                        break;
                }
            } catch (Exception exception) {
                Debug.WriteLine(exception);
            }
        }

        /// <summary>
        /// Effectue une action de contrôle multimédia en fonction de l'action spécifiée.
        /// </summary>
        /// <param name="action">Action de contrôle multimédia</param>
        public void MediaControlAction(string action) {
            try {
                switch (action) {
                    case "Volume Down":
                        inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VOLUME_DOWN);
                        break;
                    case "Volume Up":
                        inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VOLUME_UP);
                        break;
                    case "Move Forward 10s":
                        inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                        break;
                    case "Go back 10s":
                        inputSimulator.Keyboard.KeyPress(VirtualKeyCode.LEFT);
                        break;
                    case "Up":
                        inputSimulator.Keyboard.KeyPress(VirtualKeyCode.UP);
                        break;
                    case "Down":
                        inputSimulator.Keyboard.KeyPress(VirtualKeyCode.DOWN);
                        break;
                    case "Next":
                        inputSimulator.Keyboard.KeyPress(VirtualKeyCode.MEDIA_NEXT_TRACK);
                        break;
                    case "Previous":
                        inputSimulator.Keyboard.KeyPress(VirtualKeyCode.MEDIA_PREV_TRACK);
                        break;
                    case "Play/Pause":
                        inputSimulator.Keyboard.KeyPress(VirtualKeyCode.MEDIA_PLAY_PAUSE);
                        break;
                }
            } catch (Exception exception) {
                Debug.WriteLine(exception);
            }
        }
    }
}
