using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FilmSitesi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SinavSorusu21.Models;

namespace SinavSorusu21.Controllers
{

    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly AppDbContext _context;
        public StudentController(ILogger<StudentController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var students=_context.Students.ToList();
            return View(students);
        }

          public IActionResult Create()
        {
            // Boş bir Create sayfasını döndürür
            return View();
        }
        // Formdan gelen verileri işleyen aksiyon (POST)
        [HttpPost]
        // Tarif verilerini ve yüklenen fotoğraf dosyasını parametre olarak alır
        public IActionResult Create(Student student,IFormFile Photo)
        {
            if(ModelState.IsValid){
            // Eğer bir dosya seçilmişse (null değilse)
            if (Photo != null)
            {
                // Yüklenen dosyanın uzantısını alır (.jpg, .png vb.)
                var uzanti=Path.GetExtension(Photo.FileName);
                // Dosyaya benzersiz bir isim (GUID) verip uzantısını ekler
                var yeniAd=Guid.NewGuid()+"."+uzanti;
                // Dosyanın kaydedileceği tam fiziksel yolu oluşturur (wwwroot/images altı)
                var yol=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/images",yeniAd);
                // Dosya tipinin sadece png veya jpg olup olmadığını kontrol eder
                if (Photo.ContentType == "image/png" || Photo.ContentType == "image/jpg"|| Photo.ContentType == "image/jpeg")
                {
                    // Belirlenen yola dosyayı yazmak için bir akış (stream) oluşturur
                    using(var stream=new FileStream(yol, FileMode.Create))
                    {
                        // Hata oluşma ihtimaline karşı try-catch bloğu
                        try
                        {
                            // Dosyayı sunucudaki fiziksel konuma kopyalar
                            Photo.CopyTo(stream);  
                            student.Photo=yeniAd;
                            _context.Students.Add(student);
                            _context.SaveChanges();
                            
                            // İşlem başarılıysa Index sayfasına yönlendirir
                            return RedirectToAction("Index","Student");
                        }
                        // Hata durumunda çalışacak blok
                        catch (Exception ex)
                        {
                            
                            // Hata mesajını arayüze göndermek için ViewBag kullanır
                            ViewBag.Error="Kayıt Hatası : "+ex.Message;
                        }
                        
                    }
                }
                // Dosya tipi uygun değilse hata mesajı verir
                else {ViewBag.Error="Sadece jpg yada png yükkle!";}

            }
            // Dosya seçilmemişse hata mesajı verir
            else {ViewBag.Error="Önce bir dosya yükle!";}        
    
            // Hata durumunda aynı sayfayı tekrar gösterir
            }
            return View(student);
        }
        public IActionResult Detail(int id)
        {
            if (id != null)
            {
               var student=_context.Students.FirstOrDefault(x=>x.Id.Equals(id));
                if (student != null)
                {
                    return View(student);
                }
            }


            return NotFound();
        }
        [Route("/Student/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var student=_context.Students.FirstOrDefault(x=>x.Id.Equals(id));
            if (student != null)
            {
               var yol=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/images",student.Photo);
               System.IO.File.Delete(yol);
                _context.Students.Remove(student);
                _context.SaveChanges();
                 TempData["Mesaj"]="Öğrenci Silindi";
            }else
            TempData["Mesaj"]="Öğrenci Bulunamadı";
            return RedirectToAction("Index","Student");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}