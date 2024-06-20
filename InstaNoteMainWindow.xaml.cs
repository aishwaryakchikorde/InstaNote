using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace InstaNote
{
  /// <summary>
  /// Interaction logic for InstaNoteMainWindow.xaml
  /// </summary>
  public partial class InstaNoteMainWindow : Window
  {
    private bool _textChanged;
    public InstaNoteMainWindow()
    {
      InitializeComponent();
    }

    #region File
    private void newFile_Click(object sender, RoutedEventArgs e)
    {
      if (_textChanged)
      {
        MessageBoxResult result = MessageBox.Show("Do you want to save the changes to Untitled?", "Unsaved Changes", MessageBoxButton.YesNoCancel);
        if (result == MessageBoxResult.No)
        {
          this.InstaNote_MainWindow.Title = "Untitled - InstaNote"; // Using superscript plus character as a visual indicator
          this.tbInsta.Text = "";
          this.tbInsta.Focus();
          _textChanged = false;
        }

        if (result == MessageBoxResult.Yes)
        {
          SaveTextBoxContent();
        }
      }
    }

    private void MnNewWindow_OnClick(object sender, RoutedEventArgs e)
    {
      InstaNoteMainWindow instaNoteMainWindow = new InstaNoteMainWindow();
      instaNoteMainWindow.Show();
    }

    private void MnOpen_OnClick(object sender, RoutedEventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "Text Files|*.txt";
      openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
      openFileDialog.ShowDialog();

      if (openFileDialog.FileName != string.Empty)
      {
        this.tbInsta.Text = System.IO.File.ReadAllText(openFileDialog.FileName, Encoding.Unicode);
      }
    }

    private void MnSave_OnClick(object sender, RoutedEventArgs e)
    {
      SaveTextBoxContent();
    }

    private void MnExit_OnClick(object sender, RoutedEventArgs e)
    {
      Application.Current.Shutdown();
    }

    private void SaveTextBoxContent()
    {
      SaveFileDialog saveDialog = new SaveFileDialog
      {
        Filter = "Text Files|*.txt",
        InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments),
        AddExtension = true,
        OverwritePrompt = true,
        FileName = "Untitled"
      };
      saveDialog.ShowDialog();

      if (saveDialog.FileName != string.Empty)
      {
        System.IO.File.WriteAllText(saveDialog.FileName, tbInsta.Text, Encoding.Unicode);
        MessageBox.Show($"The file is successfully saved in '{saveDialog.FileName}' path.", "InstaNote");

        this.InstaNote_MainWindow.Title = "Untitled - InstaNote"; // Using superscript plus character as a visual indicator
        this.tbInsta.Text = "";
        this.tbInsta.Focus();
        _textChanged = false;
      }
    }

    #endregion

    #region Edit

    

    #endregion







    private void TbInsta_OnTextChanged(object sender, TextChangedEventArgs e)
    {

      if (_textChanged == false)
      {
        this.InstaNote_MainWindow.Title = "\u207AUntitled - InstaNote"; // Using superscript plus character as a visual indicator
        _textChanged = true;
      }
    }

    private void MnUndo_OnClick(object sender, RoutedEventArgs e)
    {
      this.tbInsta.Undo();
    }

    private void MnRedo_OnClick(object sender, RoutedEventArgs e)
    {
      this.tbInsta.Redo();
    }

    private void MnCut_OnClick(object sender, RoutedEventArgs e)
    {
      this.tbInsta.Cut();
    }

    private void MnCopy_OnClick(object sender, RoutedEventArgs e)
    {
      this.tbInsta.Copy();
    }

    private void MnPaste_OnClick(object sender, RoutedEventArgs e)
    {
      this.tbInsta.Paste();
    }

    private void MnSelect_OnClick(object sender, RoutedEventArgs e)
    {
      this.tbInsta.Focus();
      this.tbInsta.SelectAll();
    }

    private void MnWordWrap_OnClick(object sender, RoutedEventArgs e)
    {
      if (this.mnWordWrap.IsChecked)
      {
        this.tbInsta.TextWrapping = TextWrapping.Wrap;
        this.tbInsta.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
      }
      else
      {
        this.tbInsta.TextWrapping = TextWrapping.NoWrap;
        this.tbInsta.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
      }
    }


    private void MnZoomIn_OnClick(object sender, RoutedEventArgs e)
    {
      if (this.tbInsta.FontSize < 200)
      {
        tbInsta.FontSize += 5;
      }
    }

    private void MnZoomOut_OnClick(object sender, RoutedEventArgs e)
    {
      if (this.tbInsta.FontSize >5)
      {
        tbInsta.FontSize -= 5;
      }
    }

    private void DefaultZoom_OnClick(object sender, RoutedEventArgs e)
    {
      tbInsta.FontSize = 15;
    }

   

    private void MnAboutUs_OnClick(object sender, RoutedEventArgs e)
    {
      AboutUs aboutUs = new AboutUs();
      aboutUs.Show();
    }

   
  }
}
