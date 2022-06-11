<Window x:Name="CatalogWin" x:Class="Project8.CatalogWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:Project8"
        mc:Ignorable="d"
        Title="Каталог" Height="900" Width="1600" Icon="Images/icon.ico" WindowStartupLocation="CenterScreen" MinWidth="1200" MinHeight="450">
    <Grid x:Name="catalogGrid" Margin="1,0,1,-21" MinWidth="795" MinHeight="445" Height="890" VerticalAlignment="Top">
        <Image x:Name="logoCatalogImage" Margin="24,10,0,0" Source="Images/logoGrid.png" Stretch="Fill" Height="64" VerticalAlignment="Top" HorizontalAlignment="Left" Width="64"/>
        <Label x:Name="logoCatalogLabel" Content="ООО &quot;Творчество&quot;" Margin="93,10,0,0" FontFamily="Comic Sans MS" FontSize="18" HorizontalContentAlignment="Center" VerticalContentAlignment="Center" Background="{x:Null}" Foreground="Black" FontWeight="Bold" Height="64" VerticalAlignment="Top" HorizontalAlignment="Left" Width="180"/>
        <Label x:Name="userLabel" Content="РЕЖИМ ГОСТЯ" HorizontalAlignment="Right" Margin="0,17,155,0" VerticalAlignment="Top" Height="50" Width="350" FontSize="16" FontFamily="Comic Sans MS" HorizontalContentAlignment="Center" VerticalContentAlignment="Center" FontWeight="Bold" Background="#FF76E383" BorderBrush="#FF498C51" BorderThickness="3"/>
        <Button x:Name="catalogExitButton" Content="ВЫЙТИ" Margin="0,17,10,0" HorizontalAlignment="Right" Width="140" MinWidth="140" MinHeight="50" Background="#FF76E383" BorderBrush="#FF498C51" Foreground="Black" BorderThickness="3" FontFamily="Comic Sans MS" FontSize="16" FontWeight="Bold" Click="catalogExitButton_Click" Height="50" VerticalAlignment="Top"/>
        <ListView x:Name="catalogListView" Margin="90,131,90,169" Background="#FF76E383" BorderBrush="#FF498C51" Foreground="Black" BorderThickness="3" MinWidth="705" MinHeight="295">
            <ListView.View>
                <GridView>
                    <GridViewColumn Header="Изображение товара" Width="300">
                        <GridViewColumn.CellTemplate>
                            <DataTemplate>
                                <StackPanel>
                                    <Image Width="260" Source="{Binding ProductPhoto}"/>
                                </StackPanel>
                            </DataTemplate>
                        </GridViewColumn.CellTemplate>
                    </GridViewColumn>
                    <GridViewColumn Header="Информация о товаре" Width="800">
                        <GridViewColumn.CellTemplate>
                            <DataTemplate>
                                <StackPanel>
                                    <TextBlock Width="760" TextWrapping="Wrap" Text="{Binding ProductArticleNumber}"/>
                                    <TextBlock Width="760" TextWrapping="Wrap" Text="{Binding ProductName}"/>
                                    <TextBlock Width="760" TextWrapping="Wrap" Text="{Binding ProductDescription}"/>
                                    <TextBlock Width="760" TextWrapping="Wrap" Text="{Binding ProductManufacturer}"/>
                                    <TextBlock Width="760" TextWrapping="Wrap" Text="{Binding ProductCost}"/>
                                </StackPanel>
                            </DataTemplate>
                        </GridViewColumn.CellTemplate>
                    </GridViewColumn>
                    <GridViewColumn Header="Наличие на складе" Width="300" DisplayMemberBinding="{Binding ProductQuantityInStock}"/>
                </GridView>
            </ListView.View>
        </ListView>
        <Border x:Name="catalogBorder" BorderBrush="#FF498C51" BorderThickness="3" Margin="0,745,90,45" Background="#FF76E383" HorizontalAlignment="Right" Width="736">
            <Grid x:Name="catalogControlGrid">
                <Button x:Name="addButton" Content="ДОБАВИТЬ" Margin="45,22,485,22" Background="#FF76E383" BorderBrush="#FF498C51" FontFamily="Comic Sans MS" FontSize="16" BorderThickness="3" FontWeight="Bold" Width="200" Click="addButton_Click"/>
                <Button x:Name="deleteButton" Content="УДАЛИТЬ" Margin="265,22" Background="#FF76E383" BorderBrush="#FF498C51" FontFamily="Comic Sans MS" FontSize="16" BorderThickness="3" FontWeight="Bold" Click="deleteButton_Click"/>
                <Button x:Name="editButton" Content="РЕДАКТИРОВАТЬ" Margin="485,22,45,22" Background="#FF76E383" BorderBrush="#FF498C51" FontFamily="Comic Sans MS" FontSize="16" BorderThickness="3" FontWeight="Bold" Click="editButton_Click"/>
            </Grid>
        </Border>
        <Label x:Name="roleLabel" Content="" Margin="0,17,510,0" VerticalAlignment="Top" Height="50" HorizontalAlignment="Right" Width="250" Background="#FF76E383" BorderBrush="#FF498C51" HorizontalContentAlignment="Center" VerticalContentAlignment="Center" FontFamily="Comic Sans MS" FontSize="16" BorderThickness="3" FontWeight="Bold" Visibility="Hidden"/>
    </Grid>
