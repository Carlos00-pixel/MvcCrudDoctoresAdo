using Microsoft.AspNetCore.Mvc;
using MvcCrudDoctoresAdo.Models;
using MvcCrudDoctoresAdo.Repositories;

namespace MvcCrudDoctoresAdo.Controllers
{
    public class DoctoresController : Controller
    {
        private RepositoryDoctores repo;

        public DoctoresController()
        {
            this.repo = new RepositoryDoctores();
        }
        public IActionResult Index()
        {
            List<Doctor> doctores = this.repo.GetDoctores();
            return View(doctores);
        }

        public IActionResult NuevoDoctor()
        {
            List<Doctor> doctores = this.repo.GetDoctores();
            List<Hospital> hospitales =
                this.repo.GetHospitales();
            ViewData["HOSPITALES"] = hospitales;
            return View();
        }

        [HttpPost]
        public IActionResult NuevoDoctor(Doctor doctor)
        {
            this.repo.CrearDoctor(doctor.IdHospital, doctor.IdDoctor, doctor.Apellido, doctor.Especialidad, doctor.Salario);
            List<Hospital> hospitales =
                this.repo.GetHospitales();
            ViewData["HOSPITALES"] = hospitales;

            return RedirectToAction("Index");
        }

        public IActionResult Detalles(int iddoctor)
        {
            Doctor doctor = this.repo.FindDoctor(iddoctor);
            return View(doctor);
        }

        public IActionResult Modificar(int iddoctor)
        {
            Doctor doctor = this.repo.FindDoctor(iddoctor);
            return View(doctor);
        }

        [HttpPost]
        public IActionResult Modificar(Doctor doc)
        {
            this.repo.UpdateDoctor(doc.IdHospital, doc.IdDoctor, doc.Apellido, doc.Especialidad, doc.Salario);
            return RedirectToAction("Index");
        }

        public IActionResult Borrar(int iddoctor)
        {
            this.repo.DeleteDoctor(iddoctor);
            return RedirectToAction("Index");
        }
    }
}
