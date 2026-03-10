### DEĞERLENDİRME TABLOSU

| Bölüm | Beklenen Gereksinim | Durum / Hata | Puan |
| :--- | :--- | :--- | :--- |
| **1. Model & DbContext** | 'Student' modeli, [Required] ve [Display] kullanımı | 'Tarif' modeli kullanılmış, gerekli Data Annotation'lar (Required/Display) tamamen eksik. | 2/10 |
| **2. DI & Bağlantı** | AppDbContext ve Program.cs ayarları | DbContext tanımlı ancak Controller'da kullanılmıyor, statik liste (`static List<Tarif>`) üzerinden işlem yapılmış. | 4/10 |
| **3. Listeleme (Index)** | Döngü, <img> gösterimi, Detay/Sil butonları | Index sayfasında listeleme yapılmış, <img> etiketi mevcut. | 12/15 |
| **4. Kayıt & Foto Yükleme** | Guid, klasör, uzantı kontrolü | Guid ve uzantı kontrolü yapılmış ancak veritabanı bağlantısı kurulmadığı için veriler kalıcı değil. | 10/15 |
| **5. Mükerrer No Kontrolü** | Aynı numara ile kayıt engelleme | Projede öğrenci numarası alanı yok, mükerrer kontrolü hiç yapılmamış. | 0/15 |
| **6. Detay Sayfası** | Id ile veri bulma ve görüntüleme | Detay sayfası tasarlanmamış (sadece Edit var). | 0/10 |
| **7. Silme & JS & Fiziksel** | confirm(), fiziksel dosya silme | `Delete` action'ında fiziksel dosya silme kodu yok. JS `confirm()` onayı eksik. | 3/15 |
| **8. Kod Düzeni & Kalite** | İsimlendirme, ViewBag/TempData | Controller içinde gereksiz dosya yapısı, Model adlandırma karmaşası mevcut. | 4/10 |
| **TOPLAM** | | | **35/100** |

---

### Genel Geri Bildirim

1. **Model ve Veritabanı:** Model tasarımı istenen kriterlere (Student, Number, Name, Surname vb.) uymuyor. Entity Framework Core ile DB işlemleri yapılması gerekirken, `static List` kullanılarak proje geçici bir belleğe hapsedilmiş.
2. **Kriter Eksikliği:** Mükerrer öğrenci numarası kontrolü, fiziksel dosya silme (`System.IO.File.Delete`), JS `confirm()` onayı gibi kritik sınav kriterlerinin hiçbiri uygulanmamış.
3. **Standartlar:** `[Required]` ve `[Display]` gibi Data Annotation kullanımı zorunluyken bunların hiçbiri eklenmemiş. Controller içerisinde gereksiz dosya kalabalığı ve projenin "Yemek" üzerine kurulması (sınav konusu "Öğrenci" olmasına rağmen) projenin kopyalandığı veya yanlış odaklandığı izlenimini veriyor.

**Öneri:** Veri tabanı entegrasyonunu `static list` yerine `DbContext` üzerinden yapmayı öğrenmeli, `System.IO.File.Delete` metodunu araştırıp dosya silme işlemlerini `Delete` metoduna entegre etmelidir. Ayrıca C# isimlendirme standartlarına (PascalCase) daha fazla dikkat edilmelidir.