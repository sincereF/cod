<Window x:Name="AuthorizationWin" x:Class="Project8.AuthorizationWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:Project8"
        mc:Ignorable="d"
        Title="Авторизация" Height="600" Width="800" WindowStartupLocation="CenterScreen" ResizeMode="NoResize" Icon="Images/icon.ico">
    <Grid x:Name="authorizationGrid">
        <Image x:Name="logoImage" Margin="10,10,0,0" Source="Images/logoGrid.png" Stretch="Fill" Width="64" Height="64" HorizontalAlignment="Left" VerticalAlignment="Top"/>
        <Label x:Name="logoLabel" Content="ООО &quot;Творчество&quot;" HorizontalAlignment="Left" Margin="79,10,0,0" VerticalAlignment="Top" FontFamily="Comic Sans MS" FontSize="18" Height="64" HorizontalContentAlignment="Center" VerticalContentAlignment="Center" Background="{x:Null}" Foreground="Black" FontWeight="Bold" Width="180"/>
        <Button x:Name="loginGuestButton" Content="Гостевой режим" Margin="636,10,0,0" VerticalAlignment="Top" Height="50" FontFamily="Comic Sans MS" FontSize="16" Background="#FF76E383" BorderBrush="#FF498C51" Foreground="Black" FontWeight="Bold" Padding="1" BorderThickness="3" HorizontalAlignment="Left" Width="140" Click="loginGuestButton_Click"/>
        <Border x:Name="authorizationBorder" BorderBrush="#FF498C51" BorderThickness="3" Margin="177,79,0,0" HorizontalAlignment="Left" Width="440" Height="438" VerticalAlignment="Top" Background="#FF76E383">
            <Grid x:Name="authorizationCentreGrid">
                <TextBox x:Name="loginTextBox" Height="30" Margin="67,80,0,0" TextWrapping="Wrap" VerticalAlignment="Top" Width="300" HorizontalAlignment="Left" VerticalContentAlignment="Center" FontFamily="Comic Sans MS" FontSize="14"/>
                <TextBox x:Name="passwordTextBox" HorizontalAlignment="Left" Height="30" Margin="67,140,0,0" TextWrapping="Wrap" VerticalAlignment="Top" Width="300" FontSize="14" FontFamily="Comic Sans MS" VerticalContentAlignment="Center"/>
                <TextBox x:Name="captchaGenTextBox" Height="30" Margin="97,246,132,0" TextWrapping="Wrap" VerticalAlignment="Top" HorizontalContentAlignment="Center" VerticalContentAlignment="Center" FontSize="20" FontFamily="Kristen ITC" IsReadOnly="True" Visibility="Hidden"/>
                <TextBox x:Name="captchaTextBox" HorizontalAlignment="Left" Height="30" Margin="97,281,0,0" TextWrapping="Wrap" VerticalAlignment="Top" Width="240" VerticalContentAlignment="Center" FontSize="14" FontFamily="Comic Sans MS" Visibility="Hidden"/>
                <Label x:Name="loginLabel" Content="Логин" HorizontalAlignment="Left" Margin="67,50,0,0" VerticalAlignment="Top" FontSize="16" FontFamily="Comic Sans MS"/>
                <Label x:Name="passwordLabel" Content="Пароль" HorizontalAlignment="Left" Margin="67,110,0,0" VerticalAlignment="Top" FontSize="16" FontFamily="Comic Sans MS"/>
                <Label x:Name="captchaLabel" Content="Пройдите проверку" HorizontalAlignment="Left" Margin="97,215,0,0" VerticalAlignment="Top" FontSize="16" FontFamily="Comic Sans MS" Visibility="Hidden"/>
                <Button x:Name="loginButton" Content="ВОЙТИ" Margin="127,355,127,0" Width="180" FontSize="24" FontFamily="Comic Sans MS" FontWeight="Bold" Height="50" VerticalAlignment="Top" BorderBrush="#FF498C51" Background="#FF76E383" BorderThickness="3" Click="loginButton_Click"/>
                <Button x:Name="refreshButton" Content="О" HorizontalAlignment="Left" Margin="307,246,0,0" VerticalAlignment="Top" Width="30" Height="30" FontFamily="Comic Sans MS" Background="#FF76E383" BorderBrush="#FF498C51" FontSize="14" BorderThickness="3" Foreground="Black" FontWeight="Bold" Click="refreshButton_Click" Visibility="Hidden"/>
                <CheckBox x:Name="showPasswordCheckBox" Content="Показать пароль" HorizontalAlignment="Left" Margin="67,174,0,0" VerticalAlignment="Top" FontSize="13" FontFamily="Comic Sans MS" Click="showPasswordCheckBox_Click"/>
                <PasswordBox x:Name="passwordBox" HorizontalAlignment="Left" Margin="67,140,0,0" VerticalAlignment="Top" Width="300" Height="30" FontSize="14" VerticalContentAlignment="Center"/>
                <Label x:Name="blockLabel" Content="" Margin="27,354,27,0" VerticalAlignment="Top" Height="50" VerticalContentAlignment="Center" HorizontalContentAlignment="Center" FontFamily="Comic Sans MS" FontSize="20" FontWeight="Bold" Visibility="Hidden"/>
            </Grid>
        </Border>
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Data;
using System.Data.SqlClient;

