using AutoMapper;
using Api.ApplicationCore.DTOs;
using Api.ApplicationCore.Entities;
using Api.ApplicationCore.Exceptions;
using Api.ApplicationCore.Utils;
using Api.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.ApplicationCore.Interfaces;

namespace Api.Infrastructure.Persistence.Repositories
{
    public class BookRepository :IBookRepository
    {
        private readonly ApiContext apiContext;
        private readonly IMapper mapper;

        public BookRepository(ApiContext apiContext, IMapper mapper)
        {
            this.apiContext = apiContext;
            this.mapper = mapper;
        }

        public BookResponse CreateBook(CreateBookRequest request)
        {
            var book = this.mapper.Map<Book>(request);
            book.Title = request.Title;
            book.Summary = request.Summary;
            book.Pages = request.Pages;
            book.Price = request.Price;
            book.InStock = true;
            book.CreatedAt = book.UpdatedAt = DateUtil.GetCurrentDate();

            this.apiContext.Books.Add(book);
            this.apiContext.SaveChanges();

            return this.mapper.Map<BookResponse>(book);
        }

        public void DeleteBookById(int bookId)
        {
            var book = this.apiContext.Books.Find(bookId);
            if (book != null)
            {
                this.apiContext.Books.Remove(book);
                this.apiContext.SaveChanges();
            }
            else 
            {
                throw new NotFoundException();
            }
        }

        public BookResponse GetBookById(int bookId)
        {
            var book = this.apiContext.Books.Find(bookId);
            if (book != null)
            {
                return this.mapper.Map<BookResponse>(book);
            }
            throw new NotFoundException();
        }

        public List<BookResponse> GetBooks()
        {
            return this.apiContext.Books.Select(b => this.mapper.Map<BookResponse>(b)).ToList();
        }

        public BookResponse UpdateBook(int bookId, UpdateBookRequest request)
        {
            var book = this.apiContext.Books.Find(bookId);
            if (book != null)
            {
                book.Title = request.Title;
                book.Summary = request.Summary;
                book.Pages = request.Pages;
                book.Price = request.Price;
                book.InStock = request.InStock;
                book.UpdatedAt = DateUtil.GetCurrentDate();

                this.apiContext.Books.Update(book);
                this.apiContext.SaveChanges();

                return this.mapper.Map<BookResponse>(book);
            }

            throw new NotFoundException();
        }
    }
}
