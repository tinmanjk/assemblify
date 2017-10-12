using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assemblify.Web.ViewModels.Home
{
    public class PostSearchViewModel
    {
        public string SearchTerm { get; set; }

        public IEnumerable<PostViewModel> FoundPosts { get; set; }

    }
}