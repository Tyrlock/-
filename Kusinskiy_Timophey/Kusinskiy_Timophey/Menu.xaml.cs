using CoreLibarary.Interfaces;
using CoreLibarary.Models;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace Kusinskiy_Timophey
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        private IMenuService _MenusService;
        private IUsersService _UsersService;

        public Menu(IMenuService MenuService, IUsersService UsersService)
        {
            InitializeComponent();
            _MenusService = MenuService;
            _UsersService = UsersService;
        }

        public string path { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = _MenusService.Get();
            btn_del.IsEnabled = false;
            btn_edit.IsEnabled = false;
        }

        private void btn_del_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var del = ListViewMenu.SelectedItem as Dish;
                _MenusService.Delete(del.Id);
                _MenusService.Menu.Remove(del);
                ListViewMenu.ItemsSource = _MenusService.Get();
                clear();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Can't delite. Error: '{ex.Message}'", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var list = ListViewMenu.SelectedItem as Dish;
                var menu = new Dish();

                menu.Id = list.Id;
                menu.Cost = Convert.ToInt32(txtCost.Text);
                menu.Description = txtDescription.Text;
                switch (ComboBoxCategory.SelectedIndex)
                {
                    case 0:
                        menu.CategoryId = (Category)1;
                        break;
                    case 1:
                        menu.CategoryId = (Category)2;
                        break;
                    case 2:
                        menu.CategoryId = (Category)3;
                        break;
                    case 3:
                        menu.CategoryId = (Category)4;
                        break;
                    case 4:
                        menu.CategoryId = (Category)5;
                        break;
                }
                _MenusService.Update(menu);
                ListViewMenu.ItemsSource = _MenusService.Get();
                clear();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Can't Edit dish. Error: '{ex.Message}'", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtProductName.Text == ""
                    || txtDescription.Text == ""
                    || txtCost.Text == "")
                {
                    throw new Exception("Чтобы добавить пункт меню, все поля должны быть заполнены");
                }
                else
                {
                    int Cost = int.Parse(txtCost.Text);
                    int CategoryId = ComboBoxCategory.SelectedIndex;
                    var list = new Dish();
                    switch (CategoryId)
                    {
                        case 0:
                            CategoryId = 1;
                            break;
                        case 1:
                            CategoryId = 2;
                            break;
                        case 2:
                            CategoryId = 3;
                            break;
                        case 3:
                            CategoryId = 4;
                            break;
                        case 4:
                            CategoryId = 5;
                            break;
                    }
                    if (txtProductName.Text == list.ProductName)
                    {
                        throw new Exception("Данный пункт меню уже создан");
                    }
                    else
                    {
                        byte[] imageBytes;
                        if (path == null)
                        {
                            throw new Exception("Вы не выбрали изображение");
                        }
                        else
                        {
                            imageBytes = File.ReadAllBytes(path);
                        }

                        _MenusService.Add(txtProductName.Text, Cost, txtDescription.Text, CategoryId, _UsersService.CurrentUser.Id, DateTimeOffset.Now,imageBytes);
                        ListViewMenu.ItemsSource = _MenusService.Get();
                        clear();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Can't add dish: '{ex.Message}'", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void ListViewMenu_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                var list = ListViewMenu.SelectedItem as Dish;
                if (list != null)
                {
                    txtProductName.Text = list.ProductName;
                    txtCost.Text = list.Cost.ToString();
                    txtDescription.Text = list.Description;
                    ComboBoxCategory.SelectedIndex = Convert.ToInt32(list.CategoryId - 1);
                    btn_del.IsEnabled = true;
                    btn_edit.IsEnabled = true;
                    txtProductName.IsEnabled = false;
                    btn_image.IsEnabled = false;
                    btn_add.IsEnabled = false;
                    var stream = new MemoryStream(list.image);
                    var bitmapimage = new BitmapImage();
                    bitmapimage.BeginInit();
                    bitmapimage.StreamSource = stream;
                    bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapimage.EndInit();

                    image.Source = bitmapimage;
                }
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show($"Error: '{ex.Message}'", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void txtCost_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtProductName_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {

            var regex = new Regex("[^a-zA-Zа-яА-Я]+");
            e.Handled = regex.IsMatch(e.Text);

        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            clear();
        }

        public void clear()
        {
            txtProductName.IsEnabled = true;
            btn_edit.IsEnabled = false;
            btn_del.IsEnabled = false;
            btn_image.IsEnabled = true;
            btn_add.IsEnabled = true;
            txtProductName.Clear();
            txtCost.Clear();
            txtDescription.Clear();
            ComboBoxCategory.SelectedIndex = -1;
            ListViewMenu.SelectedIndex = -1;
            image.Source = null;
        }
        private void btn_image_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var list = ListViewMenu.SelectedItem as Dish;
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG, *.ICO, *.EMF, *.WMF)|*.bmp;*.jpg;*.gif; *.tif; *.png; *.ico; *.emf; *.wmf";
                fileDialog.ShowDialog();
                if (fileDialog.FileName == "")
                {
                    throw new Exception("Вы не выбрали изображения");
                }
                else
                {
                    path = fileDialog.FileName;
                    image.Source = new BitmapImage(new Uri(path));
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"'{ex.Message}'", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtDescription_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var regex = new Regex("[^a-zA-Zа-яА-Я0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
