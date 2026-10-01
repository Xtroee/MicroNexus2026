# Known Issues / Problems — MicroNexus (audit sesi berjalan)

Dokumen ini nampung temuan dari audit menyeluruh project, dikumpulin bertahap (loop otomatis). Tiap entry
dikasih lokasi file + penjelasan singkat, biar gampang di-follow-up nanti. Status: **sedang berjalan**.

---

## Tick 1 — sanity check + dead code sweep

**Sanity check (0 masalah)**:
- Console Unity: 0 error/warning.
- onClick binding project-wide: 90 pasangan unik (class, method) di semua `.unity`/`.prefab`, semua valid — 0 yang putus. Termasuk scene Stage 7-12 yang baru dibikin, gak ada regresi.

### Dead code — script yang GAK PERNAH ditempelin ke GameObject manapun (di scene ATAUPUN prefab manapun)

Dicek dengan cocokin guid tiap `.cs.meta` ke semua referensi `m_Script` di seluruh `.unity`+`.prefab`. 20 dari 133
script ke-flag; abis dicek satu-satu, kebagi jadi 2 kelompok:

**Kemungkinan besar dead code beneran (prioritas cleanup)**:
- [Assets/Scripts/Projectile/Projectile.cs](../Assets/Scripts/Projectile/Projectile.cs) — implementasi lengkap
  (`Update()` gerak + damage field) tapi BUKAN base class buat `RedProjectile.cs` dkk (mereka semua
  `: MonoBehaviour` langsung, bukan `: Projectile`). Kemungkinan draft awal sebelum tiap warna dipisah jadi
  script sendiri-sendiri, gak pernah dihapus.
- [Assets/Scripts/BasicShooter/BasicShooter.cs](../Assets/Scripts/BasicShooter/BasicShooter.cs) — sama
  persis polanya kayak Projectile.cs di atas, versi "generic" yang gak pernah dipake, kalah sama
  `BasicShooterRed.cs` dkk yang independen.
- [Assets/Scripts/Finish/FinishPoint.cs](../Assets/Scripts/Finish/FinishPoint.cs) — **udah dikonfirmasi dead
  sejak sesi sebelumnya** (ada bug case-sensitivity PlayerPrefs key juga: baca `"unlockedLevel"`, tulis
  `"UnlockedLevel"` — gak konsisten). Gak nempel di scene manapun.
- [Assets/Scripts/DragDrop.cs](../Assets/Scripts/DragDrop.cs) & [Assets/Scripts/ObatSingleDrag.cs](../Assets/Scripts/ObatSingleDrag.cs)
  — DUA-duanya gak nempel di manapun. Kemungkinan 2 percobaan drag-and-drop "obat" yang berurutan, keduanya
  ditinggalin pas gameplay-nya pindah ke sistem klik-tile (`GameManager.cs`) yang sekarang aktif.
- [Assets/Scripts/Bacteria.cs](../Assets/Scripts/Bacteria.cs) — kemungkinan draft awal sebelum
  `BacteriaController.cs` + varian warna (yang sekarang dipake).
- [Assets/Scripts/Level/StageSelection.cs](../Assets/Scripts/Level/StageSelection.cs) &
  [Assets/Scripts/Level Helper/LevelHelper.cs](../Assets/Scripts/Level Helper/LevelHelper.cs) &
  [Assets/Scripts/Level/LevelMenu.cs](../Assets/Scripts/Level/LevelMenu.cs) — kemungkinan sisa sistem level-select
  LAMA, sebelum `LevelMapController.cs` (yang sekarang dipake) dibikin sesi kemarin.
- [Assets/Scripts/NameSave/PlayerNameInput.cs](../Assets/Scripts/NameSave/PlayerNameInput.cs) — kemungkinan
  draft awal sebelum `UserManager.cs`/`ChangeUserPanel` (sistem ganti-nama yang sekarang dipake).
- [Assets/Scripts/ItemDrop.cs](../Assets/Scripts/ItemDrop.cs), [Assets/Scripts/RedCapsule.cs](../Assets/Scripts/RedCapsule.cs),
  [Assets/Scripts/RedCapsuleSource.cs](../Assets/Scripts/RedCapsuleSource.cs),
  [Assets/Scripts/ZombieBacteria.cs](../Assets/Scripts/ZombieBacteria.cs) — namanya gak nyambung ke sistem
  manapun yang sekarang aktif (gak ada "Zombie" atau capsule generik "Red" tunggal di gameplay saat ini).
  Kemungkinan prototype super awal.
