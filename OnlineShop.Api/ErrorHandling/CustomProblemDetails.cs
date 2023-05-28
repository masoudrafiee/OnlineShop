using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Api.ErrorHandling
{
    public class CustomProblemDetails : ProblemDetails
    {
        public IEnumerable<InvalidParam> InvalidParams { get; set; }
        public string TraceId { get; set; }
    }

    public class InvalidParam
    {
        public string Name { get; private set; }
        public IEnumerable<string> Reasons { get; private set; }

        public InvalidParam(string name, string reasons)
        {
            if (!string.IsNullOrEmpty(name))
            {
                if (name.Contains("$."))
                    name = name.Substring(2);

                Name = Char.ToLowerInvariant(name[0]) + name.Substring(1);
            }

            Reasons = reasons?
                .Split(",")
                .Select(o => Char.ToLowerInvariant(o[0]) + o.Substring(1)) ??
                new string[0];

            if (Reasons.Any(o => o.Contains("required")))
            {
                Reasons = Reasons.Select(o => {
                    if (o.Contains("required"))
                        o = "isRequired";

                    return o;
                });
            }
        }
    }
}
