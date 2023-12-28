﻿using GestaoDeClientes.Shared;
using GestaoDeClientes.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GestaoDeClientes.UI
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ChipOperador.Content = Global.Instance;
        }

        public void ShowPopUpBackground(bool show)
        {
            if (show)
                PopUpBackground.Visibility = Visibility.Visible;
            else
                PopUpBackground.Visibility = Visibility.Collapsed;
        }

        private void LimparViews()
        {
            clienteView.Visibility = Visibility.Hidden;
        }

        private void switchSreen(UserControl screen)
        {
            if(screen != null)
            {
                if (screen.GetType() == typeof(ClienteView))
                {
                    clienteView.Visibility = Visibility.Visible;
                    return;
                }
            }
        }

        private void btnCliente_Click(object sender, RoutedEventArgs e)
        {
            switchSreen(clienteView);
        }

        private void btnSair_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
