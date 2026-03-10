### DEĞERLENDİRME TABLOSU

| Bölüm | Beklenen Gereksinim | Durum / Hata | Puan |
| :--- | :--- | :--- | :--- |
| **1. Model & DbContext** | Id, Name, Surname, Number, Class, Photo, [Required], [Display] | [Required] ve [Display] kullanılmış ancak Modeldeki Property isimleri ve etiketleri hatalı eşleşmiş (Name alanına "Soyad" validate mesajı verilmiş). | 7/10 |
| **2. DI & Bağlantı** | AppDbContext ve Program.cs ayarları | DbContext ve ConnectionString yapılandırması doğru. | 10/10 |
| **3. Listeleme (Index)** | Döngü, <img> gösterimi, Detay/Sil butonları | Görünüm "Tarif" üzerinden kopyalanmış, Student Index'te veriler yanlış Property'lere (Name: Yapılışı gibi) atanmış. | 8/15 |
| **4. Kayıt & Foto Yükleme** | Guid kullanımı, images klasörü, uzantı kontrolü | Klasör yolu `wwwroot\\img` olarak belirtilmiş ancak `wwwroot/images` istenmişti. Uzantı kontrolü mevcut. | 12/15 |
| **5. Mükerrer No Kontrolü** | Aynı numara ile kayıt engelleme | **EKSİK:** Kodda mükerrer kayıt kontrolü (Number check) bulunmuyor. | 0/15 |
| **6. Detay Sayfası** | Id ile veri bulma ve görüntüleme | **EKSİK:** Controller ve View tarafında bir detay (Details) sayfası oluşturulmamış. | 0/10 |
| **7. Silme & JS & Fiziksel** | confirm(), document.location, fiziksel dosya silme | **EKSİK:** Fiziksel dosya silme (`System.IO.File.Delete`) kodu yok. JS fonksiyon ismi ile çağrılan isim uyuşmuyor. | 3/15 |
| **8. Kod Düzeni & Kalite** | İsimlendirme, ViewBag/TempData kullanımı | Proje genelinde "Tarif" ve "Student" ifadeleri birbirine karışmış. Kodda çok fazla kopyala-yapıştır hatası var. | 5/10 |
| **TOPLAM** | | | **45/100** |

---

### Genel Geri Bildirim
Daha önce yaptığı bir "Tarifler" projesinin kodlarını "Student" projesine adapte etmeye çalışmış ancak bu süreçte çok fazla kopukluk ve isim karmaşası yaşanmış. 

**Önemli Eksikler ve Uyarılar:**
1. **Kopyala-Yapıştır Hataları:** Controller ve View içerisinde "tarif", "yapılışı", "içindekiler" gibi ifadelerin kalmış olması profesyonel bir yaklaşım değildir.
2. **Fiziksel Silme:** `File.Delete` fonksiyonunu kullanmadan sadece veritabanından veri silmek, sunucuda "çöp" dosya birikmesine yol açar; bu sınavın en kritik noktalarından biriydi.
3. **Mükerrer Kontrolü:** `_appDbContext.students.Any(s => s.Number == x.Number)` gibi bir kontrol, öğrenci numarasının benzersizliğini garanti altına almak için mutlaka eklenmeliydi.
4. **View Sorunları:** `Create.cshtml` dosyasında aynı alanlar (Surname) birden fazla kez tanımlanmış ve doğrulama mesajları (Validation) yanlış Property'lere atanmış.

**Öneri:** Öğrenci, mevcut kodunu temizleyip "tarif" ile ilgili tüm kalıntıları temizlemeli ve projesini sıfırdan, veri modelleriyle tutarlı bir şekilde yeniden yapılandırmalıdır.