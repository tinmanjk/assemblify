using Assemblify.Data.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Data.Models
{
    public class Post : DataModel
    {
        public Post(string title, string content, User author)
        {
            this.Title = title;
            this.Content = content;
            this.Author = author;
        }

        public Post()
        {

        }

        public string Title { get; set; }

        public string Content { get; set; }

        public virtual User Author { get; set; }
    }
}
