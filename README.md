
  
# پروژه Samar Planner

> یک اپلیکیشن مدیریت اهداف و وظایف زندگی، با معماری **Modular Monolith** روی **.NET 10**

## 📖 دربارهٔ پروژه

پروزه **Samar Planner** یک اپلیکیشن برنامه‌ریزی شخصی است که به شما امکان می‌دهد:

- اهداف زندگی خود را به‌صورت **درختی** (Goal با والد/فرزند) تعریف کنید.
- برای هر هدف، **وظایف (Task)** قابل تکرار یا یک‌باره ایجاد کنید.
- هر وظیفه را در قالب **رخدادهای مجزا (TaskOccurrence)** پیگیری و امتیازدهی کنید.
- در آینده (پس از تکمیل) گزارش‌های دوره‌ای و یادداشت‌های شخصی نیز داشته باشید.

سند کامل طراحی دامنه (Entityها، رفتارها، قوانین کسب‌وکار) در [`DDD.md`](./DDD.md) موجود است.


## ✨ ویژگی‌های کلیدی

- **مدیریت اهداف سلسله‌مراتبی** – ایجاد، ویرایش و حذف اهداف با ساختار والد-فرزندی.
- **وظایف با الگوی تکرار** – پشتیبانی از تکرار روزانه، هفتگی (روزهای مشخص) و ماهانه (روزهای مشخص).
- **پیگیری رخدادهای وظیفه** – هر وظیفه به‌صورت خودکار یا دستی رخداد (Occurrence) تولید می‌کند که می‌توان وضعیت، تاریخ و زمان آن را تغییر داد.
- **Soft Delete & Restore** – قابلیت حذف منطقی و بازیابی برای اهداف، وظایف و رخدادها.
- **احراز هویت با شماره موبایل** – ثبت‌نام و ورود با شماره موبایل و رمز عبور، همراه با صدور توکن JWT.
- **معماری ماژولار** – هر دامنه کاملاً ایزوله با لایه‌های Core، Application، Infrastructure و DbContext مستقل.
- **پشتیبانی از تاریخ شمسی** – در فرانت‌اند (Blazor) با `PersianDateHelper`.
- **مستندسازی Swagger** – برای تست و مشاهدهٔ APIها.


## 🏗️ معماری

پروژه به‌صورت **Modular Monolith** طراحی شده است: هر دامنه (Module) به‌طور کامل از دامنه‌های دیگر مجزا است و خودش لایه‌های `Core . Application . Infrastructure . Host` و حتی **DbContext و Migration مستقل خودش** را دارد.



### الگوی هر ماژول

هر ماژول (`Goal`, `Identity`, `Task`) از لایه‌های زیر تشکیل شده است:
<table dir="rtl">
  <thead>
    <tr>
      <th>لایه</th>
      <th>مسئولیت</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td><code>*.Core</code></td>
      <td>Entity های خالص دامنه، Enum ها، بدون وابستگی به فریم‌ورک</td>
    </tr>
    <tr>
      <td><code>*.Application</code></td>
      <td>UseCase ها به سبک CQRS (Command/Query + Handler + Validator با MediatR و FluentValidation)</td>
    </tr>
    <tr>
      <td><code>*.Infrastructure</code></td>
      <td>پیاده‌سازی Repository، EF Core DbContext و Migrations مخصوص همان ماژول</td>
    </tr>
    <tr>
      <td><code>*</code> (Host)</td>
      <td>Controller ها و ثبت سرویس‌ها (<code>ServiceCollectionConfiguration</code>)</td>
    </tr>
  </tbody>
</table>
<div dir="rtl">
<div dir="rtl">

<h2>✅ وضعیت پیاده‌سازی</h2>

<h3>بخش‌های تکمیل‌شده</h3>

