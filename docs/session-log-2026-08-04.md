# MicroNexus — Session Log (2026-08-04)

Lanjutan dari [session-log-2026-08-03.md](session-log-2026-08-03.md). Baca itu dulu buat context skripsi & kerjaan sebelumnya.

---

## 1. Level Map — dot spacing di jalur penghubung

User laporan titik-titik penghubung antar node keliatan gak rata/overlap. Dua ronde fix di [LevelMapController.cs](../Assets/Scripts/Level/LevelMapController.cs) `DrawPathLine()`:

**Ronde 1 — dot numpuk ke lingkaran node**: dot pertama/terakhir tiap segmen ditaruh cuma `dotSpacing` (36px) dari **pusat** node, padahal radius node 45.5px → dot nongol di dalam lingkaran. Fix: reserve margin = nodeRadius + dotRadius + clearance sebelum mulai nempatin dot.

**Ronde 2 — gap node-ke-dot beda sama gap dot-ke-dot**: margin fix di ronde 1 independen dari gap dot-ke-dot, jadi keliatan gak konsisten (user marking di screenshot). Fix: hitung SATU gap seragam yang dipakai buat semua celah dalam satu segmen (node→dot pertama, dot→dot, dot terakhir→node), pakai formula `distance = 2*nodeRadius + n*dotDiameter + (n+1)*gap` buat nyari `n` (jumlah dot) dan `gap` sekali jalan.

**Bug turunan yang baru ketauan pas ronde 2**: radius node yang dipakai buat itung (`nodeDiameter/2` = 45.5) itu radius **hit-area/root** LevelNode, bukan radius lingkaran yang beneran KELIATAN (`Image_Base` child, cuma 65x65 → radius 32.5, sisa resize "65%" sesi kemarin). Dua ukuran ini independen dan gampang ke-lupa saat salah satunya diubah lagi nanti. Fix: tambah `GetVisualNodeRadius()` yang baca ukuran `Image_Base` langsung dari prefab (bukan hardcode), jadi otomatis nyesuain kalau ukurannya diubah lagi.

Final: `dotSpacing = 13.7`, 5 dot per segmen, semua gap identik dalam satu segmen (diverifikasi lewat query posisi live, bukan screenshot).

---

## 2. Rename Ranger dari nama warna ke nama antibiotik

Permintaan: `Ranger_<Color>` → `Ranger_<Antibiotic>`. Mapping udah ketemu dari data existing di `RangerBioManager` (dipake buat teks yang ditampilin ke player):

| Warna lama | Antibiotik |
|---|---|
| White | Amoxicillin |
| Blue | Vancomisin |
| Pink | Cefixime |
| Red | Azitromisin |
| Green | Ciprofloxacin |
| Yellow | Levofloksasin |
| Black | Cotrimoxazole |

### Yang diubah
1. **[RangerBioManager.cs](../Assets/Scripts/BioScript/RangerBioManager.cs)** — field `CharacterData` + method `ShowWhite()` dst → `ShowAmoxicillin()` dst. Pakai `[FormerlySerializedAs]` biar data yang udah keisi di Inspector (nama, deskripsi, gambar, tombol) gak ilang pas field-nya di-rename.
2. **[RangerCanvasManager.cs](../Assets/Scripts/BioScript/RangerCanvasManager.cs)** — sama pattern-nya, field canvas + method `ShowXCanvas()`.
3. **[BioManager.cs](../Assets/Scripts/BioScript/BioManager.cs)** — bagian ranger-nya aja (bagian enemy/bacteria dibiarin, itu sistem beda).
4. **35 GameObject** di `Assets/Prefab/RangerBio/RangerGroup.prefab` — panel utama (`Bio Ranger Pink` → `Ranger_Cefixime`, dst) + child di dalamnya (`Character Pink` → `Character Cefixime`, `PinkSideButton` → `CefiximeSideButton`, dst).
5. **34 binding tombol OnClick** (di prefab + scene lama `Stage Selection.unity`, yang ternyata juga punya instance `RangerBioManager`/`RangerCanvasManager` sendiri) yang manggil method lama, diupdate ke nama baru via script (match exact `m_MethodName:` line, bukan string-replace biasa — supaya gak ke-trigger ke method LAIN yang namanya kebetulan mirip, kayak `ShowBacteriaRed`/`ShowEnemyPink` di sistem enemy yang beda).

**Verifikasi**: compile bersih, semua CharacterData/reference ke-migrasi bener (dicek satu-satu lewat `SerializedObject`), sweep semua tombol via reflection (86 binding di prefab + 104 di scene) — 0 yang rusak dari perubahan ini. Nemu 2 binding rusak yang **gak terkait** (`EnemyGameObjectManager.ShowRedEnemy()`/`ShowPurpleEnemy()` yang emang dari dulu gak ada method-nya) — di-spawn jadi task terpisah, belum dikerjain.

