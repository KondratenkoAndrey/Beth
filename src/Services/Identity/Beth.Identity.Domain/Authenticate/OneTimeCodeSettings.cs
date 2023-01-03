using System;

namespace Beth.Identity.Domain.Authenticate;

public class OneTimeCodeSettings
{
    public TimeSpan Duration { get; set; }
}