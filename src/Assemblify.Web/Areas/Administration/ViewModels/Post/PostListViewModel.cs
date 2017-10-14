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

    public class PostListViewModel: IMapFrom<Post>
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public virtual User Author { get; set; }

        public Guid Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}