using ColegioMirim.Core.DomainObjects;
using ColegioMirim.Core.Extensions;
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
            if (string.IsNullOrEmpty(_email))
                return false;

            if (_email.Trim() != _email)
                return false;

            if (_email.TrimMiddle() != _email)
                return false;

            var validator = new EmailAddressAttribute();
            return validator.IsValid(_email);
        }
    }
}
