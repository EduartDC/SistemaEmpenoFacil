using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class CodeError
    {
        public static int SUCCESS = 1;
        public static int ERROR = 0;
        public static int ERROR_USER_NOT_FOUND = 100;
        public static int ERROR_USER_ALREADY_EXISTS = 101;
        public static int CONNECTION_ERROR = 200;
        public static int MODEL_ALREADY_EXISTS = 301;
    }
}
