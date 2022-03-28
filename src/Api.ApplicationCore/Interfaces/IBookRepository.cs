using System;
using System.Collections.Generic;
using System.Text;
using Api.ApplicationCore.DTOs;

namespace Api.ApplicationCore.Interfaces
{
    public interface IBookRepository
    {
        List<BookResponse> GetBooks();

        BookResponse GetBookById(int id);

        void DeleteBookById(int bookId);

        BookResponse CreateBook(CreateBookRequest request);

        BookResponse UpdateBook(int bookId, UpdateBookRequest request);
    }
}