---

## 3. Main Menu redesign — **SELESAI, JANGAN DISENTUH tanpa diminta user**

User eksplisit bilang di akhir sesi: main menu udah final, jangan diutak-atik lagi sampai ada instruksi baru.

### Yang berubah
- **Background**: `Assets/Art/Background/mainBackgroundReworkUpscaled.png` (key art baru dengan karakter + logo "MICRONEXUS UII"). Sempat salah target (ada 2 Image serupa: `Background` yang inactive, `Image` yang beneran aktif) — dikoreksi.
- **Font**: `Retro Gaming.ttf` (dari `Assets/Art/Main Menu Redesign asset/retro_gaming.zip`) → dibikin TMP Font Asset di `Assets/Font/Asset/Retro Gaming SDF.asset`.
- **Tombol**: border sprite baru (`bigButtonBorder.png`/`mediumButtonBorder.png`/`smallButtonBorder.png`/`switchButton.png` dari folder yang sama) buat Mulai Bermain / Halaman Belajar / Leaderboard / Keluar.
- User awalnya minta full rebuild otomatis (posisi + reskin) → hasilnya berantakan (posisi ga jelas, teks keluar border, cuma 3 dari 4 tombol keliatan) → **di-undo semua sama user**, lanjut **diatur manual sendiri di Editor**. Sesudahnya Claude cuma bantu fix bug-bug spesifik yang ditemuin user selama dia atur manual (lihat gotcha di bawah), bukan lagi pegang layout-nya.
- **UsernameSlot** (teks nama player + icon ganti-user): awalnya icon di samping teks pakai Horizontal Layout Group biar jarak konsisten walau panjang nama beda-beda → user gak suka rubber-band effect-nya pas resize manual → **Layout Group & ContentSizeFitter dilepas lagi**, icon dipindah jadi tombol terpisah **di bawah** teks username (posisi fixed, gak lagi ngikutin panjang teks).

---

---

## 4. Level Map — floating idle animation

Node level di Level Map sekarang ngambang pelan naik-turun. [FloatingIdle.cs](../Assets/Scripts/Level/FloatingIdle.cs) — nambahin offset sinus kecil di atas posisi zigzag yang udah dihitung `LevelMapController`, gak nimpa/bentrok sama layout-nya. Attached ke prefab `LevelNode.prefab`. Tiap node punya fase acak sendiri (`randomizePhase`) biar gak ngambang bareng-bareng. Parameter (`amplitude`, `speed`) bisa diubah di Inspector.

---

## 5. Audit mandiri — bug ketemu & difix (atas inisiatif sendiri, user minta "take over dulu")

### Sweep binding tombol project-wide
Semua `OnClick` binding di semua scene + prefab (88 unique method reference) dicek via reflection — nemu **3 yang rusak** (nunjuk ke method yang emang gak ada):
- `HighscoreTable.ResetLeaderboard()` di `Leaderboard.unity` → seharusnya `ResetScoresFromButton()`
- `EnemyGameObjectManager.ShowRedEnemy()` / `ShowPurpleEnemy()` di `Stage Selection.unity` → seharusnya `ShowBacteriaRed()` / `ShowBacteriaPurple()`

Semua diperbaiki dengan cara yang sama kayak rename Ranger kemarin (match exact `m_MethodName:` line). Re-sweep setelahnya: 0 rusak dari 88 binding.

### Bug besar: progress level gak pernah ke-save
Ternyata **gak ada satupun kode di jalur gameplay asli yang nambahin `GameData.Data.UnlockedLevel`**. Yang nyentuh cuma:
- Method debug-only di `LevelMapController` (context menu, testing doang)
- `FinishPoint.cs` — kode lama yang gak nempel di GameObject manapun, gak pernah dipanggil, dan ada bug case-sensitivity (`PlayerPrefs.GetInt("unlockedLevel", ...)` beda key sama `PlayerPrefs.SetInt("UnlockedLevel", ...)`)

Efeknya: badge centang di Level Map gak akan pernah nyala buat siapapun, progress reset tiap buka game lagi.

**Fix** — [GameData.cs](../Assets/Scripts/Game%20Data/GameData.cs):
- Constructor sekarang load `UnlockedLevel` dari `PlayerPrefs` saat start.
- Method baru `UnlockLevelFromSceneName(string sceneName)` — parse nama scene (`"Level X Stage Y"`, pola yang sama kayak yang dipakai `LevelMapController` buat generate nama scene), hitung ordinal globalnya, terus unlock level berikutnya + save ke `PlayerPrefs` langsung. Gak pernah nurunin progress (menang ulang level lama = no-op), dan otomatis skip Endless Mode.
- Di-panggil dari [WaveManager.cs](../Assets/Scripts/Wave%20Manager/WaveManager.cs) `TriggerWin()` — titik kemenangan yang beneran aktif di gameplay.