- [Assets/Scripts/Hyperlink/TMPHyperlinkHandler.cs](../Assets/Scripts/Hyperlink/TMPHyperlinkHandler.cs) — ada
  saudara `TMPLinkClickOnly.cs` di folder yang sama; kemungkinan salah satunya aja yang kepake, yang lain
  ditinggal.
- [Assets/Scripts/Funnel/FunnelEntry.cs](../Assets/Scripts/Funnel/FunnelEntry.cs) — belum pasti, perlu dicek
  apa di-instantiate lewat kode (bukan attach manual di scene) — `FunnelSpawner.cs` yang jadi tetangganya
  emang dipake, tapi `FunnelEntry.cs` sendiri gak ketemu referensi attach statis di manapun.

**BUKAN dead code — sengaja gak nempel di scene (via kode/runtime)**:
- `Assets/Scripts/Game Data/GameData.cs` — plain C# class (singleton `GameData.Data`), emang bukan
  MonoBehaviour buat ditempel, dipake lewat referensi statis.
- `Assets/Scripts/Bacteria Controller/BacteriaEffectivenessPopup.cs`,
  `Assets/Scripts/Bacteria Controller/FloatingPopupText.cs`,
  `Assets/Scripts/Bacteria Controller/BacteriaLabelFactory.cs` — dibikin/ditempelin lewat `AddComponent<>()` di
  runtime (fitur popup efektivitas & hover-name dari sesi kemarin), bukan lewat scene/prefab statis — normal.

**Rekomendasi**: sebelum dihapus, worth di-double-check manual (kasih tau developer lain kalau ada, atau cek git
history kalau nanti udah pakai git) — tapi kalau emang beneran gak kepake, ini semua aman dibersihin buat
ngurangin clutter project.

---

## Update scope (dari user, mid-loop)

**Stage 7-12 di-exclude dari audit ini** — masih placeholder/WIP (lihat `stage-implementation-status.md`), belum
worth di-audit sampe karakter/bakteri final-nya diimplementasi. Fokus dialihin ke **menu-menu di Main Menu dan
sub-flow-nya** (bukan konten gameplay stage). Catatan: Main Menu sendiri statusnya "jangan disentuh dulu" per
instruksi user sebelumnya di sesi ini — jadi tick-tick berikutnya sifatnya **audit/laporan doang, BUKAN
fix langsung**, kecuali user eksplisit minta diperbaiki.

---

## Tick 2 — telusurin Main Menu & sub-flow-nya

**New MainMenu.unity** (scene utama):
- Semua tombol aktif ke-cek: `QuitButton`→`UIManager.QuitGame`, `Play Button (1)`→`ChangeScene("Mode Selection")`,
  `Play Button (2)`→`ChangeScene("MainMenu disease")` (ini tombol "Halaman Belajar"), `ChangeUserButton`→
  `GameObjectDropdownSwitcher.Show(1)`, `Button Leaderboard`→`ChangeScene("Leaderboard Main Menu")`. Semua
  valid, semua nyambung ke scene/method yang beneran ada.
- **Minor — naming kurang jelas**: GameObject "Play Button (2)" itu sebenernya tombol "Halaman Belajar", bukan
  "Play" kedua. Gak ngefek ke gameplay, cuma bikin bingung kalau ada yang buka hierarchy-nya nanti.
- **Minor — clutter**: ada "Play Button" & "Quit Button" (beda dari yang aktif dipake) yang INACTIVE, sisa dari
  percobaan redesign tombol yang di-undo user sebelumnya di sesi ini. Aman (gak nempel ke logic apapun), tapi
  bisa dibersihin biar hierarchy-nya rapi.
- Alur ganti user (`ChangeUserPanel`/`PlayerInputPanel`/`UserManager`/`GameObjectDropdownSwitcher`) — dicek
  detail, semua field ke-assign, logic delete-user (fallback ke user lain, larangan hapus "Player"/user
  terakhir) aman, gak ada null-ref risk saat ini.

**Mode Selection.unity**: bersih. 3 tombol (`Button Back`→New MainMenu, `Button Level Biasa`→Level Map,
`Button Level Endless`→Level Endless), semua valid.

**Leaderboard.unity** + **Leaderboard Sample.unity**: alur bolak-balik dicek, valid — `Leaderboard Sample`
namanya kesannya "scene demo/testing" tapi sebenernya itu layar **Final Result / Trophy** (abis maen), bukan
sample beneran. Cuma soal penamaan bikin bingung, bukan bug.
- Ada `Reset Button` (inactive) di `Leaderboard.unity` dengan argumen `"Level 2 Stage 2 Correction 1"` — nama
  level yang gak ada di project sekarang, kelihatan sisa debug tool lama. Aman karena inactive.