namespace Project8
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public DispatcherTimer timer = new DispatcherTimer();

        public int intrerv = 10;

        public AuthorizationWindow()
        {
            InitializeComponent();

            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(timer_Tick);
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            loginButton.Visibility = Visibility.Hidden;
            blockLabel.Visibility = Visibility.Visible;

            intrerv--;
            if (intrerv == 0)
            {
                loginButton.Visibility = Visibility.Visible;
                blockLabel.Visibility = Visibility.Hidden;

                Classes.CaptchaGenerator captchaGenerator = new Classes.CaptchaGenerator();
                captchaGenerator.captchaGenerator(this);
                timer.Stop();
                intrerv = 10;
            }
            blockLabel.Content = "Блокировка авторизации " + intrerv.ToString() + " секунд";
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            Classes.Login login = new Classes.Login();
            login.login(this);
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            Classes.Refresh refresh = new Classes.Refresh();
            refresh.refresh(this);
        }

        private void showPasswordCheckBox_Click(object sender, RoutedEventArgs e)
        {
            Classes.ShowPassword showPassword = new Classes.ShowPassword();
            showPassword.showPassword(this);
        }

        private void loginGuestButton_Click(object sender, RoutedEventArgs e)
        {
            CatalogWindow catwin = new CatalogWindow();
            catwin.Show();
            AuthorizationWin.Close();
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
    class Connection
    {
        public DataTable Select(string selectSQL)
        {
            DataTable dataTable = new DataTable("dataBase");
            SqlConnection sqlConnection = new SqlConnection("server=DESKTOP-7NIK29D\\SQLEXPRESS;Trusted_Connection=Yes;DataBase=Trade;");
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = selectSQL;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
    }
}

using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project8.Classes
{
    class CaptchaGenerator
    {
        public void captchaGenerator(AuthorizationWindow AW)
        {
            AW.captchaTextBox.Clear();
            AW.captchaGenTextBox.Visibility = System.Windows.Visibility.Visible;
            AW.captchaTextBox.Visibility = System.Windows.Visibility.Visible;
            AW.refreshButton.Visibility = System.Windows.Visibility.Visible;
            AW.captchaLabel.Visibility = System.Windows.Visibility.Visible;

            char[] chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789".ToCharArray();
            string randomString = "";
            Random ran = new Random();
            for (int i = 0; i < 5; i++)
            {
                randomString += chars[ran.Next(0, chars.Length)];
            }
            AW.captchaGenTextBox.Text = randomString;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project8.Classes
{
    class Refresh
    {
        public void refresh(AuthorizationWindow AW)
        {
            String allowchar = "";
            allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";
            allowchar += "1,2,3,4,5,6,7,8,9,0";
            char[] a = { ',' };
            String[] ar = allowchar.Split(a);
            String pwd = "";
            string temp = "";

            Random r = new Random();
            for (int i = 0; i < 4; i++)
            {
                temp = ar[(r.Next(0, ar.Length))];
                pwd += temp;
            }

            if (AW.captchaGenTextBox.Text != "")
            {
                AW.captchaGenTextBox.Text = null;
            }

            AW.captchaGenTextBox.Text = pwd;
        }
    }
}

using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project8.Classes
{
    class ShowPassword
    {
        public void showPassword(AuthorizationWindow AW)
        {
            if (AW.showPasswordCheckBox.IsChecked == true)
            {
                AW.passwordTextBox.Text = AW.passwordBox.Password;
                AW.passwordTextBox.Visibility = Visibility.Visible;
                AW.passwordBox.Visibility = Visibility.Hidden;
            }
            else
            {
                AW.passwordBox.Password = AW.passwordTextBox.Text;
                AW.passwordTextBox.Visibility = Visibility.Hidden;
                AW.passwordBox.Visibility = Visibility.Visible;
            }
        }
    }
}

using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace Project8.Classes
{
    class Login
    {
        string log;
        int index;
        public void login(AuthorizationWindow AW)
        {
            if (AW.loginTextBox.Text.Length > 0)
            {
                if (AW.passwordBox.Password.Length > 0)
                {
                    if (AW.captchaTextBox.Text == AW.captchaGenTextBox.Text || AW.captchaGenTextBox.Text.Length == 0)
                    {
                        Classes.Connection connection = new Classes.Connection();
                        DataTable dt_Trade = connection.Select("SELECT * FROM [dbo].[User] WHERE [UserLogin] = '" + AW.loginTextBox.Text + "' AND [UserPassword] = '" + AW.passwordBox.Password + "'");
                        if (dt_Trade.Rows.Count > 0)
                        {
                            dt_Trade = connection.Select("SELECT * FROM [dbo].[User]");
                            for (int i = 0; i < dt_Trade.Rows.Count; i++)
                            {
                                log = dt_Trade.Rows[i][4].ToString();
                                if (log == AW.loginTextBox.Text.ToString())
                                {
                                    index = i;
                                }

                            }
                            CatalogWindow catwin = new CatalogWindow();
                            catwin.roleLabel.Visibility = Visibility.Visible;
                            if (dt_Trade.Rows[index][6].ToString() == "1")
                            {
                                catwin.roleLabel.Content = "Администратор";
                                catwin.catalogBorder.Visibility = Visibility.Visible;
                            }
                            if (dt_Trade.Rows[index][6].ToString() == "2")
                            {
                                catwin.roleLabel.Content = "Менеджер";
                            }
                            if (dt_Trade.Rows[index][6].ToString() == "3")
                            {
                                catwin.roleLabel.Content = "Клиент";
                            }
                            catwin.userLabel.Content = dt_Trade.Rows[index][1].ToString() + " " + dt_Trade.Rows[index][2].ToString() + " " + dt_Trade.Rows[index][3].ToString();
                            catwin.Show();
                            AW.Close();
                        }
                        else
                        {
                            MessageBox.Show("Неверный логин или пароль");
                            {
                                Classes.CaptchaGenerator captchaGenerator = new Classes.CaptchaGenerator();
                                captchaGenerator.captchaGenerator(AW);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Каптча неверная");
                        {
                            AW.timer.Start();
                            Classes.CaptchaGenerator captchaGenerator = new Classes.CaptchaGenerator();
                            captchaGenerator.captchaGenerator(AW);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Пароль отсутствует");
                }
            }
            else
            {
                MessageBox.Show("Логин отсутствует");
            }
        }
    }
}
