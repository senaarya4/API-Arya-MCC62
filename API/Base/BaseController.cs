using API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;
        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public ActionResult Post(Entity entity)
        {
            var result = repository.Insert(entity);
            if (result != 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Berhasil Insert Data!" });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, result, message = "Gagal Insert Data!" });
            }
        }

        [HttpGet]//all
        public ActionResult<Entity> Get()
        {
            var result = repository.Get();
            return Ok(result);
        }

        [HttpGet("{key}")]//nik
        public ActionResult<Entity> Get(Key key)
        {
            var result = repository.Get(key);
            return Ok(result);
        }

        [HttpDelete]
        public ActionResult<Entity> Delete(Key key)
        {
            var result = repository.Delete(key);
            return Ok(result);
        }

        [HttpPut]
        public ActionResult<Entity> Put(Entity entity, Key key)
        {
            var result = repository.Update(entity, key);
            if (result != 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Berhasil Ubah Data!" });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, result, message = "Data Tidak Ditemukan!" });
            }
        }
    }
}
