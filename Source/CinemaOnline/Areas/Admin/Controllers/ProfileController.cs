using BELibrary.Core.Entity;
using BELibrary.Core.Utils;
using BELibrary.DbContext;
using BELibrary.Utils;
using CinemaOnline.Areas.Admin.Authorization;
using System;
using System.Web.Mvc;

namespace CinemaOnline.Areas.Admin.Controllers
{
    public class ProfileController : BaseController
    {
        // GET: Admin/Profile

        private string _keyElement = "Tài khoản";

        public ActionResult Index()
        {
            ViewBag.Feature = "Hồ sơ";
            ViewBag.Element = _keyElement;
            var user = GetCurrentUser();

            if (user == null)
                return Redirect("/admin");

            return View(user);
        }

        public ActionResult Edit()
        {
            ViewBag.Feature = "Cập nhật";
            ViewBag.Element = _keyElement;
            var user = GetCurrentUser();

            if (user == null)
                return Redirect("/admin");

            ViewBag.Genders = new SelectList(GenderKey.GetDic(), "Value", "Text");

            return View(user);
        }

        [HttpPost, ValidateInput(false)]
        public JsonResult UpdateInfo(BELibrary.Entity.Admin input, Guid? facultyId, string rePassword)
        {
            try
            {
                using (var workScope = new UnitOfWork(new CinemaOnlineDbContext()))
                {
                    var account = GetCurrentUser();

                    if (account != null) //update
                    {
                        //xu ly password
                        if (!string.IsNullOrEmpty(input.Password) || rePassword != "")
                        {
                            if (!CookiesManage.Logined())
                            {
                                return Json(new { status = false, mess = "Chưa đăng nhập" });
                            }
                            if (input.Password != rePassword)
                            {
                                return Json(new { status = false, mess = "Mật khẩu không khớp" });
                            }

                            var passwordFactory = input.Password + VariableExtensions.KeyCryptor;
                            var passwordCryptor = CryptorEngine.Encrypt(passwordFactory, true);
                            input.Password = passwordCryptor;
                        }
                        else
                        {
                            input.Password = account.Password;
                        }

                        input.Id = account.Id;
                        input.UserName = account.UserName;

                        var role = workScope.Roles.FirstOrDefault(r => r.RoleEnum == RoleKey.Admin);

                        input.RoleId = role.Id;

                        account = input;
                        workScope.Admins.Put(account, account.Id);

                        workScope.Complete();

                        return Json(new { status = true, mess = "Cập nhập thành công " });
                    }
                    else
                    {
                        return Json(new { status = false, mess = "Không tồn tại " + _keyElement });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, mess = "Có lỗi xảy ra: " + ex.Message });
            }
        }
    }
}