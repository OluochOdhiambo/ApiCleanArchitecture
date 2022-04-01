using Xunit;
using AutoMapper;
using Api.ApplicationCore.DTOs;
using Api.ApplicationCore.Mappings;
using Api.ApplicationCore.Exceptions;
using Api.Infrastructure.Persistence.Repositories;
using System;

namespace Api.IntegrationTests.Repositories
{
    public class BookRepositoryTests : IClassFixture<SharedDatabaseFixture>
    {
        private readonly IMapper _mapper;
        private SharedDatabaseFixture Fixture { get; }

        public BookRepositoryTests(SharedDatabaseFixture fixture)
        {
            Fixture = fixture;

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<GeneralProfile>();
            });

            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public void GetBooks_ReturnsAllBooks()
        {
            using (var context = Fixture.CreateContext())
            {
                var repository = new BookRepository(context, _mapper);

                var books = repository.GetBooks();

                Assert.Equal(10, books.Count);
            }
        }

        [Fact]
        public void GetBookById_BookDoesntExist_ThrowsNotFoundException()
        {
            using (var context = Fixture.CreateContext())
            {
                var repository = new BookRepository(context, _mapper);
                var bookId = 57;

                Assert.Throws<NotFoundException>(() => repository.GetBookById(bookId));
            }
        }

        [Fact]
        public void CreateBook_SavesCorrectData()
        {
            using (var transaction = Fixture.Connection.BeginTransaction())
            {
                var bookId = 0;

                var request = new CreateBookRequest
                {
                    Title = "An Interesting Book Title",
                    Summary = "This is a book with an interesting title",
                    Pages = 499,
                    Price = 23.25,
                    InStock = true
                };

                using (var context = Fixture.CreateContext(transaction))
                {
                    var repository = new BookRepository(context, _mapper);

                    var book = repository.CreateBook(request);
                    bookId = book.Id;
                }

                using (var context = Fixture.CreateContext(transaction))
                {
                    var repository = new BookRepository(context, _mapper);

                    var book = repository.GetBookById(bookId);

                    Assert.NotNull(book);
                    Assert.Equal(request.Title, book.Title);
                    Assert.Equal(request.Summary, book.Summary);
                    Assert.Equal(request.Pages, book.Pages);
                    Assert.Equal(request.Price, book.Price);
                    Assert.Equal(request.InStock, book.InStock);
                }
            }
        }

        [Fact]
        public void UpdateBook_SavesCorrectData()
        {
            using (var transaction = Fixture.Connection.BeginTransaction())
            {
                var bookId = 1;

                var request = new UpdateBookRequest
                {
                    Title = "My first Book",
                    Summary = "This is a summary of my first book",
                    Pages = 309,
                    Price = 18.99,
                    InStock = false
                };

                using (var context = Fixture.CreateContext(transaction))
                {
                    var repository = new BookRepository(context, _mapper);

                    repository.UpdateBook(bookId, request);
                }

                using (var context = Fixture.CreateContext(transaction))
                {
                    var repository = new BookRepository(context, _mapper);

                    var book = repository.GetBookById(bookId);

                    Assert.NotNull(book);
                    Assert.Equal(request.Title, book.Title);
                    Assert.Equal(request.Summary, book.Summary);
                    Assert.Equal(request.Pages, book.Pages);
                    Assert.Equal(request.Price, book.Price);
                    Assert.Equal(request.InStock, book.InStock);
                }
            }
        }

        [Fact]
        public void UpdateBook_BookDoesntExist_ThrowsNotFoundException()
        {
            var bookId = 62;

            var request = new UpdateBookRequest
            {
                Title = "My Sixty-second Book",
                Summary = "This is a summary of my  sixty-second book",
                Pages = 309,
                Price = 18.99,
                InStock = false
            };

            using (var context = Fixture.CreateContext())
            {
                var repository = new BookRepository(context, _mapper);
                var action = () => repository.UpdateBook(bookId, request);

                Assert.Throws<NotFoundException>(action);
            }
        }

        [Fact]
        public void DeleteBookById_EnsuresBookIsDeleted()
        {
            using (var transaction = Fixture.Connection.BeginTransaction())
            {
                var bookId = 2;

                using (var context = Fixture.CreateContext(transaction))
                {
                    var repository = new BookRepository(context, _mapper);
                    var books = repository.GetBooks();

                    repository.DeleteBookById(bookId);
                }

                using (var context = Fixture.CreateContext(transaction))
                {
                    var repository = new BookRepository(context, _mapper);
                    var action = () => repository.GetBookById(bookId);

                    Assert.Throws<NotFoundException>(action);
                }
            }
        }

        [Fact]
        public void DeleteBookById_BookDoesntExist_ThrowsNotFoundException()
        {
            var bookId = 123;

            using (var context = Fixture.CreateContext())
            {
                var repository = new BookRepository(context, _mapper);
                var action = () => repository.DeleteBookById(bookId);

                Assert.Throws<NotFoundException>(action);
            }
        }
    }
}
