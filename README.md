
Bu README dosyası, 

.NET Core Web API projelerini Docker kullanarak kurmayı ve Docker-Compose ile çalıştırmayı içerir.
Ayrıca, Docker image'lerinin Visual Studio 2022 üzerinden build edilmesi sağlanmıştır.

## Gereksinimler

- Windows 10/11 işletim sistemi
- Visual Studio 2022
- Docker Desktop


## Eksiklikler
    
- Projede herhangi bir loglama kutuphanesi kullanilmadi ve log kaydi tutulmadi.
- Rapor servisi kullanici verilerini almak icin contact web api'sine baglanirken url bilgisi static olarak setlendi. 'Service Registry' mantigi kurgulanabilirdi.
- Rapor servisi urettigi raporlari dosyaya yazmak yerine json formatinda rapor apisine gonderdi ve rapor apisi bu veriyi dogrudan db'ye yazdi.
- Rapor servisi RabbitMQ'dan gelen mesajlari bir db ye yazmadan direk olarak isledi. Bu durum servislerden herhangi birinin crash olmasi durumunda veri tutarsizligi olmasina sebep olabilir.


NOT: Yukarida belirttigim eksiklikler zaman kaybi olmamasi icin goz ardi edilmis eksikliklerdir. 


## Adımlar

1. **Özel Docker Ağı Oluşturma:**
  
    Developer Command Prompt for vs 2022 uzerinde asagidaki komutu calistirin
 
   ```bash
   docker network create Test_Network
   ```

2. **RabbitMQ ve PostgreSQL image lerinin cekilmesi:**

   Developer Command Prompt for vs 2022 uzerinde asagidaki komutlari calistirin

   ```bash
   docker pull postgres
   ```

   ```bash
   docker pull rabbitmq:management
   ```

3. **Docker Image Oluşturma:**

    ContactService.API, ReportService.API ve ReportGeneratorService projeleri için Docker image'leri oluşturmak için aşağıdaki adımları izleyin:

    - Visual Studio 2022 üzerinde Solution Configuration olarak Release secin
    - Her proje klasöründe, Dockerfile'ı sağ tıklayın ve "Build Docker Image" seçeneğini tiklayin. Bu, Docker Desktop üzerinde bir Docker imajı oluşturacaktır.

4. **Docker-Compose Dosyasının Çalıştırılması:**

    Docker-Compose dosyası ile hazirladigimiz servisleri çalıştırmak için aşağıdaki adımları izleyin:

    - Developer Command Prompt for vs 2022 ile Solution altinda _Docker_Compose klasorune gidin.
    
    ```bash
    cd "........\_Docker_Compose"
    ```    
    
    - Docker-Compose dosyasını çalıştırmak için asagidaki komutu calistirin.
    
     ```bash
     docker-compose up -d
     ```

