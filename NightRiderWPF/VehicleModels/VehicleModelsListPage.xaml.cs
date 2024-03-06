﻿using DataObjects;
using LogicLayer;
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

namespace NightRiderWPF.VehicleModels
{
    /// <summary>
    /// Interaction logic for VehicleModelsListPage.xaml
    /// </summary>
    public partial class VehicleModelsListPage : Page
    {
        private IVehicleModelManager _vehicleModelManager;
        private IEnumerable<VehicleModel> _vehicleModels;

        public VehicleModelsListPage(IVehicleModelManager vehicleModelManager)
        {
            _vehicleModelManager = vehicleModelManager;

            InitializeComponent();
        }

        /// <summary>
        ///     Handle double click event on vehicle model list datagrid;
        ///     open vehicle model for selected vehicle model row
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-03-02
        /// </remarks>
        private void dat_vehicleModelsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        /// <summary>
        ///     Handle page load event; load active vehicle models
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-03-02
        /// </remarks>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _vehicleModels = _vehicleModelManager.GetVehicleModels();

            dat_vehicleModelsList.ItemsSource = _vehicleModels;
        }
    }
}