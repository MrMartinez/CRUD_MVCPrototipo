using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCPrototipo.DTOs;
using MVCPrototipo.Models;

namespace MVCPrototipo.Controllers
{
    public class InstructorController : Controller
    {
        private static List<Instructor> _context = new List<Instructor>();
        private static List<Grado> _lsGrados = new List<Grado>();
        public InstructorController()
        {
            _lsGrados = new List<Grado>() { 
                new Grado { 
                    GradoId = 1,
                    Descripcion = "Primero A"
                },
                  new Grado {
                    GradoId = 2,
                    Descripcion = "Primero B"
                },
                    new Grado {
                    GradoId = 3,
                    Descripcion = "Segundo A"
                },
                      new Grado {
                    GradoId = 4,
                    Descripcion = "Segundo B"
                },
            };
        }
        // GET: Instructor
        public IActionResult Index()
        {
            return View( _context);
        }

        // GET: Instructor/Details/5
        public IActionResult Details(int id)
        {
            if (_context[id] != null)
            {
                return View(_context[id]);
            }

            return NotFound(); 
        }

        // GET: Instructor/Create
        public IActionResult Create()
        {
            ViewData["Grados"] = ViewBag.Pacientes = new SelectList(_lsGrados, "GradoId", "Descripcion");

            return View();
        }

        //Ya no necesito bindear las propiedades, porque estoy recibiendo un DTO el cual voy a convertir cada una de sus 
        //Propiedades en propiedades de la entidad Instructor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InstructorDTO instructorDTO)
        {
            //Claro esta esta construccion de un InstructorDTO a Instructor puedo crear un metodo que revisa un instrcutorDTO iguale 
            //las propiedades y devuelta una entidad instructor o viceversa. Tambien podria usar AutoMapper para evitarme crear estos metodos 
            Instructor instructor = new Instructor { 
                InstructorId = instructorDTO.InstructorId,
                Nombre = instructorDTO.Nombre,
                Apellidos = instructorDTO.Apellidos,
                Grado = instructorDTO.GradoId,
                FotoPerfil =instructorDTO.FotoPerfil

            };
            if (ModelState.IsValid)
            {
                _context.Add(instructor);
                return RedirectToAction(nameof(Index));
            }
            return View(instructorDTO);
        }

        // GET: Instructor/Edit/5
        public IActionResult Edit(int id)
        {
            //Este IF siempre te va arrojar false porque un INT siempre tiene valor, si no envian nada asume 0
            //Yo pensaria en preguntar si es igual a 0; o hacer la propiedad nuleable y preguntar por su valor id.Value
            if (id == null)
            {
                return NotFound();
            }

            if (_context[id] == null)
            {
                return NotFound();
            }
            return View(_context[id]);
        }

        //Igual que en el create voy a recibir un DTO; ya no tengo que estar bindeando las propiedades como parametros recibidos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, InstructorDTO instructorDTO)
        {
            //Fijate que el nombre del metodo (Edit) te resalta que estas haciendo un metodo async pero nunca usas un await.
            // En este caso seria en el _context.Add o si en algun savechanges que no veo. Busquemos mas info de eso; porque en el ADD me dice
            // que no aplica porque la lista no es asincronica


            //Por eso comente sobre un metodo que haga esto o usar automapper porque fijate como vuelvo hacer el mismo proceso de DTO a entidad
            Instructor instructor = new Instructor
            {
                InstructorId = instructorDTO.InstructorId,
                Nombre = instructorDTO.Nombre,
                Apellidos = instructorDTO.Apellidos,
                Grado = instructorDTO.GradoId,
                FotoPerfil = instructorDTO.FotoPerfil

            };
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(instructor);
                    

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstructorExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(instructorDTO);
        }

        // GET: Instructor/Delete/5
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            //Este seria el instrcutor que hay en la lista para luego convertirlo en un DTO
            var instructor = _context.Find(x => x.InstructorId == id);
            if (instructor == null)
            {
                return NotFound();
            }

            //Aqui hay que hacer el metodo contrario al DTO que se hizo ahorita; aqui convertir de Entidad instructor a un instructorDTO
            //Porque el usuario no deberia recibir una entidad de la base de datos
            // Aunque parezca canson o tedioso debe ser asi; usar siempre Objet Data Transfer (DTO); recuerda que esta el paquete
            // de auto mapper para hacer esto por uno.

            InstructorDTO instructorDTO = new InstructorDTO() { 
                InstructorId = instructor.InstructorId,
                Nombre = instructor.Nombre,
                Apellidos = instructor.Apellidos,
                GradoId = instructor.Grado,
                FotoPerfil = instructor.FotoPerfil
            };
            return View(instructorDTO);
        }

        // POST: Instructor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var instructorDB = _context.Find(x => x.InstructorId == id);
            _context.Remove(instructorDB);

            return RedirectToAction(nameof(Index));

        }

        private bool InstructorExists(int id)
        {
            if (_context[id] != null)
                return true;
            else
                return false;
        }
    }
}
