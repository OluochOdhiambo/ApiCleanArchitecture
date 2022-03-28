using Microsoft.AspNetCore.Mvc;
using Api.ApplicationCore.DTOs;
using Api.ApplicationCore.Exceptions;
using Api.ApplicationCore.Interfaces;
using System.Collections.Generic;

namespace Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
        private readonly IBookRepository bookRepository;
        public BooksController(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        [HttpGet]
        public ActionResult<List<BookResponse>> GetBooks()
        {
            return Ok(this.bookRepository.GetBooks());
        }

        [HttpGet("{id}")]
        public ActionResult GetBookById(int id)
        {
            try 
            {
                var book = this.bookRepository.GetBookById(id);
                return Ok(book);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult Create(CreateBookRequest request)
        {
            var book = this.bookRepository.CreateBook(request);
            return Ok(book);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, UpdateBookRequest request)
        {
            try 
            {
                var book = this.bookRepository.UpdateBook(id, request);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                this.bookRepository.DeleteBookById(id);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
