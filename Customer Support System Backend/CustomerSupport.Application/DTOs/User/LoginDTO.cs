﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSupport.Application.DTOs.User
{
    public record LoginDTO
    (
        string Email,
        string Password
    );
}
