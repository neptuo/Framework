﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    public interface IBindingConverter
    {
        bool TryConvert(string sourceValue, IFieldDefinition targetField, out object targetValue);
    }
}