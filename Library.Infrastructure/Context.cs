using Microsoft.EntityFrameworkCore;

using MediatR;
using Dotseed.Context;

using Library.Domain.Aggregates.Book;
using Library.Domain.Aggregates.Account;
using Library.Infrastructure.EntityConfigures;

namespace Library.Infrastructure;

public class Context : UnitOfWorkContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Account> Accounts { get; set; }

    public Context(DbContextOptions options, IMediator mediator) : base(options, mediator) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new AccountEntityConfig());
    }
}