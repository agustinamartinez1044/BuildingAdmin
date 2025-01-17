﻿using Microsoft.AspNetCore.Mvc;
using IServices;
using DTO.In;
using DTO.Out;
using WebApi.Filters;
using WebApi.Constants;

namespace WebApi.Controllers
{
    [Route("api/v2/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpPost]
        [AuthenticationFilter(Role = RoleConstants.AdministratorRole)]
        public IActionResult CreateCategory([FromBody] CreateCategoryInput createCategoryModel)
        {
            var category = _service.CreateCategory(createCategoryModel.Name, createCategoryModel.ParentCategoryId);
            return Ok(new CategoryOutput(category));
        }

        [HttpGet]
        public IActionResult Get([FromQuery] int? id)
        {
            if(id == null)
            {
                var categories = _service.GetAll();
                List<GetCategoryOutput> response = new List<GetCategoryOutput>();
                categories.ForEach(category =>
                {
                    response.Add(new GetCategoryOutput(category));
                });
                return Ok(response);
            }
            else 
            {
                var category = _service.Get(id.Value);
                return Ok(new GetCategoryOutput(category));
            }
            
        }
    }
}
