#win 10 UWP ��ǩ

������Ҫ���룺http://visuallylocated.com/post/2015/02/20/Creating-a-WrapPanel-for-your-Windows-Runtime-apps.aspx    http://depblog.weblogs.us/2015/02/18/how-to-add-a-tag-list-into-winrt-universal-apps/

������Ҫ���û��ܶ��ǩ��������Ҫʹ��һ���ؼ������ĳ����Ǳ仯�����Կ��ٷţ���������wrapPancel����������Ҫ����Ϊ�����ֱ��д������������������Կ�

![����дͼƬ����](http://img.blog.csdn.net/20160428154345998)

���ǵ���Ӿͻ���ӱ�ǩ������ɾ����ǩ�ͺܿ��Ű档

����ʹ��RichBox��������������Ǳ�ǩ

Դ������Ϊ����д�ĺ�UWP��һ�����Ҹ�UWP������https://github.com/lindexi/TagList

����Ч��
![����дͼƬ����](http://img.blog.csdn.net/20160429102218298)

�����ť
![����дͼƬ����](http://img.blog.csdn.net/20160429102248655)

ɾ��
![����дͼƬ����](http://img.blog.csdn.net/20160429102311111)

���ʹ�ã���add
![����дͼƬ����](http://img.blog.csdn.net/20160429102913148)

�������û�ѡ������������û����룬ʹ���е��ѣ�����ʹ���û�����ת���룬�����Զ���ΪԤ��һ��

```
Դ.Add(new Tag() {Id = "id",Label = "�û�����"});
```
![����дͼƬ����](http://img.blog.csdn.net/20160429103328037)

ѡ���ǩ��ѡ����ɱ���

![����дͼƬ����](http://img.blog.csdn.net/20160429103355334)

![����дͼƬ����](http://img.blog.csdn.net/20160429103411522)

���Կ�����ҳ

![����дͼƬ����](http://img.blog.csdn.net/20160429103435514)

��ǩʹ������תMainPage

```
if (e.NavigationMode == NavigationMode.Back)
```

���ǰ�ѡ�񱣴�

```
General.GetInstance().TagSelection
```

��`SetTags`�Ǳ��㷨����Ҫ

��������ȫ���¼Ӻͱ�ɾ��

```
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
```

```
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
```

���ɾ����ť��ɾ��id

```
            string tagId = ((Button) sender).Name;
            General.GetInstance()
                .TagSelection.Tags.Remove(General.GetInstance().TagSelection.Tags.Single(item => item.Id.Equals(tagId)));
            SetTags();
```


Դ�룺https://github.com/Depechie/TagList