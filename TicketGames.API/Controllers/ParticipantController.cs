﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TicketGames.API.Models.Participant;
using TicketGames.CrossCutting.Cache;
using TicketGames.CrossCutting.Cache.Redis;
using TicketGames.Domain.Contract;
using TicketGames.Domain.Services;
using TicketGames.Infrastructure.Repositories;

namespace TicketGames.API.Controllers
{
    [RoutePrefix("v1/participant")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class ParticipantController : ApiController
    {
        private readonly IParticipantService _participantService;
        public ParticipantController(IParticipantService participantService)
        {
            this._participantService = participantService;
        }
        public ParticipantController()
            : this(new ParticipantService(new ParticipantRepository()))
        {
            CacheManager.SetProvider(new CacheProvider());
        }
        [HttpGet, Route("session/{session}")]
        public IHttpActionResult GetParticipantBySession(string session)
        {
            if (string.IsNullOrEmpty(session))
                return BadRequest("Sessão invalida!");

            Participant participant = null;

            var key = string.Concat("Participant:Sessions:", session);

            participant = CacheManager.GetObject<Participant>(key);

            if (participant == null)
            {

                var result = this._participantService.GetParticipant(session);

                participant = new Participant(result);

                if (participant != null)
                    CacheManager.StoreObject(key, participant, LifetimeProfile.Longest);
            }

            return Ok(participant);
        }

        [HttpPost, Route()]
        public IHttpActionResult Post(Participant participant)
        {
            if (participant.Id == 0)
            {
                var result = this._participantService.GetParticipant(participant.CPF, participant.CPF);

                if (result == null)
                {
                    var participantDomain = participant.MappingDomain();


                    if (this._participantService.CreateOrUpdate(participantDomain))
                    {
                        return Ok("Participante cadastrado com sucesso!");
                    }
                    else
                    {
                        return BadRequest("Não foi possível cadastrar o participante!");
                    }

                }
            }
            else
            {

                var result = this._participantService.GetParticipant(participant.CPF, participant.CPF);

                if (!string.IsNullOrEmpty(participant.Session))
                {
                    var condition = this._participantService.ValidateSession(participant.Session, result.Id);

                    if (condition)
                    {
                        participant.Id = result.Id;                       

                        var participantDomain = participant.MappingDomain();

                        participantDomain.Sessions.Add(this._participantService.GetSession(participant.Session));

                        if (this._participantService.CreateOrUpdate(participantDomain))
                            return Ok("Cadastro atualizado.");
                        else
                            return BadRequest("Não foi possível atualizar o cadastro.");

                    }
                }

                return BadRequest("Participante já cadastrado!");
            }

            return Ok();
        }
    }
}
