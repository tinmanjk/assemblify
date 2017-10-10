using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assemblify.Web.ViewModels.Home
{
    public class HomeViewModel
    {
        public IEnumerable<PostViewModel> Posts { get; set; }
    }
}