using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace gamex.Auth.Services;

public sealed  class UpdateAuditableEntitiesInterceptor(IUserSessionService sessionService) : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData dbContextEvent,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default
    ) 
    {

        DbContext? dbContext = dbContextEvent.Context;
        if (dbContext is null) 
            return base.SavingChangesAsync(dbContextEvent, result, cancellationToken);


        UserSessionDto? sessionUser = sessionService.Get();
        if (sessionUser is null) 
            return base.SavingChangesAsync(dbContextEvent, result, cancellationToken);

        
        // Load changes entities
        IEnumerable<EntityEntry<EntityBase<Guid>>> changesEntities = dbContext.ChangeTracker.Entries<EntityBase<Guid>>();

        foreach (EntityEntry<EntityBase<Guid>> entityEntry in changesEntities)
        {
            // WHEN ENTITY IS ADDED
            if ( entityEntry.State == EntityState.Added ) {
                entityEntry.Property(e=> e.CreatedById).CurrentValue = sessionUser.UserId;
                entityEntry.Property(e=> e.CreatedOn).CurrentValue = DateTime.UtcNow;
            }

            // WHEN ENTITY IS UPDATED
            if ( entityEntry.State == EntityState.Modified ) {
                entityEntry.Property(e=> e.UpdatedById).CurrentValue = sessionUser.UserId;
                entityEntry.Property(e=> e.UpdatedOn).CurrentValue = DateTime.UtcNow;
            }


        }

        return base.SavingChangesAsync(dbContextEvent, result, cancellationToken);
    }
}