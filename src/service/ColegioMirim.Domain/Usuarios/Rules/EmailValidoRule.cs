using ColegioMirim.Core.DomainObjects;
using System.ComponentModel.DataAnnotations;

namespace ColegioMirim.Domain.Usuarios.Rules
{
    public class EmailValidoRule : IBusinessRule
    {
        private readonly string _email;

        public EmailValidoRule(string email)
        {
            _email = email;
        }

        public static string ErrorMessage => "O email é inválido";

        public bool IsValid()
        {
            var validator = new EmailAddressAttribute();
            return validator.IsValid(_email);
        }
    }
}
