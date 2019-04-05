using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Challenge_App.Data;
using Challenge_App.Repo;
using Challenge_App.Repo.Repositories;
using Challenge_App.Repo.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Challenge_App.Repo.DTO.Challenge;
using Challenge_App.Repo.Helper;
using Challenge_App.Helper;

namespace Challenge_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChallengesController : ControllerBase
    {
        private readonly IChallengeRepository _challengeRepository;

        public ChallengesController(IChallengeRepository challengeRepository)
        {
            _challengeRepository = challengeRepository;
        }

        // GET: api/Challenges
        //[HttpGet]
        //public async Task<IActionResult> GetChallenges()
        //{
        //    var challenges = await _challengeRepository.GetAll();

        //    return Ok(challenges);
        //}

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetChallenges(int userId)
        {
            //if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            //    return Unauthorized();

            var challenges = await _challengeRepository.GetChallengesOfUser(userId);
            IEnumerable<ChallengeForListDTO> challengesToReturn = challenges.Select(x => new ChallengeForListDTO {

                Id = x.Id,
                Name = x.Name,
                CreatedAt = x.CreatedAt,
                challengeItemCount =  _challengeRepository.GetChallengeItemsCount(x.Id)

            });
            return Ok(challengesToReturn);
        }

        [HttpGet]
        public async Task<IActionResult> GetChallenges([FromQuery]ChallengeParameters challengeParameters)
        {

            int userId = -1;
            int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out userId);

            if (userId == -1)
                return Unauthorized();

            var challenges = await _challengeRepository.GetChallengesAsync(challengeParameters, userId);
            IEnumerable<ChallengeForListDTO> challengesToReturn = challenges.Select(x => new ChallengeForListDTO
            {

                Id = x.Id,
                Name = x.Name,
                CreatedAt = x.CreatedAt,
                challengeItemCount = _challengeRepository.GetChallengeItemsCount(x.Id)

            });

            Response.AddPagination(challenges.CurrentPage, challenges.PageSize, challenges.TotalCount, challenges.TotalPages);


            return Ok(challengesToReturn);
        }



        [HttpGet("items/{id}")]
        public async Task<IActionResult> GetChallengeItems(int id)
        {
            var challengeItems= await _challengeRepository.GetChallengeItems(id);

            return Ok(challengeItems);
        }

        [HttpPost]
        public IActionResult AddChallenge(ChallengeForAddDTO challengeForAdd)
        {

   
            //TO:DO add validation

            //if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            //    return Unauthorized();

            int userId = 10;

            

            Challenge newChallenge = new Challenge()
            {
                Name = challengeForAdd.Name,
                CreatedAt = challengeForAdd.CreatedAt,
            };
            _challengeRepository.Add(newChallenge);

            //Challenge User

            ChallengeUsers challengeUsers = new ChallengeUsers()
            {
                Role = "owner",
                ChallengeId = newChallenge.Id,
                UserId = userId
            };

            _challengeRepository.AddChallengeUser(challengeUsers);

            //Challenge Items

            IList<ChallengeItem> challengeItems = new List<ChallengeItem>();

            for(int i = 0;i<challengeForAdd.NumberOfItems;i++)
            {
                ChallengeItem challengeItem = new ChallengeItem()
                {
                    OnDate = newChallenge.CreatedAt,
                    ChallengeId = newChallenge.Id
                };

                challengeItems.Add(challengeItem);
            }

            _challengeRepository.AddChallengeItems(challengeItems);


            //Challenge Item Users

            challengeForAdd.UsersInvited.Add(userId);

            IList<ChallengeItemUsers> challengeItemUsers = new List<ChallengeItemUsers>();

            foreach(var item in challengeItems)
            {
                for (int i = 0; i < challengeForAdd.UsersInvited.Count(); i++)
                {
                    ChallengeItemUsers challengeItemUser = new ChallengeItemUsers()
                    {
                        UserId = challengeForAdd.UsersInvited[i],
                        ChallengeItemId = item.Id,
                        Completed = false
                    };

                    challengeItemUsers.Add(challengeItemUser);
                }
            }

            _challengeRepository.AddChallengeItemUsers(challengeItemUsers);

             _challengeRepository.SaveChanges();

            return Ok();


        }

    }
}
