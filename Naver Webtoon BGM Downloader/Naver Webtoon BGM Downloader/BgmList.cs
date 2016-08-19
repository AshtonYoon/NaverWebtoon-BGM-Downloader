using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Net;
using System.IO;
using System;
using System.Windows.Media.Imaging;

namespace Naver_Webtoon_BGM_Downloader
{
    class BgmList
    {
        private Image BgmImage;
        private string BgmName;

        public BgmList(string BgmImage, string BgmName)
        {
            this.BgmImage = new Image
            {
                Width = 55,
                Height = 55 * 0.594,
                Source = new BitmapImage(new Uri(BgmImage, UriKind.RelativeOrAbsolute))
            };
            this.BgmName = BgmName;
        }
    }
}
