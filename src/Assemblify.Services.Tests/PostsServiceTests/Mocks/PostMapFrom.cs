using Assemblify.Data.Models;
using Assemblify.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Services.Tests.PostsServiceTests.Mocks
{
    public class PostMapFrom: IMapFrom<Post>
    {
        public string Title { get; set; }
    }
}
