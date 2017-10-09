using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assemblify.Web.ViewModels.Home
{
    public class HomeViewModel
    {
        public ICollection<PostViewModel> Posts { get; set; }
    }
}