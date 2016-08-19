using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Naver_Webtoon_BGM_Downloader
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        List<BgmList> bgmList = new List<BgmList>();

        public string WebtoonId { get; set; }
        public string WebtoonNumber { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            textBox.Text = string.Empty;

            Color color = (Color)ColorConverter.ConvertFromString("#28dc19");
            Brush brush = new SolidColorBrush(color);
            textBox.BorderBrush = brush;

            SearchButton.IsEnabled = true;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text == string.Empty)
            {
                MessageBox.Show("주소를 입력해주세요");
            }
            else
            {
                //이전 목록페이지에 노출되는 썸네일을 가져오기 위해서 주소를 변경
                string ListPageUrl = textBox.Text.Replace("detail", "list");

                //웹툰 정보 넣어줌 (웹툰 id, 화수)
                Regex rxWebtoonNumber = new Regex("no=[0-9]+", RegexOptions.IgnoreCase & RegexOptions.IgnorePatternWhitespace);
                Regex rxWebtoonId = new Regex("Id=[0-9]+", RegexOptions.IgnoreCase & RegexOptions.IgnorePatternWhitespace);

                WebtoonNumber = rxWebtoonNumber.Match(ListPageUrl).ToString().Replace("no=", string.Empty);
                WebtoonId = rxWebtoonId.Match(ListPageUrl).ToString().Replace("Id=", string.Empty);

                //특정 화수를 나타내는 no=?를 제거
                Regex RemoveNumber = new Regex("no=[0-9]+", RegexOptions.IgnoreCase & RegexOptions.IgnorePatternWhitespace);
                ListPageUrl = ListPageUrl.Replace(RemoveNumber.Match(ListPageUrl).ToString(), string.Empty);

                for (int i = 0; i < GetMp3(GetHtml(textBox.Text)).Length; i++)
                {
                    StackPanel bgmListItem = new StackPanel();
                    bgmListItem.Orientation = Orientation.Horizontal;

                    bgmListItem.Children.Add(new Image
                    {
                        Width = 50,
                        Source = new BitmapImage(new Uri(GetImgs(GetHtml(ListPageUrl)), UriKind.RelativeOrAbsolute)),
                        Margin = new Thickness(5, 0, 5, 0)
                    });

                    bgmListItem.Children.Add(new TextBlock
                    {
                        Text = GetMp3(GetHtml(textBox.Text))[i],
                        FontSize = 15
                    });

                    BgmlistView.Items.Add(bgmListItem);
                }
            }
        }

        /// <summary>
        /// 웹툰의 썸네일 이미지 가져오기
        /// </summary>
        /// <param name="Html"></param>
        public string GetImgs(string Html)
        {
            //이미지 태그만 가져오기
            Regex rxImages = new Regex("<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase & RegexOptions.IgnorePatternWhitespace);
            MatchCollection mc = rxImages.Matches(Html);

            //찾은 이미지 태그들을 문자열로 넣어줌
            string[] ImageTags = new string[mc.Count];
            for (int i = 0; i < mc.Count; i++)
                ImageTags[i] = mc[i].ToString();

            //문자열 배열을 문자열로
            string ImageTag = ConvertStringArrayToString(ImageTags);

            //해당 화에 맞는 썸네일 이미지 가져오기
            Regex rxMatchImage = new Regex("http://thumb.comic.naver.net/webtoon/" + WebtoonId + "/" + WebtoonNumber + "+" + "/inst_thumbnail_.+?.jpg", RegexOptions.IgnoreCase & RegexOptions.IgnorePatternWhitespace);
            ImageTag = rxMatchImage.Match(ImageTag).ToString();

            return ImageTag;
        }

        /// <summary>
        /// 추출한 Html에서 mp3주소만 추출해줌 
        /// </summary>
        /// <param name="Html"></param>
        private string[] GetMp3(string Html)
        {
            Regex rxMp3 = new Regex("\".+.mp3\"", RegexOptions.IgnoreCase & RegexOptions.IgnorePatternWhitespace);
            MatchCollection mc = rxMp3.Matches(Html);

            string[] mp3Urls = new string[mc.Count];
            for (int i = 0; i < mc.Count; i++)
            {
                //큰따옴표 지워주기
                mp3Urls[i] = mc[i].ToString().Replace("\"", string.Empty);
            }

            return mp3Urls;
        }

        private string GetHtml(string Url)
        {
            HttpWebRequest WebtoonRequest = (HttpWebRequest)WebRequest.Create(Url);
            HttpWebResponse WebtoonResponse = (HttpWebResponse)WebtoonRequest.GetResponse();

            Stream WebtoonStream = WebtoonResponse.GetResponseStream();
            StreamReader WebtoonReader = new StreamReader(WebtoonStream);

            return WebtoonReader.ReadToEnd();
        }

        private void BgmlistBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = WebtoonId + "_" + WebtoonNumber;
            dlg.DefaultExt = ".mp3"; // Default file extension
            dlg.Filter = "All Supported Audio | *.mp3; *.wma | MP3s | *.mp3 | WMAs | *.wma"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile((VisualTreeHelper.GetChild(BgmlistView.SelectedItem as StackPanel, 1) as TextBlock).Text, dlg.FileName);
                }
            }
        }

        private string ConvertStringArrayToString(string[] array)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string value in array)
                builder.Append(value);

            return builder.ToString();
        }

        public void MoveAnim(Grid target, double newX, double newY)
        {
            Vector offset = VisualTreeHelper.GetOffset(target);

            var top = offset.Y;
            var left = offset.X;

            TranslateTransform trans = new TranslateTransform();
            target.RenderTransform = trans;

            DoubleAnimation anim1 = new DoubleAnimation(0, newY - top, TimeSpan.FromSeconds(0.8));
            DoubleAnimation anim2 = new DoubleAnimation(0, newX - left, TimeSpan.FromSeconds(0.8));

            anim1.AccelerationRatio = 0.1;
            anim1.DecelerationRatio = 0.9;

            anim2.AccelerationRatio = 0.1;
            anim2.DecelerationRatio = 0.9;

            trans.BeginAnimation(TranslateTransform.YProperty, anim1);
            trans.BeginAnimation(TranslateTransform.XProperty, anim2);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MoveAnim(Test, 7, 0);
            MoveAnim(Test2, 250, -2);
        }

        /// <summary>
        /// Finds a Child of a given item in the visual tree. 
        /// </summary>
        /// <param name="parent">A direct parent of the queried item.</param>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="childName">x:Name or Name of child. </param>
        /// <returns>The first parent item that matches the submitted type parameter. 
        /// If not matching item can be found, 
        /// a null parent is being returned.</returns>
        public static T FindChild<T>(DependencyObject parent, string childName)
           where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }

        private void DownloadImage_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            DownloadAnim.Play();
            DownloadAnim.MediaEnded += DownloadAnim_MediaEnded;
        }

        private void DownloadAnim_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            DownloadAnim.Position = new TimeSpan(0, 0, 1);
            DownloadAnim.MediaEnded -= DownloadAnim_MediaEnded;
        }

        private void DownloadAnim_MediaEnded(object sender, RoutedEventArgs e)
        {
            DownloadAnim.Position = new TimeSpan(0, 0, 1);
            DownloadAnim.Play();
        }

        private void DownloadAnim_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            DownloadAnim.Play();
            DownloadAnim.MediaEnded += DownloadAnim_MediaEnded;
        }
    }
}
