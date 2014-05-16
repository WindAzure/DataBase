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
            PageSwitcher.Switch(new DrinkInformationForm());
        }

        private void AddItemOfShopCar(String name, int price, int quantity)
        {
            TextBlock deleteButton = new TextBlock();
            deleteButton.FontSize = 18;
            deleteButton.Foreground = Brushes.Gray;
            deleteButton.Padding = new Thickness(5, 0, 5, 0);
            deleteButton.Margin = new Thickness(0, 0, 0, 30);
            deleteButton.MouseEnter += MouseEnterDeleteButton;
            deleteButton.MouseLeave += MouseLeaveDeleteButton;
            deleteButton.MouseDown += MouseDownDeleteButton;
            deleteButton.MouseUp += MouseUpDeleteButton;
            deleteButton.Text = "刪除";
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
            block3.Text = quantity.ToString();
            block3.Width = 50;
            block3.Height = 25;
            block3.Margin = new Thickness(0, 0, 0, 30);
            block3.TextChanged += ChangedBlock3Text;
            _quantityStackPanel.Children.Add(block3);
        }

        void MouseUpDeleteButton(object sender, MouseButtonEventArgs e)
        {
            TextBlock block = sender as TextBlock;
            block.Foreground = Brushes.White;
            block.Background = Brushes.Black;

            int index = _deleteStackPanel.Children.IndexOf(block);
            TextBlock name = _nameStackPanel.Children[index] as TextBlock;
            TextBox quantity = _quantityStackPanel.Children[index] as TextBox;

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["DataBaseProject.Properties.Settings.NTUT_DataBaseConnectionString"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand("DELETE FROM [dbo].[Has] WHERE ([FKOid] IN (SELECT 	[Oid] FROM [Member] inner join [OrderRecord] ON [Member].[Account]=[OrderRecord].[FKAccount] WHERE Account='" + PageSwitcher._account + "' and ConfirmState='False')) AND ([FKName] IN (SELECT [FKName] FROM [Member] inner join [OrderRecord] ON [Member].[Account]=[OrderRecord].[FKAccount] inner join [Has] ON [OrderRecord].[Oid]=[Has].[FKOid] inner join [Drink] ON [Has].FKName=[Drink].ENName WHERE Account='" + PageSwitcher._account + "' and ConfirmState='False' and Name='" + name.Text + "'))", connection);
            command.ExecuteScalar();
            connection.Close();

            _deleteStackPanel.Children.RemoveAt(index);
            _nameStackPanel.Children.RemoveAt(index);
            _priceStackPanel.Children.RemoveAt(index);
            _quantityStackPanel.Children.RemoveAt(index);
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

        bool SumUp(ref int total)
        {
            bool flag = false;
            int per = _quantityStackPanel.Children.Count;
            for (int i = 0; i < per; i++)
            {
                TextBlock block = _priceStackPanel.Children[i] as TextBlock;
                TextBox box = _quantityStackPanel.Children[i] as TextBox;
                String[] number = block.Text.Split('元');

                try
                {
                    int n1 = Convert.ToInt32(number[0]);
                    int n2 = Convert.ToInt32(box.Text);
                    checked { total += n1 * n2; }
                }
                catch (Exception)
                {
                    flag = true;
                }
            }
            return flag;
        }

        void CalculateTotal()
        {
            int total = 0;
            SumUp(ref total);
            _totalPrice.Text = "總價：" + total.ToString() + "元";
        }

        void ChangedBlock3Text(object sender, TextChangedEventArgs e)
        {
            int total = 0;
            TextBox quantity = sender as TextBox;
            if (quantity != null && !SumUp(ref total))
            {
                int index = _quantityStackPanel.Children.IndexOf(quantity);
                TextBlock name = _nameStackPanel.Children[index] as TextBlock;

                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["DataBaseProject.Properties.Settings.NTUT_DataBaseConnectionString"].ConnectionString;
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE [dbo].[Has] SET [Quantity] = " + quantity.Text + "WHERE	([FKOid] IN ( SELECT [Oid] FROM [Member] inner join [OrderRecord] ON [Member].[Account]=[OrderRecord].[FKAccount] WHERE Account='" + PageSwitcher._account + "' and ConfirmState='False')) AND ([FKName] IN (SELECT [FKName] FROM [Member] inner join [OrderRecord] ON [Member].[Account]=[OrderRecord].[FKAccount] inner join [Has] ON [OrderRecord].[Oid]=[Has].[FKOid] inner join [Drink] ON [Has].FKName=[Drink].ENName	WHERE Account='" + PageSwitcher._account + "' and ConfirmState='False' and Name='" + name.Text + "'))", connection);
                command.ExecuteScalar();
                connection.Close();
                _totalPrice.Text = "總價：" + total.ToString() + "元";
            }
            else
            {
                _totalPrice.Text = "總價：";
            }
        }

        void GetPS()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["DataBaseProject.Properties.Settings.NTUT_DataBaseConnectionString"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT [PS] FROM [dbo].[Member] inner join [dbo].[OrderRecord] ON Account=FKAccount WHERE Account='" + PageSwitcher._account + "' and ConfirmState='false'", connection);
            _psTextBox.Text = command.ExecuteScalar() as String;
            connection.Close();
        }

        private void LoadShopCarData()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["DataBaseProject.Properties.Settings.NTUT_DataBaseConnectionString"].ConnectionString;
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT [Name],[Price],[Quantity] FROM [Member] inner join [OrderRecord] ON [Member].[Account]=[OrderRecord].[FKAccount] inner join [Has]  ON [OrderRecord].[Oid]=[Has].[FKOid] inner join [Drink] ON  [Has].[FKName]=[Drink].[ENName] WHERE Account='" + PageSwitcher._account + "' and ConfirmState='False'", connection);

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);

            int tableRow = dataSet.Tables[0].Rows.Count;
            int tableColumn = dataSet.Tables[0].Columns.Count;
            for (int i = 0; i < tableRow; i++)
            {
                AddItemOfShopCar(dataSet.Tables[0].Rows[i].ItemArray[0] as String, (int)dataSet.Tables[0].Rows[i].ItemArray[1], (int)dataSet.Tables[0].Rows[i].ItemArray[2]);
            }
            CalculateTotal();
            GetPS();
            connection.Close();
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

            int total = 0;
            if (_psTextBox.Text.Length > 200)
            {
                MessageBox.Show("飲品需求及意見長度不可超過200");
            }
            else if (SumUp(ref total))
            {
                MessageBox.Show("數量輸入錯誤");
            }
            else if (_quantityStackPanel.Children.Count == 0)
            {
                MessageBox.Show("請先選擇欲購買之飲料");
            }
            else if (total == 0)
            {
                MessageBox.Show("數量不可為零"); 
            }
            else
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["DataBaseProject.Properties.Settings.NTUT_DataBaseConnectionString"].ConnectionString;
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE [dbo].[OrderRecord] SET [ConfirmState] = 'true', [ConfirmDate] = '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "', [PS] = '" + _psTextBox.Text + "' WHERE FKAccount='" + PageSwitcher._account + "' and ConfirmState='False'", connection);
                command.ExecuteScalar();
                connection.Close();

                if (MessageBox.Show("是否繼續購買？", "結帳成功", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    PageSwitcher.Switch(new DrinkInformationForm());
                }
                else
                {
                    Environment.Exit(0);
                }
            }
        }

        private void WheelList(double delta)
        {
            _deleteScrollViewer.ScrollToVerticalOffset(_deleteScrollViewer.VerticalOffset - delta);
            _nameScrollViewer.ScrollToVerticalOffset(_nameScrollViewer.VerticalOffset - delta);
            _priceScrollViewer.ScrollToVerticalOffset(_priceScrollViewer.VerticalOffset - delta);
            _quantityScrollViewer.ScrollToVerticalOffset(_quantityScrollViewer.VerticalOffset - delta);
        }

        private void PreviewMouseWheelNameScrollViewer(object sender, MouseWheelEventArgs e)
        {
            WheelList(e.Delta);
        }

        private void PreviewMouseWheelDeleteScrollViewer(object sender, MouseWheelEventArgs e)
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

        private void ChangedPsTextBoxText(object sender, TextChangedEventArgs e)
        {
            if (_psTextBox.Text.Length > 200)
            {
                MessageBox.Show("飲品需求及意見長度不可超過200");
            }
            else
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["DataBaseProject.Properties.Settings.NTUT_DataBaseConnectionString"].ConnectionString;
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE [dbo].[OrderRecord] SET [PS] = '" + _psTextBox.Text + "' WHERE FKAccount='" + PageSwitcher._account + "' and ConfirmState='False'", connection);
                command.ExecuteScalar();
                connection.Close();
            }
        }
    }
}
