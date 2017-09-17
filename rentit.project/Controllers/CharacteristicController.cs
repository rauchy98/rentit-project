using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RentIt.Project.Models;
using RentIt.Project.Repositories;

namespace RentIt.Project.Controllers
{
    [RoutePrefix("api/Characteristic")]
    public class CharacteristicController : ApiController
    {
        IUnitOfWork _db;

        public CharacteristicController()
        {
            _db = new UnitOfWork(new IDbContext());
        }

        public CharacteristicController(IUnitOfWork db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("Product/{productId}")]
        public IHttpActionResult Get(int productId)
        {
            Product product;
            try
            {
                product = _db.Products.Get(productId);
            }
            catch
            {
                return NotFound();
            }
            var characteristic = product.Characteristics;
            var listDTO = new List<CharacteristicDTO>();
            foreach(var item in characteristic)
            {
                listDTO.Add(Map(item));
            }
            return Ok(listDTO);
        }

        [HttpPost]
        [Route("Product/{productId}/Filter/{filterId}")]
        public IHttpActionResult Post(int productId, int filterId, CharacteristicDTO characteristicDTO)
        {
            Filter filter;
            try
            {
                filter = _db.Filters.Get(filterId);
            }
            catch
            {
                return NotFound();
            }
            Product product;
            try
            {
                product = _db.Products.Get(productId);
            }
            catch
            {
                return NotFound();
            }
            product.Characteristics.Add(new Characteristic
            {
                Value = characteristicDTO.Value,
                Filter = filter,
            });
            _db.Save();
            return Ok();
        }

        private CharacteristicDTO Map(Characteristic characteristic)
        {
            var characteristicDTO = new CharacteristicDTO
            {
                Id = characteristic.Id,
                Name = characteristic.Filter.Name,
                Value = characteristic.Value
            };
            return characteristicDTO;
        }
    }
}
