### DEĞERLENDİRME TABLOSU

| Bölüm | Beklenen Gereksinim | Durum / Hata | Puan |
| :--- | :--- | :--- | :--- |
| **1. Model & DbContext** | Id, Name, Surname, Number, Class, Photo, [Required], [Display] | Property isimleri (Ad, Soyad) kriterle uyuşmuyor, Photo alanı eksik. | 7/10 |
| **2. DI & Bağlantı** | AppDbContext ve Program.cs ayarları | SQLite yapılandırması ve EF Core başarılı. | 10/10 |
| **3. Listeleme (Index)** | Döngü, <img> gösterimi, Detay/Sil butonları | Index görüntüsü çalışıyor, ancak Detay metodu hatalı. | 12/15 |
| **4. Kayıt & Foto Yükleme** | Guid kullanımı, images klasörü, uzantı kontrolü | Klasör yolu (wwwroot\img) sabitlenmiş, mantık doğru. | 13/15 |
| **5. Mükerrer No Kontrolü** | Aynı numara ile kayıt engelleme | Kontrol mekanizması kodda mevcut değil. | 0/15 |
| **6. Detay Sayfası** | Id ile veri bulma ve görüntüleme | Sayfa bomboş, veri çekme mantığı kurulmamış. | 2/10 |
| **7. Silme & JS & Fiziksel** | confirm(), document.location, fiziksel dosya silme | Fiziksel dosya silme kodu eksik, DB silme hatalı (Statik liste kullanılmış). | 3/15 |
| **8. Kod Düzeni & Kalite** | İsimlendirme, ViewBag/TempData kullanımı | Controller içerisinde `static List<Student>` kullanımı çok hatalı. | 4/10 |
| **TOPLAM** | | | **51/100** |

---

### Genel Geri Bildirim:

**Önemli Tespitler:**
1. **Mimari Hata:** Controller içerisinde `public static List<Student> Students` tanımlanması, Entity Framework ile çalışırken yapılan en büyük hatadır. Veritabanı ile çalışıyorsanız, verileri `_appDbContext.Students` üzerinden sorgulamalısınız. Statik liste veriyi veritabanından çekmez, uygulama hafızasında tutar.
2. **Silme İşlemi:** `Delete` metodunda veritabanı silme işlemi yerine statik listeden silme yapılmış. Ayrıca kriterde istenen `System.IO.File.Delete` ile fiziksel dosya temizleme kodu tamamen unutulmuş.
3. **Mükerrer Kayıt:** `Create` metodunda öğrenci numarası için veritabanında `Any()` sorgusu ile bir kontrol mekanizması kurulmamış.
4. **Detay Sayfası:** Detay sayfası için View oluşturulmuş ancak Controller tarafında id parametresi ile veritabanından nesne çeken (`Find` veya `FirstOrDefault`) bir mantık yazılmamış.
5. **İsimlendirme:** Modeldeki property isimleri (`Ad`, `Soyad` vs.) kriterlerdeki (`Name`, `Surname`) isimlerle birebir eşleşmiyor.

