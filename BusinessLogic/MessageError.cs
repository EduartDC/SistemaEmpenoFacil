using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class MessageError
    {
        public static string ERROR_USER_NOT_FOUND = "El usuario no existe";
        public static string USER_AND_PASSWORD_NOT_FOUND = "El usuario y la contraseña no coinciden";
        public static string ERROR_USER_ALREADY_EXISTS = "El usuario ya existe, no puede registrar dos veces la misma informacion";
        public static string CONNECTION_ERROR = "Error de conexión, intentelo de nuevo mas tarde o comuniquese con el administrador";
        public static string FIELDS_EMPTY = "Los campos no pueden estar vacíos, intentelo de nuevo llenando los campos marcados";
        public static string FIELDS_NOT_VALID = "Los campos no son validos, intentelo de nuevo llenando los campos marcados";
        public static string DECIMAL_FORMAT_ERROR = "Error de Formato Decimal. Por favor, ingrese un punto decimal.";
        public static string CUSTOMER_IN_BLACK_LIST = "El cliente se encuentra en lista negra.\nOperacion cancelada.";
    }
}
