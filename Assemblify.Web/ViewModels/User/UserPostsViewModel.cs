using Assemblify.Data.Models;
using Assemblify.Infrastructure.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assemblify.Web.ViewModels.User
{
    public class UserPostsViewModel: IMapFrom<Post>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PostedOn { get; set; }

        public void CreateMappingsForMe(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Post, UserPostsViewModel>()
                .ForMember(postViewModel => postViewModel.PostedOn, cfg => cfg.MapFrom(post => post.CreatedOn));
        }

        public void CreateMappingsForDestination(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<UserPostsViewModel, Post>()
                .ForMember(post => post.CreatedOn, cfg => cfg.MapFrom(postViewModel => postViewModel.PostedOn));
        }

    }
}