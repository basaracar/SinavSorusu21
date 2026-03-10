### DEĞERLENDİRME TABLOSU

| Bölüm | Beklenen Gereksinim | Durum / Hata | Puan |
| :--- | :--- | :--- | :--- |
| **1. Model & DbContext** | Id, Name, Surname, Number, Class, Photo, [Required], [Display] | Modelde `_Sınıf` isimlendirmesi hatalı. Required/Display etiketleri var. | 8/10 |
| **2. DI & Bağlantı** | AppDbContext ve Program.cs ayarları | DbContext konfigürasyonu doğru yapılmış. | 10/10 |
| **3. Listeleme (Index)** | Döngü, <img> gösterimi, Detay/Sil butonları | <img> yolu hatalı (wwwroot/img vs img/...). Detay linki 'Edit' olarak yazılmış. | 10/15 |
| **4. Kayıt & Foto Yükleme** | Guid kullanımı, images klasörü, uzantı kontrolü | Guid ve uzantı kontrolü var ancak `Directory.GetCurrentDirectory()` kullanımı bazen hata verir, `IWebHostEnvironment` kullanılmalıydı. | 12/15 |
| **5. Mükerrer No Kontrolü** | Aynı numara ile kayıt engelleme | **Eksik.** Controller içerisinde veritabanı sorgusu ile numara kontrolü yapılmamış. | 0/15 |
| **6. Detay Sayfası** | Id ile veri bulma ve görüntüleme | `Students` listesi üzerinden değil, DB'den `Find` ile veri çekilmeliydi. | 5/10 |
| **7. Silme & JS & Fiziksel** | confirm(), document.location, fiziksel dosya silme | **Kritik Eksik:** Fiziksel dosya (System.IO.File.Delete) silme kodu yok. `Students` static listesi üzerinden silme yapmaya çalışılmış. | 5/15 |
| **8. Kod Düzeni & Kalite** | İsimlendirme, ViewBag/TempData kullanımı | Static `Students` listesi kullanımı hatalı. İsimlendirmeler standart dışı (`_Sınıf`). | 5/10 |
| **TOPLAM** | | | **55/100** |

---

### Genel Geri Bildirim ve İyileştirme Önerileri:

1.  **Static List Kullanımı:** Controller içerisinde `public static List<Student> Students` tanımlamanız MVC'de büyük bir hatadır. Verileri her zaman `_appDbContext.Students` üzerinden sorgulamalısınız. Statik liste, uygulamanız her çalıştığında boşalacak ve veritabanı ile eşleşmeyecektir.
2.  **Fiziksel Silme:** Delete aksiyonunda sadece veritabanından silmek yetmez. `System.IO.File.Delete(yol)` komutu ile ilgili klasördeki fotoğrafı da fiziksel olarak silmelisiniz.
3.  **Mükerrer Kontrolü:** `Create` metodunun başında `_appDbContext.Students.Any(x => x.Numara == student.Numara)` kontrolü yaparak kullanıcıyı uyarmanız gerekiyordu.
4.  **İsimlendirme:** C# isimlendirme standartlarında (PascalCase) `_Sınıf` gibi alt tire ile başlayan değişken isimleri kullanılmaz. Sadece `Sinif` kullanınız.
5.  **Dosya Yolları:** `wwwroot` altındaki dosyaları `img/` olarak çağırmak yerine `@Url.Content("~/img/" + item.Foto)` yapısını kullanmanız daha güvenlidir.
6.  **Hata Yönetimi:** Detay sayfasında `Students.FirstOrDefault` kullanılmış; ancak `Students` artık bir veritabanı tablosu değil, static bir liste. Bu yüzden detay sayfası boş dönecektir. `_appDbContext.Students.Find(id)` kullanmalısınız.

*Çalışmalarınızda başarılar dilerim. Daha fazla pratik yapmanız gerektiğini düşünüyorum.*