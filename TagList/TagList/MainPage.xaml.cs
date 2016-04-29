// lindexi
// 10:37

#region

using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using TagList.Framework;
using TagList.Models;

#endregion

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace TagList
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            NavigationCacheMode = NavigationCacheMode.Required;
        }

        #region 

        private void OnTagButtonClicked(object sender, RoutedEventArgs e)
        {
            string tagId = ((Button) sender).Name;
            General.GetInstance()
                .TagSelection.Tags.Remove(General.GetInstance().TagSelection.Tags.Single(item => item.Id.Equals(tagId)));
            SetTags();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (SelectionPage));
        }

        #endregion

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                SetTags();
            }
        }

        private void SetTags()
        {
            if (General.GetInstance().TagSelection.Tags.Any())
            {
                Paragraph tagParagraph = (Paragraph) (from paragraph in TagRichTextBlock.Blocks
                    where paragraph.Name.StartsWith("Tags")
                    select paragraph).FirstOrDefault();

                IEnumerable<string> tagIds = from tag in General.GetInstance().TagSelection.Tags
                    select tag.Id;

                IEnumerable<InlineUIContainer> buttonsToRemove =
                    from item in tagParagraph.Inlines.Cast<InlineUIContainer>()
                    where !tagIds.Contains(((Button) item.Child).Name)
                    select item;

                foreach (InlineUIContainer container in buttonsToRemove)
                {
                    tagParagraph.Inlines.Remove(container);
                }

                IEnumerable<string> buttonIds = from item in tagParagraph.Inlines.Cast<InlineUIContainer>()
                    select ((Button) item.Child).Name;

                IEnumerable<Tag> tagsToAdd = from item in General.GetInstance().TagSelection.Tags
                    where !buttonIds.Contains(item.Id)
                    select item;

                foreach (Tag tag in tagsToAdd)
                {
                    InlineUIContainer container = new InlineUIContainer();
                    RichTextBlock inlineRichTextBlock = new RichTextBlock()
                    {
                        IsTextSelectionEnabled = false
                    };
                    Paragraph inlineParagraph = new Paragraph();
                    inlineParagraph.Inlines.Add(new Run()
                    {
                        Text = string.Format("{0} ", tag.Label),
                        FontSize = 14
                    });
                    inlineParagraph.Inlines.Add(new Run()
                    {
                        Text = "\uE106",
                        FontFamily = new FontFamily("Segoe UI Symbol"),
                        FontSize = 10
                    });
                    inlineRichTextBlock.Blocks.Add(inlineParagraph);

                    Button tagButton = new Button()
                    {
                        Content = inlineRichTextBlock,
                        Style = (Style) Application.Current.Resources["TagButtonStyle"],
                        Name = tag.Id
                    };
                    tagButton.Click += OnTagButtonClicked;
                    container.Child = tagButton;

                    tagParagraph.Inlines.Add(container);
                }
            }
            else
            {
                TagRichTextBlock.Blocks.Clear();
            }
        }
    }
}