using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556
using TagList.Annotations;
using TagList.Models;

namespace TagList
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SelectionPage : Page, INotifyPropertyChanged
    {
        private ObservableCollection<Tag> _tagList = new ObservableCollection<Tag>(); 
        public ObservableCollection<Tag> TagList
        {
            get { return _tagList; }
            set
            {
                if(value == _tagList)
                    return;

                _tagList = value;
                OnPropertyChanged();
            }
        }
 
        public SelectionPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.TagList.Add(new Tag() { Id = "1", Label = "Windows 95" });
            this.TagList.Add(new Tag() { Id = "2", Label = "Windows 98" });
            this.TagList.Add(new Tag() { Id = "3", Label = "Windows Me" });
            this.TagList.Add(new Tag() { Id = "4", Label = "Windows XP" });
            this.TagList.Add(new Tag() { Id = "5", Label = "Windows Vista" });
            this.TagList.Add(new Tag() { Id = "6", Label = "Windows 7" });
            this.TagList.Add(new Tag() { Id = "7", Label = "Windows 8" });
            this.TagList.Add(new Tag() { Id = "8", Label = "Windows 8.1" });
            this.TagList.Add(new Tag() { Id = "9", Label = "Windows 10" });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var t = this.TagListView.SelectedItems;

            this.Frame.GoBack();
        }
    }
}
