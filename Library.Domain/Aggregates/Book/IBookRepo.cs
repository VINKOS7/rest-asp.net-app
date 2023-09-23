using Dotseed.Domain;

namespace Library.Domain.Aggregates.Book;

public interface IBookRepo : IRepository<Book>
{
    public Task AddAsync(Book book);

    public Task RemoveByIdAsync(Guid id);

    public Task<Book> FindByIdAsync(Guid Id);

    public Task<ICollection<Book>> FetchAsync(int offset, int size);
}
