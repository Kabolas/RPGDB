using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace RPG_Jahr_words
{
    public enum Imaging
    {
        Continent_ville,
        Continent_region,
        Ville,
        Regions,
        Persos,
        Creature,
        Objet,
    }

    public class ComboColorItem
    {
        private Brush _color;
        private string name;

        public Brush Color { get => _color; set => _color = value; }
        public string Name { get => name; set => name = value; }
    }
    /// <summary>
    /// Logique d'interaction pour AddImage.xaml
    /// </summary>
    public partial class AddImage : Window
    {
        private Object _toImage;
        private double width, height;
        private Imaging _imageFor;
        private Tabitem _tabitem;
        private string _nameToPrint;
        private bool _draws;
        private bool drawing = false;
        private Polygon polygon;
        private List<Brushes> colors = new List<Brushes>();
        //private Image paste;
        public Imaging ImageFor
        {
            get => _imageFor; set
            {
                _imageFor = value; //SearchBut.IsEnabled = ImageFor == Imaging.Continent;
                                   //SaveBut.IsEnabled = !Draws;
                switch (ImageFor)
                {
                    case Imaging.Continent_ville:
                        Draws = false;
                        break;
                    case Imaging.Continent_region:
                        Draws = false;
                        break;
                    case Imaging.Ville:
                        ImageSource Villesou = new BitmapImage(new Uri((ToImage as Ville).Region1.Continent1.carte_villes));
                        PrintedName.Text = (ToImage as Ville).Region1.Continent1.carte_villes;
                        width = Canva.Width = Width = Villesou.Width;
                        Height = Villesou.Height + 37.0;
                        height = Canva.Height = Villesou.Height;
                        Canva.Background = new ImageBrush
                        {
                            Stretch = Stretch.UniformToFill,
                            ImageSource = Villesou
                        };
                        SearchBut.IsEnabled = false;
                        ResizeMode = ResizeMode.NoResize;
                        switch ((ToImage as Ville).Ville_type.type)
                        {
                            case "Abandonnée":
                                paste.Source = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/Ressources/marks/StandardTown.png", UriKind.Absolute));
                                break;
                            case "Capitale":
                                paste.Source = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/Ressources/marks/StandardCapital.png", UriKind.Absolute));
                                break;
                            case "Maudite":
                                paste.Source = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/Ressources/marks/StandardTown.png", UriKind.Absolute));
                                break;
                            case "Sousterraine":
                                paste.Source = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/Ressources/marks/StandardTown.png", UriKind.Absolute));
                                break;
                            case "Village":
                                paste.Source = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/Ressources/marks/StandardVillage.png", UriKind.Absolute));
                                break;
                            case "Ville":
                                paste.Source = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/Ressources/marks/StandardTown.png", UriKind.Absolute));
                                break;
                            default:
                                break;
                        }
                        paste.Height = paste.Source.Height;
                        paste.Width = paste.Source.Width;
                        break;
                    case Imaging.Regions:
                        Color.Visibility = Visibility.Visible;
                        Color.IsEnabled = true;
                        foreach (PropertyInfo prop in typeof(Brushes).GetProperties())
                            Color.Items.Add(new ComboColorItem { Color = prop.GetValue(null, null) as Brush, Name = prop.Name });
                        //SearchBut.IsEnabled = false;
                        Draws = true;
                        //ImageSource Regsou = new BitmapImage(new Uri((ToImage as Region).Continent1.carte_region));
                        //width = Width = Canva.Width = Regsou.Width;
                        //Canva.Height = Regsou.Height;
                        //height = Height = Canva.Height + 37;
                        //PrintedName.Text = (ToImage as Region).Continent1.carte_region;
                        //Canva.Background = new ImageBrush
                        //{
                        //    ImageSource = Regsou,
                        //    Stretch = Stretch.UniformToFill
                        //};
                        break;
                    case Imaging.Persos:
                        Draws = false;
                        break;
                    case Imaging.Creature:
                        Draws = false;
                        break;
                    case Imaging.Objet:
                        Draws = false;
                        break;
                    default:
                        break;
                }
            }
        }
        public bool Draws { get => _draws; set => _draws = value; }
        public string NameToPrint { get => _nameToPrint; set => TextToPrint.Text = _nameToPrint = value; }
        public object ToImage { get => _toImage; set => _toImage = value; }

        public AddImage()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void SearchImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog searchFile = new OpenFileDialog { Multiselect = false, Filter = "Image files | *.png; *.jpeg; *.bmp;" };
            if (searchFile.ShowDialog() == true)
            {
                PrintedName.Text = searchFile.SafeFileName;
                ImageSource sou = new BitmapImage(new Uri(searchFile.FileName, UriKind.Absolute));
                width = Canva.Width = Width = sou.Width;
                Height = sou.Height + 37.0;
                height = Canva.Height = sou.Height;
                Canva.Background = new ImageBrush
                {
                    Stretch = Stretch.UniformToFill,
                    ImageSource = sou
                };
            }
        }

        private void SaveImage(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog
            {
                InitialDirectory = System.IO.Directory.GetCurrentDirectory() + "\\Ressources\\InDB\\",
                Filter = "Image files | *.png; *.jpeg; *.bmp;",
                AddExtension = true,
                DefaultExt = ".png",
            };
            if (save.ShowDialog() == true)
            {
                RenderTargetBitmap rtb = new RenderTargetBitmap((int)Canva.RenderSize.Width, (int)Canva.RenderSize.Height, 96d, 96d, System.Windows.Media.PixelFormats.Default);
                rtb.Render(Canva);

                BitmapEncoder coder = new PngBitmapEncoder();
                coder.Frames.Add(BitmapFrame.Create(rtb));
                using (var fs = System.IO.File.OpenWrite(save.FileName))
                    coder.Save(fs);
            }
        }

        private void Pass(object sender, RoutedEventArgs e)
        {

        }

        private void ImageMoove(object sender, MouseEventArgs e)
        {
            if (Canva.IsMouseOver && Draws)
            {
                switch (ImageFor)
                {
                    case Imaging.Ville:
                        Point point = e.GetPosition(Canva);
                        paste.SetValue(Canvas.TopProperty, point.Y - paste.Height);
                        paste.SetValue(Canvas.LeftProperty, point.X - paste.Width / 2);
                        TextToPrint.SetValue(Canvas.LeftProperty, point.X - paste.Width / 2);
                        TextToPrint.SetValue(Canvas.TopProperty, point.Y - 14 - paste.Height);
                        return;
                    case Imaging.Regions:

                        return;
                    default:
                        return;
                }
            }
        }

        private void SetImagePos(object sender, MouseButtonEventArgs e)
        {
            switch (ImageFor)
            {
                case Imaging.Continent_ville:
                case Imaging.Continent_region:
                    break;
                case Imaging.Ville:
                    Draws = false;
                    Point point = e.GetPosition(Canva);
                    paste.SetValue(Canvas.TopProperty, point.Y - paste.Height);
                    paste.SetValue(Canvas.LeftProperty, point.X - paste.Width / 2);
                    TextToPrint.SetValue(Canvas.LeftProperty, point.X - paste.Width / 2);
                    TextToPrint.SetValue(Canvas.TopProperty, point.Y - 14 - paste.Height);
                    return;
                case Imaging.Regions:
                    if (Draws)
                    {
                        if (!drawing)
                        {
                            polygon = new Polygon
                            {
                                StrokeThickness = 2,
                                Stroke = (Color.SelectedItem as ComboColorItem)?.Color,
                                FillRule = FillRule.Nonzero,
                                Fill = (Color.SelectedItem as ComboColorItem)?.Color,
                            };
                            drawing = true;
                            Canva.Children.Add(polygon);
                        }
                        polygon.Points.Add(e.GetPosition(Canva));
                    }
                    else if (Color.SelectedItem != null)
                    {
                        TextToPrint.Text = NameToPrint;
                        TextToPrint.Foreground = (Color.SelectedItem as ComboColorItem).Color;
                        TextToPrint.SetValue(Canvas.LeftProperty, e.GetPosition(Canva).X);
                        TextToPrint.SetValue(Canvas.TopProperty, e.GetPosition(Canva).Y);
                    }
                    break;
                case Imaging.Persos:
                    break;
                case Imaging.Creature:
                    break;
                case Imaging.Objet:
                    break;
                default:
                    break;
            }

        }

        private void EndDraw(object sender, MouseButtonEventArgs e)
        {
            switch (ImageFor)
            {
                case Imaging.Continent_ville:
                    break;
                case Imaging.Continent_region:
                    break;
                case Imaging.Ville:
                    break;
                case Imaging.Regions:
                    drawing = Draws = false;
                    Color.SelectedIndex = -1;
                    Color.IsEnabled = true;
                    break;
                case Imaging.Persos:
                    break;
                case Imaging.Creature:
                    break;
                case Imaging.Objet:
                    break;
                default:
                    break;
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            switch (ImageFor)
            {
                case Imaging.Continent_ville:
                    break;
                case Imaging.Continent_region:
                    break;
                case Imaging.Ville:
                    break;
                case Imaging.Regions:
                    polygon.Points.Clear();
                    drawing = false;
                    Canva.Children.Remove(polygon);
                    TextToPrint.Text = "";
                    TextToPrint.Foreground = Brushes.Black;
                    Color.IsEnabled = true;
                    Color.SelectedItem = null;
                    break;
                case Imaging.Persos:
                    break;
                case Imaging.Creature:
                    break;
                case Imaging.Objet:
                    break;
                default:
                    break;
            }
        }

        private void TextColorChange(object sender, SelectionChangedEventArgs e)
        {
            switch (ImageFor)
            {
                case Imaging.Continent_ville:
                    break;
                case Imaging.Continent_region:
                    break;
                case Imaging.Ville:
                    break;
                case Imaging.Regions:
                    if ((sender as ComboBox).SelectedItem != null)
                        if (!Draws && !drawing)
                            TextToPrint.Foreground = ((sender as ComboBox).SelectedItem as ComboColorItem).Color;
                        else if (polygon != null) polygon.Fill = polygon.Stroke = ((sender as ComboBox).SelectedItem as ComboColorItem).Color;
                    break;
                case Imaging.Persos:
                    break;
                case Imaging.Creature:
                    break;
                case Imaging.Objet:
                    break;
                default:
                    break;
            }
        }
    }
}
