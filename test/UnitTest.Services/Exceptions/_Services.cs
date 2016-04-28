﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Exceptions.Handlers;
using Neptuo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Exceptions
{
    public class ArgumentExceptionHandler : IExceptionHandler<ArgumentException>
    {
        public int CallCount { get; private set; }

        public void Handle(ArgumentException exception)
        {
            Assert.IsNotNull(exception);
            CallCount++;
        }
    }

    public class InnerExceptionIsNullHandler : IExceptionHandler<Exception>
    {
        public int CallCount { get; private set; }

        public void Handle(Exception exception)
        {
            Assert.IsNull(exception.InnerException);
            CallCount++;
        }
    }

    public class MessageLongerThanTenHandler : IExceptionHandler<Exception>
    {
        public int CallCount { get; private set; }

        public void Handle(Exception exception)
        {
            Assert.IsTrue(exception.Message.Length > 10);
            CallCount++;
        }
    }

}
