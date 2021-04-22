using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetShopAPI.Models;
using PetShopAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace PetShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToyController : ControllerBase
    {
        IToyRepository toyRepository;
        public ToyController(IToyRepository _toyRepository)
        {
            toyRepository = _toyRepository;
        }

        [HttpGet]
        [Route("GetToys")]
        public async Task<IActionResult> GetToys()
        {
            try
            {
                var toys = await toyRepository.GetToys();
                if (toys == null)
                {
                    return NotFound();
                }

                return Ok(toys);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetToy")]
        public async Task<IActionResult> GetToy(int? toyId)
        {
            if (toyId == null)
            {
                return BadRequest();
            }

            try
            {
                var toy = await toyRepository.GetToy(toyId);

                if (toy == null)
                {
                    return NotFound();
                }

                return Ok(toy);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddToy")]
        public async Task<IActionResult> AddToy([FromBody] Toy model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var toyId = await toyRepository.AddToy(model);
                    if (toyId > 0)
                    {
                        return Ok(toyId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("DeleteToy")]
        public async Task<IActionResult> DeleteToy(int? toyId)
        {
            int result = 0;

            if (toyId == null)
            {
                return BadRequest();
            }

            try
            {
                result = await toyRepository.DeleteToy(toyId);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }


        [HttpPut]
        [Route("UpdateToy")]
        public async Task<IActionResult> UpdateToy([FromBody] Toy model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await toyRepository.UpdateToy(model);

                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName ==
                             "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }

    }
}