Sekalian ketemu: event `WaveManager.OnLevelComplete` dideklarasiin dan di-subscribe sama `AtomSpawner.StopSpawning()` (buat berhenti spawn atom pas menang) tapi **gak pernah di-invoke** — jadi atom spawner (dipake di Endless Mode) gak pernah beneran berhenti. Ditambahin `OnLevelComplete?.Invoke();` di `TriggerWin()`.

**Verifikasi** (live di Play mode, bukan cuma baca kode): simulasi menang Level 1 & Level 2 → `UnlockedLevel` naik 1→3 dengan benar, ke-save ke `PlayerPrefs`, tetap kebaca abis instance `GameData` baru dibikin (simulasi restart app), dan badge centang di Level Map beneran nyala cuma di Level 1 & 2 abis `RefreshMap()`.

---

---

## 6. Bio-File Ranger & Enemy dipindah ke Halaman Belajar

Sebelumnya tombol "BIO-FILE (RANGER)" / "BIO-FILE (ENEMY)" ada di `Stage Selection.unity` (scene lama, udah gak jadi bagian alur utama). Diminta pindah ke `MainMenu disease.unity` (scene di balik tombol "Halaman Belajar" di Main Menu).

### Tahap 1 — placeholder
2 tombol placeholder ditambahin di `MainMenu disease.unity` (baris baru di atas grid 6 tombol penyakit yang udah ada — grid-nya padat, gak ada ruang kosong). Belum ada onClick, sengaja non-fungsional dulu.

### Tahap 2 — wire fungsi beneran
- `Assets/Prefab/RangerBio/RangerGroup.prefab` (isinya seluruh sistem Bio-File Ranger+Enemy, dipake juga di `Stage Selection.unity`) di-instantiate ke `MainMenu disease.unity` sebagai GameObject root baru, default inactive.
- Script baru [BioFileEntry.cs](../Assets/Scripts/DiseaseMenuScript/BioFileEntry.cs) — jembatan buka/tutup, karena navigasi internal `RangerGroup` (`QuestionMarkManager`) gak tau cara balik ke menu penyakit (itu spesifik per-scene).
- Tombol "Back" bawaan yang dari dulu targetnya kosong/putus (harusnya balik ke "Stage Selection" tapi reference-nya ilang) — diarahin ke `BioFileEntry.Close()`.
- **Gotcha besar**: fix onClick di GameObject dalam PREFAB INSTANCE (bukan prefab asset, bukan object baru) ternyata TIDAK tersimpan di file lewat cara biasa (`btn.onClick = new ButtonClickedEvent(); AddPersistentListener(...)` + `SetDirty` + save) — beberapa kali dicoba, hasilnya selalu balik ke binding lama pas di-reload. **Fix yang kerja**: pakai `SerializedObject`/`SerializedProperty` langsung (bukan API level tinggi `UnityEventTools`), plus panggil `PrefabUtility.RecordPrefabInstancePropertyModifications()` eksplisit.
- **Jebakan verifikasi**: override di prefab instance TIDAK disimpan sebagai `m_MethodName: X` biasa di file scene — formatnya beda, pakai list `- target: {fileID/guid}` + `propertyPath: ...m_MethodName` + `value: X`. Grep buat cari `m_MethodName: Close` gak bakal ketemu apa-apa walau datanya udah bener kesimpen — sempat muter-muter panjang gara-gara ini, verifikasi ulang pake pattern yang bener (`propertyPath.*MethodName` + baris `value:` sesudahnya) baru ketauan datanya udah benar dari awal.

### Fix layering
Tombol placeholder awalnya ditaruh sebagai sibling PALING AKHIR di Canvas → nongol di atas panel penyakit yang lagi aktif (harusnya ketutup). Fix: `SetSiblingIndex()` ke posisi sebelum daftar panel, sama kayak tombol lain.