<ul>
  <li>✅ ساختار Solution و معماری ماژولار پایه (Core/Application/Infrastructure برای هر ماژول)</li>
  <li>✅ ماژول <strong>Identity</strong>: ثبت‌نام/ورود همزمان با شماره موبایل، صدور JWT</li>
  <li>✅ ماژول <strong>Goal</strong>: ایجاد، ویرایش، حذف، دریافت لیست اهداف کاربر</li>
  <li>✅ ماژول <strong>Task</strong> (بخش عمده):
    <ul>
      <li>✅ ایجاد، ویرایش، حذف و Soft Delete/Restore وظیفه</li>
      <li>✅ تعریف الگوی تکرار (<code>RepeatPattern</code>: Daily / WeeklyOnDays / MonthlyOnDays)</li>
      <li>✅ ایجاد، تغییر زمان/تاریخ/وضعیت، Skip، Soft Delete/Restore برای Occurrence</li>
      <li>✅ دریافت وظیفه به همراه Occurrenceهای آن</li>
    </ul>
  </li>
  <li>✅ Pipeline اعتبارسنجی مشترک (<code>ValidationBehavior</code>) با MediatR</li>
  <li>✅ صفحات اولیه Blazor (ورود، لیست وظایف روزانه، فرم افزودن/ویرایش وظیفه)</li>
  <li>✅ پشتیبانی از تاریخ شمسی در فرانت (<code>PersianDateHelper</code>)</li>
</ul>

<h3>🚧 کارهای باقی‌مانده</h3>

<h4>دامنه و منطق کسب‌وکار</h4>
<ul>
  <li>❌ پیاده‌سازی کامل ماژول <strong>Report</strong> (Entity, Core, Application, Infrastructure, Controller)</li>
  <li>❌ پیاده‌سازی کامل ماژول <strong>Note</strong> (Entity, Core, Application, Infrastructure, Controller)</li>
</ul>

<h4>کیفیت کد و تست</h4>
<ul>
  <li>❌ افزودن پروژه‌های <strong>Unit Test</strong> (اولویت: منطق تولید Occurrence از RepeatPattern، و CommandHandlerهای حساس)</li>
  <li>❌ افزودن <strong>Integration Test</strong> برای Endpointهای اصلی API</li>
</ul>

<h4>زیرساخت و DevOps</h4>
<ul>
  <li>❌ افزودن <code>Dockerfile</code> برای API و فرانت</li>
  <li>❌ افزودن <code>docker-compose.yml</code> (API + SQL Server) برای اجرای راحت پروژه</li>
  <li>❌ راه‌اندازی CI با GitHub Actions (Build + Test روی هر Push/PR)</li>
  <li>❌ افزودن <code>appsettings.Example.json</code> یا مستندسازی متغیرهای محیطی لازم</li>
</ul>

<h4>امنیت</h4>
<ul>
  <li>❌ افزودن مکانیزم تایید شماره موبایل (OTP) برای جلوگیری از ثبت‌نام با شماره دیگران (در حال حاضر فقط شماره + پسورد است)</li>
</ul>

</div>

## 📂 ساختار پوشه‌ها (خلاصه)
<div dir="ltr">

```
src/
├── Modules/
│ ├── SamarPlanner.Goal/ # Host: Controllers
│ ├── SamarPlanner.Goal.Core/ # Entities
│ ├── SamarPlanner.Goal.Application/ # UseCases (CQRS)
│ ├── SamarPlanner.Goal.Infrastructure/ # EF Core, Repository
│ ├── SamarPlanner.Identity.Core/ # Entities Identity
│ ├── SamarPlanner.Identity.Application/ # UseCases Identity
│ ├── SamarPlanner.Identity.Infrastructure/
│ ├── SamarPlanner.Task.Core/ # Entities Task
│ ├── SamarPlanner.Task.Application/ # UseCases Task
│ └── SamarPlanner.Task.Infrastructure/ # EF Core Task
├── SamarPlanner.Api/ # نقطهٔ ورود API
├── SamarPlanner.Web/ # کلاینت Blazor WASM
└── Shared/
│  ├── SamarPlanner.Shared/ # BaseController, Extensions
│  ├── SamarPlanner.Shared.Kernel/ # Result, Settings
│  ├── SamarPlanner.Shared.Contracts/ # Commands/Queries مشترک
│  ├── SamarPlanner.Shared.Application/ # Pipeline Behaviors
|  └── SamarPlanner.Shared.Infrastructure/ # Base DbContext
```

</div>

  
