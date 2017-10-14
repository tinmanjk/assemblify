namespace Assemblify.Web.Areas.Administration.ViewModels.Post
{
    using Assemblify.Infrastructure.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Assemblify.Data.Models;
    using System.Web.Mvc;
    using AutoMapper;

    public class PostDeleteViewModel : IMapFrom<Post>
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public bool IsDeleted { get; set; }

        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AuthorUserName { get; set; }
    }
}