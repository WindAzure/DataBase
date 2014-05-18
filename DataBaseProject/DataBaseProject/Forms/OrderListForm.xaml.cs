using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for OrderListForm.xaml
    /// </summary>
    public partial class OrderListForm : UserControl//,INotifyPropertyChanged
    {
        //    public event PropertyChangedEventHandler PropertyChanged;

        //    protected void OnPropertyChanged(String name)
        //    {
        //        if (PropertyChanged != null)
        //        {
        //            PropertyChanged(this, new PropertyChangedEventArgs(name));
        //        }
        //    }

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

        public OrderListForm()
        {
            InitializeComponent();
            _orderListGrid.ExpandAllGroups();
            _dateColumn.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
        }

        private void ClickBackButton(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch(new ManagerHomeForm());
        }

        private void ClickCloseButton(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void ColumnsPopulatedGridControl(object sender, RoutedEventArgs e)
        {
            foreach (var column in _orderListGrid.Columns)
            {
                column.ActualEditSettings.HorizontalContentAlignment = EditSettingsHorizontalAlignment.Center;
            }
        }

        private String GetMapping(String drink)
        {
            if (drink == "蘋果汁")
                return "appleJuice";
            else if (drink == "香蕉汁")
                return "bananaJuice";
            else if (drink == "葡萄汁")
                return "grapeJuice";
            else if (drink == "葡萄柚汁")
                return "grapeFruitJuice";
            else if (drink == "芭樂汁")
                return "guavaJuice";
            else if (drink == "奇異果汁")
                return "kiwiFruitJuice";
            else if (drink == "柳橙汁")
                return "orangeJuice";
            else if (drink == "鐵釘木瓜汁")
                return "papayaJuice";
            else if (drink == "百香果汁")
                return "passionFruitJuice";
            else if (drink == "番茄汁")
                return "tomatoeJuice";
            else if (drink == "西瓜汁")
                return "waterMelonJuice";
            else if (drink == "藍莓冰沙")
                return "blueBerrySmoothies";
            else if (drink == "芒果冰沙")
                return "mangoSmoothies";
            else if (drink == "百香果冰沙")
                return "passionFruitSoomthies";
            else if (drink == "鳳梨冰沙")
                return "pineAppleSmoothies";
            else if (drink == "百香果冰沙")
                return "passionFruitSoomthies";
            else if (drink == "西瓜冰沙")
                return "waterMelonSmoothies";
            else if (drink == "錫蘭紅茶")
                return "blackTea";
            else if (drink == "高山綠茶")
                return "greenTea";
            else if (drink == "台式抹茶")
                return "matchaTea";
            else if (drink == "阿里山烏龍茶")
                return "oolongTea";
            else if (drink == "梅子綠茶")
                return "greengageTea";
            else if (drink == "百香綠茶")
                return "passionFruitTea";
            else if (drink == "高山普洱茶")
                return "puerTea";
            else if (drink == "洛神花茶")
                return "roselleTea";
            else if (drink == "多多綠茶")
                return "yakultTea";
            else
                return "";
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox box = sender as CheckBox;
            DataRowView selectRow = _orderListGrid.GetFocusedRow() as DataRowView;

            String account = selectRow.Row.ItemArray[0].ToString();
            String date = (Convert.ToDateTime(selectRow.Row.ItemArray[1].ToString())).ToString("yyyy-MM-dd HH:mm:ss") + ".000";
            String ps = selectRow.Row.ItemArray[2].ToString();
            String drinkName = selectRow.Row.ItemArray[4].ToString();
            String quantity = selectRow.Row.ItemArray[5].ToString();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["DataBaseProject.Properties.Settings.NTUT_DataBaseConnectionString"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand("UPDATE [dbo].[Has] SET	[MakeState]='" + box.IsChecked.ToString() + "' WHERE ([FKOid] IN ( SELECT OrderRecord.Oid FROM [dbo].OrderRecord WHERE FKAccount='" + account + "' and ConfirmDate='" + date + "' and PS='"+ps+"' and ConfirmState='true')) and ([FKName]='" + GetMapping(drinkName) + "') and ([Quantity]='" + quantity + "')", connection);
            command.ExecuteScalar();
            connection.Close();
        }

        private void MouseUpRefreshButton(object sender, MouseButtonEventArgs e)
        {
            TextBlock block = sender as TextBlock;
            block.Foreground = Brushes.White;
            block.Background = Brushes.Black;
            PageSwitcher.Switch(new OrderListForm());
        }

        private void MouseDownRefreshButton(object sender, MouseButtonEventArgs e)
        {
            TextBlock block = sender as TextBlock;
            block.Foreground = Brushes.Black;
            block.Background = Brushes.LightGray;
        }

        private void MouseEnterRefreshButton(object sender, MouseEventArgs e)
        {
            TextBlock block = sender as TextBlock;
            block.Foreground = Brushes.Gray;
            block.Background = Brushes.White;
        }

        private void MouseLeaveRefreshButton(object sender, MouseEventArgs e)
        {
            TextBlock block = sender as TextBlock;
            block.Foreground = Brushes.White;
            block.Background = Brushes.Black;
        }
    }
}
