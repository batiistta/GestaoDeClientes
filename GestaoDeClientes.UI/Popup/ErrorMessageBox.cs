using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestaoDeClientes.UI.Popup
{
    public class GCMessageBox
    {
        public static bool Show(string message)
        {
            var msgForm = new GCMessageBoxDefault(message);
            msgForm.btnConfirmExhibition.Visibility = Visibility.Visible;

            return DisplayDefaultMessageBox(msgForm);
        }

        public static bool Show(string message, MessageBoxStatus status)
        {
            var msgForm = new GCMessageBoxDefault(message, status);
            msgForm.btnConfirmExhibition.Visibility = Visibility.Visible;

            return DisplayDefaultMessageBox(msgForm);
        }

        public static bool Show(string message, string title)
        {
            var msgForm = new GCMessageBoxDefault(title, message);
            msgForm.btnConfirmExhibition.Visibility = Visibility.Visible;

            return DisplayDefaultMessageBox(msgForm);
        }

        public static bool Show(string message, string title, MessageBoxStatus status)
        {
            var msgForm = new GCMessageBoxDefault(title, message, status);
            msgForm.btnConfirmExhibition.Visibility = Visibility.Visible;

            return DisplayDefaultMessageBox(msgForm);
        }

        public static bool Confirm(string message)
        {
            var msgForm = new GCMessageBoxDefault(message);
            msgForm.btnYesNoExhibition.Visibility = Visibility.Visible;

            return DisplayDefaultMessageBox(msgForm);
        }
        public static bool Confirm(string message, MessageBoxStatus status)
        {
            var msgForm = new GCMessageBoxDefault(message, status);
            msgForm.btnYesNoExhibition.Visibility = Visibility.Visible;

            return DisplayDefaultMessageBox(msgForm);
        }


        public static bool Confirm(string message, string title)
        {
            var msgForm = new GCMessageBoxDefault(title, message);
            msgForm.btnYesNoExhibition.Visibility = Visibility.Visible;

            return DisplayDefaultMessageBox(msgForm);
        }

        public static bool Confirm(string message, string title, MessageBoxStatus status)
        {
            var msgForm = new GCMessageBoxDefault(title, message, status);
            msgForm.btnYesNoExhibition.Visibility = Visibility.Visible;

            return DisplayDefaultMessageBox(msgForm);
        }

        public static bool DisplayDefaultMessageBox(GCMessageBoxDefault form)
        {
            var result = false;

            if (ScreenContext.Instance.mainWindow != null)
            {
                form.Owner = ScreenContext.Instance.mainWindow;

                ScreenContext.Instance.mainWindow.ShowPopUpBackground(true);

                SystemSounds.Exclamation.Play();
                result = (bool)form.ShowDialog();

                ScreenContext.Instance.mainWindow.ShowPopUpBackground(false);
            }
            else
            {
                SystemSounds.Exclamation.Play();
                result = (bool)form.ShowDialog();
            }

            return result;
        }

        public enum MessageBoxStatus
        {
            Ok,
            Warning,
            Information,
            Error
        }
    }
}
