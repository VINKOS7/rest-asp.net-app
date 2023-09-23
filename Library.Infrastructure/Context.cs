using Microsoft.EntityFrameworkCore;

using MediatR;
using Dotseed.Context;

using Library.Domain.Aggregates.Book;

namespace Library.Infrastructure;

public class Context : UnitOfWorkContext
{
    public DbSet<Book> Books { get; set; }

    public Context(DbContextOptions options, IMediator mediator) : base(options, mediator) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //use this code? then you added sub entities to Book 
        //modelBuilder.ApplyConfiguration(new BookEntityConfiguration());
    }
}
