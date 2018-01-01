using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using TicketGames.API.Models.Participant;
using TicketGames.CrossCutting.Cryptography;
using TicketGames.Domain.Contract;
using TicketGames.Domain.Repositories;
using TicketGames.Domain.Services;
using TicketGames.Infrastructure.Repositories;

namespace TicketGames.API.Security
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        //private readonly IParticipantService _participantService;
        //public ParticipantController(IParticipantService participantService)
        //{
        //    this._participantService = participantService;
        //}
        //public ParticipantController()
        //    : this(new ParticipantService(new ParticipantRepository()))
        //{
        //    CacheManager.SetProvider(new CacheProvider());
        //}


        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            IParticipantService _participantService = new ParticipantService(new ParticipantRepository());
            Participant participantModel = new Participant();
            Hash baseCrypt = new Hash();

            var user = _participantService.GetParticipant(context.UserName, context.UserName);
            string password = baseCrypt.GetHash(context.Password, user.Salt, CypherType.SHA512);

            var participant = _participantService.Authenticate(context.UserName, password, user.Salt);

            if (participant == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }            

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Sid, participant.Id.ToString()));
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));
            identity.AddClaim(new Claim("participant_Id", participant.Id.ToString()));

            context.Validated(identity);            
        }
    }
}