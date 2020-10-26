using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using JuntoApi.Models;
using JuntoApi.Repositories;
using JuntoApi.ViewModels;
using System;

namespace JuntoApi.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _repository;
    
        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [Route("users")]
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _repository.Get();
        }

        [Route("users")]
        [HttpPost]
        public User Post([FromBody]User user)
        {
            return _repository.Save(user);
        }

        [Route("users")]
        [HttpPut]
        public User Put([FromBody]User user)
        {
            return _repository.Update(user);
        }

        [Route("users/changepassword")]
        [HttpPut]
        public IActionResult ChangePassword([FromBody]ChangePasswordViewModel model)
        {
            try
            {
                var user = _repository.Find(model.UserName, model.CurrentPassword);
                
                Validate(user, model);
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //Atribui nova senha.
                user.Password = model.NewPassword;

                return Ok(_repository.Update(user));   
            }
            catch
            {
                return BadRequest();
            }
        }

        private void Validate(User user, ChangePasswordViewModel model)
        {
            if(user == null)
            {
                ModelState.AddModelError("NotFound", "Usuário não encontrado.");
                return;
            }
            if (string.IsNullOrEmpty(model.NewPassword))
            {
                ModelState.AddModelError("EmptyPassword", "A senha não pode ser vazia.");
                return;
            }
            if(model.NewPassword != model.ConfirmPassword)
            {
                ModelState.AddModelError("MatchPassword", "A confirmação não confere com a nova senha");
                return;
            }
        }

        [Route("users")]
        [HttpDelete]
        public User Delete([FromBody]User user)
        {
            return _repository.Delete(user);
        }

    }
}