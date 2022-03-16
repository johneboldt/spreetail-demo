using System;
using System.Collections.Generic;
using System.Linq;

namespace Spreetail.Demo.Repository
{
    public class MembersResponse<TMember> : Response
    {
        public MembersResponse(string error) : base(error)
        {
            Members = Enumerable.Empty<TMember>();
        }

        public MembersResponse(IEnumerable<TMember> members) : base()
        {
            if (members is null)
            {
                throw new ArgumentNullException(nameof(members));
            }

            Members = members;
        }

        public IEnumerable<TMember> Members { get; }
    }
}
