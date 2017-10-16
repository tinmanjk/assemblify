

namespace Assemblify.Web.ViewModels.Post
{
    using Assemblify.Data.Models;
    using Assemblify.Infrastructure.Mapping;
    using AutoMapper;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class UserPostsViewModel: IMapFrom<Post>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PostedOn { get; set; }

        public void CreateMappingsForMe(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Post, UserPostsViewModel>()
                .ForMember(postViewModel => postViewModel.PostedOn, cfg => cfg.MapFrom(post => post.CreatedOn));
        }
    }
}