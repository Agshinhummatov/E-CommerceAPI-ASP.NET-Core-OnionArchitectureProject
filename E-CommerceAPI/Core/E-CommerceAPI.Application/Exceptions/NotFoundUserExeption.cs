using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Exceptions
{
    public class NotFoundUserExeption : Exception
    {
        public NotFoundUserExeption() : base("Username and password are incorrect")
        {
        }

        public NotFoundUserExeption(string? message) : base(message)
        {
        }

        public NotFoundUserExeption(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
