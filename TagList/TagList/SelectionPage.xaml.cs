// lindexi
// 10:38

#region

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using TagList.Framework;
using TagList.Models;

#endregion

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace TagList
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SelectionPage : Page, INotifyPropertyChanged
    {
        public SelectionPage()
        {
            InitializeComponent();
        }

        #region 

        public event PropertyChangedEventHandler PropertyChanged;

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            General.GetInstance().TagSelection.Tags = TagListView.SelectedItems.Cast<Tag>().ToList();
            Frame.GoBack();
        }

        #endregion

        public ObservableCollection<Tag> SelectedTags
        {
            set
            {
                if (value == _selectedTags)
                {
                    return;
                }

                _selectedTags = value;
                OnPropertyChanged();
            }
            get
            {
                return _selectedTags;
            }
        }

        public ObservableCollection<Tag> TagList
        {
            set
            {
                if (value == _tagList)
                {
                    return;
                }

                _tagList = value;
                OnPropertyChanged();
            }
            get
            {
                return _tagList;
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            TagList.Add(new Tag()
            {
                Id = "1",
                Label = "Windows 95"
            });
            TagList.Add(new Tag()
            {
                Id = "2",
                Label = "Windows 98"
            });
            TagList.Add(new Tag()
            {
                Id = "3",
                Label = "Windows Me"
            });
            TagList.Add(new Tag()
            {
                Id = "4",
                Label = "Windows XP"
            });
            TagList.Add(new Tag()
            {
                Id = "5",
                Label = "Windows Vista"
            });
            TagList.Add(new Tag()
            {
                Id = "6",
                Label = "Windows 7"
            });
            TagList.Add(new Tag()
            {
                Id = "7",
                Label = "Windows 8"
            });
            TagList.Add(new Tag()
            {
                Id = "8",
                Label = "Windows 8.1"
            });
            TagList.Add(new Tag()
            {
                Id = "9",
                Label = "Windows 10"
            });
            TagList.Add(new Tag()
            {
                Id = "10",
                Label = "Windows Whatever the name will be"
            });

            foreach (Tag item in General.GetInstance().TagSelection.Tags)
            {
                SelectedTags.Add(TagList.Single(tag => tag.Id.Equals(item.Id)));
            }
        }

        private ObservableCollection<Tag> _selectedTags = new ObservableCollection<Tag>();
        private ObservableCollection<Tag> _tagList = new ObservableCollection<Tag>();


        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}