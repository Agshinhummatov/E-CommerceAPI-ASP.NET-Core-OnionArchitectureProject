using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Exceptions
{
    public class PasswordChangeFailedExeption : Exception
    {
        public PasswordChangeFailedExeption() : base("There was a problem updating the password")
        {
        }

        public PasswordChangeFailedExeption(string? message) : base(message)
        {
        }

        public PasswordChangeFailedExeption(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
