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
        public static string FIELDS_EMPTY = "Los campos no pueden estar vacíos, intentelo de nuevo.";
        public static string FIELDS_NOT_VALID = "Los campos no son validos, intentelo de nuevo llenando los campos marcados";
        public static string DECIMAL_FORMAT_ERROR = "Error de Formato Decimal. Por favor, ingrese un punto decimal.";
        public static string CUSTOMER_IN_BLACK_LIST = "El cliente se encuentra en lista negra.\nOperacion cancelada.";
        public static string CANCEL_OPERATION = "¿Desea cancelar la operacion?";
        public static string ERROR_ADD_OPERATION = "Error al agregar la operacion";
        public static string INSUFFICIENT_AMOUNT = "El monto ingresado es menor al monto a cubrir";
        public static string ITEM_NOT_SELECTED = "Para continuar favor de seleccionar un elemento de la tabla";
        public static string CUSTOMER_NOT_INTRODUCED = "Para buscar un cliente, favor de introducir una informacion de curp (18 caracteres)";
        public static string INVALID_NUMBER = "El campo de telefono debe tener 10 digitos y todos tiene que ser numeros.";
        public static string UPLOAD_SUCCESS = "Se ha subido la informacion correctamente";
        public static string USER_NOT_FOUND = "El usuario o contraselña son erroneos.";
        public static string USER_FOUND = "Se han validado las credenciales.";
        public static string CUTOFF_SUCCESS = "Se ha realizado el corte correctamente, su turno ha finalizado, ya puede cerrar sesion.";
        public static string ARTICLE_NOT_FOUND = "No se ha encontrado ningun articulo con ese codigo de barras.";
        public static string AMOUNT_RECEIVED_ERROR = "El monto ingresado es demasiodo grande, favor de verificar el monto ingresado.";
        public static string UPDATE_ERROR = "Error al actualizar la informacion.";
        public static string ERROR_ADD_ASIDE = "Error al registrar un nuevo apartado";
        public static string ERROR_RENOVATION_CONTRACT = "El plazo de tiempo permitido para renovar el contrato es mayor a 24 horas";
    }
}
