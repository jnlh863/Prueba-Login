namespace MealMasterAPI.Excepcions
{
    public class EmailNotSendException : Exception
    {
        public EmailNotSendException() : base("No se puedo enviar el correo, intentelo de nuevo")
        {
        }
    }
}
