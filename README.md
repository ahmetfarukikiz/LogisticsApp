<div align="left">
  <a href="#-türkçe">🇹🇷 Türkçe</a> | <a href="#-english">🇬🇧 English</a>
</div>

---

#### 🇬🇧 English

# 📦 Smart Supply and Logistics Management System

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET 9.0](https://img.shields.io/badge/.NET-9.0-purple?style=for-the-badge)
![Application Type](https://img.shields.io/badge/App_Type-Console-lightgrey?style=for-the-badge)
![Design Patterns](https://img.shields.io/badge/Design_Patterns-7_GoF-orange?style=for-the-badge)
![Architecture](https://img.shields.io/badge/Architecture-N--Tier_/_MVC-brightgreen?style=for-the-badge)

A robust **C# .NET 9.0 Console Application** developed as a Logistics Management System for an undergraduate Object-Oriented Analysis and Design (OOAD) course. This project features an advanced multi-layered architecture, 7 classic GoF design patterns, and complete UML 2.0 diagrams.

The solution covers the end-to-end software development lifecycle (SDLC), including requirements analysis, system design, object-oriented implementation, and automated unit testing. It is strictly structured according to **SOLID principles** to eliminate rigid conditional statements (if-else/switch-case) and ensure a highly maintainable, polymorphic, and loosely coupled codebase.

## ✨ Key Features (Modules)

* **Dynamic Product & Inventory Management:** Supports both simple and complex (assembled) products. Triggers automated internal notifications to the Warehouse and email alerts to the Purchasing Department when stock falls below critical thresholds.
* **Order & Payment Workflows:** State-machine-driven order lifecycle (Pending -> Approved -> Preparing -> Shipped -> Delivered / Returned). Supports dynamic selection of payment methods (Credit Card, Wire Transfer) at runtime.
* **Logistics & Cargo Strategies:** Integrates multiple cargo APIs (e.g., Aras, Yurtiçi, GlobalExpress) under a single standard interface. Dynamically calculates shipping costs based on weight, distance, and selected extra services.
* **Authorization & System Logging:** Role-based access control (Admin, Warehouse Worker, Courier, Customer) restricting available operations. A globally accessible, single-instance logger records critical system events (stock changes, payment approvals).

## 🏗️ Architecture & Project Structure

The solution is structured using an **N-Tier (Clean Architecture style)** approach combined with the **MVC pattern** in the presentation layer.

> 💡 **Architectural Flexibility:** The core business logic is completely isolated from external infrastructures. Thanks to this decoupling, the data persistence layer or the current Console UI can easily be swapped with a real database system (SQL) or a rich web/desktop GUI in the future without changing a single line of Domain code.

```text
Logistics.Solution
│
├── Logistics.Domain/          # Core Business Logic (Pure C#)
│   ├── Dependencies
│   ├── Entities/              # Enums, Kullanicilar, Siparisler, Urunler, DTOs
│   ├── Interfaces/            # Core Contracts (Dependency Inversion)
│   └── Services/              # Domain Services (Bildirim, Kargo, Odeme, Log, Siparis, Stok)
│
├── Logistics.Infrastructure/  # External Concerns & Infrastructure Layer
│   ├── Dependencies
│   ├── Bildirimler/           # Notification Implementations
│   ├── KargoAdaptors/         # Adapters mapping external APIs to Domain Interfaces
│   ├── KargoAPIs/             # Simulated 3rd Party Cargo APIs
│   ├── Logging/               # File/DB Logger Implementations
│   └── Repositories/          # Data Access Implementations
│
├── Logistics.Presentation/    # Presentation Layer (Console User Interface)
│   ├── Dependencies
│   ├── Controllers/           # Request Handling & Flow Control
│   ├── Resolvers/             # Strategy/Factory Resolvers
│   ├── Views/                 # Console UI/Renderers & Menu Displays
│   └── Program.cs             # Application Entry Point
│
└── Logistics.Tests/           # Automated Unit Testing
    ├── Dependencies
    ├── KargoServiceTests.cs
    ├── KarmasikUrunTests.cs
    └── MusteriBakiyeTests.cs
```

## 🧩 Applied Design Patterns

To ensure maximum flexibility and adherence to the Open/Closed Principle, the following 7 GoF Design Patterns were implemented to solve specific business problems:

| Category | Pattern | Solved Problem / Implementation |
| :--- | :--- | :--- |
| **Creational** | **Singleton** | Ensures a single, globally accessible `Logger` instance across the entire system. |
| **Creational** | **Factory Method** | Dynamically generates user-specific menus based on roles without modifying existing code. |
| **Structural** | **Composite** | Treats simple and complex products uniformly via an `IUrun` interface for price calculation. |
| **Structural** | **Adapter** | Wraps incompatible third-party Cargo APIs into a unified, standard interface for the domain. |
| **Behavioral** | **Observer** | Establishes a publish-subscribe mechanism to notify departments when stock levels drop. |
| **Behavioral** | **State** | Manages order behavior dynamically depending on its current lifecycle stage. |
| **Behavioral** | **Strategy** | Allows the dynamic, runtime selection of different payment algorithms. |

## 🚀 Getting Started & How to Run

### Prerequisites
* **.NET 9.0 SDK** must be installed on your system. You can download it from the official [Microsoft .NET Website](https://dotnet.microsoft.com/download).

### Data Persistence
* 💾 **In-Memory Execution:** This application runs entirely in RAM using simulated data. There is no external database installation or connection string configuration required to lift the system up.

### Installation & Execution
1. **Clone the repository:**
   ```bash
   git clone https://github.com/ahmetfarukikiz/LogisticsApp.git
   ```

2. **Navigate to the Presentation layer directory:**
   ```bash
   cd LogisticsApp/src/Logistics.Presentation
   ```

3. **Build and run the application:**
   ```bash
   dotnet run
   ```

4. **Run the Automated Tests:**
   To verify that core business scenarios and design pattern contracts pass, execute the following command in the root solution folder:
   ```bash
   dotnet test
   ```

## 📊 UML Diagrams & Documentation

Comprehensive system analysis and conceptual models have been documented using UML 2.0 standards. All related files can be found in the repository:

* **Analysis Report:** Requirement specifications, system actors, and functional details.
* **Design Report:** In-depth technical explanations of the chosen design patterns and structural choices.
* **UML Diagrams:** Class, Use-Case, Activity, Sequence, and State Machine diagrams are provided in PDF format.

## 👤 Developer

**Ahmet Faruk İKİZ**
*Computer Engineering, Sakarya University*


<br><br>

---


#### 🇹🇷 Türkçe

# 📦 Akıllı Tedarik ve Lojistik Yönetim Sistemi

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET 9.0](https://img.shields.io/badge/.NET-9.0-purple?style=for-the-badge&logo=dotnet)
![Application Type](https://img.shields.io/badge/App_Type-Console-lightgrey?style=for-the-badge)
![Design Patterns](https://img.shields.io/badge/Design_Patterns-7_GoF-orange?style=for-the-badge)
![Architecture](https://img.shields.io/badge/Architecture-MVC_%7C_N--Tier-brightgreen?style=for-the-badge)

Lisans düzeyindeki Nesne Yönelimli Analiz ve Tasarım (OOAD) dersi için bir Lojistik Yönetim Sistemi olarak geliştirilmiş güçlü bir **C# .NET 9.0 Konsol Uygulamasıdır**. Bu proje, **MVC (Model-View-Controller)** desenini içeren gelişmiş bir çok katmanlı (N-Tier) mimariyi, 7 klasik GoF tasarım desenini ve eksiksiz UML 2.0 diyagramlarını barındırır.

Sistem; gereksinim analizi, sistem tasarımı, nesne yönelimli programlama ve birim testleri (unit test) dahil olmak üzere uçtan uca yazılım geliştirme yaşam döngüsünü (SDLC) kapsar. Katı koşullu ifadeleri (if-else/switch-case) ortadan kaldırmak ve son derece sürdürülebilir, polimorfik ve gevşek bağlı (loosely coupled) bir kod tabanı sağlamak için **SOLID prensiplerine** sıkı sıkıya bağlı kalınarak yapılandırılmıştır.

## ✨ Temel Özellikler (Modüller)

* **Dinamik Ürün ve Stok Yönetimi:** Basit ve karmaşık (montajlı) ürünleri destekler. Stok kritik eşiğin altına düştüğünde Depoya otomatik sistem içi bildirim, Satın Alma departmanına ise e-posta uyarısı tetikler.
* **Sipariş ve Ödeme Akışları:** Durum makinesi (State Machine) yönlendirmeli sipariş yaşam döngüsü (Beklemede -> Onaylandı -> Hazırlanıyor -> Kargoda -> Teslim Edildi / İade). Çalışma zamanında (runtime) Kredi Kartı veya Havale gibi ödeme yöntemlerinin dinamik seçimini destekler.
* **Lojistik ve Kargo Stratejileri:** Birden fazla kargo API'sini (ör. Aras, Yurtiçi, GlobalExpress) tek bir standart arayüz altında entegre eder. Ağırlık, mesafe ve seçilen ek hizmetlere göre kargo maliyetlerini dinamik olarak hesaplar.
* **Yetkilendirme ve Sistem Günlükleri:** Mevcut işlemleri kısıtlayan rol tabanlı erişim kontrolü (Yönetici, Depo Görevlisi, Kurye, Müşteri). Küresel olarak erişilebilen tekil bir log nesnesi, kritik sistem olaylarını (stok değişiklikleri, ödeme onayları) kaydeder.

## 🏗️ Mimari ve Proje Yapısı

Çözüm, Bağımlılıkların Tersine Çevrilmesi (Dependency Inversion) ve sorumlulukların ayrılması ilkelerini katı bir şekilde uygulamak için sunum katmanında **MVC deseniyle** birleştirilmiş **N-Tier (Temiz Mimari tarzı)** bir yaklaşım kullanılarak yapılandırılmıştır.

> 💡 **Mimari Esneklik:** Çekirdek iş mantığı (Models/Domain) dış altyapılardan tamamen izole edilmiştir. Sunum katmanı kullanıcı isteklerini `Controllers` aracılığıyla işler ve çıktıları `Views` aracılığıyla sunar. Bu ayrıştırma sayesinde, mevcut Konsol arayüzü gelecekte Domain kodunun tek bir satırı bile değiştirilmeden gerçek bir veritabanı sistemine (SQL) veya zengin bir web arayüzüne kolayca dönüştürülebilir.

```text
Logistics.Solution
│
├── Logistics.Domain/          # Çekirdek İş Mantığı (Modeller ve Servisler)
│   ├── Dependencies
│   ├── Entities/              # Enumlar, Kullanıcılar, Siparişler, Ürünler, DTO'lar
│   ├── Interfaces/            # Çekirdek Sözleşmeler (Dependency Inversion)
│   └── Services/              # Domain Servisleri (Bildirim, Kargo, Odeme, Log vb.)
│
├── Logistics.Infrastructure/  # Dış Bağımlılıklar ve Altyapı Katmanı
│   ├── Dependencies
│   ├── Bildirimler/           # Bildirim Gerçeklemeleri
│   ├── KargoAdaptors/         # Dış API'leri Domain arayüzlerine bağlayan Adaptörler
│   ├── KargoAPIs/             # Simüle Edilmiş 3. Parti Kargo API'leri
│   ├── Logging/               # Dosya/Veritabanı Log Gerçeklemeleri
│   └── Repositories/          # Veri Erişim Gerçeklemeleri
│
├── Logistics.Presentation/    # Sunum Katmanı (MVC: Controllers & Views)
│   ├── Dependencies
│   ├── Controllers/           # İstek Yönetimi ve Akış Kontrolü
│   ├── Resolvers/             # Strategy/Factory Çözümleyicileri
│   ├── Views/                 # Konsol Arayüzü ve Menü Ekranları
│   └── Program.cs             # Uygulama Başlangıç Noktası
│
└── Logistics.Tests/           # Otomatik Birim Testleri
    ├── Dependencies
    ├── KargoServiceTests.cs
    ├── KarmasikUrunTests.cs
    └── MusteriBakiyeTests.cs
```

## 🧩 Uygulanan Tasarım Desenleri

Maksimum esneklik ve Açık/Kapalı Prensibine (Open/Closed) uyum sağlamak için, belirli iş problemlerini çözmek amacıyla aşağıdaki 7 GoF Tasarım Deseni uygulanmıştır:

| Kategori | Desen | Çözülen Problem / Uygulama |
| :--- | :--- | :--- |
| **Yaratımsal** | **Singleton** | Tüm sistem genelinde tek ve küresel olarak erişilebilir bir `Logger` nesnesinin olmasını sağlar. |
| **Yaratımsal** | **Factory Method** | Mevcut kodu değiştirmeden rollere dayalı olarak kullanıcıya özel menüleri dinamik olarak üretir. |
| **Yapısal** | **Composite** | Fiyat hesaplaması için basit ve karmaşık ürünleri bir `IUrun` arayüzü üzerinden tek tip olarak ele alır. |
| **Yapısal** | **Adapter** | Uyumsuz 3. parti Kargo API'lerini domain için birleştirilmiş, standart bir arayüze sarar. |
| **Davranışsal** | **Observer** | Stok seviyeleri düştüğünde ilgili departmanları bilgilendirmek için bir yayıncı-abone (publish-subscribe) mekanizması kurar. |
| **Davranışsal** | **State** | Sipariş davranışlarını mevcut yaşam döngüsü aşamasına bağlı olarak dinamik bir şekilde yönetir. |
| **Davranışsal** | **Strategy** | Farklı ödeme algoritmalarının çalışma zamanında (runtime) dinamik olarak seçilmesine olanak tanır. |

## 🚀 Başlangıç ve Çalıştırma

### Önkoşullar
* Sisteminizde **.NET 9.0 SDK** yüklü olmalıdır. Resmi [Microsoft .NET Web Sitesinden](https://dotnet.microsoft.com/download) indirebilirsiniz.

### Veri Kalıcılığı
* 💾 **In-Memory (RAM) Çalışma:** Bu uygulama, simüle edilmiş veriler kullanarak tamamen RAM üzerinde çalışır. Sistemi ayağa kaldırmak için herhangi bir dış veritabanı kurulumu veya bağlantı dizesi (connection string) yapılandırması gerekmez.

### Kurulum ve Yürütme
1. **Depoyu klonlayın:**
   ```bash
   git clone https://github.com/ahmetfarukikiz/LogisticsApp.git
   ```

2. **Sunum (Presentation) katmanı dizinine gidin:**
   ```bash
   cd LogisticsApp/src/Logistics.Presentation
   ```

3. **Uygulamayı derleyin ve çalıştırın:**
   ```bash
   dotnet run
   ```

4. **Otomatik Testleri Çalıştırın:**
   Temel iş senaryolarının ve tasarım deseni sözleşmelerinin geçtiğini doğrulamak için kök çözüm (solution) klasöründe aşağıdaki komutu çalıştırın:
   ```bash
   dotnet test
   ```

## 📊 UML Diyagramları ve Dokümantasyon

Kapsamlı sistem analizi ve kavramsal modeller UML 2.0 standartları kullanılarak belgelenmiştir. İlgili tüm dosyalar depoda bulunabilir:

* **Analiz Raporu:** Gereksinim özellikleri, sistem aktörleri ve işlevsel detaylar.
* **Tasarım Raporu:** Seçilen tasarım desenlerinin ve yapısal tercihlerin derinlemesine teknik açıklamaları.
* **UML Diyagramları:** Sınıf (Class), Kullanım Durumu (Use-Case), Aktivite (Activity), Sıralama (Sequence) ve Durum Makinesi (State Machine) diyagramları PDF formatında sunulmuştur.

## 👤 Geliştirici

**Ahmet Faruk İKİZ**
*Bilgisayar Mühendisliği, Sakarya Üniversitesi*
