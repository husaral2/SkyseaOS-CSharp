using System;
using System.Collections.Generic;

namespace Skysea.Usage
{
    internal class UserManager
    {
        public static List<User> UserList = new List<User>();

        public static bool DeleteUser(int UserIndex)
        {
            if (UserIndex >= UserList.Count)
                return false;
            UserList.RemoveAt(UserIndex);
            for(int i = 0; i < UserList.Count - 1; ++i)
            {
                if (UserList[i] == null && UserList[i + 1] != null)
                {
                    UserList[i] = UserList[i + 1];
                    UserList.RemoveAt(i + 1);
                }
            }
            return true;
        }
    }
}
