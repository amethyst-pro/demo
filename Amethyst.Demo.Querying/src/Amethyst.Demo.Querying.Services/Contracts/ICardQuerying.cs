using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amethyst.Demo.Querying.Models.Entities;

namespace Amethyst.Demo.Querying.Services.Contracts
{
    public interface ICardQuerying
    {
        Task<IReadOnlyCollection<Card>> GetAsync();

        Task<Card> GetAsync(Guid cardId);
    }
}