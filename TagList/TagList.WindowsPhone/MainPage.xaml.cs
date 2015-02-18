using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
using TagList.Framework;
using TagList.Models;

namespace TagList
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
                this.SetTags();
        }

        private void SetTags()
        {
            if (General.GetInstance().TagSelection.Tags.Any())
            {
                var tagParagraph = (Paragraph) (from paragraph in TagRichTextBlock.Blocks
                    where paragraph.Name.StartsWith("Tags")
                    select paragraph).FirstOrDefault();

                var tagIds = from tag in General.GetInstance().TagSelection.Tags
                    select tag.Id;

                var buttonsToRemove = from item in tagParagraph.Inlines.Cast<InlineUIContainer>()
                    where !tagIds.Contains(((Button) item.Child).Name)
                    select item;

                foreach (InlineUIContainer container in buttonsToRemove)
                    tagParagraph.Inlines.Remove(container);

                var buttonIds = from item in tagParagraph.Inlines.Cast<InlineUIContainer>()
                    select ((Button) item.Child).Name;

                var tagsToAdd = from item in General.GetInstance().TagSelection.Tags
                    where !buttonIds.Contains(item.Id)
                    select item;

                foreach (Tag tag in tagsToAdd)
                {
                    var container = new InlineUIContainer();
                    RichTextBlock inlineRichTextBlock = new RichTextBlock() {IsTextSelectionEnabled = false};
                    Paragraph inlineParagraph = new Paragraph();
                    inlineParagraph.Inlines.Add(new Run() {Text = string.Format("{0} ", tag.Label), FontSize = 14});
                    inlineParagraph.Inlines.Add(new Run()
                    {
                        Text = "\uE106",
                        FontFamily = new FontFamily("Segoe UI Symbol"),
                        FontSize = 10
                    });
                    inlineRichTextBlock.Blocks.Add(inlineParagraph);

                    var tagButton = new Button()
                    {
                        Content = inlineRichTextBlock,
                        Style = (Style)Application.Current.Resources["TagButtonStyle"],
                        Name = tag.Id
                    };
                    tagButton.Click += OnTagButtonClicked;
                    container.Child = tagButton;

                    tagParagraph.Inlines.Add(container);
                }
            }
            else
                TagRichTextBlock.Blocks.Clear();
        }

        private void OnTagButtonClicked(object sender, RoutedEventArgs e)
        {
            var tagId = ((Button) sender).Name;
            General.GetInstance().TagSelection.Tags.Remove(General.GetInstance().TagSelection.Tags.Single(item => item.Id.Equals(tagId)));
            this.SetTags();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof (SelectionPage));
        }
    }
}
