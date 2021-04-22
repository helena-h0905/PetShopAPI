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
    public class AnimalController : ControllerBase
    {
        IAnimalRepository animalRepository;
        public AnimalController(IAnimalRepository _animalRepository)
        {
            animalRepository = _animalRepository;
        }

        [HttpGet]
        [Route("GetAnimals")]
        public async Task<IActionResult> GetAnimals()
        {
            try
            {
                var animals = await animalRepository.GetAnimals();
                if (animals == null)
                {
                    return NotFound();
                }

                return Ok(animals);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetAnimal")]
        public async Task<IActionResult> GetAnimal(int? animalId)
        {
            if (animalId == null)
            {
                return BadRequest();
            }

            try
            {
                var animal = await animalRepository.GetAnimal(animalId);

                if (animal == null)
                {
                    return NotFound();
                }

                return Ok(animal);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddAnimal")]
        public async Task<IActionResult> AddAnimal([FromBody] Animal model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var animalId = await animalRepository.AddAnimal(model);
                    if (animalId > 0)
                    {
                        return Ok(animalId);
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
        [Route("DeleteAnimal")]
        public async Task<IActionResult> DeleteAnimal(int? animalId)
        {
            int result = 0;

            if (animalId == null)
            {
                return BadRequest();
            }

            try
            {
                result = await animalRepository.DeleteAnimal(animalId);
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
        [Route("UpdateAnimal")]
        public async Task<IActionResult> UpdateAnimal([FromBody] Animal model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await animalRepository.UpdateAnimal(model);

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