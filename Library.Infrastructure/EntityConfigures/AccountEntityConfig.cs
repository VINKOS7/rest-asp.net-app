using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Library.Domain.Aggregates.Account;

namespace Library.Infrastructure.EntityConfigures;

public class AccountEntityConfig : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder) => builder
        .OwnsMany(acc => acc.Devices);
}
