using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Identity
{
    public class AppUser : IdentityUser //uses string as a property id as new guid string value
    {
        public string DisplayName { get; set; }
        public Address address { get; set; }
    }
}

//the reason we need to add this pakage (Microsoft.Extensions.Identity.Stores) in core.csproj
//is to let the AppUser.cs class to be able to derive from identity user class which is found in this pakage.