using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetShopAPI.Models;
using PetShopAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace PetShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ReactPolicy")]
    public class FoodController : ControllerBase
    {
        IFoodRepository foodRepository;
        public FoodController(IFoodRepository _foodRepository)
        {
            foodRepository = _foodRepository;
        }

        [HttpGet]
        [Route("GetAllFood")]
        public async Task<IActionResult> GetAllFood()
        {
            try
            {
                var food = await foodRepository.GetAllFood();
                if (food == null)
                {
                    return NotFound();
                }

                return Ok(food);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetFood")]
        public async Task<IActionResult> GetFood(int? foodId)
        {
            if (foodId == null)
            {
                return BadRequest();
            }

            try
            {
                var food = await foodRepository.GetFood(foodId);

                if (food == null)
                {
                    return NotFound();
                }

                return Ok(food);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetFoodForAnimal")]
        public async Task<IActionResult> GetFoodForAnimal(int? animalId)
        {
            if (animalId == null)
            {
                return BadRequest();
            }

            try
            {
                var food = await foodRepository.GetFoodForAnimal(animalId);

                if (food == null)
                {
                    return NotFound();
                }

                return Ok(food);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddFood")]
        public async Task<IActionResult> AddFood([FromBody] Food model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var foodId = await foodRepository.AddFood(model);
                    if (foodId > 0)
                    {
                        return Ok(foodId);
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
        [Route("DeleteFood")]
        public async Task<IActionResult> DeleteFood(int? foodId)
        {
            int result = 0;

            if (foodId == null)
            {
                return BadRequest();
            }

            try
            {
                result = await foodRepository.DeleteFood(foodId);
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
        [Route("UpdateFood")]
        public async Task<IActionResult> UpdateFood([FromBody] Food model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await foodRepository.UpdateFood(model);

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

        [HttpGet]
        [Route("GetFoodKinds")]
        public async Task<IActionResult> GetFoodKinds()
        {
            try
            {
                var foodKinds = await foodRepository.GetFoodKinds();
                if (foodKinds == null)
                {
                    return NotFound();
                }

                return Ok(foodKinds);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetFoodKind")]
        public async Task<IActionResult> GetFoodKind(int? foodKindId)
        {
            if (foodKindId == null)
            {
                return BadRequest();
            }

            try
            {
                var foodKind = await foodRepository.GetFoodKind(foodKindId);

                if (foodKind == null)
                {
                    return NotFound();
                }

                return Ok(foodKind);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddFoodKind")]
        public async Task<IActionResult> AddFoodKind([FromBody] FoodKind model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var foodKindId = await foodRepository.AddFoodKind(model);
                    if (foodKindId > 0)
                    {
                        return Ok(foodKindId);
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
        [Route("DeleteFoodKind")]
        public async Task<IActionResult> DeleteFoodKind(int? foodKindId)
        {
            int result = 0;

            if (foodKindId == null)
            {
                return BadRequest();
            }

            try
            {
                result = await foodRepository.DeleteFoodKind(foodKindId);
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
        [Route("UpdateFoodKind")]
        public async Task<IActionResult> UpdateFoodKind([FromBody] FoodKind model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await foodRepository.UpdateFoodKind(model);

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