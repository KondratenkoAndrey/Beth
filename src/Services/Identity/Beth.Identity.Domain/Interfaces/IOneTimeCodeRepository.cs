﻿using System;
using System.Threading.Tasks;
using Beth.Identity.Domain.Authenticate;

namespace Beth.Identity.Domain.Interfaces;

public interface IOneTimeCodeRepository
{
    public Task<OneTimeCode> FindActiveCodeAsync(string mobilePhone);
    public Task AddCodeAsync(OneTimeCode code);
}