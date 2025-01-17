﻿using DTO.In;
using DTO.Out;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/v2/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ISessionService _sessionService;
        public LoginController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginInput loginInput)
        {
            var session = _sessionService.Login(loginInput.Email, loginInput.Password);
            var response = new SuccessfulLoginOutput(session);
            return Ok(response);
        }

    }
}