**Leaderboard Main Menu.unity** (dibuka dari tombol "Button Leaderboard" di Main Menu) — **ketemu bug beneran**:
- `Button Stage Selection` (inactive) → `UIManager.ChangeScene("Stage Selection Trial")` — **scene "Stage
  Selection Trial" GAK ADA** di project (bukan di Build Settings, bukan di manapun).
- `Button Retry Survival` (inactive) → `UIManager.ChangeScene("Level 2 Stage 2 Correction 1")` — **scene ini
  juga GAK ADA**.
- `Reset Button` (inactive) → argumen sama, "Level 2 Stage 2 Correction 1" — sama-sama gak ada.
- Ketiganya INACTIVE sekarang jadi gak bisa diklik pemain — **bukan bug yang keliatan sekarang**, tapi kalau
  nanti ada yang re-enable tombol ini tanpa sadar itu udah rusak (misal pas mau nambahin fitur retry-survival
  beneran), bakal langsung gagal pindah scene. Worth diperbaiki referensinya atau dihapus aja kalau emang gak
  kepake.
- Tombol yang AKTIF di scene ini (`Back`→New MainMenu) valid, jadi bagian yang keliatan pemain sekarang aman.

**Catatan metodologi**: audit onClick sebelumnya (90 pasangan project-wide) cuma ngecek METHOD-nya ada
(`UIManager.ChangeScene` emang ada), bukan ngecek apa STRING ARGUMENT-nya (nama scene tujuan) beneran valid.
Ini kategori bug yang lolos dari audit itu.

**Audit susulan langsung dijalanin** (ngecek 78 pemanggilan `ChangeScene("...")` project-wide vs daftar scene di
Build Settings) — nemu 1 lagi:
- [Assets/Scenes/Stage Selection/Stage 2 Selection.unity](../Assets/Scenes/Stage Selection/Stage 2 Selection.unity) 
  — `Button Level 2` (inactive) → `ChangeScene("Level 2 Stage 2")` — **scene ini juga gak ada**. Kelihatannya
  sisa rencana awal Stage 2 punya beberapa level bernomor (kayak Stage 1), sebelum disederhanain jadi cuma 1
  mode "Endless".
- Total sekarang: **3 scene-reference yang gak valid**, semuanya di tombol INACTIVE (gak ada yang bisa diklik
  pemain sekarang): `"Stage Selection Trial"`, `"Level 2 Stage 2 Correction 1"` (2x, tombol beda), `"Level 2
  Stage 2"`.
- Juga ketemu 1 tombol inactive lain, `Button Play` di New MainMenu → `ChangeScene("MainMenu")` (scene LAMA,
  beda dari "New MainMenu" yang aktif) — ini VALID (scene-nya ada), cuma dead/gak kepake, bukan broken link.

---

## Tick 3 — MainMenu disease.unity (Halaman Belajar / Bio-File)

Scene ini paling kompleks (109 tombol, banyak sub-panel bertingkat). Dicek semua tombol yang GAK PUNYA listener
sama sekali (beda kategori dari "target invalid" — ini "gak ada aksi apa-apa pas diklik"):

- Ketemu 6 tombol tanpa listener: `PurpleSideButton`, `RedSideButton`, `FungiSideButton`, `VirusSideButton`,
  `ProtozoaSideButton`, `HelmithSideButton` — semuanya di
  `RangerGroup (Bio-File)/General Canvas/Bio Enemy Default`.
- **Setelah ditelusurin ke akar**: panel `"Bio Enemy Default"` ini ternyata **gak pernah diaktifin sama script
  manapun** — dicek field `QuestionMarkManager.EnemyBioChoose` (yang beneran dipanggil pas buka "Bio-File
  Enemy") itu nunjuk ke panel LAIN (`"Bio Enemy Choose Panel"`), dan field `canvasDefault` (yang mestinya nunjuk
  ke default panel) malah kosong/unassigned. Grep ke file prefab juga cuma nemu 1 referensi ke nama
  `"Bio Enemy Default"` (deklarasi objeknya sendiri doang, gak ada field manapun yang nunjuk ke situ).
  → **Kesimpulan: panel ini orphaned/gak pernah keliatan pemain**, jadi 6 tombol rusak di dalemnya gak ngefek
  ke gameplay saat ini. Worth dihapus aja pas beres-beres (mengurangi 1 GameObject nyasar + 6 tombol
  percuma), tapi BUKAN bug yang urgent.
