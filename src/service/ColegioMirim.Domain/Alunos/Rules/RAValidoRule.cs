using ColegioMirim.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ColegioMirim.Domain.Alunos.Rules
{
    public class RAValidoRule : IBusinessRule
    {
        private readonly string _ra;

        public RAValidoRule(string ra)
        {
            _ra = ra;
        }

        public static string ErrorMessage => "O RA deve ser composto por 7 números";

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(_ra))
                return false;

            var match = Regex.Match(_ra, @"\d{7}").Value;
            return _ra == match;
        }
    }
}
