using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Common
{
    public class GlobalConstants
    {
        public const string AdministratorRoleName = "Admin";
        public const string UserRoleName = "User";
        public const string AdministratorUserName = "admin";
        public const string AdministratorEmail = "admin@admin.com";
        public const string AdministratorPassword = "asdasd";

        public const string CachingUserNames = "userNames";
        public const int CachingDefaultDuration = 60 * 1;

        public const string ErrorNotCreatedPostKey = "notCreatedError";
        public const string ErrorNotCreatedPostValue = "Post could not be created";

        public const string ErrorNotEditedPostKey = "notEdited";
        public const string ErrorNotEditedPostValue = "Post could not be edited";


    }
}
