using System;
using System.Collections.Generic;
using System.Text;

namespace SportsHub.Utilities
{
    class Enums
    {
    }
    public enum LogType
    {
        Email = 1,
        Sms = 2
    }
    public enum AttainmentLevel
    {
        zero = 0,
        one = 1,
        two = 2,
        three = 3
    }
    public enum AssessmentType
    {
        Post = 1,
        Pre = 2,
        Internal = 4,
        Final = 3
    }
    public enum RoleName
    {
        SuperAdmin = 4,
        Admin = 1,
        Trainer = 2,
        Member = 3,
        Driver = 2048,
        masterAdmin=5,
        HOD = 2072

    }
   public enum MailboxTypes { Inbox, Sent, Trash, Compose, Reply, Forward }

}