</Window>

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
using System.Windows.Shapes;

namespace Project8
{
    /// <summary>
    /// Логика взаимодействия для CatalogWindow.xaml
    /// </summary>
    public partial class CatalogWindow : Window
    {
        public CatalogWindow()
        {
            InitializeComponent();

            catalogListView.Items.Clear();
            Classes.LoadProducts loadProducts = new Classes.LoadProducts();
            loadProducts.loadProducts(this);
        }

        private void catalogExitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AuthorizationWindow AW = new AuthorizationWindow();
                AW.Show();
                this.Close();
            }
            catch (Exception)
            {

                MessageBox.Show("Ошибка выхода");
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddWindow AddW = new AddWindow();
            AddW.Show();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            /*DeleteWindow DelW = new DeleteWindow();
            DelW.Show();*/
            dynamic itemSelectList = catalogListView.SelectedItem;
            if (itemSelectList != null)
            {
                Classes.DeleteProducts deleteProducts = new Classes.DeleteProducts();
                deleteProducts.deleteProducts(this);
            }
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            EditWindow EditW = new EditWindow();
            EditW.Show();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace Project8.Classes
{
    class LoadProducts
    {
        public class products
        {
            public string ProductArticleNumber { get; set; }
            public string ProductPhoto { get; set; }
            public string ProductName { get; set; }
            public string ProductDescription { get; set; }
            public string ProductManufacturer { get; set; }
            public string ProductCost { get; set; }
            public string ProductQuantityInStock { get; set; }
        }

        public void loadProducts(CatalogWindow catwin)
        {
            Classes.Connection connection = new Classes.Connection();
            DataTable dt_TradeProduct = connection.Select("SELECT * FROM [dbo].[Product]"); 
            for (int i = 0; i < dt_TradeProduct.Rows.Count; i++) 
            {
                products dataProduct = new products()
                {
                    ProductArticleNumber = dt_TradeProduct.Rows[i][0].ToString(),
                    ProductPhoto = dt_TradeProduct.Rows[i][4].ToString(),
                    ProductName = dt_TradeProduct.Rows[i][1].ToString(),
                    ProductDescription = dt_TradeProduct.Rows[i][2].ToString(),
                    ProductManufacturer = dt_TradeProduct.Rows[i][5].ToString(),
                    ProductCost = dt_TradeProduct.Rows[i][6].ToString(),
                    ProductQuantityInStock = dt_TradeProduct.Rows[i][9].ToString(),
                };
                catwin.catalogListView.Items.Add(dataProduct); 
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace Project8.Classes
{
    class RecordProducts
    {
        public void recordProducts(AddWindow AddW)
        {
            DataTable dataTable = new DataTable("dataBase");
            SqlConnection sqlConnection = new SqlConnection("server=DESKTOP-7NIK29D\\SQLEXPRESS;Trusted_Connection=Yes;DataBase=Trade;");
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = "Insert into Product (ProductArticleNumber, ProductPhoto,ProductName,ProductDescription,ProductManufacturer,ProductCost,ProductQuantityInStock)values(@PNumber, @PPhoto,@PName,@PDescription,@PManufacturer,@PCost,@PQuantityInStock)";
            sqlCommand.Parameters.AddWithValue("@PNumber", AddW.numTextBox.Text);
            sqlCommand.Parameters.AddWithValue("@PPhoto", AddW.photoTextBox.Text);
            sqlCommand.Parameters.AddWithValue("@PName", AddW.nameTextBox.Text);
            sqlCommand.Parameters.AddWithValue("@PDescription", AddW.descriptionTextBox.Text);
            sqlCommand.Parameters.AddWithValue("@PManufacturer", AddW.manufacturerTextBox.Text);
            sqlCommand.Parameters.AddWithValue("@PCost", AddW.costTextBox.Text);
            sqlCommand.Parameters.AddWithValue("@PQuantityInStock", AddW.quantityInStockTextBox.Text);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);

            CatalogWindow catwin = new CatalogWindow();
            Classes.LoadProducts loadProducts = new Classes.LoadProducts();
            loadProducts.loadProducts(catwin);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace Project8.Classes
{
    class DeleteProducts
    {
        public void deleteProducts(CatalogWindow catwin)
        {
            dynamic itemSelectList = catwin.catalogListView.SelectedItem;
            var id = itemSelectList.ProductArticleNumber;
            DataTable dataTable = new DataTable("dataBase");
            SqlConnection sqlConnection = new SqlConnection("server=DESKTOP-7NIK29D\\SQLEXPRESS;Trusted_Connection=Yes;DataBase=Trade;");
            sqlConnection.Open();                                           
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = "DELETE FROM OrderProduct WHERE [ProductArticleNumber] = @id";
            sqlCommand.CommandText = "DELETE FROM Product WHERE [ProductArticleNumber] = @id";
            sqlCommand.Parameters.AddWithValue("@id", id); 
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand); 
            sqlDataAdapter.Fill(dataTable); ;
            catwin.catalogListView.Items.Clear();

            Classes.LoadProducts loadProducts = new Classes.LoadProducts();
            loadProducts.loadProducts(catwin);

        }
    }
}
