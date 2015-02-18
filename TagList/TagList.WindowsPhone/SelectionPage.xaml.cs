using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using TagList.Annotations;
using TagList.Framework;
using TagList.Models;
using System.Linq;

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

        private ObservableCollection<Tag> _selectedTags = new ObservableCollection<Tag>();
        public ObservableCollection<Tag> SelectedTags
        {
            get { return _selectedTags; }
            set
            {
                if (value == _selectedTags)
                    return;

                _selectedTags = value;
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
            this.TagList.Add(new Tag() { Id = "10", Label = "Windows Whatever the name will be" });

            foreach (Tag item in General.GetInstance().TagSelection.Tags)
                this.SelectedTags.Add(this.TagList.Single(tag => tag.Id.Equals(item.Id)));
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
            General.GetInstance().TagSelection.Tags = this.TagListView.SelectedItems.Cast<Tag>().ToList();
            this.Frame.GoBack();
        }
    }
}
