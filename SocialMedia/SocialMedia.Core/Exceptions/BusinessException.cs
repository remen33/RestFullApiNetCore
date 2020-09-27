namespace SocialMedia.Core.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BusinessException : Exception
    {
        public BusinessException()
        {

        }

        public BusinessException(string message): base(message)
        {

        }
    }
}