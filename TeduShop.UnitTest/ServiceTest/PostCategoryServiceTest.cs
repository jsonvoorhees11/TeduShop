using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;
using TeduShop.Service;

namespace TeduShop.UnitTest.ServiceTest
{
    [TestClass]
    public class PostCategoryServiceTest
    {
        private Mock<IPostCategoryRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IPostCategoryService _categoryService;
        private List<PostCategory> _listCategory;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IPostCategoryRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _categoryService = new PostCategoryService(_mockRepository.Object, _mockUnitOfWork.Object);
            _listCategory = new List<PostCategory>()
            {
                new PostCategory(){ID=1, Name="DM1", Status=true},
                new PostCategory(){ID=2, Name="DM2", Status=true},
                new PostCategory(){ID=3, Name="DM3", Status=true}
            };
        }

        [TestMethod]
        public void PostCategory_Service_GetAll()
        {
            _mockRepository.Setup(m => m.GetAll(null)).Returns(_listCategory.AsQueryable());

            var result = _categoryService.GetAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public void PostCategory_Service_Create()
        {
            var postCategory = new PostCategory
            {
                ID = 1,
                Name = "test",
                Alias = "test",
                Status = true,
            };
            _mockRepository.Setup(m => m.Add(postCategory)).Returns((PostCategory p) =>
            {
                p.ID = 1;
                return p;
            });
            var result = _categoryService.Add(postCategory);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }
    }
}