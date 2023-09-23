using Dotseed.Domain;
using Library.Domain.Aggregates.Book;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure;

public class BookRepo : IBookRepo
{
    private readonly Context _db;

    public BookRepo(Context db) => _db = db;

    public IUnitOfWork UnitOfWork => _db;

    public async Task AddAsync(Book book) => await _db.Books.AddAsync(book);

    public async Task<ICollection<Book>> FetchAsync(int offset, int size) => await _db.Books
        .Skip(offset)
        .Take(size)
        .OrderBy(b => b)
        .ToListAsync();
     
    public async Task<Book> FindByIdAsync(Guid Id) => await _db.Books.FirstOrDefaultAsync(p => p.Id == Id);

    public async Task RemoveByIdAsync(Guid Id)
    {
        var book =  await _db.Books.FirstOrDefaultAsync(b => b.Id == Id);

        if (book is not null) _db.Books.Remove(book);
    }
}