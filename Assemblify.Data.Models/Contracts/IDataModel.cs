using System;

namespace Assemblify.Data.Models.Contracts
{
    public interface IDataModel : IAuditable, IDeletable
    {
        Guid Id { get; set; }
    }
}