### Bug lanjutan yang ketemu & difix
- **Scroll horizontal gak perlu**: `ScrollRect` di "Bio Ranger Canvas" defaultnya `horizontal=true` DAN `vertical=true` sekaligus. Diset `horizontal=false`.
- **Tombol "?" nyasar di pojok kanan atas**: `QuestionMarkCanvas` (child root RangerGroup, isinya icon bantuan) aktif secara default dari prefab, independen dari sistem show/hide manapun — nongol tiap kali RangerGroup di-aktifin. Di-nonaktifin permanen buat instance di scene ini.
- **`QuestionMarkManager.cs` pakai `?.SetActive()`** di field yang emang sengaja kosong (`canvasDefault`) → throw `UnassignedReferenceException` beneran (bukan di-skip kayak null check biasa — `?.` di `UnityEngine.Object` itu punya gotcha terkenal, gak selalu match sama overload `==` custom-nya Unity). Diganti ke helper `SafeSetActive()` pakai null check eksplisit di semua pemanggilan.
- **False alarm yang diverifikasi BUKAN bug**: 7 tombol pemilih ranger (`AmoxicillinSideButton` dkk) sempat kelihatan numpuk di posisi yang sama pas dicek langsung setelah `SetActive(true)`. Ternyata itu cuma karena `RangerBioManager.Start()` (yang nyisain 1 karakter doang) belum sempet jalan di frame yang sama — nunggu 1 frame lebih, semuanya udah bener cuma 1 yang aktif.

---

## Gotcha teknis baru (tambahan dari yang di log kemarin)

- **Perubahan yang dibuat script pas Play mode itu KE-REVERT begitu Stop** — ini udah pernah kejadian kemarin, tapi kejadian lagi berkali-kali sesi ini (paling parah: fix `sizeDelta` teks yang invisible sempat keulang 2x gara-gara lupa cek `Application.isPlaying` sebelum edit). **Selalu cek `UnityEngine.Application.isPlaying` sebelum bikin perubahan yang mau di-save** — kalau `true`, redo di edit mode dulu baru save.
- **TMP Font Asset yang dibikin via script (`TMP_FontAsset.CreateFontAsset`) TIDAK otomatis nyimpen Material & Texture atlas-nya sebagai sub-asset** — kalau cuma `AssetDatabase.CreateAsset(fontAsset, path)` doang, keliatan jalan normal SELAMA masih di sesi yang sama (baca dari object di memory), tapi begitu di-reload dari disk (misal: buka Inspector buat assign font-nya), `material` dan `atlasTextures` jadi `null` → `UnassignedReferenceException`/`NullReferenceException` di GUI. **Fix wajib**: setelah `CreateAsset` pada font asset utama, panggil `AssetDatabase.AddObjectToAsset(atlasTexture, path)` DAN `AddObjectToAsset(material, path)` sebelum `SaveAssets()`. Verifikasi selalu pakai `AssetDatabase.ImportAsset(path, ForceUpdate)` dulu baru re-load, biar bener-bener ngetest state yang ke-persist, bukan cache in-memory.
- **Field `sourceFontFile` di TMP Font Asset juga suka ke-reset ke `null`** kalau asset-nya diutak-atik berkali-kali (regenerate, ganti render mode, dll) dalam satu sesi — assign ulang via `SerializedObject.FindProperty("m_SourceFontFile")` kalau ketemu lagi.
- **Testing glyph SDF font jangan cuma sample titik tengah (`GetPixel` di center)** — banyak huruf (u, H, dst) punya "lubang"/celah kosong di tengah bentuknya secara desain, bukan berarti glyph-nya rusak. Sample banyak titik di seluruh `glyphRect` dan ambil MAX alpha buat ngecek valid/nggak.
- **Pola bug berulang: TMP Text child dengan stretch-anchor (`anchorMin=(0,0)`, `anchorMax=(1,1)`) yang `sizeDelta`-nya (inset) dikalibrasi buat parent yang lebih GEDE** — begitu parent (tombol)-nya di-resize lebih kecil (manual atau script), inset itu jadi lebih besar dari parent-nya sendiri → area teks negatif → teks invisible, dan efek visual "rubber-band" pas nge-drag resize parent-nya di Editor. Ini kejadian berulang kali sesi ini di berbagai tombol (QuitButton, Button Play, Button Learn). **Cara cepat scan semua yang kena**: loop semua `TextMeshProUGUI` yang stretch-anchor, cek `parent.rect.size + sizeDelta` — kalau x atau y hasilnya < 5, itu rusak. Fix: set `sizeDelta` ke inset kecil yang wajar (misal `(-6,-4)`), bukan warisan dari ukuran parent lama.
- **GameObject/Component yang dihapus+dibikin ulang di path yang sama (`DeleteAsset` + `CreateAsset`) dapet GUID baru** — kalau ada reference lain (misal user assign manual di Inspector) ke asset lama, referensinya putus. Selalu kabarin user kalau abis regenerate asset yang mungkin udah di-reference di tempat lain.
- **Destructive asset operation (`AssetDatabase.DeleteAsset`) di-block sama safety_checks default** — perlu `safety_checks=false` eksplisit kalau emang kepake (misal buat regenerate asset yang jelas dibuat sendiri sesi ini, bukan punya user).
