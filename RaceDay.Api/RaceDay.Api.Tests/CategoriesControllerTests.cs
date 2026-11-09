using Xunit;
using RaceDay.Api.Controllers;
using RaceDay.Api.Data;
using RaceDay.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RaceDay.Api.Tests
{
    public class CategoriesControllerTests
    {
        private readonly RaceDayDbContext _context;

        public CategoriesControllerTests()
        {
            // Create a fresh in-memory database for each test
            var options = new DbContextOptionsBuilder<RaceDayDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_Categories")
                .Options;

            _context = new RaceDayDbContext(options);
            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetCategories_ReturnsAllCategories()
        {
            // Arrange - Clear database then add test data
            _context.Categories.RemoveRange(_context.Categories);
            await _context.SaveChangesAsync();

            _context.Categories.Add(new Category { CategoryId = 1, Name = "Road Race", Description = "Long distance road racing" });
            _context.Categories.Add(new Category { CategoryId = 2, Name = "Trail Run", Description = "Off-road trail running" });
            await _context.SaveChangesAsync();

            var controller = new CategoriesController(_context);

            // Act
            var result = await controller.GetCategories();

            // Assert
            Assert.NotNull(result.Value);
            var categories = Assert.IsType<List<Category>>(result.Value);
            Assert.Equal(2, categories.Count);
        }

        [Fact]
        public async Task GetCategory_WhenIdExists_ReturnsCorrectCategory()
        {
            // Arrange
            _context.Categories.RemoveRange(_context.Categories);
            await _context.SaveChangesAsync();

            var testCategory = new Category { CategoryId = 5, Name = "Sprint", Description = "Short distance sprint" };
            _context.Categories.Add(testCategory);
            await _context.SaveChangesAsync();

            var controller = new CategoriesController(_context);

            // Act
            var result = await controller.GetCategory(5);

            // Assert
            Assert.NotNull(result.Value);
            Assert.Equal("Sprint", result.Value.Name);
        }
    }
}