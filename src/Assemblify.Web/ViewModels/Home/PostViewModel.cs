

namespace Assemblify.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using System.Web.Mvc;
    using Assemblify.Data.Models;
    using Assemblify.Infrastructure.Mapping;

    public class PostViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [Display(Name = "Pesho's name field")]
        public string Title { get; set; }

        public string Content { get; set; }

        public string AuthorEmail { get; set; }

        // may lead to conflict if there are custom display templates
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime PostedOn { get; set; }

        public void CreateMappingsForMe(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Post, PostViewModel>()
                .ForMember(postViewModel => postViewModel.AuthorEmail, cfg => cfg.MapFrom(post => post.Author.Email))
                .ForMember(postViewModel => postViewModel.PostedOn, cfg => cfg.MapFrom(post => post.CreatedOn))
                .ReverseMap();
        }
    }
}