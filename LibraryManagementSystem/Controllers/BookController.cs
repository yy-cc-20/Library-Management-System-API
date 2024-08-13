using Contracts;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using System.Text.Json;
using static System.Reflection.Metadata.BlobBuilder;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public BookController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooksWithFilteringAndSorting([FromQuery] BookParameters bookParameters)
        {
            try
            {
                PagedList<Book> books = _repository.Book.GetBooksWithFilteringAndSorting(bookParameters);

                var metadata = new
                {
                    books.CurrentPage,
                    books.TotalPages,
                    books.PageSize,
                    books.TotalCount,
                    books.HasPrevious,
                    books.HasNext,
                    books.OrderByAscending
                };

                Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metadata));

                _logger.LogInfo($"Returned {books.TotalCount} books from database.");

                //PagedList<BookForViewDto> booksForViewDto = new PagedList<BookForViewDto>();

                //PagedList<BookForViewDto>.ToPagedList(books,
                //bookParameters.PageNumber,
                //bookParameters.PageSize);

                //foreach (var book in books)
                //{
                //    booksForViewDto.Add(_mapper.Map<PagedList<BookForViewDto>>(book));
                //}

                //return Ok(booksForViewDto);

                return Ok(books);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarn($"Invalid query parameter: {ex.Message}");
                return BadRequest("Invalid query parameter");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while fetching books: {ex}");
                return StatusCode(500, $"An error occurred while processing your request.{ex}");
            }
        }

        // GET: api/book/{id}
        [HttpGet("{id}", Name = "GetBookById")]
        public IActionResult GetBookById(Guid id)
        {
            try
            {
                var book = _repository.Book.GetBookById(id);

                _logger.LogInfo($"Returned book with id: {id}");
                return Ok(book);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError($"Book with id: {id}, hasn't been found in db.");
                return NotFound(new { message = ex.Message }); // Returns 404 Not Found
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetBookById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /*
         * sample data to test post api
         * {
                "name": "Chu",
                "dateOfBirth": "1994-02-08",
                "address": "0"
            }
        */
        // the owner para not from Uri but from the request body
        [HttpPost]
        public IActionResult CreateBook([FromBody] BookForCreationDto bookForCreationDto)
        {
            try
            {
                if (bookForCreationDto is null)
                {
                    _logger.LogError("Book object sent from client is null.");
                    return BadRequest("Book object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid book object sent from client.");
                    return BadRequest("Invalid book object");
                }

                var bookModel = _mapper.Map<Book>(bookForCreationDto);

                _repository.Book.CreateBook(bookModel);
                _repository.Save();
                _logger.LogInfo($"Create book with id: {bookModel.Id}");

                return CreatedAtRoute("GetBookById", new { id = bookModel.Id }, bookModel);
                // return a status code 201, which stands for Created
                // provide the route that can retrieve the created entity, can send GET request using this url
                // it will populate the body of the response with the new owner object
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateBook action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(Guid id, [FromBody] BookForUpdateDto bookDto)
        {
            try
            {
                if (bookDto is null)
                {
                    _logger.LogError("Book object sent from client is null.");
                    return BadRequest("Book object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid book object sent from client.");
                    return BadRequest("Invalid book object");
                }

                var bookModel = _repository.Book.GetBookById(id);
                if (bookModel is null)
                {
                    _logger.LogError($"Book with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                // Load the new category
                var newCategory = _repository.Category.GetCategoryById(bookDto.Category_Id);
                if (newCategory is null)
                {
                    _logger.LogError($"Category with id: {bookDto.Category_Id}, hasn't been found in db.");
                    return NotFound();
                }

                // must have for one to many relationship
                bookModel.Category = newCategory;

                _mapper.Map(bookDto, bookModel);

                _repository.Book.UpdateBook(bookModel);
                _repository.Save();
                _logger.LogInfo($"Update book with id: {id}");

                return NoContent(); // return NoContent status code 204
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(Guid id)
        {
            try
            {
                var book = _repository.Book.GetBookById(id);
                if (book == null)
                {
                    _logger.LogError($"Book with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                //// didn’t allow cascade delete in our database configuration
                //if (_repository.Account.AccountsByOwner(id).Any())
                //{
                //    _logger.LogError($"Cannot delete owner with id: {id}. It has related accounts. Delete those accounts first");
                //    return BadRequest("Cannot delete owner. It has related accounts. Delete those accounts first");
                //}

                _logger.LogInfo($"Delete book with id: {id}");
                _repository.Book.DeleteBook(book);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteBook action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
