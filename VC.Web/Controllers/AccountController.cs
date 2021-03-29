using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VC.Web.Data;
using VC.Web.Data.Entities;
using VC.Web.Helpers;
using VC.Web.Models;

namespace VC.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IUserHelper _userHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly IApiRest _apiRest;
        //public AccountController(DataContext context, IUserHelper userHelper)
        //{
        //    _context = context;
        //    _userHelper = userHelper;
        //}
        public AccountController(DataContext context, IUserHelper userHelper, IBlobHelper blobHelper, UserManager<User> userManager, IApiRest apiRest)
        {
            _context = context;
            _userHelper = userHelper;
            _blobHelper = blobHelper;
            _userManager = userManager;
            _apiRest = apiRest;
        }


        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, Startup.messageLoginFail);
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult Register()
        {
            AddUserViewModel model = new AddUserViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;

                if (model.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "users");
                }
                UserType userType = _context.UserTypes.Where(u => u.Name == "User").FirstOrDefault();
                //User user = await _userHelper.AddUserAsync(model, imageId, userType);

                //
                User user = new User
                {
                    Address = model.Address,
                    Document = model.Document,
                    Email = model.Username,
                    FirstName = _userHelper.ReplaceAcento(model.FirstName),
                    LastName = _userHelper.ReplaceAcento(model.LastName),
                    MotherLastName = _userHelper.ReplaceAcento(model.MotherLastName),
                    Image = imageId,
                    PhoneNumber = model.PhoneNumber,
                    //City = await _context.Cities.FindAsync(model.CityId),
                    UserName = model.Username,
                    UserType = userType
                };
                User _user = _context.Users.Where(u => u.Document == user.Document).FirstOrDefault();
                if(_user!=null)
                {
                    ModelState.AddModelError(string.Empty, "El documento de indentidad ya ha sido registrado");
                    return View(model);
                }
                IdentificationDocument _dni = await _apiRest.GetDni(user.Document);
                if (_dni == null)
                {
                    ModelState.AddModelError(string.Empty, "El documento de indentidad no existe");
                    return View(model);
                }
                if (_dni != null)
                {
                    if (_dni.nombres.ToUpper() != user.FirstName.ToUpper() || _dni.apellido_paterno.ToUpper() != user.LastName.ToUpper() || _dni.apellido_materno.ToUpper() != user.MotherLastName.ToUpper())
                    {
                        ModelState.AddModelError(string.Empty, "El nombre y apellidos no concuerda con el DNI");
                        return View(model);
                    }
                }
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result != IdentityResult.Success)
                {
                    foreach (IdentityError i in result.Errors.ToList())
                    {
                        if(i.Code!= "DuplicateUserName")
                            ModelState.AddModelError(string.Empty, _userHelper.TranslateError(i.Code));
                    }
                    return View(model);
                }
                
                user = await _userHelper.GetUserAsync(model.Username);
                await _userHelper.AddUserToRoleAsync(user, userType.Name);
                //return newUser;
                //
                //if (user == null)
                //{
                //    ModelState.AddModelError(string.Empty, Startup.messageRegisterUser);
                //    return View(model);
                //}

                LoginViewModel loginViewModel = new LoginViewModel
                {
                    Password = model.Password,
                    RememberMe = false,
                    Username = model.Username
                };

                var result2 = await _userHelper.LoginAsync(loginViewModel);

                if (result2.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ChangeUser()
        {
            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            EditUserViewModel model = new EditUserViewModel
            {
                Address = user.Address,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MotherLastName = user.MotherLastName,
                PhoneNumber = user.PhoneNumber,
                ImageId = user.Image,
                Id = user.Id,
                Document = user.Document
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = model.ImageId;

                if (model.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "users");
                }

                User user = await _userHelper.GetUserAsync(User.Identity.Name);

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Address = model.Address;
                user.PhoneNumber = model.PhoneNumber;
                user.Image = imageId;
                user.Document = model.Document;

                await _userHelper.UpdateUserAsync(user);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserAsync(User.Identity.Name);
                if (user != null)
                {
                    var result = await _userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("ChangeUser");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User no found.");
                }
            }

            return View(model);
        }

    }

}
