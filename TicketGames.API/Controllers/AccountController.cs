using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TicketGames.API.Models.Account;
using TicketGames.CrossCutting.Cache;
using TicketGames.CrossCutting.Cache.Redis;
using TicketGames.Domain.Contract;
using TicketGames.Domain.Services;
using TicketGames.Infrastructure.Repositories;

namespace TicketGames.API.Controllers
{
    [RoutePrefix("v1/account")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class AccountController : ApiController
    {
        private readonly ITransactionService _transactionService;
        public AccountController(ITransactionService transactionService)
        {
            this._transactionService = transactionService;
        }
        public AccountController()
            : this(new TransactionService(new TransactionRepository()))
        {
            CacheManager.SetProvider(new CacheProvider());
        }

        [Authorize]
        [HttpGet, Route()]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                long participantId;

                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

                long.TryParse(principal.Claims.Where(c => c.Type == "participant_Id").Single().Value, out participantId);

                if (participantId > 0)
                {
                    Account account = null;

                    var result = this._transactionService.GetTransactions(participantId);

                    if(result.Count > 0)
                    {
                        account = new Account().Balances(result);

                        return Ok(account);
                    }
                    else
                    {
                        return Ok(new Account());
                    }                    
                }
                else
                {
                    return Ok(new Account());
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            };
        }
    }
}
