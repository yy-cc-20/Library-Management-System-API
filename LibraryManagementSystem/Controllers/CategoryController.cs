using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public CategoryController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCategory()
        {
            try
            {
                var categories = _repository.Category.GetCategories();

                _logger.LogInfo($"Returned {categories.Count()} categories from database.");

                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while fetching books: {ex}");
                return StatusCode(500, $"An error occurred while processing your request. {ex}");
            }
        }
    }
}
