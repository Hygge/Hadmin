using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.Attributes
{
    public class RequiredPermission : AuthorizeAttribute, IAuthorizationRequirement, IAuthorizationRequirementData
    {
        public RequiredPermission(string key) => Key = key;
        public string Key { get; }

        public IEnumerable<IAuthorizationRequirement> GetRequirements()
        {
            yield return this;
        }

    }
}
