﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioMirim.WebAPI.Core.Swagger
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SwaggerExcludeAttribute : Attribute
    {
    }
}