- **Panel yang BENERAN kepake** (`Bio Enemy Choose Panel`, `Bio Enemy Bacteria Panel`, `Bio Enemy Other Panel`)
  — dicek juga, **semua tombolnya punya listener, gak ada yang bolong**. Alur Bio-File Enemy yang sebenernya
  dipake pemain itu bersih.
- **Sisi Ranger (ensiklopedia antibiotik) juga dicek full** — 93 tombol total di seluruh `RangerGroup.prefab`,
  cuma 6 yang tanpa listener dan itu ya 6 yang sama di "Bio Enemy Default" tadi. Gak ada tombol bolong lain di
  sisi Ranger. Bio-File (Ranger + Enemy) secara keseluruhan **bersih** kecuali 1 panel orphaned itu.

**Kesimpulan Main Menu & sub-flow**: gak ada bug yang KELIATAN pemain sekarang (semua yang aktif udah bener).
Semua temuan di atas ada di tombol INACTIVE — aman buat sekarang, tapi jadi jebakan laten kalau nanti ada yang
gak sadar re-enable salah satunya. Rekomendasi: bersihin aja tombol-tombol mati ini (`Button Level 2`, `Button
Stage Selection`/`Button Retry Survival`/`Reset Button` di Leaderboard Main Menu, `Button Play` di New
MainMenu, `Play Button`/`Quit Button` lama) — mengurangi resiko + beresin clutter sekalian.


---

## Popup/UI audit (loop) — Tick 1: arah scroll ScrollRect

**Bug user**: halaman belajar (Bio-File) bakteri bisa di-scroll ke segala arah, harusnya vertical doang.

**Penyebab**: semua `Scroll View` (list card) dibuat dengan `Horizontal + Vertical` sama-sama ON, padahal content-nya
grid vertikal yang lebarnya < viewport (91 vs 197) — jadi horizontal gak ada gunanya, cuma bikin elastic drag ke samping.

**Sudah difix** (horizontal OFF, vertical ON):
- `Assets/Prefab/RangerBio/RangerGroup.prefab`: `Bio Enemy Bacteria Panel/Scroll View` (list bakteri) dan
  `Bio Ranger Canvas/Scroll View` (list antibiotik) — propagate otomatis ke instance di `MainMenu disease.unity`.
- `MainMenu disease.unity` (scene-own, inactive/legacy): `Hospital-Acquired Pneumonia Panel/Scroll View`,
  `Community-Acquired Pneumonia (CAP) Panel/Scroll View`.

**Belum difix (sengaja)**:
- `New MainMenu.unity` → `Canvas/ChangeUserPanel/User List Area/Scroll View` masih H+V (content width == viewport,
  jadi horizontal gak guna). Scene ini status "jangan disentuh" — dilaporin aja, fix = matiin `Horizontal` doang.
- `Stage Selection.unity` (deprecated/orphaned) → 2 Scroll View H+V, gak dipakai di alur live, dibiarin.
- `Level Map.unity` H-only = memang sengaja (jalur level horizontal).

Sisa 50-an ScrollRect lain (`TextContainer` dll) udah V-only, aman.

## Popup/UI audit (loop) — Tick 2: popup Pause / Win UI / Death di semua scene gameplay

Dicek 13 scene (Level 1-12 Stage 1 + Level Endless), struktur `Canvas/Pause`, `Canvas/Win UI (1)`, `Canvas/Death`:
- **Konsisten di semua scene, gak ada yang beda/rusak**: Pause & Win UI (1) default inactive, punya Image
  raycastTarget (jadi ngeblok klik ke belakang), masing-masing 2 tombol. Pause 1175x487 > canvas 800x450 (nutup
  seluruh layar); Win UI (1) cuma 100x100 tapi kartu HUD di-disable lewat `WaveManager.disableOnWin` jadi aman.
- `Death` = wrapper full-stretch, activeSelf=True, TANPA Image (gak ngeblok raycast) — isinya baru diaktifin `Lose`/
  `LoseEndless.objectsToActivate`. Pola yang sama di semua scene, bukan bug.
- Belum bisa dicek visual (render popup): game view di edit mode gak nge-repaint UI Overlay lewat MCP. Perlu dicek
  manual di Play mode (Esc/pause, menang, kalah) kalau mau mastiin tampilannya.
- Catatan lama masih berlaku: orphan `Win UI` (tanpa "(1)") ada di semua scene, dead clutter (lihat audit sebelumnya).
