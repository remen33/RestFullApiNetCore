﻿namespace SocialMedia.Api.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SocialMedia.Api.Response;
    using SocialMedia.Core.DTOs;
    using SocialMedia.Core.Entities;
    using SocialMedia.Core.Enumerations;
    using SocialMedia.Core.Interfaces;
    using System.Threading.Tasks;

    [Authorize(Roles = nameof(RoleType.Administrator))]    
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService securityService;
        private readonly IMapper mapper;
        public SecurityController(ISecurityService securityService, IMapper mapper)
        {
            this.securityService = securityService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(SecurityDto securityDto)
        {
            var security = this.mapper.Map<Security>(securityDto);
            await this.securityService.RegisterUser(security);
            securityDto = this.mapper.Map<SecurityDto>(security);
            var response = new ApiResponse<SecurityDto>(securityDto);
            return Ok(response);
        }
    }
}
