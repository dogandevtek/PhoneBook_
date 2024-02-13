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
    public class UserContactController : ControllerBase {

        private IUserContactService _userContactService { get; }
        private IUserService _userService { get; }
        private IMapper _mapper { get; }
        private IValidator<UserContactUDTO> _validator { get; }

        public UserContactController(IUserContactService userContactService, IUserService userService, IMapper mapper, IValidator<UserContactUDTO> validator) {
            _userContactService = userContactService;
            _userService = userService;
            _mapper = mapper;
            _validator = validator;
        }

        //TODO pagination ve filtreleme eklenebilir
        [HttpGet("[action]")]
        public async Task<ActionResult<List<UserContactDTO>>> GetAllByUserIdAsync(int userId) {
            var user = await _userService.GetAsync(userId);
            if (user == null)
                return NotFound("User not found!");

            var userContactList = await _userContactService.GetAllByUserIdAsync(userId);

            return Ok(_mapper.Map<List<UserContactDTO>>(userContactList));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserContactDTO>> GetAsync(int id) {
            var userContact = await _userContactService.GetAsync(id);
            if (userContact == null)
                return NotFound("UserContact not found!");

            return Ok(_mapper.Map<UserContactDTO>(userContact));
        }

        [HttpPost]
        public async Task<ActionResult<UserContactDTO>> PostAsync([FromBody] UserContactUDTO userContactDto) {
            var validationResult = _validator.Validate(userContactDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var user = await _userService.GetAsync(userContactDto.UserId);
            if (user == null) return NotFound("User not found!");

            var userContact = await _userContactService.CreateAsync(_mapper.Map<UserContact>(userContactDto));

            return Ok(_mapper.Map<UserContactDTO>(userContact));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserContactDTO>> PutAsync(int id, [FromBody] UserContactUDTO userContactDto) {
            var validationResult = _validator.Validate(userContactDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var userContact = await _userContactService.GetAsync(id);
            if (userContact == null) return NotFound("UserContact not found!");

            if (userContact.UserId != userContactDto.UserId)
                return BadRequest("UserId can't be modified!");

            userContact = _mapper.Map(userContactDto, userContact);
            userContact = await _userContactService.UpdateAsync(userContact);

            return Ok(_mapper.Map<UserContactDTO>(userContact));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id) {
            var userContact = await _userContactService.GetAsync(id);
            if (userContact == null)
                return NotFound("UserContact not found!");

            await _userContactService.DeleteAsync(userContact);
            return Ok();
        }
    }
}
