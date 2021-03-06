﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
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
        private readonly IConfigurationService _configurationService;

        public ParticipantController(IParticipantService participantService, IConfigurationService configurationService)
        {
            this._participantService = participantService;
            this._configurationService = configurationService;
        }
        public ParticipantController()
            : this(new ParticipantService(new ParticipantRepository()), new ConfigurationService(new ConfigurationRepository()))
        {
            CacheManager.SetProvider(new CacheProvider());
        }
        [Authorize]
        [HttpGet, Route("me")]
        public IHttpActionResult Get()
        {
            try
            {
                long participantId;

                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

                long.TryParse(principal.Claims.Where(c => c.Type == "participant_Id").Single().Value, out participantId);

                if (participantId > 0)
                {
                    Participant participant = null;

                    var key = string.Concat("Participant:Id:", participantId.ToString(), ":Register");

                    participant = CacheManager.GetObject<Participant>(key);

                    if (participant == null)
                    {
                        var result = this._participantService.GetParticipant(participantId);

                        participant = new Participant(result);

                        if (participant != null)
                            CacheManager.StoreObject(key, participant, LifetimeProfile.Longest);
                    }

                    return Ok(participant);
                }
                else
                {
                    return BadRequest("Você precisa está logado.");
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
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
                        var keySession = string.Concat("Participant:Sessions:", participant.Session);

                        participant.Id = result.Id;

                        var participantDomain = participant.MappingDomain();

                        CacheManager.StoreObject(keySession, participant, LifetimeProfile.Short);

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

        [Authorize]
        [HttpGet, Route("session")]
        public IHttpActionResult GetSessionPayment()
        {
            try
            {
                long participantId;

                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

                long.TryParse(principal.Claims.Where(c => c.Type == "participant_Id").Single().Value, out participantId);

                if (participantId > 0)
                {

                    var settingsKey = "Settings:Configuration";
                    List<Domain.Model.Configuration> settings = null;
                    settings = CacheManager.GetObject<List<Domain.Model.Configuration>>(settingsKey);

                    if (settings == null)
                    {
                        settings = this._configurationService.GetSettings();

                        if (settings != null && settings.Count > 0)
                            CacheManager.StoreObject(settingsKey, settings, LifetimeProfile.Longest);
                    }

                    string session = null;

                    var key = string.Concat("Participant:Id:", participantId.ToString(), ":Session");

                    session = CacheManager.GetObject<string>(key);

                    if (string.IsNullOrEmpty(session))
                    {
                        session = this._participantService.GetSession(settings);

                        if (!string.IsNullOrEmpty(session))
                            CacheManager.StoreObject(key, session, LifetimeProfile.Long);
                    }

                    return Ok(session);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
