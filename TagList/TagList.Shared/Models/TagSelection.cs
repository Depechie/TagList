using System.Collections.Generic;

namespace TagList.Models
{
    public class TagSelection
    {
        public List<Tag> Tags { get; set; }

        public TagSelection()
        {
            this.Tags = new List<Tag>();
        }
    }
}
