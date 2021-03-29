using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using VC.Web.Data;
using VC.Web.Data.Entities;
using VC.Web.Models;

namespace VC.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        public UserHelper(DataContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(
                model.Username,
                model.Password,
                model.RememberMe,
                false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }


        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task CheckRoleAsync(string roleName)
        {
            bool roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }
        }

        public async Task<User> GetUserAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> IsUserInRoleAsync(User user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<User> AddUserAsync(AddUserViewModel model, Guid imageId, UserType userType)
        {
            User user = new User
            {
                Address = model.Address,
                Document = model.Document,
                Email = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MotherLastName = model.MotherLastName,
                Image = imageId,
                PhoneNumber = model.PhoneNumber,
                //City = await _context.Cities.FindAsync(model.CityId),
                UserName = model.Username,
                UserType = userType
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result != IdentityResult.Success)
            {                
                return null;
            }

            User newUser = await GetUserAsync(model.Username);
            await AddUserToRoleAsync(newUser, userType.Name);
            return newUser;
        }
        public string TranslateError(String code)
        {
            string result;
            switch(code)
            {
                case "PasswordRequiresNonAlphanumeric": result = "El campo contraseña requiere al menos un caracter especial"; break;
                case "PasswordRequiresUpper": result = "El campo contraseña requiere al menos una letra mayúscula (A-Z)"; break;
                case "PasswordRequiresDigit": result = "El campo contraseña requiere al menos un número"; break;
                case "DuplicateUserName": result = "El correo ya existe"; break;
                case "DuplicateEmail": result = "El correo ya existe"; break;
                default: result = code; break;
            }
            return result;
        }

        public async Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<User> GetUserAsync(Guid userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId.ToString());
        }
        public async Task<User> GetUserAsyncxDocument(string document)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Document == document);
        }

        public string ReplaceAcento(string text)
        {
            if (text.Contains("á"))
                text = text.Replace("á", "a");
            if (text.Contains("é"))
                text = text.Replace("é", "e");
            if (text.Contains("í"))
                text = text.Replace("í", "i");
            if (text.Contains("ó"))
                text = text.Replace("ó", "o");
            if (text.Contains("ú"))
                text = text.Replace("ú", "u");
            return text;
        }
    }

}
