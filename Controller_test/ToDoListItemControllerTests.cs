using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ToDoListApi.Controllers;
using ToDoListApi.Abstractions;
using ToDoListApi.Containers;
using System.Collections.Generic;

namespace ToDoListApi.Tests
{
    public class ToDoListItemControllerTests
    {
        private readonly ToDoListItemController _controller;
        private readonly MemToDoListItem _container;
        private readonly Mock<IToDoListContainer> _mockContainer;

        public ToDoListItemControllerTests()
        {
            _mockContainer = new Mock<IToDoListContainer>();
            _container = new MemToDoListItem(_mockContainer.Object);
            _controller = new ToDoListItemController(_container);
        }

        [Fact]
        public void Add_Valid_Item()
        {
            // Populate one list
            var newItem = new ToDoListItem { Id = 0, Description = "String", IsCompleted = false };
            var newList = new ToDoList { Id = 0, Items = new List<ToDoListItem>() };
            _mockContainer.Setup(container => container.GetByList_Id(0)).Returns(newList);

            // Add the item to the list
            _controller.Add(newItem, 0);
            var result = _controller.Get(0, 0);


            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<ToDoListItem>(okResult.Value);
            Assert.Equal(newItem.Id, returnValue.Id);
        }

        [Fact]
        public void Get_Invalid_Item_Id()
        {
            // Populate one list
            var newItem = new ToDoListItem { Id = 0, Description = "String", IsCompleted = false };
            var newList = new ToDoList { Id = 0, Items = new List<ToDoListItem>() };
            _mockContainer.Setup(container => container.GetByList_Id(0)).Returns(newList);
            _controller.Add(newItem, 0);

            var result = _controller.Get(1, 1);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Complete_Valid_Item_Id()
        {
            // Populate one list
            var newItem = new ToDoListItem { Id = 0, Description = "String", IsCompleted = false };
            var newList = new ToDoList { Id = 0, Items = new List<ToDoListItem>() };
            _mockContainer.Setup(container => container.GetByList_Id(0)).Returns(newList);
            _controller.Add(newItem, 0);

            var result = _controller.Complete(0, 0);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ToDoListItem>(okResult.Value);
            Assert.True(returnValue.IsCompleted);
        }

        [Fact]
        public void Complete_InValid_Item_Id()
        {
            // Populate one list
            var newItem = new ToDoListItem { Id = 0, Description = "String", IsCompleted = false };
            var newList = new ToDoList { Id = 0, Items = new List<ToDoListItem>() };
            _mockContainer.Setup(container => container.GetByList_Id(0)).Returns(newList);
            _controller.Add(newItem, 0);

            var result = _controller.Complete(1, 1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Update_Valid_Item_Id()
        {
            // Populate one list
            var newItem = new ToDoListItem { Id = 0, Description = "String", IsCompleted = false };
            var newItem2 = new ToDoListItem { Id = 0, Description = "New_Item", IsCompleted = false };
            var newList = new ToDoList { Id = 0, Items = new List<ToDoListItem>() };
            _mockContainer.Setup(container => container.GetByList_Id(0)).Returns(newList);
            _controller.Add(newItem, 0);

            var result = _controller.Update(0, newItem2, 0);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ToDoListItem>(okResult.Value);
            Assert.Equal("New_Item", returnValue.Description);
        }

        [Fact]
        public void Update_InValid_Item_Id()
        {
            // Populate one list
            var newItem = new ToDoListItem { Id = 0, Description = "String", IsCompleted = false };
            var newItem2 = new ToDoListItem { Id = 0, Description = "New_Item", IsCompleted = false };
            var newList = new ToDoList { Id = 0, Items = new List<ToDoListItem>() };
            _mockContainer.Setup(container => container.GetByList_Id(0)).Returns(newList);
            _controller.Add(newItem, 0);

            var result = _controller.Update(1, newItem2, 1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_Valid_Item_Id()
        {
            // Populate one list
            var newItem = new ToDoListItem { Id = 0, Description = "String", IsCompleted = false };
            var newItem2 = new ToDoListItem { Id = 1, Description = "New_Item", IsCompleted = false };
            var newList = new ToDoList { Id = 0, Items = new List<ToDoListItem>() };
            _mockContainer.Setup(container => container.GetByList_Id(0)).Returns(newList);
            // Add both items to the list
            _controller.Add(newItem, 0);
            _controller.Add(newItem2, 0);

            // Delete the first list
            var result = _controller.Delete(0, 0);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Delete_InValid_Item_Id()
        {
            // Populate one list
            var newItem = new ToDoListItem { Id = 0, Description = "String", IsCompleted = false };
            var newItem2 = new ToDoListItem { Id = 1, Description = "New_Item", IsCompleted = false };
            var newList = new ToDoList { Id = 0, Items = new List<ToDoListItem>() };
            _mockContainer.Setup(container => container.GetByList_Id(0)).Returns(newList);
            // Add both items to the list
            _controller.Add(newItem, 0);
            _controller.Add(newItem2, 0);

            // Delete the first list
            var result = _controller.Delete(2, 0);

            Assert.IsType<NotFoundResult>(result);
        }

    }

}
