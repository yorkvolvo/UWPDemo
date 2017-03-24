using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Search;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FolderEnumeration
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
            StorageFolder picturesFolder = KnownFolders.PicturesLibrary;
            StringBuilder outputText = new StringBuilder();

            IReadOnlyList<StorageFile> fileList = await picturesFolder.GetFilesAsync();
            outputText.AppendLine("Files:");

            foreach (StorageFile file in fileList)
            {
                outputText.Append(file.Name + "\n");
            }

            IReadOnlyList<StorageFolder> folderList = await picturesFolder.GetFoldersAsync();
            outputText.AppendLine("Folders:");

            foreach (StorageFolder folder in folderList)
            {
                outputText.Append(folder.DisplayName + "\n");
            }
            //Debug.WriteLine(outputText.ToString()+"");
            MyText.Text = outputText.ToString() + "\nPath:==" + picturesFolder.Path;
            MyText2.Text = picturesFolder.Path;
        }

        private async void Button_Click2(object sender, RoutedEventArgs e)
        {
            StorageFolder picturesFolder = KnownFolders.PicturesLibrary;
            StringBuilder outputText = new StringBuilder();

            IReadOnlyList<IStorageItem> itemsList = await picturesFolder.GetItemsAsync();

            foreach (var item in itemsList)
            {
                if (item is StorageFolder)
                {
                    outputText.Append(item.Name + " folder\n");

                }
                else
                {
                    outputText.Append(item.Name + "\n");

                }
            }
            MyText.Text = outputText.ToString() + "\nPath:==" + picturesFolder.Path;
        }

        private async void Button_Click3(object sender, RoutedEventArgs e)
        {
            StorageFolder picturesFolder = KnownFolders.PicturesLibrary;

            StorageFolderQueryResult queryResult = picturesFolder.CreateFolderQuery(CommonFolderQuery.GroupByMonth);

            IReadOnlyList<StorageFolder> folderList = await queryResult.GetFoldersAsync();

            StringBuilder outputText = new StringBuilder();

            foreach (StorageFolder folder in folderList)
            {
                IReadOnlyList<StorageFile> fileList = await folder.GetFilesAsync();

                // Print the month and number of files in this group.
                outputText.AppendLine(folder.Name + " (" + fileList.Count + ")");

                foreach (StorageFile file in fileList)
                {
                    // Print the name of the file.
                    outputText.AppendLine("   " + file.Name);
                }
            }
            MyText.Text = outputText.ToString() + "\nPath:==" + picturesFolder.Path;
        }
    }
}
