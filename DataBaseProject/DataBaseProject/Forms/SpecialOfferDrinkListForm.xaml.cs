using DataBaseProject.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
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

namespace DataBaseProject.Forms
{
    /// <summary>
    /// Interaction logic for SpecialOfferDrinkListForm.xaml
    /// </summary>
    public partial class SpecialOfferDrinkListForm : UserControl, INotifyPropertyChanged
    {
        private String _price1;
        private String _price2;
        private String _price3;
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(String name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public String Price1
        {
            set
            {
                _price1 = value;
                OnPropertyChanged("Price1");
            }
            get
            {
                return _price1;
            }
        }

        public String Price2
        {
            set
            {
                _price2 = value;
                OnPropertyChanged("Price2");
            }
            get
            {
                return _price2;
            }
        }

        public String Price3
        {
            set
            {
                _price3 = value;
                OnPropertyChanged("Price3");
            }
            get
            {
                return _price3;
            }
        }

        public SpecialOfferDrinkListForm()
        {
            InitializeComponent();
            DataContext = this;
            String[] data1 = System.IO.File.ReadAllLines("../../DrinkInformation/_matchaTea.txt", Encoding.UTF8);
            String[] data2 = System.IO.File.ReadAllLines("../../DrinkInformation/_tomatoeJuice.txt", Encoding.UTF8);
            String[] data3 = System.IO.File.ReadAllLines("../../DrinkInformation/_waterMelonSmoothies.txt", Encoding.UTF8);
            Price1 = "特價：" + data1[2] + " 元";
            Price2 = "特價：" + data2[2] + " 元";
            Price3 = "特價：" + data3[2] + " 元";
        }

        private void DrawTitle()
        {
            _title.Children.Clear();
            double height = _title.ActualHeight;
            double width = _title.ActualWidth;
            for (double i = 10.0; i < width; i += 10.0)
            {
                Line line = new Line();
                line.X1 = i;
                line.Y1 = 0.0;
                line.X2 = i - 10.0;
                line.Y2 = height - 10;
                line.StrokeThickness = 0.5;
                line.Stroke = Brushes.Black;
                _title.Children.Add(line);
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            DrawTitle();
        }

        private void ClickBackButton(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch(new DrinkInformationForm());
        }

        private void ClickCloseButton(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void SendOrderSqlCommand(String drinkName)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["DataBaseProject.Properties.Settings.NTUT_DataBaseConnectionString"].ConnectionString;
            connection.Open();
            SqlCommand command1 = new SqlCommand("SELECT count(*) FROM [dbo].[Member] inner join [dbo].[OrderRecord] ON [dbo].[Member].Account=[dbo].[OrderRecord].FKAccount WHERE Account='" + PageSwitcher._account + "' and ConfirmState='false'", connection);
            if (Convert.ToBoolean(command1.ExecuteScalar()))
            {
                SqlCommand command2 = new SqlCommand("SELECT count(*) FROM [dbo].[Member] inner join [dbo].[OrderRecord] ON Account=FKAccount inner join [dbo].[Has] ON Oid=FKOid inner join [dbo].[Drink] ON FKName=ENName WHERE Account='" + PageSwitcher._account + "' and ConfirmState='false' and ENName='" + drinkName + "'", connection);
                if (!Convert.ToBoolean(command2.ExecuteScalar()))
                {
                    SqlCommand command3 = new SqlCommand("INSERT INTO [dbo].[Has] ([FKName],[FKOid],[Quantity]) VALUES (('" + drinkName + "'), (SELECT [Oid] FROM [dbo].[Member] inner join [dbo].[OrderRecord] ON Account=FKAccount WHERE ConfirmState='false' and Account='" + PageSwitcher._account + "' ),('1'))", connection);
                    command3.ExecuteScalar();
                }
            }
            else
            {
                SqlCommand command4 = new SqlCommand("INSERT INTO [dbo].[OrderRecord] ([Oid],[ConfirmState],[ConfirmDate],[DeliveryState],[PS],[FKAccount]) VALUES (NEWID(),'false',NULL,'false','','" + PageSwitcher._account + "')", connection);
                command4.ExecuteScalar();
                SqlCommand command5 = new SqlCommand("INSERT INTO [dbo].[Has] ([FKName],[FKOid],[Quantity]) VALUES (('" + drinkName + "'), (SELECT [Oid] FROM [dbo].[Member] inner join [dbo].[OrderRecord] ON Account=FKAccount WHERE ConfirmState='false' and Account='" + PageSwitcher._account + "' ),('1'))", connection);
                command5.ExecuteScalar();
            }
            connection.Close();
            PageSwitcher.Switch(new ShopCarForm());
        }

        private void OnMataTeaItemMouseUp(object sender, MouseButtonEventArgs e)
        {
            SendOrderSqlCommand("matchaTea");
        }

        private void OnTomatoItemMouseUp(object sender, MouseButtonEventArgs e)
        {
            SendOrderSqlCommand("tomatoeJuice");
        }
    }
}
