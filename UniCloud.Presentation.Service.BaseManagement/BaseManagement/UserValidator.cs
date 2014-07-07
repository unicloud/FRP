#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：21:20
// 方案：FRP
// 项目：Service.BaseManagement
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations;

#endregion

namespace UniCloud.Presentation.Service.BaseManagement.BaseManagement
{
    public class UserValidator
    {
        public static ValidationResult ValidateUserName(string userName, ValidationContext validationContext)
        {
            var user = validationContext.ObjectInstance as UserDTO;
            if (user != null && string.IsNullOrWhiteSpace(user.UserName))
            {
                return new ValidationResult("登录名必填。", new[] {validationContext.MemberName});
            }
            return ValidationResult.Success;
        }

        public static ValidationResult ValidatePassword(string userName, ValidationContext validationContext)
        {
            var user = validationContext.ObjectInstance as UserDTO;
            if (user != null && string.IsNullOrWhiteSpace(user.Password))
            {
                return new ValidationResult("密码必填。", new[] {validationContext.MemberName});
            }
            return ValidationResult.Success;
        }

        public static ValidationResult ValidatePasswordConfirm(string userName, ValidationContext validationContext)
        {
            var user = validationContext.ObjectInstance as UserDTO;
            if (user != null && string.IsNullOrWhiteSpace(user.PasswordConfirm))
            {
                return new ValidationResult("密码确认必填。", new[] {validationContext.MemberName});
            }
            if (user != null && !user.PasswordConfirm.Equals(user.Password))
            {
                return new ValidationResult("密码确认不一致。", new[] {validationContext.MemberName});
            }
            return ValidationResult.Success;
        }
    }
}