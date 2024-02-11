using AutoMapper;
using ContactService.Application.Models.Request;
using ContactService.Application.Models.Response;
using ContactService.Application.Services;
using ContactService.Application.Validator;
using ContactService.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContactService.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {

        private IUserService _userService { get; }
        private IMapper _mapper { get; }
        private IValidator<UserUDTO> _validator { get; }

        public UserController(IUserService userService, IMapper mapper, IValidator<UserUDTO> validator) {
            _userService = userService;
            _mapper = mapper;
            _validator = validator;
        }

        //TODO pagination ve filtreleme eklenebilir
        [HttpGet("[action]")]
        public async Task<ActionResult<List<UserDTO>>> GetAllAsync() {
            var userList = await _userService.GetAllAsync();

            return Ok(_mapper.Map<List<UserDTO>>(userList));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserEDTO>> GetAsync(int id) {
            var user = await _userService.GetAsync(id);
            if (user == null)
                return NotFound("User not found!");

            return Ok(_mapper.Map<UserEDTO>(user));
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostAsync([FromBody] UserUDTO userDto) {
            var validationResult = _validator.Validate(userDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var user = await _userService.CreateAsync(_mapper.Map<User>(userDto));

            return Ok(_mapper.Map<UserDTO>(user));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserDTO>> PutAsync(int id, [FromBody] UserUDTO userDto) {
            var validationResult = _validator.Validate(userDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var user = await _userService.GetAsync(id);
            if (user == null)
                return NotFound("User not found!");

            user = _mapper.Map(userDto, user);
            user = await _userService.UpdateAsync(user);

            return Ok(_mapper.Map<UserDTO>(user));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id) {
            var user = await _userService.GetAsync(id);
            if (user == null) return NotFound();

            await _userService.DeleteAsync(user);
            return Ok();
        }
    }
}
