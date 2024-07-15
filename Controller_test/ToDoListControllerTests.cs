using Xunit;
using Microsoft.AspNetCore.Mvc;
using ToDoListApi.Controllers;
using ToDoListApi.Abstractions;
using ToDoListApi.Containers;
using System.Collections.Generic;

namespace ToDoListApi.Tests
{
    public class ToDoListControllerTests
    {
        private readonly ToDoListController _controller;
        private readonly MemToDoList _container;

        public ToDoListControllerTests()
        {
            _container = new MemToDoList();
            _controller = new ToDoListController(_container);
        }

        [Fact]
        public void Add_and_Get_Valid_List()
        {
            // Populate one list
            var newItem = new ToDoListItem { Id = 0, Description = "String", IsCompleted = false };
            var newList = new ToDoList { Id = 0, Items = new List<ToDoListItem>() };
            _container.Add(newList);

            var result = _controller.Get(0);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<ToDoList>(okResult.Value);
            Assert.Equal(newList.Id, returnValue.Id);
        }

        [Fact]
        public void Get_Invalid_Id()
        {
            // Populate one list
            var newItem = new ToDoListItem { Id = 0, Description = "String", IsCompleted = false };
            var newList = new ToDoList { Id = 0, Items = new List<ToDoListItem>() };
            _container.Add(newList);

            // Get a non-existing list
            var result = _controller.Get(1);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Update_Valid_Id()
        {
            // Populate one list
            var newItem = new ToDoListItem { Id = 0, Description = "String", IsCompleted = false };
            var newList = new ToDoList { Id = 0, Title = "Old List", Items = new List<ToDoListItem>() };
            _container.Add(newList);

            // Create a new list
            var newItem_2 = new ToDoListItem { Id = 0, Description = "New_Item", IsCompleted = false };
            var newList_2 = new ToDoList { Id = 0, Title = "New List", Items = new List<ToDoListItem>() };

            // Get a non-existing list
            var result = _controller.Update(0, newList_2);

            var okResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<ToDoList>(okResult.Value);
            Assert.Equal(newList_2.Title, returnValue.Title);
        }

        [Fact]
        public void Update_Invalid_Id()
        {
            // Populate one list
            var newItem = new ToDoListItem { Id = 0, Description = "String", IsCompleted = false };
            var newList = new ToDoList { Id = 0, Title = "Old List", Items = new List<ToDoListItem>() };
            _container.Add(newList);

            // Create a new list
            var newItem_2 = new ToDoListItem { Id = 0, Description = "New_Item", IsCompleted = false };
            var newList_2 = new ToDoList { Id = 0, Title = "New List", Items = new List<ToDoListItem>() };

            // Get a non-existing list
            var result = _controller.Update(1, newList_2);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_Valid_Id()
        {
            // Populate one list
            var newItem = new ToDoListItem { Id = 0, Description = "String", IsCompleted = false };
            var newList = new ToDoList { Id = 0, Title = "Old List", Items = new List<ToDoListItem>() };
            _container.Add(newList);

            // Create a new list
            var newItem_2 = new ToDoListItem { Id = 0, Description = "New_Item", IsCompleted = false };
            var newList_2 = new ToDoList { Id = 1, Title = "New List", Items = new List<ToDoListItem>() };

            // Add another list
            _container.Add(newList_2);

            // Delete the first list
            var result = _controller.Delete(0);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<ToDoList>>(okResult.Value);

            // Check if the only list is the second list
            Assert.Equal(newList_2.Id, returnValue[0].Id);
        }
        
        [Fact]
        public void Delete_Invalid_Id()
        {
            // Populate one list
            var newItem = new ToDoListItem { Id = 0, Description = "String", IsCompleted = false };
            var newList = new ToDoList { Id = 0, Title = "Old List", Items = new List<ToDoListItem>() };
            _container.Add(newList);

            // Create a new list
            var newItem_2 = new ToDoListItem { Id = 0, Description = "New_Item", IsCompleted = false };
            var newList_2 = new ToDoList { Id = 1, Title = "New List", Items = new List<ToDoListItem>() };

            // Add another list
            _container.Add(newList_2);

            // Delete the first list
            var result = _controller.Delete(3);

            Assert.IsType<NotFoundResult>(result);
        }
    }

}
