using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using System.Speech.Synthesis;

namespace ZAMETKEE
{
    public partial class MainWindow : Window
    {
        public static string putch = @"";
        SpeechSynthesizer synth;
        public string Speech;

        public MainWindow()
        {
            InitializeComponent();
            pause.Visibility = Visibility.Hidden;
        }

        private void ShriftChange(object sender, EventArgs e)
        {
            string str = shrift.Text;
            tex.FontFamily = new FontFamily(str);
        }

        private void ColourShriftChange(object sender, EventArgs e)
        {
            string str = word.Text;
            switch (str)
            {
                case "green":
                    tex.Foreground = Brushes.Green;
                    break;
                case "black":
                    tex.Foreground = Brushes.Black;
                    break;
                case "red":
                    tex.Foreground = Brushes.Red;
                    break;
                case "yellow":
                    tex.Foreground = Brushes.Yellow;
                    break;
            }
        }

        private void ColourBackgroundChange(object sender, EventArgs e)
        {
            string str = back.Text;
            switch (str)
            {
                case "green":
                    tex.Background = Brushes.Green;
                    break;
                case "black":
                    tex.Background = Brushes.Black;
                    break;
                case "red":
                    tex.Background = Brushes.Red;
                    break;
                case "yellow":
                    tex.Background = Brushes.Yellow;
                    break;
                case "white":
                    tex.Background = Brushes.White;
                    break;
            }
        }

        private void CB(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.IsChecked == true & checkBox.Content.ToString() == "Bolt")
            {
                tex.FontWeight = FontWeights.Bold;
            }
            else if (checkBox.IsChecked == false & checkBox.Content.ToString() == "Bolt")
            {
                tex.FontWeight = FontWeights.Regular;
            }

            else if (checkBox.IsChecked == true & checkBox.Content.ToString() == "Cursive")
            {
                tex.FontStyle = FontStyles.Italic;

            }
            else
            {
                tex.FontStyle = FontStyles.Normal;
            }
        }

        private void SIZE(object sender, RoutedEventArgs e)
        {
            try
            {
                tex.FontSize = Int32.Parse(size.Text);
            }
            catch (FormatException)
            {
                tex.FontSize = 12;
            }

        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            using (StreamWriter stream = new StreamWriter(putch, false))
            {
                stream.WriteLine(tex.Text);
            }
            MessageBox.Show("Всё сохранено успешно");
        }

        public string GetPath()
        {
            try
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.Filter = "txt|*.txt"; // Фильтр файлов в проводнике
                Nullable<bool> result = dlg.ShowDialog();
                if (result == true)
                {
                    return dlg.FileName;
                }
                return null;
            }
            catch (Exception) { return null; }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                putch = GetPath();
                tex.Text = "";
                tex.Text = File.ReadAllText(putch);

            }
            catch (Exception) { }
        }

        private void Speake(object sender, RoutedEventArgs e)
        {
            try
            {
                Speech = File.ReadAllText(putch);
                synth = new SpeechSynthesizer();
                pause.Visibility = Visibility.Visible;
                speak.Visibility = Visibility.Hidden;
                synth.SetOutputToDefaultAudioDevice();
                synth.SpeakAsync(Speech);

            }
            catch (Exception) { }

        }

        private void Pause(object sender, RoutedEventArgs e)
        {
            try
            {
                speak.Visibility = Visibility.Visible;
                pause.Visibility = Visibility.Hidden;
                synth.SpeakAsyncCancelAll();
            }
            catch (Exception) { }
        }
    }
}