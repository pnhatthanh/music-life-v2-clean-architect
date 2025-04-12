using Microsoft.AspNetCore.Http;
using MusicLife.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.Modules.CurrentUser
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public CurrentUserService(HttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public Guid UserId
        {
            get
            {
                var userIdClaims = _contextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)
                            ?? throw new UnAuthorizedException();
                return Guid.Parse(userIdClaims.Value);
            }
        }
    }
}
