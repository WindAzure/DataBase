using DataBaseProject.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataBaseProject.Controls
{
    /// <summary>
    /// Interaction logic for PricePanel.xaml
    /// </summary>
    public partial class PricePanel : UserControl, INotifyPropertyChanged
    {
        private String _drinkName;
        private String _price;
        private String _calories;
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(String name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public String Price
        {
            set
            {
                _price = value;
                OnPropertyChanged("Price");
            }
            get
            {
                return _price;
            }
        }

        public String Calories
        {
            set
            {
                _calories = value;
                OnPropertyChanged("Calories");
            }
            get
            {
                return _calories;
            }
        }

        public PricePanel(String fileName)
        {
            InitializeComponent();
            DataContext = this;
            _drinkName = fileName;
            String[] data = System.IO.File.ReadAllLines("../../DrinkInformation/" + fileName + ".txt", Encoding.UTF8);
            Calories = "熱量：" + data[1]+" 卡";
            Price = "建議售價："+data[2] + " 元";
        }

        public void StartMove()
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.To = 0;
            animation.Duration = ConstValue.INGREDIENT_SHOP_CAR_SPEED;
            Storyboard board = new Storyboard();
            board.Children.Add(animation);
            Storyboard.SetTargetProperty(animation, new PropertyPath("RenderTransform.X"));
            Storyboard.SetTarget(animation, _pricePanel);
            board.Begin();
        }

        private void OnDrinkLogoItemMouseUp(object sender, MouseButtonEventArgs e)
        {
            SqlConnection connection=new SqlConnection();
            connection.ConnectionString=ConfigurationManager.ConnectionStrings["DataBaseProject.Properties.Settings.NTUT_DataBaseConnectionString"].ConnectionString;
            connection.Open();
            SqlCommand command1 = new SqlCommand("SELECT count(*) FROM [dbo].[Member] inner join [dbo].[OrderRecord] ON [dbo].[Member].Account=[dbo].[OrderRecord].FKAccount WHERE Account='" + PageSwitcher._account + "' and ConfirmState='false'", connection);
            if (Convert.ToBoolean(command1.ExecuteScalar()))
            {
                SqlCommand command2 = new SqlCommand("SELECT count(*) FROM [dbo].[Member] inner join [dbo].[OrderRecord] ON Account=FKAccount inner join [dbo].[Has] ON Oid=FKOid inner join [dbo].[Drink] ON FKName=ENName WHERE Account='" + PageSwitcher._account + "' and ConfirmState='false' and ENName='" + _drinkName.Substring(1, _drinkName.Length - 1) + "'", connection);
                if (!Convert.ToBoolean(command2.ExecuteScalar()))
                {
                    SqlCommand command3 = new SqlCommand("INSERT INTO [dbo].[Has] ([FKName],[FKOid],[Quantity],[MakeState]) VALUES (('" + _drinkName.Substring(1, _drinkName.Length - 1) + "'), (SELECT [Oid] FROM [dbo].[Member] inner join [dbo].[OrderRecord] ON Account=FKAccount WHERE ConfirmState='false' and Account='" + PageSwitcher._account + "' ),('1'),('false'))", connection);
                    command3.ExecuteScalar();
                }
            }
            else
            {
                SqlCommand command4 = new SqlCommand("INSERT INTO [dbo].[OrderRecord] ([Oid],[ConfirmState],[ConfirmDate],[DeliveryState],[PS],[FKAccount]) VALUES (NEWID(),'false',NULL,'false','','" + PageSwitcher._account + "')", connection);
                command4.ExecuteScalar();
                SqlCommand command5 = new SqlCommand("INSERT INTO [dbo].[Has] ([FKName],[FKOid],[Quantity],[MakeState]) VALUES (('" + _drinkName.Substring(1, _drinkName.Length - 1) + "'), (SELECT [Oid] FROM [dbo].[Member] inner join [dbo].[OrderRecord] ON Account=FKAccount WHERE ConfirmState='false' and Account='" + PageSwitcher._account + "' ),('1'),('false'))", connection);
                command5.ExecuteScalar();
            }
            connection.Close();
            PageSwitcher.Switch(new ShopCarForm());
        }
    }
}
