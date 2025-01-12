using BookshelfManager.Models;
using MongoDB.Driver;

namespace BookshelfManager.Repositories
{
public class BookRepository
{
    private readonly IMongoDatabase _database;
    private readonly IMongoCollection<Book> _booksCollection;

    public BookRepository(IMongoDatabase database)
    {
        _database = database;
        _booksCollection = _database.GetCollection<Book>("Books");
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _booksCollection.Find(book => true).ToListAsync();
    }

    public async Task<Book> GetBookByIdAsync(string id)
    {
        return await _booksCollection.Find(book => book.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateBookAsync(Book book)
    {
        await _booksCollection.InsertOneAsync(book);
    }

    public async Task UpdateBookAsync(string id, Book book)
    {
        await _booksCollection.ReplaceOneAsync(b => b.Id == id, book);
    }

    public async Task DeleteBookAsync(string id)
    {
        await _booksCollection.DeleteOneAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<Book>> SearchBooksAsync(string genre, string author)
    {
        var filter = Builders<Book>.Filter.Empty;

        if (!string.IsNullOrEmpty(genre))
        {
            filter &= Builders<Book>.Filter.Eq(b => b.Genre, genre);
        }

        if (!string.IsNullOrEmpty(author))
        {
            filter &= Builders<Book>.Filter.Eq(b => b.Author, author);
        }

        return await _booksCollection.Find(filter).ToListAsync();
    }
}
}
