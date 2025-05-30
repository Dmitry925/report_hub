﻿using Duende.IdentityServer.Models;

namespace Exadel.ReportHub.RA.Abstract;

public interface IIdentityRepository
{
    Task<IList<TDocument>> GetAllAsync<TDocument>(CancellationToken cancellationToken);

    Task<IList<TDocument>> GetByNamesAsync<TDocument>(IEnumerable<string> names, CancellationToken cancellationToken)
        where TDocument : Resource;

    Task<IList<ApiResource>> GetApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames, CancellationToken cancellationToken);

    Task<Client> GetClientByIdAsync(string clientId, CancellationToken cancellationToken);
}
