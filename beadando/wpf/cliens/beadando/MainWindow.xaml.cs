using System;
using System.Collections.Generic;
using System.Configuration;
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
using RestSharp;
using RestSharp.Serialization.Json;
using System.Net.Http;
using Newtonsoft.Json;

namespace beadando
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RestClient restClient = null;
        user User = null;
        List<products> Prodicts = new List<products>();
        char[] charsToTrim = { '[', '{' ,'"'};

        public MainWindow()
        {
            InitializeComponent();
            List();
        }

        private void List()
        {
            using (var client = new HttpClient())
            {
                Prodicts = new List<products>();
                var endpoint = new Uri("http://localhost/REST/index.php?products=0");
                var resault = client.GetAsync(endpoint).Result;
                var json = resault.Content.ReadAsStringAsync().Result;

                string[] lines = json.Split("},");
                for (int i = 0; i < lines.Length; i++)
                {
                    products PRduct = new products();
                    
                    string[] cateoy = lines[i].Trim(charsToTrim).Split(",");
                    PRduct.id = int.Parse(cateoy[0].Split(":")[1].Split('"')[1]);
                    PRduct.category = cateoy[1].Split(":")[1].Split('"')[1];
                    PRduct.name = cateoy[2].Split(":")[1].Split('"')[1];
                    PRduct.description = cateoy[3].Split(":")[1].Split('"')[1];
                    PRduct.price = cateoy[4].Split(":")[1].Split('"')[1];
                    PRduct.stock = cateoy[5].Split(":")[1].Split("}]")[0].Split('"')[1];
                    Prodicts.Add(PRduct);
                }
                ListPlace.ItemsSource = Prodicts;
            }
        }

        private void ListButtob_Click(object sender, RoutedEventArgs e)
        {
            List();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (User == null)
            {
                using (var client = new HttpClient())
                {
                    var endpoint = new Uri("http://localhost/REST/index.php?users=login");

                    var newPost = new login()
                    {
                        name = LoginName.Text,
                        password = LoginPw.Text
                    };
                    var newPostJson = JsonConvert.SerializeObject(newPost);
                    var payLoad = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    var result = client.PostAsync(endpoint, payLoad).Result.Content.ReadAsStringAsync().Result;
                    if (result != "null")
                    {
                        user Usre = new user();
                        string[] cateoy = result.Trim(charsToTrim).Split(",");
                        Usre.id = int.Parse(cateoy[0].Split(":")[1].Split('"')[1]);
                        Usre.name = cateoy[1].Split(":")[1].Split('"')[1];
                        Usre.admin = cateoy[2].Split(":")[1].Split("}]")[0].Split('"')[1];
                        User = Usre;
                        LoginButton.Content = "Logout";
                        LoginName.Text = "";
                        LoginPw.Text = "";
                        NameLabel.Content = User.name.ToString();
                        if (User.admin == "admin")
                        {
                            AdminLabel.Content = "1";
                        }
                        else
                        {
                            AdminLabel.Content = null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid user");
                    }

                }

                if (LoginName.Text == null || LoginPw.Text == null)
                {
                    MessageBox.Show("You must set login datas");
                    return;
                }
            }
            else
            {
                User = null;
                NameLabel.Content = null;
                AdminLabel.Content = null;
                LoginButton.Content = "Login";
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (User != null && User.admin == "admin")
            {
                using (var client = new HttpClient())
                {
                    var endpoint = new Uri("http://localhost/REST/index.php?products");

                    var newPost = new ProductAdd()
                    {
                        category = category.Text,
                        name = name.Text,
                        description = decpition.Text,
                        price = price.Text
                    };
                    var newPostJson = JsonConvert.SerializeObject(newPost);
                    var payLoad = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    var result = client.PostAsync(endpoint, payLoad).Result.Content.ReadAsStringAsync().Result;
                }
                List();
            }
            else
            MessageBox.Show("You have to login as admin");
        }
        

        private void RentButton_Click(object sender, RoutedEventArgs e)
        {
            if (User != null)
            {
                using (var client = new HttpClient())
                {
                    var endpoint = new Uri("http://localhost/REST/index.php?products=rent");

                    var newPost = new ProductRent()
                    {

                        stock = User.id.ToString(),
                        id = id.Text
                    };
                    var newPostJson = JsonConvert.SerializeObject(newPost);
                    var payLoad = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    var result = client.PostAsync(endpoint, payLoad).Result.Content.ReadAsStringAsync().Result;
                }
                List();
            }
            else
                MessageBox.Show("You have to login");
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (User != null)
            {
                using (var client = new HttpClient())
                {
                    var endpoint = new Uri("http://localhost/REST/index.php?products=back");

                    var newPost = new ProductRent()
                    {
                        id = id.Text,
                        stock = User.id.ToString()
                    };
                    var newPostJson = JsonConvert.SerializeObject(newPost);
                    var payLoad = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    var result = client.PostAsync(endpoint, payLoad).Result.Content.ReadAsStringAsync().Result;
                }
                List();
            }
            else
                MessageBox.Show("You have to login");
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (User != null && User.admin == "admin")
            {
                using (var client = new HttpClient())
                {
                    var endpoint = new Uri("http://localhost/REST/index.php?products=delete");

                    var newPost = new ProductRent()
                    {
                        id = id.Text
                    };
                    var newPostJson = JsonConvert.SerializeObject(newPost);
                    var payLoad = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    var result = client.PostAsync(endpoint, payLoad).Result.Content.ReadAsStringAsync().Result;
                }
                List();
            }
            else
                MessageBox.Show("You have to login as admin");
        }

        private void ListPlace_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                id.Text = Prodicts[ListPlace.SelectedIndex].id.ToString();
                category.Text = Prodicts[ListPlace.SelectedIndex].category;
                name.Text = Prodicts[ListPlace.SelectedIndex].name;
                decpition.Text = Prodicts[ListPlace.SelectedIndex].description;
                price.Text = Prodicts[ListPlace.SelectedIndex].price;
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
