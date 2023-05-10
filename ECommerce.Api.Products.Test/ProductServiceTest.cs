using AutoMapper;
using Castle.Core.Logging;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Interfaces;
using ECommerce.Api.Products.Models;
using ECommerce.Api.Products.Profile;
using ECommerce.Api.Products.Providers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace ECommerce.Api.Products.Test
{
    public class ProductServiceTest
    {
        public ProductServiceTest()
        {

        }


        [Fact]
        public async Task GetProductAsync__ReturnListOfProduct()
        {
            //arrang
            var options = new DbContextOptionsBuilder<ProductDbContext>().
                UseInMemoryDatabase(nameof(GetProductAsync__ReturnListOfProduct)).Options;

            var dbContext = new ProductDbContext(options);
            CreateProduct(dbContext);
            var profile = new ProductProfile();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(config);

            //act
            var _sut = new ProductProvider(dbContext, null, mapper);
            var result = await _sut.GetProductsAsync();

            //assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Products.Any());
            Assert.Null(result.ErrorMessage);
        }


        [Fact]
        public async Task GetProductAsync_ReturnProduct_UsingValidId()
        {
            //arrang
            var options = new DbContextOptionsBuilder<ProductDbContext>().
                UseInMemoryDatabase(nameof(GetProductAsync_ReturnProduct_UsingValidId)).Options;

            var dbContext = new ProductDbContext(options);
            CreateProduct(dbContext);
            var profile = new ProductProfile();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(config);

            //act
            var _sut = new ProductProvider(dbContext, null, mapper);
            var result = await _sut.GetProductAsync(1);

            //assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Product);
            Assert.True(result.Product.Id == 1);
            Assert.Null(result.ErrorMessage);
        }


        [Fact]
        public async Task GetProductAsync_DoNotReturnProduct_UsingInValidId()
        {
            //arrang
            var options = new DbContextOptionsBuilder<ProductDbContext>().
                UseInMemoryDatabase(nameof(GetProductAsync_DoNotReturnProduct_UsingInValidId)).Options;

            var dbContext = new ProductDbContext(options);
            CreateProduct(dbContext);
            var profile = new ProductProfile();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(config);

            //act
            var _sut = new ProductProvider(dbContext, null, mapper);
            var result = await _sut.GetProductAsync(-1);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Null(result.Product);
            Assert.NotNull(result.ErrorMessage);
        }

        private void CreateProduct(ProductDbContext dbContext)
        {
            for (int i = 1; i <= 10; i++)
            {
                dbContext.Products.Add(new Product { Id = i, Name = Guid.NewGuid().ToString(), Inventory = "Inventory" + i.ToString(), Price = i * 100 });
            }
            dbContext.SaveChanges();
        }
    }
}