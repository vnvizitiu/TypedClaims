using System;
using System.Security.Claims;

namespace TypedClaims.Controllers
{
    class UserCurrentDateTimeClaim
    {
        private readonly DateTime _dateTimeValue;

        public UserCurrentDateTimeClaim(DateTime dateTimeValue)
        {
            _dateTimeValue = dateTimeValue;
        }

        public static implicit operator Claim(UserCurrentDateTimeClaim userCurrentDateTimeClaim)
        {
            return new Claim(nameof(UserCurrentDateTimeClaim), userCurrentDateTimeClaim._dateTimeValue.ToString());
        }

        public static implicit operator UserCurrentDateTimeClaim(Claim claim)
        {
            return new UserCurrentDateTimeClaim(DateTime.Parse(claim.Value));
        }
    }
}