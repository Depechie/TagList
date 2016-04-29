// lindexi
// 10:38

#region

using System.Collections.Generic;

#endregion

namespace TagList.Models
{
    public class TagSelection
    {
        public TagSelection()
        {
            Tags = new List<Tag>();
        }

        public List<Tag> Tags
        {
            set;
            get;
        }
    }
}