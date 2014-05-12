using DataBaseProject.Controls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataBaseProject.Forms
{
    /// <summary>
    /// Interaction logic for ShopCarForm.xaml
    /// </summary>
    public partial class ShopCarForm : UserControl
    {
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

        public ShopCarForm()
        {
            InitializeComponent();
            LoadShopCarData();
        }

        private void ClickBackButton(object sender, RoutedEventArgs e)
        {
        }

        private void AddItemOfShopCar(String name, int price, int quantity)
        {
            TextBlock deleteButton = new TextBlock();
            deleteButton.FontSize = 18;
            deleteButton.Foreground = Brushes.Gray;
            deleteButton.Padding = new Thickness(5, 0, 5, 0);
            deleteButton.Text = "刪除";
            deleteButton.Margin = new Thickness(0, 0, 0, 30);
            deleteButton.MouseEnter += MouseEnterDeleteButton;
            deleteButton.MouseLeave += MouseLeaveDeleteButton;
            deleteButton.MouseDown += MouseDownDeleteButton;
            deleteButton.MouseUp += MouseUpDeleteButton;
            _deleteStackPanel.Children.Add(deleteButton);

            TextBlock block1 = new TextBlock();
            block1.FontSize = 18;
            block1.Foreground = Brushes.Gray;
            block1.Text = name;
            block1.Margin = new Thickness(0, 0, 0, 30);
            _nameStackPanel.Children.Add(block1);

            TextBlock block2 = new TextBlock();
            block2.FontSize = 18;
            block2.Foreground = Brushes.Gray;
            block2.Text = price.ToString() + "元";
            block2.Margin = new Thickness(0, 0, 0, 30);
            _priceStackPanel.Children.Add(block2);

            TextBox block3 = new TextBox();
            block3.Width = 50;
            block3.Height = 25;
            block3.Margin = new Thickness(0, 0, 0, 30);
            block3.TextChanged += ChangedBlock3Text;
            block3.Text = quantity.ToString();
            _quantityStackPanel.Children.Add(block3);
        }

        void MouseUpDeleteButton(object sender, MouseButtonEventArgs e)
        {
            TextBlock block = sender as TextBlock;
            block.Foreground = Brushes.White;
            block.Background = Brushes.Black;
        }

        void MouseDownDeleteButton(object sender, MouseButtonEventArgs e)
        {
            TextBlock block = sender as TextBlock;
            block.Background = Brushes.LightGray;
        }

        void MouseLeaveDeleteButton(object sender, MouseEventArgs e)
        {
            TextBlock block = sender as TextBlock;
            block.Foreground = Brushes.Gray;
            block.Background = Brushes.White;
        }

        void MouseEnterDeleteButton(object sender, MouseEventArgs e)
        {
            TextBlock block = sender as TextBlock;
            block.Foreground = Brushes.White;
            block.Background = Brushes.Black;
        }

        void ChangedBlock3Text(object sender, TextChangedEventArgs e)
        {
            int total = 0;
            int per = _quantityStackPanel.Children.Count;
            for (int i = 0; i < per; i++)
            {
                TextBlock block = _priceStackPanel.Children[i] as TextBlock;
                TextBox box = _quantityStackPanel.Children[i] as TextBox;
                String[] number = block.Text.Split('元');
                int n1 = Convert.ToInt32(number[0]);
                int n2 = Convert.ToInt32(box.Text);
                total += n1 * n2;
            }
            _totalPrice.Text = "總價：" + total.ToString() + "元";
        }

        private void LoadShopCarData()
        {
            /*SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["DataBaseProject.Properties.Settings.NTUT_DataBaseConnectionString"].ConnectionString;
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT [Name],[Price],[Quantity] FROM [Member] inner join [OrderRecord] ON [Member].[Account]=[OrderRecord].[FKAccount] inner join [Has]  ON [OrderRecord].[Oid]=[Has].[FKOid] inner join [Drink] ON  [Has].[FKName]=[Drink].[ENName] WHERE Account='Azure' and ConfirmState='False'", connection);

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);

            int tableRow = dataSet.Tables[0].Rows.Count;
            int tableColumn = dataSet.Tables[0].Columns.Count;
            for (int i = 0; i < tableRow; i++)
            {
                AddItemOfShopCar(dataSet.Tables[0].Rows[i].ItemArray[0] as String, (int)dataSet.Tables[0].Rows[i].ItemArray[1], (int)dataSet.Tables[0].Rows[i].ItemArray[2]);
            }*/
            AddItemOfShopCar("蘋果汁", 152, 23);
            AddItemOfShopCar("蘋果汁A", 88, 23);
            AddItemOfShopCar("蘋果汁B", 3, 23);
            AddItemOfShopCar("蘋果汁C", 2, 23);
            AddItemOfShopCar("蘋果汁D", 55, 23);
            AddItemOfShopCar("蘋果汁D", 55, 23);
            AddItemOfShopCar("蘋果汁D", 55, 23);
            AddItemOfShopCar("蘋果汁D", 55, 23);
            AddItemOfShopCar("蘋果汁D", 55, 23);
        }

        private void ClickCloseButton(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void MouseDownCheckButton(object sender, MouseButtonEventArgs e)
        {
            TextBlock block = sender as TextBlock;
            block.Foreground = Brushes.Black;
            block.Background = Brushes.LightGray;
        }

        private void MouseUpCheckButton(object sender, MouseButtonEventArgs e)
        {
            TextBlock block = sender as TextBlock;
            block.Foreground = Brushes.White;
            block.Background = Brushes.Black;
        }

        private void WheelList(double delta)
        {
            _deleteScrollViewer.ScrollToVerticalOffset(_deleteScrollViewer.VerticalOffset - delta);
            _nameScrollViewer.ScrollToVerticalOffset(_nameScrollViewer.VerticalOffset - delta);
            _priceScrollViewer.ScrollToVerticalOffset(_priceScrollViewer.VerticalOffset - delta);
            _quantityScrollViewer.ScrollToVerticalOffset(_quantityScrollViewer.VerticalOffset - delta);
        }

        private void PreviewMouseWheelDeleteScrollViewer(object sender, MouseWheelEventArgs e)
        {

            WheelList(e.Delta);
        }

        private void PreviewMouseWheelNameScrollViewer(object sender, MouseWheelEventArgs e)
        {
            WheelList(e.Delta);
        }

        private void PreviewMouseWheelPriceScrollViewer(object sender, MouseWheelEventArgs e)
        {

            WheelList(e.Delta);
        }

        private void PreviewMouseWheelQuantityScrollViewer(object sender, MouseWheelEventArgs e)
        {

            WheelList(e.Delta);
        }
    }
}
