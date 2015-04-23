﻿using System;
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

namespace EFSQLiteDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            try
            {
                using (var context = new data.DataContext())
                {
                    var customers = context.Customers;

                    customers.Add(new data.Customer { Name = "custName", Gender = "custGender" });

                    context.SaveChanges();

                    foreach (var item in customers)
                    {
                        Console.WriteLine(item.ID + " " +
                                          item.Name + " " +
                                          item.Gender);
                    }

                    customerDataGrid.ItemsSource = context.Customers.ToList();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "InEx1: " + 
                    ex.InnerException.Message + "InEx2: " +
                    ex.InnerException.InnerException.Message);
            }

            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource customerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("customerViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // customerViewSource.Source = [generic data source]
        }
    }
}
