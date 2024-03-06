﻿using LogicLayer;
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

namespace NightRiderWPF
{
    /// <summary>
    /// Interaction logic for ClientLookupPage.xaml
    /// </summary>
    public partial class ClientLookupPage : Page
    {
        public ClientLookupPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var clientManager = new ClientManager();
                var client = clientManager.GetClientByEmail(txtLookupEmail.Text);

                txtFirstName.Text = client.GivenName;
                txtLastName.Text = client.FamilyName;
                txtMiddleName.Text = client.MiddleName;
                txtDOB.Text = client.DOB.ToString("D");
                txtPostalCode.Text = client.PostalCode;
                txtCity.Text = client.City;
                txtAddress.Text = client.Address;
                txtEmail.Text = client.Email;
                txtTextNumber.Text = client.TextNumber;
                txtVoiceNumber.Text = client.VoiceNumber;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Client Retrieval Failed.", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }
    }
}