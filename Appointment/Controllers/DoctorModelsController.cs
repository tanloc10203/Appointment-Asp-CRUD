using Appointment.Data;
using Appointment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Controllers
{
    public class DoctorModelsController : Controller
    {
        private readonly AppointmentContext _context;
        private readonly IWebHostEnvironment _webHost;

        public DoctorModelsController(AppointmentContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        // GET: DoctorModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.DoctorModel.ToListAsync());
        }

        // GET: DoctorModels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorModel = await _context.DoctorModel
                .FirstOrDefaultAsync(m => m.Doctor_Id == id);
            if (doctorModel == null)
            {
                return NotFound();
            }

            return View(doctorModel);
        }

        // GET: DoctorModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DoctorModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Doctor_Id,FullName,Dob,Email,NationalId,Address,Phone,Avatar")] DoctorModel doctorModel, IFormFile avatar)
        {
            if (ModelState.IsValid)
            {
                doctorModel.Doctor_Id = Guid.NewGuid();

                if (avatar != null)
                {
                    string uploadsFolder = Path.Combine(_webHost.WebRootPath, "uploads");

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    string filename = Path.GetFileName(avatar.FileName);
                    string fileSavePath = Path.Combine(uploadsFolder, filename);

                    using (FileStream stream = new FileStream(fileSavePath, FileMode.Create))
                    {
                        await avatar.CopyToAsync(stream);
                    }

                    doctorModel.Avatar = filename;

                }

                _context.Add(doctorModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(doctorModel);
        }

        // GET: DoctorModels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorModel = await _context.DoctorModel.FindAsync(id);
            if (doctorModel == null)
            {
                return NotFound();
            }
            return View(doctorModel);
        }

        // POST: DoctorModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Doctor_Id,FullName,Dob,Email,NationalId,Address,Phone,Avatar")] DoctorModel doctorModel)
        {
            if (id != doctorModel.Doctor_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctorModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorModelExists(doctorModel.Doctor_Id))
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
            return View(doctorModel);
        }

        // GET: DoctorModels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorModel = await _context.DoctorModel
                .FirstOrDefaultAsync(m => m.Doctor_Id == id);
            if (doctorModel == null)
            {
                return NotFound();
            }

            return View(doctorModel);
        }

        // POST: DoctorModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var doctorModel = await _context.DoctorModel.FindAsync(id);
            if (doctorModel != null)
            {
                _context.DoctorModel.Remove(doctorModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorModelExists(Guid id)
        {
            return _context.DoctorModel.Any(e => e.Doctor_Id == id);
        }
    }
}
