﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApi.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string massage) : base(massage) 
        {
        }
    }
}
