using EntityFrameworkDemo3.source.data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace EntityFrameworkDemo3
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
            using (var context = new source.data.ModelDemoContainer())
            {
                context.UserSet.Add(new source.data.User
                {                 
                    
                    Name = "user2", 
                    Age = 30, 
                    Gender = "male" 
                });

                context.SaveChanges();

                foreach (var item in context.UserSet)
                {
                    Console.WriteLine(item.Name);   
                }

                // required to populate Local
                //context.UserSet.ToList();

                //var qry = from col in context.UserSet where col.Age == 30 select col;
                //var qry = from col in context.UserSet select col;

                var qry = context.UserSet.Where(user => user.Age == 34);

                // bind to datagrid
                //userDataGrid.ItemsSource = context.UserSet.Local;
                userDataGrid.ItemsSource = qry.ToList();


                // just to check
                foreach (var item in qry)
                {
                    Console.WriteLine("result: " + item.Name);
                }

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource modelDemoContainerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("modelDemoContainerViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // modelDemoContainerViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource userViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("userViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // userViewSource.Source = [generic data source]
        }
    }
}
