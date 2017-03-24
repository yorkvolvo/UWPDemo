using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FileAccess
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // Create sample file; replace if exists.
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = 
                await storageFolder.CreateFileAsync("sample.txt",CreationCollisionOption.ReplaceExisting);

            Debug.Write(""+storageFolder.Path);
        }

        private async void Button_Click2(object sender, RoutedEventArgs e)
        {
            //1.获取文件
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await storageFolder.GetFileAsync("sample.txt");

            //2.将文本写入文件
            await FileIO.WriteTextAsync(sampleFile, "Swift as a shadow");

            //3.使用缓冲区将字节写入文件（2 步）
            //3.1 首先，调用 ConvertStringToBinary 以获取你想要写入文件的字节（基于随机字符串）的缓冲区
            var buffer = Windows.Security.Cryptography.CryptographicBuffer.ConvertStringToBinary(
        "What fools these mortals be", Windows.Security.Cryptography.BinaryStringEncoding.Utf8);
            //3.2然后，通过调用 FileIO 类的 WriteBufferAsync 方法，将字节从你的缓冲区写入文件
            await FileIO.WriteBufferAsync(sampleFile, buffer);


     
        }
    }
}
