using API.Context;
using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers.old
{
    [Route("api/[controller]")]
    [ApiController]
    public class OldEmployeesController : ControllerBase
    {
        //private readonly MyContext context;
        private OldEmployeeRepository employeeRepository;
        public OldEmployeesController(OldEmployeeRepository employeeRepository)

        {
            this.employeeRepository = employeeRepository;
            //this.context = context;
        }

        [HttpPost]
        public ActionResult Post(Employee employees)
        {
            var res = employeeRepository.Insert(employees);
            if (res != 0)
            {
                if(res == 2)
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, res, message = "Email sudah dipakai!" });
                }
                else if (res == 3)
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, res, message = "Nomor Telepon sudah dipakai!" });
                }
                else if (res == 4)
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, res, message = "Email dan Nomor Telepon sudah dipakai!" });
                }
                else
                {
                    return StatusCode(200, new { status = HttpStatusCode.OK, res, message = "Berhasil menambahkan Employee!" });
                }
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, res, message = "GAGAL!" });
            }
        }

        [HttpGet("{NIK}")]
        public ActionResult Get(Employee employees)
        {
            var get = employeeRepository.Get(employees.NIK);
            if (get != null && get.NIK.Equals(employees.NIK))
            {
                employeeRepository.Get(employees.NIK);
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Data TIDAK Ditemukan" });
            }
            return Ok(new { status = HttpStatusCode.OK, get, message = "Data Berhasil Ditampilkan" });
        }

        [HttpGet]//all
        public ActionResult Get()
        {
            if(employeeRepository.Get()==null)
            {
                return BadRequest("Tidak Ditemukan");//notfound
            }
            else
            {
                employeeRepository.Get();
            }
            return Ok(employeeRepository.Get());
        }

        [HttpPut]
        public ActionResult Put(Employee employees)
        {
            var ins = employeeRepository.Put(employees);
            if (ins > 0)
            {
                return Ok(new { status = HttpStatusCode.OK, message = "Data Berhasil Diperbarui" });
            }
            var get = employeeRepository.Get(employees.NIK);
            if (get == null)
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Data Tidak Ditemukan" });
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Data Tidak Tepat" });
            }
        }

        [HttpDelete]
        public ActionResult Delete(Employee employees)
        {
            var del = employeeRepository.Get(employees.NIK);
            if (del != null && del.NIK.Equals(employees.NIK))
            {
                employeeRepository.Delete(employees.NIK);
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Data TIDAK Ditemukan" });
            }
            return Ok(new { status = HttpStatusCode.OK, del, message = "Data Berhasil Dihapus" });
        }
    }
}
