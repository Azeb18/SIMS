using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SIMS.Models;
using System.Collections.Generic;


namespace SIMS.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext context;

        public AccountController()
        {
            context = new ApplicationDbContext();
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }









        [Authorize(Roles="Security,Cafe")]
        public ActionResult Identification()
        {
            IdentificationViewModel model = new IdentificationViewModel();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Security,Cafe")]
        public ActionResult Identification(IdentificationViewModel model)
        {
            model.Student = context.Students.SingleOrDefault(m=>m.IdNumber == model.StudentIdNumber);
            if (model.Student == null)
            {
                TempData["ErrorMessage"] = "The Id Number is Invalid.";
            }
            else
            {
                TempData["SuccessMessage"] = "The Id Number is valid. See student profile below.";
            }
            return View(model);
        }

        // GET: /Account/RegisterCafe
        [Authorize(Roles = "AcademicDean")]
        public ActionResult RegisterCafe()
        {
            return View();
        }

        // POST: /Account/RegisterCafe
        [HttpPost]
        [Authorize(Roles = "AcademicDean")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterCafe(RegisterCafeViewModel model)
        {
            if (ModelState.IsValid)
            {
                string IdNumber = GenerateCafeIdNumber(model);
                string Password = "Password0-";

                var cafe = new Cafe
                {
                    //Personal Infromation
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    Sex = model.Sex,
                    DateOfBirth = model.DateOfBirth,
                    // Address
                    Region = model.Region,
                    Town = model.Town,
                    SubCity = model.SubCity,
                    Woreda = model.Woreda,
                    HouseNumber = model.HouseNumber,
                    // Account Details
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserName = IdNumber,
                    IdNumber = IdNumber,
                };

                var result = await UserManager.CreateAsync(cafe, Password);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(cafe.Id, "Cafe");

                    TempData["NewId"] = IdNumber;
                    TempData["NewPassword"] = Password;
                }
                AddErrors(result);
            }
            return View(model);
        }

        // GET: /Account/RegisterSecurity
        [Authorize(Roles = "AcademicDean")]
        public ActionResult RegisterSecurity()
        {
            return View();
        }

        // POST: /Account/RegisterSecurity
        [HttpPost]
        [Authorize(Roles = "AcademicDean")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterSecurity(RegisterSecurityViewModel model)
        {
            if (ModelState.IsValid)
            {
                string IdNumber = GenerateSecurityIdNumber(model);
                string Password = "Password0-";

                var security = new Security
                {
                    //Personal Infromation
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    Sex = model.Sex,
                    DateOfBirth = model.DateOfBirth,
                    // Address
                    Region = model.Region,
                    Town = model.Town,
                    SubCity = model.SubCity,
                    Woreda = model.Woreda,
                    HouseNumber = model.HouseNumber,
                    // Account Details
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserName = IdNumber,
                    IdNumber = IdNumber,
                };

                var result = await UserManager.CreateAsync(security, Password);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(security.Id, "Security");

                    TempData["NewId"] = IdNumber;
                    TempData["NewPassword"] = Password;
                }
                AddErrors(result);
            }
            return View(model);
        }

        // GET: /Account/RegisterAcademicDean
        [Authorize(Roles = "AcademicDean")]
        public ActionResult RegisterAcademicDean()
        {
            return View();
        }

        // POST: /Account/RegisterAcademicDean
        [Authorize(Roles = "AcademicDean")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterAcademicDean(RegisterAcademicDeanViewModel model)
        {
            if (ModelState.IsValid)
            {
                string IdNumber = GenerateAcademicDeanIdNumber(model);
                string Password = "Password0-";

                var academicDean = new AcademicDean
                {
                    //Personal Infromation
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    Sex = model.Sex,
                    DateOfBirth = model.DateOfBirth,
                    // Address
                    Region = model.Region,
                    Town = model.Town,
                    SubCity = model.SubCity,
                    Woreda = model.Woreda,
                    HouseNumber = model.HouseNumber,
                    // Account Details
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserName = IdNumber,
                    IdNumber = IdNumber,
                };

                var result = await UserManager.CreateAsync(academicDean, Password);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(academicDean.Id, "AcademicDean");

                    TempData["NewId"] = IdNumber;
                    TempData["NewPassword"] = Password;
                }
                AddErrors(result);
            }
            return View(model);
        }

        // GET: /Account/RegisterCoordinator
[Authorize(Roles = "AcademicDean")]
        public ActionResult RegisterCoordinator()
        {
            return View();
        }

        // POST: /Account/RegisterCoordinator
        [HttpPost]
        [Authorize(Roles = "AcademicDean")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterCoordinator(RegisterCoordinatorViewModel model)
        {
            if (ModelState.IsValid)
            {
                string IdNumber = GenerateCoordinatorIdNumber(model);
                string Password = "Password0-";

                var coordinator = new Coordinator
                {
                    //Personal Infromation
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    Sex = model.Sex,
                    DateOfBirth = model.DateOfBirth,
                    // Address
                    Region = model.Region,
                    Town = model.Town,
                    SubCity = model.SubCity,
                    Woreda = model.Woreda,
                    HouseNumber = model.HouseNumber,
                    // Account Details
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserName = IdNumber,
                    IdNumber = IdNumber,
                };

                var result = await UserManager.CreateAsync(coordinator, Password);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(coordinator.Id, "Coordinator");
                    switch(model.Type)
                    {
                        case "Regular":
                            UserManager.AddToRole(coordinator.Id, "DaytimeCoordinator");
                            break;
                        case "Extension and Distance":
                            UserManager.AddToRole(coordinator.Id, "ExtensionAndDistanceCoordinator");
                            break;
                        default:
                            TempData["ErrorMessage"] = "Invalid type was specified";
                            break;
                    }

                    TempData["NewId"] = IdNumber;
                    TempData["NewPassword"] = Password;
                }
                AddErrors(result);
            }
            return View(model);
        }
        
        // GET: /Account/RegisterProperty
[Authorize(Roles = "AcademicDean")]
        public ActionResult RegisterProperty()
        {
            return View();
        }

        // POST: /Account/RegisterProperty
        [HttpPost]
        [Authorize(Roles = "AcademicDean")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterProperty(RegisterPropertyViewModel model)
        {
            if (ModelState.IsValid)
            {
                string IdNumber = GeneratePropertyIdNumber(model);
                string Password = "Password0-";

                var property = new Property
                {
                    //Personal Infromation
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    Sex = model.Sex,
                    DateOfBirth = model.DateOfBirth,
                    // Address
                    Region = model.Region,
                    Town = model.Town,
                    SubCity = model.SubCity,
                    Woreda = model.Woreda,
                    HouseNumber = model.HouseNumber,
                    // Account Details
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserName = IdNumber,
                    IdNumber = IdNumber,
                };

                var result = await UserManager.CreateAsync(property, Password);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(property.Id, "Property");

                    TempData["NewId"] = IdNumber;
                    TempData["NewPassword"] = Password;
                }
                AddErrors(result);
            }
            return View(model);
        }

        // GET: /Account/RegisterRegistrar
[Authorize(Roles = "AcademicDean")]
        public ActionResult RegisterRegistrar()
        {
            return View();
        }

        // POST: /Account/RegisterRegistrar
        [HttpPost]
        [Authorize(Roles = "AcademicDean")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterRegistrar(RegisterRegistrarViewModel model)
        {
            if (ModelState.IsValid)
            {
                string IdNumber = GenerateRegistrarIdNumber(model);
                string Password = "Password0-";

                var registrar = new Registrar
                {
                    //Personal Infromation
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    Sex = model.Sex,
                    DateOfBirth = model.DateOfBirth,
                    // Address
                    Region = model.Region,
                    Town = model.Town,
                    SubCity = model.SubCity,
                    Woreda = model.Woreda,
                    HouseNumber = model.HouseNumber,
                    // Account Details
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserName = IdNumber,
                    IdNumber = IdNumber,
                };

                var result = await UserManager.CreateAsync(registrar, Password);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(registrar.Id, "Registrar");

                    TempData["NewId"] = IdNumber;
                    TempData["NewPassword"] = Password;
                }
                AddErrors(result);
            }
            return View(model);
        }

        // GET: /Account/RegisterLibrarian
[Authorize(Roles = "AcademicDean")]
        public ActionResult RegisterLibrarian()
        {
            return View();
        }

        // POST: /Account/RegisterLibrarian
        [HttpPost]
        [Authorize(Roles = "AcademicDean")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterLibrarian(RegisterLibrarianViewModel model)
        {
            if (ModelState.IsValid)
            {
                string IdNumber = GenerateLibrarianIdNumber(model);
                string Password = "Password0-";

                var librarian = new Librarian
                {
                    //Personal Infromation
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    Sex = model.Sex,
                    DateOfBirth = model.DateOfBirth,
                    // Address
                    Region = model.Region,
                    Town = model.Town,
                    SubCity = model.SubCity,
                    Woreda = model.Woreda,
                    HouseNumber = model.HouseNumber,
                    // Account Details
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserName = IdNumber,
                    IdNumber = IdNumber,
                };

                var result = await UserManager.CreateAsync(librarian, Password);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(librarian.Id, "Librarian");

                    TempData["NewId"] = IdNumber;
                    TempData["NewPassword"] = Password;
                }
                AddErrors(result);
            }
            return View(model);
        }

        // GET: /Account/RegisterTeacher
[Authorize(Roles = "AcademicDean")]
        public ActionResult RegisterTeacher()
        {
            return View();
        }

        // POST: /Account/RegisterTeacher
        [HttpPost]
        [Authorize(Roles = "AcademicDean")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterTeacher(RegisterTeacherViewModel model)
        {
            if (ModelState.IsValid)
            {
                string IdNumber = GenerateTeacherIdNumber(model);
                string Password = "Password0-";

                var teacher = new Teacher
                {
                    //Personal Infromation
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    Sex = model.Sex,
                    DateOfBirth = model.DateOfBirth,
                    // Address
                    Region = model.Region,
                    Town = model.Town,
                    SubCity = model.SubCity,
                    Woreda = model.Woreda,
                    HouseNumber = model.HouseNumber,
                    // Account Details
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserName = IdNumber,
                    IdNumber = IdNumber,
                };

                var result = await UserManager.CreateAsync(teacher, Password);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(teacher.Id, "Teacher");

                    TempData["NewId"] = IdNumber;
                    TempData["NewPassword"] = Password;
                }
                AddErrors(result);
            }
            return View(model);
        }

        // GET: /Account/RegisterStudent
[Authorize(Roles = "Registrar")]
        public ActionResult RegisterStudent()
        {
            RegisterStudentViewModel model = new RegisterStudentViewModel();
            model.Programs = context.Programs.ToList();
            model.Levels = context.Levels.ToList();
            return View(model);
        }

        // POST: /Account/RegisterStudent
        [HttpPost]
        [Authorize(Roles = "Registrar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterStudent(RegisterStudentViewModel model)
        {
            IEnumerable<Program> Programs = context.Programs.ToList();
            IEnumerable<Level> Levels = context.Levels.ToList();
            model.Programs = Programs;
            model.Levels = Levels;

            if (ModelState.IsValid)
            {
                string IdNumber = GenerateStudentIdNumber(model);
                string Password = "Password0-";

                var student = new Student
                {
                    //Personal Infromation
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    Sex = model.Sex,
                    DateOfBirth = model.DateOfBirth,
                    // Address
                    Region = model.Region,
                    Town = model.Town,
                    SubCity = model.SubCity,
                    Woreda = model.Woreda,
                    HouseNumber = model.HouseNumber,
                    // Enrollment
                    ProgramId = model.ProgramId,
                    LevelId = model.LevelId,
                    Year = 1,
                    // Account Details
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    RegisterationDate = DateTime.Now,
                    UserName = IdNumber,
                    IdNumber = IdNumber,
                };

                var result = await UserManager.CreateAsync(student, Password);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(student.Id, "Student");
                    
                    switch(model.Programs.First(m=>m.ProgramId == model.ProgramId).Name)
                    {
                        case "Regular":
                            UserManager.AddToRole(student.Id, "RegularStudent");
                            break;
                        case "Extension":
                            UserManager.AddToRole(student.Id, "ExtensionStudent");
                            break;
                        case "Distance":
                            UserManager.AddToRole(student.Id, "DistanceStudent");
                            break;
                    }

                    TempData["NewId"] = IdNumber;
                    TempData["NewPassword"] = Password;
                }
                AddErrors(result);
            }

            //if(ModelState.IsValid)
            //{
            //    string IdNumber = "Lib/" + new Random().Next(1000,9999) + "/" + DateTime.Now.Year;
            //    string Password = "Password0-";
            //    Librarian librarian = new Librarian
            //    {
            //        //Personal Infromation
            //        FirstName = model.FirstName,
            //        MiddleName = model.MiddleName,
            //        LastName = model.LastName,
            //        Sex = model.Sex,
            //        DateOfBirth = model.DateOfBirth,
            //        // Address
            //        Region = model.Region,
            //        Town = model.Town,
            //        SubCity = model.SubCity,
            //        Woreda = model.Woreda,
            //        HouseNumber = model.HouseNumber,
            //        // Account Details
            //        Email = model.Email,
            //        PhoneNumber = model.PhoneNumber,
            //        UserName = IdNumber,
            //        IdNumber = IdNumber
            //    };

            //    var result = await UserManager.CreateAsync(librarian, Password);
            //    if (result.Succeeded)
            //    {
            //        TempData["NewId"] = IdNumber;
            //        TempData["NewPassword"] = Password;
            //    }
            //}

            return View(model);
        }


        // TODO: Improve Id generation logic 
        public string GenerateStudentIdNumber(RegisterStudentViewModel model)
        {
            string IdNumber = new Random().Next(1000, 9999) + "/" + DateTime.Now.Year;
            if(context.Students.Any(m=>m.IdNumber == IdNumber))
            {
                GenerateStudentIdNumber(model);
            }
            return model.Programs.First(p => p.ProgramId == model.ProgramId).IdPrefix + "/" + IdNumber;
        }

        // TODO: Improve Id generation logic 
        public string GenerateTeacherIdNumber(RegisterTeacherViewModel model)
        {
            string IdNumber = "TCH/" + new Random().Next(1000, 9999) + "/" + DateTime.Now.Year;
            if (context.Teachers.Any(m => m.IdNumber == IdNumber))
            {
                GenerateTeacherIdNumber(model);
            }
            return IdNumber;
        }

        // TODO: Improve Id generation logic 
        public string GenerateSecurityIdNumber(RegisterSecurityViewModel model)
        {
            string IdNumber = "SEC/" + new Random().Next(1000, 9999) + "/" + DateTime.Now.Year;
            if (context.Security.Any(m => m.IdNumber == IdNumber))
            {
                GenerateSecurityIdNumber(model);
            }
            return IdNumber;
        }

        // TODO: Improve Id generation logic 
        public string GenerateCafeIdNumber(RegisterCafeViewModel model)
        {
            string IdNumber = "CAF/" + new Random().Next(1000, 9999) + "/" + DateTime.Now.Year;
            if (context.Cafe.Any(m => m.IdNumber == IdNumber))
            {
                GenerateCafeIdNumber(model);
            }
            return IdNumber;
        }


        // TODO: Improve Id generation logic 
        public string GenerateLibrarianIdNumber(RegisterLibrarianViewModel model)
        {
            string IdNumber = "LIB/" + new Random().Next(1000, 9999) + "/" + DateTime.Now.Year;
            if (context.Librarians.Any(m => m.IdNumber == IdNumber))
            {
                GenerateLibrarianIdNumber(model);
            }
            return IdNumber;
        }

        // TODO: Improve Id generation logic 
        public string GenerateRegistrarIdNumber(RegisterRegistrarViewModel model)
        {
            string IdNumber = "REG/" + new Random().Next(1000, 9999) + "/" + DateTime.Now.Year;
            if (context.Registrars.Any(m => m.IdNumber == IdNumber))
            {
                GenerateRegistrarIdNumber(model);
            }
            return IdNumber;
        }

        // TODO: Improve Id generation logic 
        public string GeneratePropertyIdNumber(RegisterPropertyViewModel model)
        {
            string IdNumber = "PRP/" + new Random().Next(1000, 9999) + "/" + DateTime.Now.Year;
            if (context.Property.Any(m => m.IdNumber == IdNumber))
            {
                GeneratePropertyIdNumber(model);
            }
            return IdNumber;
        }

        // TODO: Improve Id generation logic 
        public string GenerateCoordinatorIdNumber(RegisterCoordinatorViewModel model)
        {
            string IdNumber = "COR/" + new Random().Next(1000, 9999) + "/" + DateTime.Now.Year;
            if (context.Coordinators.Any(m => m.IdNumber == IdNumber))
            {
                GenerateCoordinatorIdNumber(model);
            }
            return IdNumber;
        }

        // TODO: Improve Id generation logic 
        public string GenerateAcademicDeanIdNumber(RegisterAcademicDeanViewModel model)
        {
            string IdNumber = "ACD/" + new Random().Next(1000, 9999) + "/" + DateTime.Now.Year;
            if (context.AcademicDean.Any(m => m.IdNumber == IdNumber))
            {
                GenerateAcademicDeanIdNumber(model);
            }
            return IdNumber;
        }








        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.UserName.Trim(), model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}