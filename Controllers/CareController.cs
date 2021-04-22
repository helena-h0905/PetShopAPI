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
    public class CareController : ControllerBase
    {
        ICareRepository careRepository;
        public CareController(ICareRepository _careRepository)
        {
            careRepository = _careRepository;
        }

        [HttpGet]
        [Route("GetCareSupplies")]
        public async Task<IActionResult> GetCareSupplies()
        {
            try
            {
                var supplies = await careRepository.GetCareSupplies();
                if (supplies == null)
                {
                    return NotFound();
                }

                return Ok(supplies);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetCareSupply")]
        public async Task<IActionResult> GetCareSupplies(int? supplyId)
        {
            if (supplyId == null)
            {
                return BadRequest();
            }

            try
            {
                var supplies = await careRepository.GetCareSupply(supplyId);

                if (supplies == null)
                {
                    return NotFound();
                }

                return Ok(supplies);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddFood")]
        public async Task<IActionResult> AddFood([FromBody] CareSupply model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var supplyId = await careRepository.AddCareSupply(model);
                    if (supplyId > 0)
                    {
                        return Ok(supplyId);
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
        [Route("DeleteCareSupply")]
        public async Task<IActionResult> DeleteCareSupply(int? supplyId)
        {
            int result = 0;

            if (supplyId == null)
            {
                return BadRequest();
            }

            try
            {
                result = await careRepository.DeleteCareSupply(supplyId);
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
        [Route("UpdateCareSupply")]
        public async Task<IActionResult> UpdateCareSupply([FromBody] CareSupply model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await careRepository.UpdateCareSupply(model);

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
        [Route("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await careRepository.GetCategories();
                if (categories == null)
                {
                    return NotFound();
                }

                return Ok(categories);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetCategory")]
        public async Task<IActionResult> GetCategory(int? categoryId)
        {
            if (categoryId == null)
            {
                return BadRequest();
            }

            try
            {
                var categories = await careRepository.GetCategory(categoryId);

                if (categories == null)
                {
                    return NotFound();
                }

                return Ok(categories);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddCategory")]
        public async Task<IActionResult> AddCategory([FromBody] CareCategory model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var categoryId = await careRepository.AddCategory(model);
                    if (categoryId > 0)
                    {
                        return Ok(categoryId);
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
        [Route("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int? categoryId)
        {
            int result = 0;

            if (categoryId == null)
            {
                return BadRequest();
            }

            try
            {
                result = await careRepository.DeleteCategory(categoryId);
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
        [Route("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory([FromBody] CareCategory model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await careRepository.UpdateCategory(model);

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