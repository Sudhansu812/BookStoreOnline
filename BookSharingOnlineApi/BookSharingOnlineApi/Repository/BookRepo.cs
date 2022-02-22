using BookSharingOnlineApi.Models;
using BookSharingOnlineApi.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSharingOnlineApi.Repository
{
    public class BookRepo : IBookRepo
    {
        private readonly BookContext _context;

        public BookRepo(BookContext context)
        {
            _context = context;
        }

        public void Create(BookModel book)
        {
            if (book == null)
            {
                throw new ArgumentNullException();
            }
            _context.Books.AddAsync(book);
        }

        public async Task<double> RateBook(int bookId, double rating)
        {
            BookModel book;
            try
            {
                book = await _context.Books.SingleOrDefaultAsync(book => book.BookId == bookId);
                if (book.BookNumberOfRatings == 0)
                {
                    book.BookRating = rating;
                }
                else
                {
                    book.BookRating = (book.BookRating + rating) / 2;
                }
                book.BookNumberOfRatings++;
                await SaveChanges();
            }
            catch
            {
                return -1;
            }
            return book.BookRating;
        }

        public async Task<BookModel> Search(string bookTitle)
        {
            BookModel book;
            try
            {
                book = await _context.Books.SingleOrDefaultAsync(book => book.BookTitle.Equals(bookTitle));
            }
            catch
            {
                return null;
            }
            return book;
        }

        public async Task<string> GetBookCoverPath(int bookId)
        {
            BookModel book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == bookId);
            return book.BookCoverPath;
        }

        public async Task<string> GetBookTitle(int bookId)
        {
            BookModel book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == bookId);
            return book.BookTitle;
        }

        public async Task<IEnumerable<BookModel>> GetAll()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<IEnumerable<BookModel>> GetByCategory(string category)
        {
            return await _context.Books.Where(book => book.Category.Equals(category)).ToListAsync();
        }

        public async Task<double> GetPrice(int id)
        {
            BookModel book = await _context.Books.SingleOrDefaultAsync(book => book.BookId == id);
            return book.BookPrice;
        }

        public async Task<BookModel> GetBook(int id)
        {
            BookModel book = await _context.Books.SingleOrDefaultAsync(book => book.BookId == id);
            return book;
        }

        public async Task<bool> DecrementQuantity(int id, int n)
        {
            BookModel book = await _context.Books.SingleOrDefaultAsync(book => book.BookId == id);
            if (book.BookQuantity <= 0)
            {
                return false;
            }
            book.BookQuantity -= n;
            book.BookQuantitySold += n;
            return await SaveChanges();
        }

        public async Task<bool> SaveChanges()
        {
            int rowsAffected = await _context.SaveChangesAsync();
            return (rowsAffected >= 0);
        }
    }
}